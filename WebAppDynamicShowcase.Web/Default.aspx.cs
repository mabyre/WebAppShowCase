using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if ( IsPostBack == false )
        {
            PageEngine.PagePost frontPage = PageEngine.PagePost.GetFrontPage();
            // Cf. BlogEngine mais je ne sais pas pourquoi cette condition est la !
            // ca marche pas et dans la version 1.3 c'est en commentaire !
            if ( /*Request.QueryString.Count == 0 &&*/ frontPage != null )
            {
                Server.Transfer( "~/page.aspx?id=" + frontPage.Id );
            }
            else
            {
                LabelDefault.Text = "<h1>" + Global.SettingsXml.TextLabelDefaut +"</h1>";
            }
        }
    }
}
