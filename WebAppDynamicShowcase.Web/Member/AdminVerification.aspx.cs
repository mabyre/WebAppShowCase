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

public partial class Member_AdminVerification : System.Web.UI.Page
{
    protected void Page_Load( object sender, System.EventArgs e )
    {
        if ( Request[ "username" ] != null )
        {
            MembershipUser member = Membership.GetUser( Request[ "username" ] );
            member.IsApproved = true;
            //ClubStarterKit.Web.UserAdministration.ApprovedEmail( Mem );
            Membership.UpdateUser( member );
            statusttl.Text = "User Verified";
            status.Text = "User " + member.UserName + " has been verified and activated.";
        }
        else
        {
            statusttl.Text = "Error";
            status.Text = "The user could not be found.";
        }
    }
}
