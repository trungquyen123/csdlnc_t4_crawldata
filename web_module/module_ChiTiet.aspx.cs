using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class web_module_module_ChiTiet : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadData();
        var getLoai = from prc in db.tbProductCates
                      where prc.productcate_id == Convert.ToInt32(RouteData.Values["id_ct"])
                      select new
                      {
                          prc.productcate_title,
                      };
        rpLoai.DataSource = getLoai;
        rpLoai.DataBind();
        var getThuongHieu = from pr in db.tbProducts
                            where pr.productcate_id == Convert.ToInt32(RouteData.Values["id_ct"])
                            group pr by pr.h1_seo into key
                            select new
                            {
                                id_TH = key.Key,
                                thuonghieu = (from prr in db.tbCities
                                              where prr.city_id == Convert.ToInt32(key.Key)
                                              select prr).FirstOrDefault().city_name,
                            };
        rpThuongHieu.DataSource = getThuongHieu;
        rpThuongHieu.DataBind();
    }

    protected void btnChangeTH_ServerClick(object sender, EventArgs e)
    {
        var t = Convert.ToInt32(RouteData.Values["id_ct"]);
        var getChiTiet = from pr in db.tbProducts
                         join prc in db.tbProductCates on pr.productcate_id equals prc.productcate_id
                         join ct in db.tbCities on Convert.ToInt32(pr.h1_seo) equals ct.city_id
                         where prc.productcate_id == Convert.ToInt32(RouteData.Values["id_ct"])
                         && pr.h1_seo == txtTH.Value
                         select new
                         {
                             pr.product_id,
                             pr.product_image,
                             pr.product_title,
                             ct.city_name,
                             pr.product_price_new,
                             pr.product_price,
                         };
        rpChiTietSanPham.DataSource = getChiTiet;
        rpChiTietSanPham.DataBind();
    }
    protected void LoadData()
    {
        var getChiTiet = from pr in db.tbProducts
                         join prc in db.tbProductCates on pr.productcate_id equals prc.productcate_id
                         join ct in db.tbCities on Convert.ToInt32(pr.h1_seo) equals ct.city_id
                         where prc.productcate_id == Convert.ToInt32(RouteData.Values["id_ct"])
                         select new
                         {
                             pr.product_id,
                             pr.product_image,
                             pr.product_title,
                             ct.city_name,
                             pr.product_price_new,
                             pr.product_price,
                         };
        rpChiTietSanPham.DataSource = getChiTiet;
        rpChiTietSanPham.DataBind();
    }
    protected void btnBoChon_ServerClick(object sender, EventArgs e)
    {
        LoadData();
    }
}