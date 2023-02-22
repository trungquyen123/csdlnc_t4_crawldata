using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_hoadon_module_HoaDonThemMoi : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    DataTable dtDichVu;
    public int STT = 01;
    public int Tongtien = 0;
    public int TongTienGiam = 0;
    public string GiamGia;
    CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            if (!IsPostBack)
            {
                Session["GiamGia"] = "";
                Session["spChiTiet"] = null;
                var getNhanVien = from u in db.admin_Users
                                  where u.groupuser_id == 3 && u.username_active == true
                                  select u;
                ddlNhanVien.DataSource = getNhanVien;
                ddlNhanVien.DataBind();
                blog_NhanVien.Visible = false;


            }
            loadData();
            // btnThemHoaDonMoi.Visible = false;
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }
    public void loadData()
    {
        try
        {
            //load ds nhóm dịch vụ
            var dichvuCate = from c in db.tbNhomDichVus
                             orderby c.position ascending
                             select c;
            rpNhomDichVu.DataSource = dichvuCate;
            rpNhomDichVu.DataBind();

            //Session["spChiTiet"] = null;
            loaddatatable();
            dtDichVu = (DataTable)Session["spChiTiet"];
            //rpDichVuDaChon.DataSource = dtDichVu;
            //rpDichVuDaChon.DataBind();
            rpHoaDonThanhToan.DataSource = dtDichVu;
            rpHoaDonThanhToan.DataBind();
            loadHoaDon();
            if (dtDichVu != null)
            {
                //tính lại tổng tiền trong hóa đơn
                Tinhtien();
                //int tt = 0;
                //if (Session["GiamGia"].ToString() == "" || Session["GiamGia"].ToString() == null)
                //{
                //    GiamGia = "";
                //    foreach (DataRow row in dtDichVu.Rows)
                //    {
                //        tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                //    }
                //    TongTienGiam = 0;
                //    Tongtien = tt;
                //}
                //else
                //{
                //    GiamGia = Session["GiamGia"].ToString();
                //    foreach (DataRow row in dtDichVu.Rows)
                //    {
                //        tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                //    }
                //    TongTienGiam = tt * Convert.ToInt32(GiamGia) / 100;
                //    Tongtien = tt - TongTienGiam;
                //}
            }
            //get các chương trình khuyến mãi
            var getKM = from km in db.tbChuongTrinhKhuyenMais
                        where km.khuyenmai_denngay.Value.Date >= DateTime.Now.Date
                        select km;
            rpKhuyenMai.DataSource = getKM;
            rpKhuyenMai.DataBind();
            string matutang = Matutang();
            txtMaHoaDon.Value = matutang;
            //blog_NhanVien.Visible = false;
        }
        catch (Exception)
        {

        }
    }
    public void Tinhtien()
    {
        int tt = 0;
        if (Session["GiamGia"].ToString() == "" || Session["GiamGia"].ToString() == null)
        {
            GiamGia = "";
            foreach (DataRow row in dtDichVu.Rows)
            {
                tt += Convert.ToInt32(row["dichvu_thanhtien"]);
            }
            TongTienGiam = 0;
            Tongtien = tt;
        }
        else
        {
            //Session["GiamGia"] = percent_km;
            GiamGia = Session["GiamGia"].ToString();
            foreach (DataRow row in dtDichVu.Rows)
            {
                tt += Convert.ToInt32(row["dichvu_thanhtien"]);
            }
            TongTienGiam = tt * Convert.ToInt32(GiamGia) / 100;
            Tongtien = tt - TongTienGiam;
        }
    }
    public void loaddatatable()
    {
        try
        {
            if (dtDichVu == null)
            {
                dtDichVu = new DataTable();
                dtDichVu.Columns.Add("dichvu_id", typeof(int));
                dtDichVu.Columns.Add("dichvu_title", typeof(string));
                dtDichVu.Columns.Add("dichvu_soluong", typeof(int));
                dtDichVu.Columns.Add("dichvu_price", typeof(string));
                dtDichVu.Columns.Add("dichvu_thanhtien", typeof(string));
            }
        }
        catch { }
    }
    public void loadHoaDon()
    {
        var getHD = from hd in db.tbHoaDonBanHangs
                    where hd.hoadon_createdate.Value.Day == DateTime.Now.Day
                    && hd.active == true
                    select hd;
        rpListKhachHang.DataSource = getHD;
        rpListKhachHang.DataBind();
        if (getHD.Count() > 0)
            btnThemHoaDonMoi.Visible = true;
        else
            btnThemHoaDonMoi.Visible = false;

    }
    protected void btnThemDichVu_ServerClick(object sender, EventArgs e)
    {
        //lấy thông tin của tk đang nhập
        var getuser = (from u in db.admin_Users
                       where u.username_username == Request.Cookies["UserName"].Value
                       select u).FirstOrDefault();
        if (txtDichvu_id.Value != "")
        {
            int _id = Convert.ToInt32(txtDichvu_id.Value);
            var checkDichVu = (from dv in db.tbDichVus where dv.dichvu_id == _id select dv).SingleOrDefault();
            //nếu là khách hàng mới vào thì add vào datatable bình thường
            if (txtHoaDon.Value == "")
            {
                if (Session["spChiTiet"] != null)
                {
                    dtDichVu = (DataTable)Session["spChiTiet"];
                    DataRow[] row_id = dtDichVu.Select("dichvu_id = '" + _id + "'");
                    /*
                     * nếu dịch vụ đã được chọn thì tăng số lượng lên 1
                     */
                    if (row_id.Length > 0)
                    {
                        alert.alert_Warning(Page, "Dịch vụ này đã được chọn!", "");
                    }
                    else
                    {
                        string a = double.Parse(checkDichVu.dichvu_price).ToString("#,###", cul.NumberFormat);
                        DataRow row = dtDichVu.NewRow();
                        row["dichvu_id"] = checkDichVu.dichvu_id;
                        row["dichvu_title"] = checkDichVu.dichvu_title;
                        row["dichvu_soluong"] = 1;
                        row["dichvu_price"] = checkDichVu.dichvu_price;
                        row["dichvu_thanhtien"] = Convert.ToInt32(row["dichvu_price"].ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                        dtDichVu.Rows.Add(row);
                        Session["spChiTiet"] = dtDichVu;
                    }
                }
                else
                {
                    loaddatatable();
                    DataRow row = dtDichVu.NewRow();
                    row["dichvu_id"] = checkDichVu.dichvu_id;
                    row["dichvu_title"] = checkDichVu.dichvu_title;
                    row["dichvu_soluong"] = 1;
                    row["dichvu_price"] = checkDichVu.dichvu_price;
                    row["dichvu_thanhtien"] = Convert.ToInt32(row["dichvu_price"].ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                    dtDichVu.Rows.Add(row);
                    Session["spChiTiet"] = dtDichVu;
                }
                //rpDichVuDaChon.DataSource = dtDichVu;
                //rpDichVuDaChon.DataBind();
                rpHoaDonThanhToan.DataSource = dtDichVu;
                rpHoaDonThanhToan.DataBind();
                //tính lại tổng tiền trong hóa đơn
                Tinhtien();
                //int tt = 0;
                //if (Session["GiamGia"].ToString() == "" || Session["GiamGia"].ToString() == null)
                //{
                //    GiamGia = "";
                //    foreach (DataRow row in dtDichVu.Rows)
                //    {
                //        tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                //    }
                //    TongTienGiam = 0;
                //    Tongtien = tt;
                //}
                //else
                //{
                //    GiamGia = Session["GiamGia"].ToString();
                //    foreach (DataRow row in dtDichVu.Rows)
                //    {
                //        tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                //    }
                //    TongTienGiam = tt * Convert.ToInt32(GiamGia) / 100;
                //    Tongtien = tt - TongTienGiam;
                //}
            }
            else
            {
                //nếu khách hàng đã tồn tại thì update lại datatable
                // 1: lấy lại giá khuyến mãi để đưa vào session
                var getData = (from hd in db.tbHoaDonBanHangs
                               where hd.hoadon_code == txtHoaDon.Value
                               select hd).SingleOrDefault();

                //  Load lại datatable

                // 2: Thêm dịch vụ vào
                if (dtDichVu == null)
                {
                    loaddatatable();
                    DataRow row = dtDichVu.NewRow();
                    row["dichvu_id"] = checkDichVu.dichvu_id;
                    row["dichvu_title"] = checkDichVu.dichvu_title;
                    row["dichvu_soluong"] = 1;
                    row["dichvu_price"] = checkDichVu.dichvu_price;
                    row["dichvu_thanhtien"] = Convert.ToInt32(row["dichvu_price"].ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                    dtDichVu.Rows.Add(row);
                    Session["spChiTiet"] = dtDichVu;
                    //rpDichVuDaChon.DataSource = dtDichVu;
                    //rpDichVuDaChon.DataBind();
                    rpHoaDonThanhToan.DataSource = dtDichVu;
                    rpHoaDonThanhToan.DataBind();
                    // 3: Tính lại tổng tiền
                    int tt = 0;
                    foreach (DataRow rows in dtDichVu.Rows)
                    {
                        tt += Convert.ToInt32(rows["dichvu_thanhtien"]);
                    }
                    GiamGia = getData.hoadon_giamgia;
                    TongTienGiam = tt * Convert.ToInt32(GiamGia) / 100;
                    Tongtien = tt - TongTienGiam;
                    //update data vào data base
                    // updata table Hoadon
                    tbHoaDonBanHang update = db.tbHoaDonBanHangs.Where(x => x.hoadon_code == txtHoaDon.Value).FirstOrDefault();
                    //update table HoaDonChiTiet
                    /* Kiểm tra xem DV được chọn đã có trong hóa đơn chưa
                     * nếu có rồi thì update trạng thái
                     * ngược lại thì thêm mới
                     */
                    var getDV = from dv in db.tbHoaDonBanHangChiTiets
                                where dv.hoadon_id == update.hoadon_id && dv.dichvu_id == _id
                                select dv;
                    if (getDV.Count() > 0)
                    {
                        getDV.FirstOrDefault().hidden = false;
                        getDV.FirstOrDefault().hdct_giamgia = update.hoadon_giamgia;
                        getDV.FirstOrDefault().hdct_soluong = 1;
                        getDV.FirstOrDefault().hdct_price = checkDichVu.dichvu_price;
                        db.SubmitChanges();
                    }
                    else
                    {
                        tbHoaDonBanHangChiTiet insert = new tbHoaDonBanHangChiTiet();
                        insert.hoadon_id = update.hoadon_id;
                        insert.dichvu_id = checkDichVu.dichvu_id;
                        insert.hdct_soluong = 1;
                        insert.hdct_price = checkDichVu.dichvu_price;
                        insert.hdct_createdate = DateTime.Now;
                        insert.username_id = getuser.username_id;
                        insert.hidden = false;
                        insert.hdct_giamgia = update.hoadon_giamgia;
                        insert.khuyenmai_id = update.khuyenmai_id;
                        db.tbHoaDonBanHangChiTiets.InsertOnSubmit(insert);
                        try
                        {
                            db.SubmitChanges();
                        }
                        catch (Exception) { }
                    }
                }
                else
                {
                    dtDichVu = (DataTable)Session["spChiTiet"];
                    DataRow[] row_id = dtDichVu.Select("dichvu_id = '" + _id + "'");
                    /*
                     * nếu dịch vụ đã được chọn thì tăng số lượng lên 1
                     */
                    if (row_id.Length > 0)
                    {
                        alert.alert_Warning(Page, "Dịch vụ này đã được chọn!", "");
                    }
                    else
                    {
                        DataRow row = dtDichVu.NewRow();
                        row["dichvu_id"] = checkDichVu.dichvu_id;
                        row["dichvu_title"] = checkDichVu.dichvu_title;
                        row["dichvu_soluong"] = 1;
                        row["dichvu_price"] = checkDichVu.dichvu_price;
                        row["dichvu_thanhtien"] = Convert.ToInt32(row["dichvu_price"].ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                        dtDichVu.Rows.Add(row);
                        Session["spChiTiet"] = dtDichVu;
                        //rpDichVuDaChon.DataSource = dtDichVu;
                        //rpDichVuDaChon.DataBind();
                        rpHoaDonThanhToan.DataSource = dtDichVu;
                        rpHoaDonThanhToan.DataBind();
                        // 3: Tính lại tổng tiền
                        int tt = 0;
                        foreach (DataRow rows in dtDichVu.Rows)
                        {
                            tt += Convert.ToInt32(rows["dichvu_thanhtien"]);
                        }
                        GiamGia = getData.hoadon_giamgia;
                        TongTienGiam = tt * Convert.ToInt32(GiamGia) / 100;
                        Tongtien = tt - TongTienGiam;
                        //update data vào data base
                        // updata table Hoadon
                        tbHoaDonBanHang update = db.tbHoaDonBanHangs.Where(x => x.hoadon_code == txtHoaDon.Value).FirstOrDefault();
                        //update table HoaDonChiTiet
                        /* Kiểm tra xem DV được chọn đã có trong hóa đơn chưa
                         * nếu có rồi thì update trạng thái
                         * ngược lại thì thêm mới
                         */
                        var getDV = from dv in db.tbHoaDonBanHangChiTiets
                                    where dv.hoadon_id == update.hoadon_id && dv.dichvu_id == _id
                                    select dv;
                        if (getDV.Count() > 0)
                        {
                            getDV.FirstOrDefault().hidden = false;
                            getDV.FirstOrDefault().hdct_giamgia = update.hoadon_giamgia;
                            getDV.FirstOrDefault().hdct_soluong = 1;
                            db.SubmitChanges();
                        }
                        else
                        {
                            tbHoaDonBanHangChiTiet insert = new tbHoaDonBanHangChiTiet();
                            insert.hoadon_id = update.hoadon_id;
                            insert.dichvu_id = checkDichVu.dichvu_id;
                            insert.hdct_soluong = 1;
                            insert.hdct_price = checkDichVu.dichvu_price;
                            insert.hdct_createdate = DateTime.Now;
                            insert.username_id = getuser.username_id;
                            insert.hidden = false;
                            insert.hdct_giamgia = update.hoadon_giamgia;
                            insert.khuyenmai_id = update.khuyenmai_id;
                            db.tbHoaDonBanHangChiTiets.InsertOnSubmit(insert);
                            try
                            {
                                db.SubmitChanges();
                            }
                            catch (Exception) { }
                        }
                    }
                }
                loadHoaDonChiTiet(getData.hoadon_id);
            }
        }
    }

    public void loadHoaDonChiTiet(int hoadon_id)
    {
        try
        {
            var getHD = from hdct in db.tbHoaDonBanHangChiTiets
                        join hd in db.tbHoaDonBanHangs on hdct.hoadon_id equals hd.hoadon_id
                        join dv in db.tbDichVus on hdct.dichvu_id equals dv.dichvu_id
                        where hd.hoadon_id == hoadon_id && hdct.hidden == false
                        select new
                        {
                            hdct.hdct_id,
                            hdct.dichvu_id,
                            hdct.hdct_soluong,
                            dv.dichvu_title,
                            dv.dichvu_price,
                            hd.hoadon_id,
                            hd.hoadon_tongtien,
                            hd.hoadon_giamgia,
                            hd.hoadon_phaitra,
                            hd.hoadon_tongtiengiam,
                            hd.hoadon_code,
                            hd.khuyenmai_id,
                            hd.khachhang_name
                        };
            //get lại data và datatable dtDichVu
            if (getHD.Count() > 0)
            {
                dtDichVu = null;
                Session["spChiTiet"] = null;
                int tt = 0;
                foreach (var item in getHD)
                {
                    if (Session["spChiTiet"] != null)
                    {
                        int dichvu_id = Convert.ToInt32(item.dichvu_id);
                        dtDichVu = (DataTable)Session["spChiTiet"];
                        DataRow row = dtDichVu.NewRow();
                        row["dichvu_id"] = item.dichvu_id;
                        row["dichvu_title"] = item.dichvu_title;
                        row["dichvu_soluong"] = item.hdct_soluong;
                        row["dichvu_price"] = item.dichvu_price;
                        row["dichvu_thanhtien"] = Convert.ToInt32(row["dichvu_price"].ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                        dtDichVu.Rows.Add(row);
                        Session["spChiTiet"] = dtDichVu;
                        tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                    }
                    else
                    {
                        loaddatatable();
                        DataRow row = dtDichVu.NewRow();
                        row["dichvu_id"] = item.dichvu_id;
                        row["dichvu_title"] = item.dichvu_title;
                        row["dichvu_soluong"] = item.hdct_soluong;
                        row["dichvu_price"] = item.dichvu_price;
                        row["dichvu_thanhtien"] = Convert.ToInt32(row["dichvu_price"].ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                        dtDichVu.Rows.Add(row);
                        Session["spChiTiet"] = dtDichVu;
                        tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                    }
                    GiamGia = item.hoadon_giamgia;
                    TongTienGiam = tt * Convert.ToInt32(GiamGia) / 100;
                    Tongtien = tt - TongTienGiam;

                }
                //rpDichVuDaChon.DataSource = dtDichVu;
                //rpDichVuDaChon.DataBind();
                rpHoaDonThanhToan.DataSource = dtDichVu;
                rpHoaDonThanhToan.DataBind();
            }
            else
            {
                rpHoaDonThanhToan.DataSource = null;
                rpHoaDonThanhToan.DataBind();
            }
        }
        catch (Exception) { }
    }

    protected void btnUpdateDichVu_ServerClick(object sender, EventArgs e)
    {
        // kiểm tra id
        int _id = Convert.ToInt32(txt_ID.Value);
        if (Session["spChiTiet"] != null)
        {
            dtDichVu = (DataTable)Session["spChiTiet"];
            // chạy foreach để lặp lại các row 
            foreach (DataRow row in dtDichVu.Rows)
            {
                string product_id = row["dichvu_id"].ToString();
                if (product_id == _id.ToString())
                {
                    // lưu data bằng input đầu vào
                    row.SetField("dichvu_soluong", txt_SoLuong.Value);
                    row.SetField("dichvu_thanhtien", txt_TongTien.Value);
                }
            }
            //nếu hóa đơn mới thì update bình thường
            if (txtHoaDon.Value == "")
            {
                rpHoaDonThanhToan.DataSource = dtDichVu;
                rpHoaDonThanhToan.DataBind();
                //tính lại tổng tiền trong hóa đơn
                Tinhtien();
                //int tt = 0;
                //if (Session["GiamGia"].ToString() == "" || Session["GiamGia"].ToString() == null)
                //{
                //    GiamGia = "";
                //    foreach (DataRow row in dtDichVu.Rows)
                //    {
                //        tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                //    }
                //    TongTienGiam = 0;
                //    Tongtien = tt;
                //}
                //else
                //{
                //    GiamGia = Session["GiamGia"].ToString();
                //    foreach (DataRow row in dtDichVu.Rows)
                //    {
                //        tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                //    }
                //    TongTienGiam = tt * Convert.ToInt32(GiamGia) / 100;
                //    Tongtien = tt - TongTienGiam;
                //}
            }
            else
            {
                //update lại số lượng trong tbHoaDonChiTiet
                var upDichVu = (from hdct in db.tbHoaDonBanHangChiTiets
                                join hd in db.tbHoaDonBanHangs on hdct.hoadon_id equals hd.hoadon_id
                                where hd.hoadon_code == txtHoaDon.Value && hdct.dichvu_id == _id
                                && hdct.hidden == false
                                select hdct).SingleOrDefault();
                upDichVu.hdct_soluong = Convert.ToInt32(txt_SoLuong.Value);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception) { }
                loadHoaDonChiTiet(Convert.ToInt32(upDichVu.hoadon_id));
            }
        }
    }

    protected void btnRemoveDichVu_ServerClick(object sender, EventArgs e)
    {
        int _id = Convert.ToInt32(txt_RemoveID.Value);
        dtDichVu = (DataTable)Session["spChiTiet"];
        /* nếu hơn đơn mới thì xóa bình thường
         * ngược lại thì update lại trong database
         */
        foreach (DataRow row in dtDichVu.Rows)
        {
            string dichvu_id = row["dichvu_id"].ToString();
            if (dichvu_id == _id.ToString())
            {
                dtDichVu.Rows.Remove(row);
                Session["spChiTiet"] = dtDichVu;
                break;
            }
        }
        if (txtHoaDon.Value == "")
        {
            //rpDichVuDaChon.DataSource = dtDichVu;
            //rpDichVuDaChon.DataBind();
            rpHoaDonThanhToan.DataSource = dtDichVu;
            rpHoaDonThanhToan.DataBind();
            //tính lại tổng tiền trong hóa đơn
            Tinhtien();
        }
        else
        {
            //1: xóa dịch vụ được chọn trong tbHoaDonChiTiet
            var dltDichVu = (from hdct in db.tbHoaDonBanHangChiTiets
                             join hd in db.tbHoaDonBanHangs on hdct.hoadon_id equals hd.hoadon_id
                             where hd.hoadon_code == txtHoaDon.Value && hdct.dichvu_id == _id
                             select hdct).FirstOrDefault();
            dltDichVu.hidden = true;
            // 2:Cập nhật lại tổng tiền trong tbHoaDon
            //tbHoaDonBanHang update = db.tbHoaDonBanHangs.Where(x => x.hoadon_code == txtHoaDon.Value).FirstOrDefault();
            try
            {
                db.SubmitChanges();
                Tinhtien();
                loadHoaDonChiTiet(Convert.ToInt32(dltDichVu.hoadon_id));
            }
            catch (Exception) { }

        }
    }

    //Hàm tự tăng
    public string Matutang()
    {
        int year = DateTime.Now.Year;
        var list = from hd in db.tbHoaDonBanHangs select hd;
        string s = "HD";
        if (list.Count() <= 0)
            s = "HD00001";
        else
        {
            var list1 = from hd in db.tbHoaDonBanHangs orderby hd.hoadon_code descending select hd;
            string chuoi = list1.First().hoadon_code;
            int k;
            k = Convert.ToInt32(chuoi.Substring(2, 5));
            k = k + 1;
            if (k < 10) s = s + "0000";
            else if (k < 100)
                s = s + "000";
            else if (k < 1000)
                s = s + "00";
            else if (k < 10000)
                s = s + "0";
            s = s + k.ToString();
        }
        return s;
    }
    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        //lấy thông tin của tk đang nhập
        var getuser = (from u in db.admin_Users
                       where u.username_username == Request.Cookies["UserName"].Value
                       select u).FirstOrDefault();
        string search = txt_Search.Value;
        //alert.alert_Warning(Page,""+search,"");
        /*
         * get data từ table customer 
         * nếu kh có sđt giống với txt search thì get tên kh vào thẻ input
         */
        var getValue = from c in db.tbCustomerAccounts
                       where c.customer_phone == search
                       select c;
        if (getValue.Count() > 0)
        {
            //kiểm tra xem khách hàng này đã tồn tại hóa đơn chưa thanh toán chưa
            // nếu có thì báo lỗi và bắt thanh toán trước 
            // nếu không có thì thêm vào bt
            var checkHoaDon = from hd in db.tbHoaDonBanHangs
                              where hd.khachhang_name == getValue.FirstOrDefault().customer_fullname
                              && hd.active == true && hd.hoadon_createdate.Value.Day == DateTime.Now.Day
                              select hd;
            if (checkHoaDon.Count() > 0)
            {
                alert.alert_Error(Page, "Khách hàng này hiện đang có hóa đơn chưa thanh toán!", "");
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "setName('" + getValue.First().customer_fullname + "')", true);
                //lưu hóa đơn vào table
                tbHoaDonBanHang insert = new tbHoaDonBanHang();
                insert.hoadon_code = txtMaHoaDon.Value;
                insert.khachhang_name = getValue.FirstOrDefault().customer_fullname;
                insert.khachhang_id = getValue.FirstOrDefault().customer_id;
                insert.khuyenmai_id = txtDanhSachChecked.Value;
                ////tính lại tổng tiền trong hóa đơn
                //int tt = 0;
                if (Session["GiamGia"].ToString() == "" || Session["GiamGia"].ToString() == null)
                {
                    insert.hoadon_giamgia = "0";
                }
                else
                {
                    insert.hoadon_giamgia = Session["GiamGia"].ToString();
                }
                insert.hoadon_createdate = DateTime.Now;
                insert.username_id = getuser.username_id;
                insert.active = true;
                insert.nhanvien_id = Convert.ToInt32(ddlNhanVien.SelectedValue);
                db.tbHoaDonBanHangs.InsertOnSubmit(insert);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception) { }
                //------Lưu vào bảng hóa đơn chi tiết
                if (dtDichVu != null)
                {
                    foreach (DataRow row in dtDichVu.Rows)
                    {
                        tbHoaDonBanHangChiTiet ins = new tbHoaDonBanHangChiTiet();
                        ins.hoadon_id = insert.hoadon_id;
                        ins.dichvu_id = Convert.ToInt32(row["dichvu_id"]);
                        ins.hdct_soluong = Convert.ToInt32(row["dichvu_soluong"]);
                        ins.hdct_price = row["dichvu_price"].ToString();
                        ins.hdct_createdate = DateTime.Now;
                        ins.username_id = insert.username_id;
                        ins.hidden = false;
                        ins.hdct_giamgia = insert.hoadon_giamgia;
                        ins.khuyenmai_id = insert.khuyenmai_id;
                        ins.nhanvien_id = Convert.ToInt32(ddlNhanVien.SelectedValue);
                        db.tbHoaDonBanHangChiTiets.InsertOnSubmit(ins);
                        try
                        {
                            db.SubmitChanges();
                        }
                        catch (Exception) { }
                    }
                }
                else { }
            }
        }
        else
        {
            dtDichVu = null;
            /* khi mà nhập khách hàng kiểu vãn lai
             */
            //lưu hóa đơn vào table
            tbHoaDonBanHang insert = new tbHoaDonBanHang();
            insert.hoadon_code = txtMaHoaDon.Value;
            insert.khachhang_name = txt_Guest.Value;
            //insert.khachhang_id = getValue.FirstOrDefault().customer_id;
            insert.khuyenmai_id = txtDanhSachChecked.Value;
            ////tính lại tổng tiền trong hóa đơn
            //int tt = 0;
            if (Session["GiamGia"].ToString() == "" || Session["GiamGia"].ToString() == null)
            {
                insert.hoadon_giamgia = "0";
            }
            else
            {
                insert.hoadon_giamgia = Session["GiamGia"].ToString();
            }
            insert.hoadon_createdate = DateTime.Now;
            insert.nhanvien_id = getuser.username_id;
            insert.active = true;
            insert.nhanvien_id = Convert.ToInt32(ddlNhanVien.SelectedValue);
            db.tbHoaDonBanHangs.InsertOnSubmit(insert);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception) { }
            //------Lưu vào bảng hóa đơn chi tiết
            if (dtDichVu != null)
            {
                foreach (DataRow row in dtDichVu.Rows)
                {
                    tbHoaDonBanHangChiTiet ins = new tbHoaDonBanHangChiTiet();
                    ins.hoadon_id = insert.hoadon_id;
                    ins.dichvu_id = Convert.ToInt32(row["dichvu_id"]);
                    ins.hdct_soluong = Convert.ToInt32(row["dichvu_soluong"]);
                    ins.hdct_price = row["dichvu_price"].ToString();
                    ins.hdct_createdate = DateTime.Now;
                    ins.nhanvien_id = insert.nhanvien_id;
                    ins.hidden = false;
                    ins.hdct_giamgia = insert.hoadon_giamgia;
                    ins.khuyenmai_id = insert.khuyenmai_id;
                    ins.nhanvien_id = Convert.ToInt32(ddlNhanVien.SelectedValue);
                    db.tbHoaDonBanHangChiTiets.InsertOnSubmit(ins);
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception) { }
                }
            }
            //alert.alert_Warning(Page, "Không tìm thấy thông tin khách hàng!", "");
        }
        loadHoaDon();
        txtHoaDon.Value = txtMaHoaDon.Value;
        string matutang = Matutang();
        txtMaHoaDon.Value = matutang;
    }

    protected void btnApDungKM_ServerClick(object sender, EventArgs e)
    {
        try
        {
            if (txtDanhSachChecked.Value != "")
            {
                if (dtDichVu == null)
                {
                    alert.alert_Warning(Page, "Vui lòng chọn dịch vụ trước khi chọn khuyến mãi", "");
                }
                else
                {
                    int percent_km = 0;
                    string checkid = txtDanhSachChecked.Value;
                    string[] getcheckid = checkid.Split(',');
                    foreach (var item in getcheckid)
                    {
                        var getCTKM = (from km in db.tbChuongTrinhKhuyenMais
                                       where km.khuyenmai_id == Convert.ToInt32(item)
                                       select km).FirstOrDefault();
                        percent_km += Convert.ToInt32(getCTKM.khuyenmai_percent);
                    }
                    //tính lại tổng tiền trong hóa đơn
                    int tt = 0;
                    Session["GiamGia"] = percent_km;
                    GiamGia = percent_km + "";
                    foreach (DataRow row in dtDichVu.Rows)
                    {
                        tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                    }
                    TongTienGiam = tt * percent_km / 100;
                    Tongtien = tt - TongTienGiam;
                    //update lại giảm giá nếu là khách hàng đã tồn tại
                    if (txtHoaDon.Value != "")
                    {
                        //tbHoaDon
                        tbHoaDonBanHang update = db.tbHoaDonBanHangs.Where(x => x.hoadon_code == txtHoaDon.Value).FirstOrDefault();
                        update.hoadon_giamgia = GiamGia;
                        update.khuyenmai_id = txtDanhSachChecked.Value;
                        //tbHoaDonChiTiet
                        var hdct = from ct in db.tbHoaDonBanHangChiTiets
                                   where ct.hoadon_id == update.hoadon_id
                                   select ct;
                        foreach (var item in hdct)
                        {
                            item.hdct_giamgia = GiamGia;
                            item.khuyenmai_id = txtDanhSachChecked.Value;
                        }
                        try
                        {
                            db.SubmitChanges();
                        }
                        catch (Exception) { }
                    }
                }
            }
            else
            {
                GiamGia = "";
                TongTienGiam = 0;
                int tt = 0;
                foreach (DataRow row in dtDichVu.Rows)
                {
                    tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                }
                Tongtien = tt;
            }
        }
        catch (Exception) { }
    }
    protected void btnXemHoaDon_ServerClick(object sender, EventArgs e)
    {
        try
        {
            if (txtHoaDon_ID.Value != "")
            {
                blog_NhanVien.Visible = true;
                int _id = Convert.ToInt32(txtHoaDon_ID.Value);
                //alert.alert_Warning(Page,"Xem hóa đơn"+_id,"");
                //load lại hóa đơn
                loadHoaDonChiTiet(_id);
                var getHD = from hd in db.tbHoaDonBanHangs
                            join u in db.admin_Users on hd.username_id equals u.username_id
                            where hd.hoadon_id == _id
                            select new
                            {
                                hd.hoadon_id,
                                hd.hoadon_code,
                                hd.khuyenmai_id,
                                hd.khachhang_name,
                                hd.nhanvien_id,
                                u.username_id,
                                u.username_fullname
                            };
                txtHoaDon.Value = getHD.FirstOrDefault().hoadon_code;
                txtKhuyenMaiDaChon.Value = getHD.FirstOrDefault().khuyenmai_id;
                txtDanhSachChecked.Value = getHD.FirstOrDefault().khuyenmai_id;
                if (getHD.FirstOrDefault().nhanvien_id != null)
                {
                    ddlNhanVien.SelectedValue = getHD.FirstOrDefault().nhanvien_id + "";
                }
                else { }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "setName('" + getHD.FirstOrDefault().khachhang_name + "','" + getHD.FirstOrDefault().hoadon_code + "')", true);
            }
        }
        catch (Exception) { }
    }
    //Kiểm tra Email có hợp lệ
    private bool isEmail(string txtEmail)
    {
        Regex re = new Regex(@"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                      RegexOptions.IgnoreCase);
        return re.IsMatch(txtEmail);
    }
    protected void btnLuuAccount_ServerClick(object sender, EventArgs e)
    {
        //lấy thông tin của tk đang nhập
        var getuser = (from u in db.admin_Users
                       where u.username_username == Request.Cookies["UserName"].Value
                       select u).FirstOrDefault();
        //Kiểm tra xem SĐT đã tồn tại chưa
        var checkAccount = from dt in db.tbCustomerAccounts
                           where dt.customer_phone.Trim() == txt_Phone.Value.Trim()
                           select dt;
        if (checkAccount.Count() > 0)
        {
            alert.alert_Error(Page, "Số điện thoại đã tồn tại", "");
        }
        else
        {
            tbCustomerAccount insert = new tbCustomerAccount();
            //kiểm tra xem email đúng không
            if (txt_Email.Value != "")
            {
                if (isEmail(txt_Email.Value) != true)
                {
                    alert.alert_Error(Page, "Email nhập không hợp lệ!", "");
                }
                else
                {
                    insert.customer_fullname = txt_NameKhachHang.Value;
                    insert.customer_phone = txt_Phone.Value;
                    insert.customer_email = txt_Email.Value;
                    insert.customer_address = txt_Address.Value;
                    insert.hidden = false;
                    db.tbCustomerAccounts.InsertOnSubmit(insert);
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception) { }
                    alert.alert_Success(Page, "Lưu thành công", "");
                    setNull();
                }
            }
            else
            {
                insert.customer_fullname = txt_NameKhachHang.Value;
                insert.customer_phone = txt_Phone.Value;
                //insert.customer_email = txt_Email.Value;
                insert.customer_address = txt_Address.Value;
                insert.hidden = false;
                db.tbCustomerAccounts.InsertOnSubmit(insert);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception) { }
                alert.alert_Success(Page, "Lưu thành công", "");
                setNull();
            }
            //lưu khách hàng mới với hóa đơn rỗng
            tbHoaDonBanHang ins = new tbHoaDonBanHang();
            ins.hoadon_code = txtMaHoaDon.Value;
            ins.khachhang_name = insert.customer_fullname;
            ins.hoadon_createdate = DateTime.Now;
            ins.khachhang_id = insert.customer_id;
            ins.username_id = getuser.username_id;
            ins.active = true;
            db.tbHoaDonBanHangs.InsertOnSubmit(ins);
            try
            {
                db.SubmitChanges();
            }
            catch (Exception) { }
            loadHoaDon();
        }
    }
    public void setNull()
    {
        txt_NameKhachHang.Value = "";
        txt_Phone.Value = "";
        txt_Email.Value = "";
        txt_Address.Value = "";
    }

    public void setThemMoi()
    {
        Session["spChiTiet"] = null;
        Session["GiamGia"] = "";
        loadData();
        try
        {
            dtDichVu.Clear();
        }
        catch (Exception) { }
        GiamGia = "";
        TongTienGiam = 0;
        Tongtien = 0;
        txtHoaDon.Value = ""; ;
        txtDanhSachChecked.Value = "";
        txtKhuyenMaiDaChon.Value = "";
        txtCountChecked.Value = "";
        string matutang = Matutang();
        txtMaHoaDon.Value = matutang;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "setNULL()", true);
    }

    protected void btnThemHoaDonMoi_ServerClick(object sender, EventArgs e)
    {
        txtHoaDon.Value = "";
        setThemMoi();
        loadData();
        blog_NhanVien.Visible = false;
        dtDichVu = null;
    }

    protected void btnThanhToan_ServerClick(object sender, EventArgs e)
    {
        //lấy thông tin của tk đang nhập
        var getuser = (from u in db.admin_Users
                       where u.username_username == Request.Cookies["UserName"].Value
                       select u).FirstOrDefault();
        //xóa hết data tạm cũ và insert lại từ đầu
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "text", "checkNULL()", true);

        //nếu chọn hóa đơn cũ thanh toán
        if (txtHoaDon.Value != "")
        {
            if (dtDichVu == null || Session["spchitiet"] == null)
            {
                alert.alert_Error(Page, "Hóa đơn này đang rỗng", "");
            }
            else if (ddlNhanVien.SelectedValue == "" || ddlNhanVien.SelectedValue == "0")
            {
                alert.alert_Error(Page, "Vui lòng chọn nhân viên trước khi thanh toán", "");
            }
            else
            {
                // update lại thông tin ở tbHoaDon
                tbHoaDonBanHang update = db.tbHoaDonBanHangs.Where(x => x.hoadon_code == txtHoaDon.Value).FirstOrDefault();
                update.nhanvien_id = Convert.ToInt32(ddlNhanVien.SelectedValue);
                update.username_id = getuser.username_id;
                db.SubmitChanges();
                //xóa hết thông tin ở table tbHoaDonChiTiet và lưu lại mới
                var getChiTiet = from ct in db.tbHoaDonBanHangChiTiets
                                 join hd in db.tbHoaDonBanHangs on ct.hoadon_id equals hd.hoadon_id
                                 where ct.hoadon_id == update.hoadon_id
                                 select ct;
                db.tbHoaDonBanHangChiTiets.DeleteAllOnSubmit(getChiTiet);
                db.SubmitChanges();
                //Tính tiền
                int tt = 0;
                foreach (DataRow row in dtDichVu.Rows)
                {
                    tt += Convert.ToInt32(row["dichvu_thanhtien"]);
                    //lưu vào table tbHoaDonChiTiet
                    tbHoaDonBanHangChiTiet ins = new tbHoaDonBanHangChiTiet();
                    ins.hoadon_id = update.hoadon_id;
                    ins.dichvu_id = Convert.ToInt32(row["dichvu_id"]);
                    ins.hdct_soluong = Convert.ToInt32(row["dichvu_soluong"]);
                    ins.hdct_price = row["dichvu_price"].ToString();
                    ins.hdct_createdate = DateTime.Now;
                    ins.username_id = getuser.username_id;
                    ins.hidden = false;
                    ins.hdct_giamgia = update.hoadon_giamgia;
                    ins.khuyenmai_id = update.khuyenmai_id;
                    ins.nhanvien_id = Convert.ToInt32(ddlNhanVien.SelectedValue);
                    db.tbHoaDonBanHangChiTiets.InsertOnSubmit(ins);
                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (Exception) { }
                }
                TongTienGiam = tt * Convert.ToInt32(update.hoadon_giamgia) / 100;
                Tongtien = tt - TongTienGiam;
                update.hoadon_tongtien = tt + "";
                update.hoadon_phaitra = Tongtien + "";
                update.hoadon_tongtiengiam = TongTienGiam + "";
                update.hoadon_giothanhtoan = DateTime.Now;
                update.active = false;
                update.hidden = false;
                db.SubmitChanges();
                loadHoaDon();
                setThemMoi();
                //chuyển sang trang report để in hóa đơn
                int idHD = update.hoadon_id;
                Response.Redirect("~/admin-xuat-hoa-don-" + idHD);
            }
        }
        else
        {
            alert.alert_Warning(Page, "Vui lòng chọn hóa đơn", "");
        }
    }

    protected void btnXoaKhachHang_ServerClick(object sender, EventArgs e)
    {
        try
        {
            if (txtHoaDon_ID.Value != "")
            {
                int _id = Convert.ToInt32(txtHoaDon_ID.Value);
                var getHD = (from hd in db.tbHoaDonBanHangs
                             where hd.hoadon_id == _id
                             select hd).FirstOrDefault();
                db.tbHoaDonBanHangs.DeleteOnSubmit(getHD);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception) { }
                var getHDCT = from hdct in db.tbHoaDonBanHangChiTiets
                              where hdct.hoadon_id == _id
                              select hdct;
                db.tbHoaDonBanHangChiTiets.DeleteAllOnSubmit(getHDCT);
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception) { }
                loadHoaDon();
                Session["GiamGia"] = "";
                Session["spChiTiet"] = null;
                loadData();
                string matutang = Matutang();
                txtMaHoaDon.Value = matutang;
            }
        }
        catch (Exception) { }
    }
    protected void btnDichVuChiTiet_ServerClick(object sender, EventArgs e)
    {
        //khi click đổ ra ds dịch vụ theo nhóm đã chọn
        if (txtNhomDichVu_id.Value != "")
        {
            int _id = Convert.ToInt32(txtNhomDichVu_id.Value);
            var loadDichvu = from dv in db.tbDichVus
                             join c in db.tbNhomDichVus on dv.dvcate_id equals c.dvcate_id
                             where c.dvcate_id == _id
                             select dv;
            rpDichVu.DataSource = loadDichvu;
            rpDichVu.DataBind();
        }
    }

    protected void ddlNhanVien_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            // khi ấn chọn nhân viên thì lưu id của nhân viên vào hóa đơn
            if (txtHoaDon.Value == "")
            {
                alert.alert_Warning(Page, "Vui lòng chọn hóa đơn trước", "");
            }
            else
            {
                var getHD = (from hd in db.tbHoaDonBanHangs
                             where hd.hoadon_code == txtHoaDon.Value
                             select hd).FirstOrDefault();
                getHD.nhanvien_id = Convert.ToInt32(ddlNhanVien.SelectedValue);
                db.SubmitChanges();
                var getHDCT = from hdct in db.tbHoaDonBanHangChiTiets
                              where hdct.hoadon_id == getHD.hoadon_id
                              select hdct;
                if (getHDCT.Count() > 0)
                {
                    foreach (var item in getHDCT)
                    {
                        item.nhanvien_id = Convert.ToInt32(ddlNhanVien.SelectedValue);
                        db.SubmitChanges();
                    }
                }
            }
        }
        catch (Exception) { }
        //int idnv = Convert.ToInt32(ddlNhanVien.SelectedValue);
        //alert.alert_Warning(Page, "" + idnv, "");
    }

    protected void btnChangeName_ServerClick(object sender, EventArgs e)
    {
        try
        {
            var getHD = (from hd in db.tbHoaDonBanHangs
                         where hd.hoadon_code == txtHoaDon.Value
                         select hd).FirstOrDefault();
            getHD.khachhang_name = txt_newName.Value;
            db.SubmitChanges();
            loadHoaDon();
        }
        catch (Exception) { }
    }
}