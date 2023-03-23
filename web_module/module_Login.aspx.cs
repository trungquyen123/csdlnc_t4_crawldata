using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class web_module_module_Login : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    cls_Alert alert = new cls_Alert();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_ServerClick(object sender, EventArgs e)
    {
        var check = from acc in db.tbCustomerAccounts
                    where acc.customer_user == txtUser.Value
                    && acc.customer_pass == txtPass.Value
                    select acc;
        if (check.Count() > 0)
        {
            tbCustomerAccount list = check.Single();
            HttpCookie ck = new HttpCookie("User");
            string s = ck.Value;
            ck.Value = txtUser.Value;
            ck.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(ck);
            Response.Redirect("/home");
        }
        else
        {
            alert.alert_Warning(Page, "Sai thông tin!", "");
        }
    }
}