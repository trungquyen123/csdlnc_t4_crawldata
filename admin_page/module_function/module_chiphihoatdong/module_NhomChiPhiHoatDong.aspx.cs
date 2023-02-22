using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_chiphihoatdong_module_NhomChiPhiHoatDong : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int _id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            if (!IsPostBack)
            {
                Session["_id"] = 0;
            }
            loadData();
        }
        else
        {
            Response.Redirect("/admin-login");
        }

    }
    private void loadData()
    {
        var getData = from sp in db.tbNhomChiPhiHoatDongs
                      where sp.hidden == false
                      orderby sp.nhomhd_id descending
                      select sp;
        grvList.DataSource = getData;
        grvList.DataBind();
    }
    private void setNULL()
    {
        txt_TenNhom.Text = "";
    }

    protected void btnThem_Click(object sender, EventArgs e)
    {
        Session["_id"] = 0;
        setNULL();
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show();", true);
    }

    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        // get value từ việc click vào gridview
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "nhomhd_id" }));
        // đẩy id vào session
        Session["_id"] = _id;
        var getData = (from nc in db.tbNhomChiPhiHoatDongs
                       where nc.nhomhd_id == _id
                       select nc).Single();
        txt_TenNhom.Text = getData.nhomhd_name;
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show();", true);
    }

    protected void btnXoa_Click(object sender, EventArgs e)
    {
        cls_NhomChiPhiHoatDong cls = new cls_NhomChiPhiHoatDong();
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "nhomhd_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-quan-ly-nhom-chi-phi-hoat-dong';})", true);
                else
                    alert.alert_Error(Page, "Xóa thất bại", "");
            }
        }
        else
            alert.alert_Warning(Page, "Bạn chưa chọn dữ liệu", "");
    }

    protected void btnLuu_Click(object sender, EventArgs e)
    {
        cls_NhomChiPhiHoatDong cls = new cls_NhomChiPhiHoatDong();
        if (Session["_id"].ToString() == "0")
        {
            if (cls.Linq_Them(txt_TenNhom.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-quan-ly-nhom-chi-phi-hoat-dong';})", true);
            }
            else alert.alert_Error(Page, "Thêm thất bại", "");
        }
        else
        {
            if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), txt_TenNhom.Text))
                ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-quan-ly-nhom-chi-phi-hoat-dong';})", true);
            else alert.alert_Error(Page, "Cập nhật thất bại", "");
        }
        popupControl.ShowOnPageLoad = false;
    }
}