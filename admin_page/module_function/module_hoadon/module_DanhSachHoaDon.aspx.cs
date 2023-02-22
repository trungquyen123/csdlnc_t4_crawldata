using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_hoadon_module_DanhSachHoaDon : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int _id;
    DataTable dtDichVu;
    CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            //admin_User logedMember = Session["AdminLogined"] as admin_User;
            //if (logedMember.groupuser_id == 3)
            //    Response.Redirect("/user-home");
            if (!IsPostBack)
            {
                Session["_id"] = 0;
                loaddatatable();
            }
            loadData();
        }
        else
        {
            Response.Redirect("/admin-login");
        }
        //phân quyền cho nhân viên chỉ được thêm
       
        //if ()
        //{

        //}
    }
    private void loadData()
    {
        // load data đổ vào var danh sách
        var getData = from n in db.tbHoaDonBanHangs
                      where n.hidden == false 
                      orderby n.hoadon_id descending
                      select n;
        // đẩy dữ liệu vào gridivew
        grvList.DataSource = getData;
        grvList.DataBind();
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin-them-moi-hoa-don");
    }
    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        // get value từ việc click vào gridview
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "hoadon_id" }));
        // đẩy id vào session
        Session["_id"] = _id;
        var getData = (from hd in db.tbHoaDonBanHangs
                       join u in db.admin_Users on hd.username_id equals u.username_id
                       where hd.hoadon_id == _id
                       select new
                       {
                           hd.hoadon_id,
                           hd.khachhang_name,
                           hd.hoadon_tongtien,
                           hd.hoadon_phaitra,
                           hd.hoadon_giothanhtoan,
                           hd.hoadon_tongtiengiam,
                           u.username_id,
                           u.username_fullname
                       }).Single();
        txt_KhachHang.Text = getData.khachhang_name;
        txt_Date.Text = getData.hoadon_giothanhtoan.Value.ToString("dd/MM/yyyy").Replace(' ', 'T');
        txt_TongHoaDon.Text = double.Parse(getData.hoadon_tongtien.ToString()).ToString("#,###", cul.NumberFormat);
        txt_PhaiTra.Text = double.Parse(getData.hoadon_phaitra.ToString()).ToString("#,###", cul.NumberFormat);
        txt_NhanVien.Text = getData.username_fullname;
        txt_GiamGia.Text = getData.hoadon_tongtiengiam;
        var getHDCT = from hdct in db.tbHoaDonBanHangChiTiets
                      join hd in db.tbHoaDonBanHangs on hdct.hoadon_id equals hd.hoadon_id
                      join dv in db.tbDichVus on hdct.dichvu_id equals dv.dichvu_id
                      join nv in db.admin_Users on hdct.username_id equals nv.username_id
                      where hd.hoadon_id == _id && hdct.hidden == false
                      select new
                      {
                          hdct.hdct_id,
                          hdct.dichvu_id,
                          hdct.hdct_soluong,
                          dv.dichvu_title,
                          hdct.hdct_price,

                      };
        //get lại data và datatable dtDichVu
        double thanhtien = 0;
        string a;
        foreach (var item in getHDCT)
        {
            if (dtDichVu != null)
            {
                DataRow row = dtDichVu.NewRow();
                row["dichvu_id"] = item.dichvu_id;
                row["dichvu_title"] = item.dichvu_title;
                row["dichvu_soluong"] = item.hdct_soluong;
                row["dichvu_price"] = double.Parse(item.hdct_price.ToString()).ToString("#,###", cul.NumberFormat);
                thanhtien = Convert.ToInt32(item.hdct_price.ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                a = double.Parse(thanhtien.ToString()).ToString("#,###", cul.NumberFormat);
                row["dichvu_thanhtien"] = a;
                dtDichVu.Rows.Add(row);
            }
            else
            {
                loaddatatable();
                DataRow row = dtDichVu.NewRow();
                row["dichvu_id"] = item.dichvu_id;
                row["dichvu_title"] = item.dichvu_title;
                row["dichvu_soluong"] = item.hdct_soluong;
                row["dichvu_price"] = double.Parse(item.hdct_price.ToString()).ToString("#,###", cul.NumberFormat);
                thanhtien = Convert.ToInt32(item.hdct_price.ToString()) * Convert.ToInt32(row["dichvu_soluong"].ToString());
                a = double.Parse(thanhtien.ToString()).ToString("#,###", cul.NumberFormat);
                row["dichvu_thanhtien"] = a;
                dtDichVu.Rows.Add(row);
            }
        }
        rpHoaDonChiTiet.DataSource = dtDichVu;
        rpHoaDonChiTiet.DataBind();
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show();", true);
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
    protected void btnXoa_Click(object sender, EventArgs e)
    {
        cls_HoaDonDichVu cls = new cls_HoaDonDichVu();
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "hoadon_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-hoa-don-ban-hang';})", true);
                }
                else
                    alert.alert_Error(Page, "Xóa thất bại", "");
            }
        }
        else
            alert.alert_Warning(Page, "Bạn chưa chọn dữ liệu", "");
    }
}