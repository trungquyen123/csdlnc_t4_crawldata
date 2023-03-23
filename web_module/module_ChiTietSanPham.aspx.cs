using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class web_module_module_ChiTietSanPham : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        var getChiTiet = from pr in db.tbProducts
                         join prc in db.tbProductCates on pr.productcate_id equals prc.productcate_id
                         join ct in db.tbCities on Convert.ToInt32(pr.h1_seo) equals ct.city_id
                         where pr.product_id == Convert.ToInt32(RouteData.Values["id_ctsp"])
                         select new
                         {
                             pr.product_id,
                             pr.product_image,
                             pr.product_title,
                             ct.city_name,
                             pr.product_price_new,
                             pr.product_price,
                         };
        rpSanPham.DataSource = getChiTiet;
        rpSanPham.DataBind();
    }
}