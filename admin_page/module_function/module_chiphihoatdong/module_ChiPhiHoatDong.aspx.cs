using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_page_module_function_module_chiphihoatdong_module_ChiPhiHoatDong : System.Web.UI.Page
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
        // load data đổ vào var danh sách
        var getData = from n in db.tbChiPhiHoatDongs
                      join nc in db.tbNhomChiPhiHoatDongs on n.nhomhd_id equals nc.nhomhd_id
                      orderby n.chiphi_id descending
                      where n.hidden == false
                      select new
                      {
                          n.chiphi_id,
                          n.chiphi_content,
                          n.chiphi_sotien,
                          n.chiphi_createdate,
                          n.chiphi_month,
                          nc.nhomhd_name
                      };
        // đẩy dữ liệu vào gridivew
        grvList.DataSource = getData;
        grvList.DataBind();

        var getCate = from c in db.tbNhomChiPhiHoatDongs
                      where c.hidden == false
                      select c;
        ddlNhomChiPhi.DataSource = getCate;
        ddlNhomChiPhi.DataBind();

    }
    private void setNULL()
    {
        txt_Content.Value = "";
        txt_Price.Text = "";
    }
    protected void btnThem_Click(object sender, EventArgs e)
    {
        Session["_id"] = null;
        setNULL();
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Insert", "popupControl.Show();", true);
    }

    protected void btnChiTiet_Click(object sender, EventArgs e)
    {
        // get value từ việc click vào gridview
        _id = Convert.ToInt32(grvList.GetRowValues(grvList.FocusedRowIndex, new string[] { "chiphi_id" }));
        // đẩy id vào session
        Session["_id"] = _id;
        var getData = (from n in db.tbChiPhiHoatDongs
                       join nc in db.tbNhomChiPhiHoatDongs on n.nhomhd_id equals nc.nhomhd_id
                       orderby n.chiphi_id descending
                       where n.chiphi_id == _id
                       select new
                       {
                           n.chiphi_id,
                           n.chiphi_content,
                           n.chiphi_sotien,
                           n.chiphi_createdate,
                           n.chiphi_month,
                           nc.nhomhd_name
                       }).Single();
        txt_Price.Text = getData.chiphi_sotien + "";
        txt_Content.Value = getData.chiphi_content;
        ddlNhomChiPhi.Text = getData.nhomhd_name;
        //txt_Month.Value = getData.chiphi_month.Split('/').First();
        ddlMonth.SelectedIndex = Convert.ToInt32(getData.chiphi_month.Split('/').First());
        ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Detail", "popupControl.Show();", true);
    }

    protected void btnXoa_Click(object sender, EventArgs e)
    {
        cls_ChiPhiHoatDong cls = new cls_ChiPhiHoatDong();
        List<object> selectedKey = grvList.GetSelectedFieldValues(new string[] { "chiphi_id" });
        if (selectedKey.Count > 0)
        {
            foreach (var item in selectedKey)
            {
                if (cls.Linq_Xoa(Convert.ToInt32(item)))
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Xóa thành công!', '','success').then(function(){window.location = '/admin-quan-ly-chi-phi-hoat-dong';})", true);
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
        if (txt_Price.Text != "")
            return true;
        else return false;
    }
    protected void btnLuu_Click(object sender, EventArgs e)
    {
        cls_ChiPhiHoatDong cls = new cls_ChiPhiHoatDong();
        if (checknull() == false)
            alert.alert_Warning(Page, "Hãy nhập giá chi phí!", "");
        else if (Convert.ToInt32(ddlNhomChiPhi.Value) == 0)
        {
            alert.alert_Warning(Page, "Vui lòng chọn loại chi phí!", "");
        }
        else if (ddlMonth.SelectedIndex.ToString() == "" || ddlMonth.Value == "0")
        {
            alert.alert_Warning(Page, "Vui lòng chọn tháng!", "");
        }
        else
        {
            if (Session["_id"] == null)
            {
                if (cls.Linq_Them(txt_Content.Value, Convert.ToInt32(txt_Price.Text), ddlMonth.SelectedIndex + "/" + DateTime.Now.Year, Convert.ToInt32(ddlNhomChiPhi.Value)))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Thêm thành công!', '','success').then(function(){window.location = '/admin-quan-ly-chi-phi-hoat-dong';})", true);
                }
                else alert.alert_Error(Page, "Thêm thất bại", "");
            }
            else
            {
                if (cls.Linq_Sua(Convert.ToInt32(Session["_id"].ToString()), txt_Content.Value, Convert.ToInt32(txt_Price.Text), ddlMonth.SelectedIndex + "/" + DateTime.Now.Year, Convert.ToInt32(ddlNhomChiPhi.Value)))
                {
                    popupControl.ShowOnPageLoad = false;
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "AlertBox", "swal('Cập nhật thành công!', '','success').then(function(){window.location = '/admin-quan-ly-chi-phi-hoat-dong';})", true);
                }
                else alert.alert_Error(Page, "Cập nhật thất bại", "");
            }
        }
    }
}