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

public partial class Controls_AdminMenu : System.Web.UI.UserControl
{
    public bool IsVisible()
    {
        return Page.User.IsInRole( "Administrateur" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        // Pour acceder a LoginViewAdminMenu il faut que l'utilisateur soit authentifie
        if ( Page.User.Identity.IsAuthenticated )
        {
            // Visible='<%#IsVisible()%>' dans le .ascx ne fonctionne pas !
            LinkButton lb = new LinkButton();
            lb = ( LinkButton )LoginViewAdminMenu.FindControl( "LinkButtonManageRoles" );
            lb.Visible = IsVisible();
            lb = ( LinkButton )LoginViewAdminMenu.FindControl( "LinkButtonManage" );
            lb.Visible = IsVisible();
            lb = ( LinkButton )LoginViewAdminMenu.FindControl( "LinkButtonPage" );
            lb.Visible = IsVisible() || Page.User.Identity.IsAuthenticated;
            lb = ( LinkButton )LoginViewAdminMenu.FindControl( "LinkButtonStyleSheet" );
            lb.Visible = IsVisible();
            lb = ( LinkButton )LoginViewAdminMenu.FindControl( "LinkButtonSiteMap" );
            lb.Visible = IsVisible();
            lb = ( LinkButton )LoginViewAdminMenu.FindControl( "LinkButtonSiteSettings" );
            lb.Visible = IsVisible();
            lb = ( LinkButton )LoginViewAdminMenu.FindControl( "LinkButtonLogUser" );
            lb.Visible = IsVisible();
            lb = ( LinkButton )LoginViewAdminMenu.FindControl( "LinkButtonSmtpServeur" );
            lb.Visible = IsVisible();
        }
    }

    protected void LinkButtonDeconnexion_Click( object sender, EventArgs e )
    {
        FormsAuthentication.SignOut();
        Response.Redirect( "~/Default.aspx" );
    }
}
