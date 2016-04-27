<%@ Page Language="C#" MasterPageFile="~/Admin/MasterAdminPage.master" ValidateRequest="false"  Debug="true" AutoEventWireup="true" Inherits="Admin_Pages_Settings" Title="Settings" Codebehind="Settings.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" runat="Server">

    <br />
    <div style="text-align: right">
        <asp:Button runat="server" ID="ButtonSaveTop" Text="Sauver" OnClick="ButtonSave_OnClick" />
    </div>
    <br />
    
    <div class="settings">
        <h1>Paramètres</h1>
        <asp:Label Width="200px" ID="Label5" runat="server" ToolTip="Pour logguer l'utilisateur savoir d'où il vient">Racine du site web :</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxVirtualPath" Width="300px" />
        <br />
        <asp:Label Width="200px" ID="Label6" runat="server" ToolTip="">Sujet des emails de mise à jour :</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxSujetMiseAJour" Width="300px" />
        <br />
        <asp:Label  Width="200px" ID="Label1" runat="server" ToolTip="Ecrire les traces dans le fichier .log">Loguer les utilisateurs :</asp:Label>
        <asp:CheckBox runat="server" ID="CheckBoxLogUser" Width="45px" />
        <br />
    </div>

    <div class="settings">
        <h1>Paramêtres Membre</h1>
        <asp:Label  Width="300px" ID="Label4" runat="server" ToolTip="Vérifier les membres qui s'enregistrent, Approved == false">Vérifier les membres :</asp:Label>
        <asp:CheckBox runat="server" ID="CheckBoxMembreVerification" Width="45px" />
        <br />
        <asp:Label Width="300px" ID="Label12" runat="server" ToolTip="Un membre vient de s'enregistrer envoyer un email à l'admin">Prévenir sur l'enregistrement d'un nouveau membre :</asp:Label>
        <asp:CheckBox runat="server" ID="CheckBoxNouveauMembrePrevenir" Width="45px" />
        <br />
        <asp:Label Width="300px" ID="Label15" runat="server" ToolTip="Un membre vient de se connecter">Prévenir sur la connexion d'un membre :</asp:Label>
        <asp:CheckBox runat="server" ID="CheckBoxConnexionMembrePrevenir" Width="45px" />
        <br />
    </div>

    <div class="settings">
        <h1>Editeur HTML</h1>
        <asp:Label  Width="120px" ID="Label2" runat="server" ToolTip="">Hauteur :</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxEditeurHauteur" Width="45px" />
        <br />
        <asp:Label  Width="120px" ID="Label3" runat="server" ToolTip="Elargir l'éditeur">Elargir :</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxEditeurElargir" Width="45px" />
        <br />
    </div>
    
    <div class="settings">
        <h1>Paramêtres Graphiques</h1>
        <asp:Label  Width="250px" ID="Label16" runat="server" ToolTip="">Afficher la colonne de droite :</asp:Label>
        <asp:CheckBox runat="server" ID="CheckBoxColonneDroiteVisible" Width="45px" />
        <br />
        <asp:Label  Width="250px" ID="Label17" runat="server" ToolTip="">Afficher l'enpied du site :</asp:Label>
        <asp:CheckBox runat="server" ID="CheckBoxEnpiedSiteVisible" Width="45px" />
        <br />
    </div>

    <div class="settings">
        <h1>Commentaires</h1>
        <asp:Label  Width="250px" ID="Label13" runat="server" ToolTip="">Avatars :</asp:Label>
        <asp:RadioButtonList runat="Server" ID="RadioButtonListAvatar" RepeatLayout="flow" RepeatDirection="horizontal">
          <asp:ListItem Text="Gravatar" Value="gravatar" />
          <asp:ListItem Text="Monster" Value="monster" />
          <asp:ListItem Text="Combiné" Value="combine" />
          <asp:ListItem Text="Ne pas montrer" Value="none" />
        </asp:RadioButtonList>
    </div>

    <div class="settings">
        <h1>Entête HTML</h1>
        <table>
            <tr>
                <td valign="top">
                    <asp:Label Width="190px" ID="Label7" runat="server" ToolTip="" Text="Code ajouté à la section HEAD HTML :" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="TextBoxHeadHtml" TextMode="MultiLine" Width="650px" Rows="10" Columns="30" />
                </td>
            </tr>
        </table>
    </div>
    
    <div class="settings">
        <h1>Rang des Pages Auteurs</h1>
        <asp:Label  Width="120px" ID="Label8" runat="server" ToolTip="">min :</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxPagesAuteurMin" Width="45px" />
        <br />
        <asp:Label  Width="120px" ID="Label9" runat="server" ToolTip="">max :</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxPagesAuteurMax" Width="45px" />
        <br />
    </div>
    
   
    <div class="settings">
        <h1>Informations</h1>
        <table border="0" cellpadding="5px">
        <tr>
            <td>
                <label title="Pour le Répertoire UserFiles">Taille des fichiers :</label>
                <asp:Label runat="server" ID="LabelTailleUserFiles" Width="300" /><br />
            </td>
        </tr>
        <tr>
            <td>
                <label title="Nombre d'utilisateurs connectés actuellement">Utilisateurs connectés :</label>
                <asp:Label runat="server" ID="LabelUtilisateursConnecte" ForeColor="blue" Width="300" />
            </td>
        </tr>
        </table>
    </div>

    <div align="right">
        <asp:Button runat="server" ID="ButtonSave" Text="Sauver" OnClick="ButtonSave_OnClick" />
    </div>
    <br />
</asp:Content>