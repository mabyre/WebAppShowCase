/*
** Display recursivly the pages in a tree menu way
**
** Robustnest of the code have been improve by building a structure of pages from zero pages 
** then destruct the all pages
**
** When a page has no more parent, that it has been deleted,
** we put Guid.Empty into the page's parent so the page is displayed again
** 
** Click action on menu item is done by "NavigateUrl"
**
*/
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
using PageEngine;

public partial class Control_MenuPanel: System.Web.UI.UserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        /* Compatibilite avec Google Chrome */
        if ( Request.UserAgent.IndexOf( "AppleWebKit" ) > 0 )
        {
            Request.Browser.Adapters.Clear();
        } 

        if ( IsPostBack == false )
        {
            buildMenu();
        }
    }

    protected void buildMenu()
    {
        MenuArticle.Items.Clear();
        foreach ( PagePost page in PagePost.Pages )
        {
            if (    ( page.ShowInList && page.IsReserved == false )
                 || ( page.ShowInList && ( page.IsReserved == true && Page.User.Identity.IsAuthenticated ) )
               )
            {
                MenuItem menu = buildMenuItem( page );
                buildParentMenu( page, menu );
            }
        }
    }

    protected MenuItem buildMenuItem( PagePost page )
    {
        MenuItem menuItem = new MenuItem();
        menuItem.Text = page.Menu;
        menuItem.ToolTip = page.Description;
        menuItem.NavigateUrl = page.RelativeLink.ToString();
        return menuItem;
    }

    protected void buildParentMenu( PagePost page, MenuItem menu )
    {
        PagePost parent = PagePost.GetPage( page.Parent );
        if ( page.Parent != Guid.Empty && parent == null ) // the parent have been deleted
        {
            page.Parent = Guid.Empty; // put the page at level 0 of the menu
        }
        if ( page.Parent == Guid.Empty )
        {
            MenuItem m = findMenu( menu.Text, MenuArticle.Items );
            if ( m == null )
            {
                MenuArticle.Items.Add( menu );
            }
        }
        else
        {
            MenuItem menuParent = buildMenuItem( parent );
            MenuItem mParent = findMenu( menuParent.Text, MenuArticle.Items );
            if ( mParent != null ) // we found the parent, it existe in the menu
            {
                MenuItem n2 = findMenu( menu.Text, MenuArticle.Items );
                if ( n2 == null ) // this node does not exist in the menu tree
                {
                    mParent.ChildItems.Add( menu );
                }
            }
            else
            {
                menuParent.ChildItems.Add( menu );
                buildParentMenu( parent, menuParent );
            }
        }
    }

    private MenuItem findMenu( string texte, MenuItemCollection menus )
    {
        foreach ( MenuItem m in menus )
        {
            if ( m.Text == texte )
            {
                return m;
            }
            else // search if there is no child's parent node
            {
                MenuItem mChild = findMenu( texte, m.ChildItems );
                if ( mChild != null )
                    return mChild;
            }
        }
        return null;
    }

    /// <summary>
    /// Found the node that the property "Text" ==
    /// </summary>
    /// <param name="texte">the text to search for</param>
    /// <param name="nodes">collection of node where to search for</param>
    /// <returns>null if the node is not found</returns>
    private TreeNode findNode( string texte, TreeNodeCollection nodes )
    {
        foreach ( TreeNode n in nodes )
        {
            if ( n.Text == texte )
            {
                return n;
            }
            else // search if there is no other child nodes
            {
                TreeNode nChild = findNode( texte, n.ChildNodes );
                if ( nChild != null )
                    return nChild;
            }
        }
        return null;
    }

    /// <summary>
    /// Return in a ArrayList all the nodes in the collection
    /// </summary>
    private ArrayList findNodes( TreeNodeCollection nodes )
    {
        ArrayList alNode = new ArrayList();

        foreach ( TreeNode node in nodes )
        {
            alNode.Add( node );
            if ( node.ChildNodes.Count > 0 )
            {
                ArrayList childNodes = findNodes( node.ChildNodes );
                if ( childNodes != null )
                {
                    foreach ( TreeNode child in childNodes )
                    {
                        alNode.Add( child );
                    }
                }
            }
        }
        return alNode;
    }

    // In a recursive way found the index of the node
    private int indexOf( ref int i, TreeNodeCollection nodeCollection, TreeNode node )
    {
        foreach ( TreeNode n in nodeCollection )
        {
            if ( n.Value == node.Value )
                return i;
            i = i + 1;
            if ( n.ChildNodes.Count > 0 )
            {
                indexOf( ref i, n.ChildNodes, node );
            }
        }
        return i;
    }
}
