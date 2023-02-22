using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_dichvu_module_DichVu : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    private int _id;
    public string image;
    protected void Page_Load(object sender, EventArgs e)
    {
        edtnoidung.Toolbars.Add(HtmlEditorToolbar.CreateStandardToolbar1());
        if (Request.Cookies["userName"] != null)
        {
            //admin_User logedMember = Session["AdminLogined"] as admin_User;
            //if (logedMember.groupuser_id == 3)
            //    Response.Redirect("/user-home");
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
        // load data đổ vào var danh sách
        var getData = from n in db.tbDichVus
                      join c in db.tbNhomDichVus on n.dvcate_id equals c.dvcate_id
                      orderby c.position ascending
                      select new
                      {
                          n.dichvu_id,
                          n.dichvu_title,
                          n.dichvu_content,
                          n.dichvu_price,
                          n.dvcate_id,
                          c.dvcate_name
                      };
        // đẩy dữ liệu vào gridivew
        grvList.DataSource = getData;
        grvList.DataBind();
        ddlNhomdichvu.DataSource = from c in db.tbNhomDichVus
                                   select c;
        ddlNhomdichvu.DataBind();
    }
    private void setNULL()
    {
        txt_Title.Text = "";
        txt_Price.Value = "";
        edtnoidung.Html = "";
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["userName"] != null)
        {
            Session["_id"] = null;
            setNULL();
            ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show();showImg('');", true);
        }
        else
        {
            Response.Redirect("/admin-login");
        }
    }

    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        // get value từ việc click vào gridview
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "dichvu_id" }));
        // đẩy id vào session
        Session["_id"] = _id;
        var getData = (from n in db.tbDichVus
                       join c in db.tbNhomDichVus on n.dvcate_id equals c.dvcate_id
                       where n.dichvu_id == _id
                       select new
                       {
                           n.dichvu_id,
                           n.dichvu_title,
                           n.dichvu_content,
                           n.dichvu_price,
                           n.dvcate_id,
                           c.dvcate_name
                       }).SingleOrDefault();
        txt_Title.Text = getData.dichvu_title;
        txt_Price.Value = getData.dichvu_price;
        edtnoidung.Html = getData.dichvu_content;
        ddlNhomdichvu.Text = getData.dvcate_name;
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show();", true);
    }

    protected void btnXoa_Click(object sender, EventArgs e)
    {
        cls_DichVu cls = new cls_DichVu();
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "dichvu_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
              
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-quan-ly-dich-vu';})", true);
                }
                else
                    alert.alert_Error(Page, "Xóa thất bại", "");
            }
        }
        else
            alert.alert_Warning(Page, "Bạn chưa chọn dữ liệu", "");
    }
    public bool checknull()
    {
        if (edtnoidung.Html != "" || txt_Price.Value != "")
            return true;
        else return false;
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        cls_DichVu cls = new cls_DichVu();
        if (checknull() == false)
            alert.alert_Warning(Page, "Hãy nhập đầy đủ thông tin!", "");
        else
        {
            if (Session["_id"] == null)
            {
                if (cls.Linq_Them(txt_Title.Text, edtnoidung.Html, txt_Price.Value,Convert.ToInt32(ddlNhomdichvu.Value)))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-quan-ly-dich-vu';})", true);
                }
                else alert.alert_Error(Page, "Thêm thất bại", "");
            }
            else
            {
                if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), txt_Title.Text, edtnoidung.Html, txt_Price.Value, Convert.ToInt32(ddlNhomdichvu.Value)))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-quan-ly-dich-vu';})", true);
                }
                else alert.alert_Error(Page, "Cập nhật thất bại", "");
            }
        }
    }
}