using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_NhapHang : System.Web.UI.Page
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
        var getdata = from nh in db.tbNhapHangs
                      join u in db.admin_Users on nh.username_id equals u.username_id
                      orderby nh.nhaphang_code descending
                      select new { 
                       nh.nhaphang_code,
                       nh.nhaphang_content,
                       nh.nhaphang_id,
                       u.username_fullname,
                       nh.nhaphang_createdate
                      };
        grvList.DataSource = getdata;
        grvList.DataBind();
    }

    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "nhaphang_id" }));
        Response.Redirect("admin-nhap-hang-" + _id);
    }
}