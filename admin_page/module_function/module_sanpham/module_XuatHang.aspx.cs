using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_sanpham_module_XuatHang : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    private int stt = 1;
    private int _id;
    protected void Page_Load(object sender, EventArgs e)
    {
        loaddata();
    }
    private void loaddata()
    {
        Session["spChiTiet"] = null;
        var getdata = from nh in db.tbXuatHangs
                      join u in db.admin_Users on nh.username_id equals u.username_id
                      orderby nh.xuathang_code descending
                      select new
                      {
                          nh.xuathang_code,
                          nh.xuathang_content,
                          nh.xuathang_id,
                          u.username_fullname,
                          nh.xuathang_createdate
                      };
        grvList.DataSource = getdata;
        grvList.DataBind();
    }
    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "xuathang_id" }));
        Response.Redirect("admin-xuat-hang-" + _id);
    }
}