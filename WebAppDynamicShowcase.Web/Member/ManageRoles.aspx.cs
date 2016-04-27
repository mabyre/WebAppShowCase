//
// Management des roles pour les utilisateurs
//

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

public partial class Member_ManageRoles : PageBase
{
    public static string RoleAdmin = "Administrateur";
    
    protected void Page_Load( object sender, EventArgs e )
    {
        if ( User.Identity.IsAuthenticated == false )
        {
            string msg = "Non authorisé";
            Response.Redirect( Tools.PageErreurPath + msg );
        }

        if ( User.IsInRole( RoleAdmin ) == false )
        {
            string msg = "Non authorisé";
            Response.Redirect( Tools.PageErreurPath + msg );
        }

        if ( IsPostBack == false )
        {
            Message.Text = "";

            // Qu'il y ai au moins un role ...
            if ( Roles.RoleExists( RoleAdmin ) == false )
            {
                Roles.CreateRole( RoleAdmin );
            }

            LabelUserName.Text = User.Identity.Name;
            BindAll();
        }
    }

    // Tout remplir
    protected void BindAll()
    {
        string[] rolesArray;
        rolesArray = Roles.GetAllRoles();

        RolesGrid.DataSource = rolesArray;
        RolesGrid.DataBind();

        DropDownListRoles.DataSource = rolesArray;
        DropDownListRoles.DataBind();
        
        ListBoxRolesApplication.DataSource = rolesArray;
        ListBoxRolesApplication.DataBind();

        BindListListBoxUtilisateurs();
        BindListBoxRolesUtilisateur( User.Identity.Name );
    }

    // Trouver tous les utilisateurs de l'application
    protected void BindListListBoxUtilisateurs()
    {
        ListBoxUtilisateurs.Items.Clear();
        MembershipUserCollection users = Membership.GetAllUsers();
        int index = 0;
        int i = 0;
        foreach ( MembershipUser u in users )
        {
            ListBoxUtilisateurs.Items.Add( u.UserName );
            if ( User.Identity.Name == u.UserName )
            {
                index = i;
            }
            i = i + 1;
        }
        ListBoxUtilisateurs.SelectedIndex = index;
        
        // Calculer le nombre de lignes visibles de la ListBox entre 5 et 10
        if ( users.Count > 5 )
            ListBoxUtilisateurs.Rows = users.Count <= 10 ? users.Count : 10;
    }

    // Trouver tous les roles d'un utilisateur
    protected void BindListBoxRolesUtilisateur( string utilisateur )
    {
        string[] rolesArray;
        rolesArray = Roles.GetRolesForUser( utilisateur );

        ListBoxRolesUtilisateur.DataSource = rolesArray;
        ListBoxRolesUtilisateur.DataBind();
    }

    // Remplace les chaines ***user*** et ***role*** dans message
    private string changeUserAndRole( string message, string user, string role )
    {
        string msg = string.Empty;

        msg = message.Replace( "***user***", "'" + user + "'" );
        msg = msg.Replace( "***role***", "'" + role + "'" );
        return msg;
    }
    
    protected void ButtonAddRole_Click( object sender, EventArgs e )
    {
        if ( ListBoxRolesApplication.SelectedItem == null )
        {
            Message.Text = Resources.labels.CreateRoleAddRoleErreurRoleSelected;
            return;
        }
        string role = ListBoxRolesApplication.SelectedItem.Text;

        if ( ListBoxUtilisateurs.SelectedItem == null )
        {
            Message.Text = Resources.labels.CreateRoleAddRoleErreurUserSelected;
            return;
        }
        string user = ListBoxUtilisateurs.SelectedItem.Text;

        // Verifier que l'utilisateur n'est pas deja dans le role
        string[] users = Roles.FindUsersInRole( role, user );
        foreach ( string u in users )
        {
            if ( u == user )
            {
                Message.Text = changeUserAndRole( Resources.labels.CreateRoleAddRoleErreurUseInRolerExist, user, role );
                return;
            }
        }

        Roles.AddUserToRole
        (
            user,
            role
        );

        Message.Text = changeUserAndRole( Resources.labels.CreateRoleAddRoleUserInRoleSuccess, user, role );
        BindListBoxRolesUtilisateur( user );
        BindListBoxUtilisateurRoles( role );
    }

    protected void ButtonRemoveRole_Click( object sender, EventArgs e )
    {
        if ( ListBoxRolesUtilisateur.SelectedItem == null )
        {
            Message.Text = Resources.labels.CreateRoleRemoveRoleErreurUserInRoleSelected;
            return;
        }
        string role = ListBoxRolesUtilisateur.SelectedItem.Text;

        if ( ListBoxUtilisateurs.SelectedItem == null )
        {
            Message.Text = Resources.labels.CreateRoleRemoveRoleErreurUserSelected;
            return;
        }
        string user = ListBoxUtilisateurs.SelectedItem.Text;

        if ( role == RoleAdmin )
        {
            string[] users;
            users = Roles.GetUsersInRole( RoleAdmin );
            if ( users.Length == 1 )
            {
                Message.Text = Resources.labels.CreateRoleRemoveRoleErreurLastAdmin;
                return;
            }
        }

        Roles.RemoveUserFromRole
        (
            user,
            role
        );

        Message.Text = changeUserAndRole( Resources.labels.CreateRoleRemoveRoleSuccess, user, role );
        BindListBoxRolesUtilisateur( user );
        BindListBoxUtilisateurRoles( role );
    }

    // Creer un role pour l'Application
    protected void CreateNewRole_OnClick( object sender, EventArgs args )
    {
        string role = RoleTextBox.Text;

        if ( role.Trim() == "" )
        {
            Message.Text = Resources.labels.CreateNewRoleErreurRoleSelected;
            return;
        }

        try
        {
            if ( Roles.RoleExists( role ) )
            {
                Message.Text = changeUserAndRole( Resources.labels.CreateNewRoleErreurRoleExist, string.Empty, role );
                return;
            }

            Roles.CreateRole( role );

            Message.Text = changeUserAndRole( Resources.labels.CreateNewRoleSuccess, string.Empty, role );
            BindAll();
        }
        catch
        {
            Message.Text = changeUserAndRole( Resources.labels.CreateNewRoleSuccessErreurCreated, string.Empty, role );
        }
    }

    protected void ButtonAnnuler_Click( object sender, EventArgs e )
    {
        Response.Redirect( "~/Default.aspx" );
    }

    protected void ListBoxUtilisateurs_SelectedIndexChanged( object sender, EventArgs e )
    {
        string userNom = ListBoxUtilisateurs.SelectedItem.Text;
        BindListBoxRolesUtilisateur( userNom );
    }

    // Trouver les utilisateurs d'un role
    protected void ListBoxRolesApplication_SelectedIndexChanged( object sender, EventArgs e )
    {
        string role = ListBoxRolesApplication.SelectedItem.Text;
        BindListBoxUtilisateurRoles( role );
        LabelRoleApplication.Text = role;
    }

    protected void BindListBoxUtilisateurRoles( string role )
    {
        string[] usersArray;
        usersArray = Roles.GetUsersInRole( role );

        ListBoxUtilisateurRoles.DataSource = usersArray;
        ListBoxUtilisateurRoles.DataBind();
    }

    protected void SupprimerUtilisateur_Click( object sender, EventArgs args )
    {
        if ( ListBoxUtilisateurs.SelectedItem == null )
        {
            Message.Text = Resources.labels.SupprimerUtilisateurErreurUserSelected; // "Sélectionner un utilisateur à supprimer.";
            return;
        }
        string user = ListBoxUtilisateurs.SelectedItem.Text;

        if ( user == User.Identity.Name )
        {
            Message.Text = Resources.labels.SupprimerUtilisateurErreurUserDeletedHimself; // "<br>Vous êtes en train de supprimer votre compte.<br>"
            //  + "Pour cela vous devez passez pas le formulaire de suppression d'un compte.";
            return;
        }

        string[] users;
        users = Roles.GetUsersInRole( RoleAdmin );
        if ( users.Length == 1 && users[ 0 ] == user )
        {
            Message.Text = changeUserAndRole( Resources.labels.SupprimerUtilisateurErreurLastAdmin, user, string.Empty ); 
            //"<br>'" + user + "' est le dernier \"Administrateur\" de cette application.<br>"
            //    + "Vous ne pouvez pas le supprimer.<br>";
            return;
        }

        try
        {
            Membership.DeleteUser( user, true );
        }
        catch ( Exception e )
        {
            Message.Text = e.Message.ToString();
        }

        Message.Text = changeUserAndRole( Resources.labels.SupprimerUtilisateurSuccess, user, string.Empty );
        //Message.Text = "'" + user + "' a été supprimé avec succès.<br>";
        BindListListBoxUtilisateurs();
        if ( ListBoxRolesApplication.SelectedItem != null )
            BindListBoxUtilisateurRoles( ListBoxRolesApplication.SelectedItem.Text );
    }

    protected void EditerUtilisateur_Click( object sender, EventArgs args )
    {
        if ( ListBoxUtilisateurs.SelectedItem == null )
        {
            Message.Text = Resources.labels.EditerUtilisateurErreurUserSelected; 
            return;
        }

        Response.Redirect( "Edit.aspx?nom=" + ListBoxUtilisateurs.SelectedItem );
    }

    // Supprimer un role de l'Application
    protected void SupprimerRole_Click( object sender, EventArgs args )
    {
        if ( ListBoxRolesApplication.SelectedItem == null )
        {
            Message.Text = Resources.labels.SupprimerRoleErreurRoleSelected; 
//            Message.Text = "Sélectionner un rôle à supprimer.";
            return;
        }

        string role = ListBoxRolesApplication.SelectedItem.Text;
        if ( role == RoleAdmin )
        {
            Message.Text = Resources.labels.SupprimerRoleErreurRoleAdminSelected;
//            Message.Text = "Vous ne pouvez pas supprimer le role \"Administrateur\".";
            return;
        }

        try
        {
            Roles.DeleteRole( role, true );
            Message.Text = changeUserAndRole( Resources.labels.SupprimerRoleSuccess, string.Empty, Server.HtmlEncode( role ) ); 
//            Message.Text = "Le Rôle '" + Server.HtmlEncode( role ) + "' supprimé avec succès.";
            BindAll();
        }
        catch ( Exception e )
        {
            Message.Text = changeUserAndRole( Resources.labels.SupprimerRoleFailed, string.Empty, Server.HtmlEncode( role ) ); 
            //Message.Text = "Le Rôle '" + Server.HtmlEncode( role ) + "' <u>n'as pas été</u> supprimé.<br>";
            Message.Text += e.Message.ToString();
        }
    }
}
