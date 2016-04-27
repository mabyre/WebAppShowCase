using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MemberInfoData;
//using Sql.Web.Data;
//using ImportExportContact;

public partial class Member_Manage : PageBase
{
    static int CurrentPageIndex = 0;

    protected void Page_Load( object sender, System.EventArgs e )
    {
        if ( IsPostBack == false )
        {
            MemberInfoCollection membres = MemberInfoCollection.GetAll();
            GridViewMembers.DataSource = membres;
            GridViewMembers.DataBind();
        }
    }

    protected void GridViewMembers_OnLoad( object sender, EventArgs e )
    {
        ComputeColumns();
    }

    protected void ComputeColumns()
    {
        int indexRow = 0;
        DataKeyArray dka = GridViewMembers.DataKeys;
        foreach ( GridViewRow r in GridViewMembers.Rows )
        {
            Label isApproved = ( Label )GridViewMembers.Rows[ indexRow ].FindControl( "LabelIsApproved" );
            Label isLocked = ( Label )GridViewMembers.Rows[ indexRow ].FindControl( "LabelLocked" );
            Label isOnline = ( Label )GridViewMembers.Rows[ indexRow ].FindControl( "LabelOnline" );
            Label lastLoginDate = ( Label )GridViewMembers.Rows[ indexRow ].FindControl( "LabelLastLoginDate" );
                
            Guid memberGuid = new Guid( dka[ indexRow ].Value.ToString() );
            MembershipUser user = Membership.GetUser( memberGuid );
            if ( user.IsApproved )
            {
                isApproved.Text = "Vrai";
                isApproved.CssClass = "LabelBlueStyle";
            }
            else
            {
                isApproved.Text = "Faux";
                isApproved.CssClass = "LabelRedStyle";
            }

            if ( user.IsLockedOut )
            {
                isLocked.Text = "Vrai";
                isLocked.CssClass = "LabelRedStyle";
            }
            else
            {
                isLocked.Text = "Faux";
                isLocked.CssClass = "LabelBlueStyle";
            }

            if ( user.IsOnline )
            {
                isOnline.Text = "Vrai";
                isOnline.CssClass = "LabelGreenStyle";
            }
            else
            {
                isOnline.Text = "Faux";
                isOnline.CssClass = "LabelBlueStyle";
            }

            lastLoginDate.Text = user.LastLoginDate.ToShortDateString();
            lastLoginDate.ToolTip = user.LastLoginDate.ToShortTimeString();

            indexRow += 1;
        }
    }

    protected void GridViewMembers_RowCreated( object sender, System.Web.UI.WebControls.GridViewRowEventArgs e )
    {
        if ( e.Row.RowType == DataControlRowType.Header )
        {
            AddGlyph( GridViewMembers, e.Row );
        }
    }

    protected void AddGlyph( GridView grid, GridViewRow item )
    {
        Image glyph = new Image();
        glyph.EnableTheming = false;
        glyph.ImageAlign = ImageAlign.Bottom;
        glyph.ImageUrl = string.Format( "~/App_Themes/" + Page.Theme.ToString() + "/images/move{0}.gif", ( string )( grid.SortDirection == SortDirection.Ascending ? "down" : "up" ) );

        int i;
        string colExpr;
        for ( i = 0;i <= grid.Columns.Count - 1;i++ )
        {
            colExpr = grid.Columns[ i ].SortExpression;
            if ( colExpr != "" && colExpr == grid.SortExpression )
            {
                item.Cells[ i ].Controls.Add( glyph );
            }
        }
    }

    protected void GridViewMembers_PageIndexChanged( object sender, System.EventArgs e )
    {
        //PersonneDataSource.SelectCommand = SqlCommand
        //(
        //    DropDownListSociete.SelectedValue,
        //    DropDownListLettre.SelectedValue
        //);
        CurrentPageIndex = GridViewMembers.PageIndex;
    }

    protected void GridViewMembers_OnDataBound( object sender, System.EventArgs e )
    {
        //PersonneDataSource.SelectCommand = SqlCommand
        //(
        //    DropDownListSociete.SelectedValue,
        //    DropDownListLettre.SelectedValue
        //);
    }

    protected void GridViewMembers_OnSorted( object sender, EventArgs e )
    {
        //PersonneDataSource.SelectCommand = SqlCommand
        //( 
        //    DropDownListSociete.SelectedValue,
        //    DropDownListLettre.SelectedValue
        //);
        GridViewMembers.PageIndex = CurrentPageIndex;
    }

    protected void GridViewMembers_OnSorting( object sender, GridViewSortEventArgs e )
    {
        int test = 3;
    }

    // Se declenche quand on clique sur les boutons edit/delete/update/cancel
    protected void GridViewMembers_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if ( e.CommandName == "Select" )
        {
            int index = Convert.ToInt32( e.CommandArgument );
            GridView gv = ( GridView )e.CommandSource;
            string MembreGUID = gv.DataKeys[ index ].Value.ToString();
            //SqlDataSourceMembreQuestionnaire.SelectCommand = string.Format( "SELECT Description, CodeAcces, QuestionnaireID FROM Questionnaire WHERE MembreGUID = '{0}'", MembreGUID );
            //DataListMembreQuestionnaire.DataBind();
        }
    }
}

