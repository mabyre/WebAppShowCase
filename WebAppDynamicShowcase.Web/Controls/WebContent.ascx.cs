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
using WebContentData;

public partial class UserControl_WebContent : System.Web.UI.UserControl
{
    private string _Section = "";
    public string Section
    {
        get { return _Section; }
        set { _Section = value ; }
    }

    protected void Page_Load( object sender, System.EventArgs e )
    {
        if ( Section == null )
        {
            LabelContent.Text = "Il faut un nom de section pour le control WebContent.";
        }
        else
        {
            string Uri = "";
            WebContent webContent = new WebContent();
            try
            {
                HiddenFieldSectionName.Value = _Section;
                webContent = WebContent.GetWebContent( _Section );

                // Creation de la section Vide
                if ( webContent == null )
                {
                    webContent.Section = _Section;
                    webContent.SectionContent = "<p></p>";
                    webContent.Create();

                    LabelContent.Text = _Section;
                }
                else
                    LabelContent.Text = webContent.SectionContent;
            }
            catch ( Exception ex )
            {
                Uri = HttpUtility.UrlEncode( ex.Message );
                Response.Redirect( Tools.PageErreurPath + Uri, true );
            }

            if ( WebContent.CanEdit() )
            {
                HyperlinkEdit.Visible = true;
                string url = "~\\WebContent\\Edit.aspx?sectionname=" + HiddenFieldSectionName.Value;
                url += "&ReturnURL=";
                url += System.Web.HttpUtility.UrlEncode( Request.Url.ToString() );
                HyperlinkEdit.NavigateUrl = url;
            }
        }
    }
}
