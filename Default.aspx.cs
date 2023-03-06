using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    dbcsdlDataContext db = new dbcsdlDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
        var getData = from spct in db.tb_SanPhamChiTiets
                      join sp in db.tb_SanPhams on spct.sp_id equals sp.sp_id
                      join dm in db.tb_DanhMucs on spct.dm_id equals dm.dm_id
                      select new
                      {
                          spct.spct_id,
                          spct.spct_name,
                          spct.spct_image,
                          spct.spct_giathanh,
                          sp.sp_name,
                          dm.dm_sp,
                      };
        rpAoQuan.DataSource = getData;
        rpAoQuan.DataBind();
    }
}