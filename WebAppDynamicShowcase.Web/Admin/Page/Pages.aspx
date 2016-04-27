<%@ Page Language="C#" MasterPageFile="~/Admin/MasterAdminPage.master" AutoEventWireup="true" ValidateRequest="false" Inherits="Admin_Pages" Title="Ajouter une page" Codebehind="Pages.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">

<script type="text/javascript">
function ToggleVisibility()
{
    var element = document.getElementById('<%=ulPages.ClientID%>');
    if (element.style.display == "none")
      element.style.display = "block";
    else
      element.style.display = "none";
}
</script>

<table border="0" cellpadding="10px" cellspacing="5px" width="100%">
<tr>
    <td align="left">
        <asp:Button ID="Button5" runat="server" CausesValidation="false" Text="Nouvelle page" OnClick="ButtonNew_Click" />
    </td>
    <td align="right">
        <asp:Button ID="ButtonHaut" runat="server" Text="Sauver" OnClick="ButtonSauver_Click" />
    </td>
</tr>
</table>

<!-- List des Pages -->
<div id="divPages" runat="server" visible="true" enableviewstate="False" style="margin-bottom: 10px">
    <a id="aPages" class="link" title="Liste des Pages" runat="server" href="javascript:void(ToggleVisibility());" />
    <ul id="ulPages" runat="server" style="display:none;list-style-type:circle" />
</div>
  
<asp:Label Width="50px" ID="Label6" runat="server" Text="Menu :" ToolTip="Affiché dans le menu dynamique" />
<asp:TextBox runat="server" ID="TextBoxMenu" Width="350px" />
<asp:RequiredFieldValidator runat="server" ControlToValidate="TextBoxMenu" Display="Dynamic" ErrorMessage="Requis" />
<br />
<br />
    
<asp:Label Width="50px" ID="Label5" runat="server" Text="Titre :" ToolTip="Utilisé dans le meta tag <title>" />
<asp:TextBox runat="server" ID="TextBoxTitle" Width="350px" />
<br />
<br />

<asp:CheckBox runat="Server" ID="cbShowInList" Text="Montrer dans le menu" Checked="true" />
<br />
<br />
    
<asp:CheckBox runat="Server" ID="CheckBoxTitleIsVisible" Text="Afficher le Titre" Checked="true" />
<br />
<br />

<asp:CheckBox runat="Server" ID="CheckBoxIsReserved" Text="Réservée aux membres" Checked="false" ToolTip="Réservée aux utilisateurs authentifiés" />
<br />
<br />

<asp:Label Width="60px" ID="Label2" runat="server" ToolTip="" Text="Parent :" />
<asp:DropDownList runat="server" id="DropDownListParent" />
<br />
<br />

<asp:Panel ID="PanelAdministrateur" runat="server" Visible="false">
<asp:CheckBox runat="Server" ID="cbIsFrontPage" />
<br />
<br />

<asp:Label Width="60px" ID="Label3" runat="server" ToolTip="" Text="Auteur :" />
<asp:DropDownList runat="Server" ID="DropDownListAuteur" TabIndex="2" />
<br />
<br />
</asp:Panel>
    
<asp:Label Width="50px" ID="Label1" runat="server" ToolTip="Les pages sont triées seulement leur rang" Text="Rang :" />
<asp:TextBox runat="server" ID="TextBoxRang" Width="30px" />
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxRang" Display="Dynamic" ErrorMessage="Donnez un rang à la page" />
<br />
<br />

<asp:Label Width="140px" ID="Label4" runat="server" Text="Jours de commentaires :" ToolTip="Si 0 alors les commentaires sont toujours possibles" />
<asp:TextBox runat="server" ID="TextBoxJoursCommentaires" Width="30px" />
<br />
<br />

<FCKeditorV2:FCKeditor ID="FCKeditor2" runat="server"
    ToolbarSet="Default" 
    SkinPath="skins/office2003/"
    Value="" 
    BasePath="~/FCKeditor/" 
    UseBROnCarriageReturn="true">
</FCKeditorV2:FCKeditor>                            

<br />
<div style="text-align:right">
    <asp:Button ID="Button1" runat="server" Text="Elargir l'editeur" CausesValidation="false" OnClick="ButtonEditorAugmenter_Click" />
</div>

<table id="entrySettings">
<tr>
    <td class="label">Transférer une image</td>
    <td>
        <asp:FileUpload runat="server" ID="txtUploadImage" Width="400" />
        <asp:Button runat="server" ID="btnUploadImage" Text="Upload" CausesValidation="False" />
    </td>
</tr>
<tr>
    <td class="label">Transmettre un fichier</td>
    <td>
        <asp:FileUpload runat="server" ID="txtUploadFile" Width="400" />        
        <asp:Button runat="server" ID="btnUploadFile" Text="Upload" CausesValidation="False" ValidationGroup="fileUpload" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUploadFile" ErrorMessage="Specify a file name" ValidationGroup="fileUpload" />
    </td>
</tr>    
<tr>
    <td class="label">Description :</td>
    <td>
      <asp:TextBox runat="server" ID="txtDescription" TextMode="multiLine" Columns="50" Rows="5" ToolTip="Utilisé dans le meta tag <description>" />
    </td>
</tr>
<tr>
    <td class="label">Mots clés :</td>
    <td>
        <asp:TextBox runat="server" ID="txtKeyword" Width="400" ToolTip="Utilisé dans le meta tag <keywords>" />
    </td>
</tr>
<tr>
    <td class="label">Paramètres</td>
    <td>
        <asp:CheckBox runat="Server" ID="cbIsPublished" Checked="true" Text="Publier" />
    </td>
</tr>
<tr>
    <td class="label"></td>
    <td>
        <asp:CheckBox runat="Server" ID="CheckBoxAutoriserCommentaire" Checked="true" Text="Autoriser les commentaires" ToolTip="Ou clôre les commentaires" />
    </td>
</tr>
<tr>
    <td class="label"></td>
    <td>
        <asp:CheckBox runat="Server" ID="CheckBoxIsCommentVisible" Checked="true" Text="Afficher les commentaires" />
    </td>
</tr>
</table>  

<table border="0" cellpadding="10px" cellspacing="5px" width="100%">
<tr>
    <td align="left" width="33%">
    <asp:Button ID="ButtonNew" runat="server" Text="Nouvelle page" OnClick="ButtonNew_Click" />
    </td>
    <td align="center" width="33%">
    <asp:Button ID="ButtonAnnuler" CausesValidation="false" runat="server" Text="Annuler" OnClick="ButtonAnnuler_Click" ToolTip="Annuler et retourner vers la page" />
    </td>
    <td align="right" width="33%">
    <asp:Button ID="ButtonSauver" runat="server" Text="Sauver" OnClick="ButtonSauver_Click" ToolTip="Sauver et retourner vers la page modifiée" />
    </td>
</tr>
</table>
    
</asp:Content>