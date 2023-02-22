using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_hoadon_module_InHoaDonDichVu : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    public string PhaiTra = "0";
    public string GiamGia = "0";
    public string TongHoaDon = "0";
    public string KhachHang_name = "", MaHD = "", date = "", time = "",Nhanvien="";
    DataTable dtDichVu;
    CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            loadHoaDonChiTiet();
        }
        catch (Exception) { }

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
    public void loadHoaDonChiTiet()
    {
        int IDHD = Convert.ToInt32(RouteData.Values["id"].ToString());
        //var getHD = (from hd in db.tbHoaDonBanHangs
        //             where hd.hoadon_id == IDHD
        //            select hd).FirstOrDefault();
        var getHD = from hdct in db.tbHoaDonBanHangChiTiets
                    join hd in db.tbHoaDonBanHangs on hdct.hoadon_id equals hd.hoadon_id
                    join dv in db.tbDichVus on hdct.dichvu_id equals dv.dichvu_id
                    join nv in db.admin_Users on hdct.nhanvien_id equals nv.username_id
                    where hd.hoadon_id == IDHD && hdct.hidden == false
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
                        hd.khachhang_name,
                        nv.username_fullname
                    };
        //get lại data và datatable dtDichVu
        dtDichVu = null;
        Session["spChiTiet"] = null;
        double thanhtien = 0;
        string a;
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
                thanhtien = Convert.ToInt32(row["dichvu_price"].ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                a = double.Parse(thanhtien.ToString()).ToString("#,###", cul.NumberFormat);
                row["dichvu_thanhtien"] = a;
                dtDichVu.Rows.Add(row);
                Session["spChiTiet"] = dtDichVu;
            }
            else
            {
                loaddatatable();
                DataRow row = dtDichVu.NewRow();
                row["dichvu_id"] = item.dichvu_id;
                row["dichvu_title"] = item.dichvu_title;
                row["dichvu_soluong"] = item.hdct_soluong;
                row["dichvu_price"] = item.dichvu_price;
                thanhtien = Convert.ToInt32(row["dichvu_price"].ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                a = double.Parse(thanhtien.ToString()).ToString("#,###", cul.NumberFormat);
                row["dichvu_thanhtien"] = a;
                dtDichVu.Rows.Add(row);
                Session["spChiTiet"] = dtDichVu;
            }
            TongHoaDon = double.Parse(item.hoadon_tongtien).ToString("#,###", cul.NumberFormat);
            GiamGia = double.Parse(item.hoadon_tongtiengiam).ToString("#,###", cul.NumberFormat);
            PhaiTra = double.Parse(item.hoadon_phaitra).ToString("#,###", cul.NumberFormat);
            KhachHang_name = item.khachhang_name;
            MaHD = item.hoadon_code;
            date = DateTime.Now.ToShortDateString();
            time = DateTime.Now.ToShortTimeString().Split(' ').First();
            Nhanvien = item.username_fullname;
        }
        rpHoaDonThanhToan.DataSource = dtDichVu;
        rpHoaDonThanhToan.DataBind();
    }
}