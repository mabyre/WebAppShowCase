<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Page_MemberEdit" Title="Edition d'un membre" Codebehind="Edit.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

<table border="0" width="100%" cellpadding="0">
<tr>
    <td valign="middle" height="40px">
        <h3>Edition d'un membre</h3>
    </td>
</tr>
</table>

<table border="0" cellpadding="3" cellspacing="0">
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label1" runat="server" Text="Nom d'utilisateur  :" CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:TextBox ID="TextBoxUserName" runat="server" CssClass="TextBoxStyle" />
            a</td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label2" runat="server" Text="Mot de Passe : " CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:TextBox ID="TextBoxMotDePasse" runat="server" CssClass="TextBoxStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label3" runat="server" Text="Nom : " CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:TextBox ID="TextBoxNom" runat="server" CssClass="TextBoxStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label4" runat="server" Text="Prénom : " CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:TextBox ID="TextBoxPrenom" runat="server" CssClass="TextBoxStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label5" runat="server" Text="Adresse : " CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:TextBox ID="TextBoxAdresse" runat="server" CssClass="TextBoxStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label6" runat="server" Text="Téléphone : " CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:TextBox ID="TextBoxTelephone" runat="server" CssClass="TextBoxStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label7" runat="server" Text="Société : " CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:TextBox ID="TextBoxSociete" runat="server" CssClass="TextBoxStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label8" runat="server" Text="E-mail : " CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="TextBoxStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label9" runat="server" Text="Approuvé : " CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:CheckBox ID="CheckBoxUserIsApproved" runat="server" CssClass="TextBoxStyle" AutoPostBack="true" OnCheckedChanged="CheckBoxUserIsApproved_CheckedChanged" ToolTip="Un utilisateur non approuvé ne peut plus se connecter" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            <asp:Label ID="Label10" runat="server" Text="Vérouillé : " CssClass="LabelStyle" />
        </td>
        <td class="TdTextBoxStyle" align="left">
            <asp:CheckBox ID="CheckBoxUserIsLocked" runat="server" CssClass="TextBoxStyle" AutoPostBack="true" OnCheckedChanged="CheckBoxUserIsLocked_CheckedChanged" ToolTip="Un utilisateur locké ne peut plus être approuvé" />
        </td>
    </tr>
</table>
<br />
<table style="border:solid 1px" border="0" cellpadding="5px" cellspacing="2px" width="60%">
    <tr>
        <td class="TdLabelStyle" align="right">
            Is Approved :
        </td>
        <td align="left">
            <asp:Label ID="LabelIsApproved" runat="server" CssClass="LabelBlueStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            Is Locked Out :
        </td>
        <td align="left">
            <asp:Label ID="LabelIsLockedOut" runat="server" CssClass="LabelBlueStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            Is Online :
        </td>
        <td align="left">
            <asp:Label ID="LabelOnline" runat="server" CssClass="LabelStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            Creation Date :
        </td>
        <td align="left">
            <asp:Label ID="LabelCreationDate" runat="server" CssClass="LabelStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            Last Login Date :
        </td>
        <td align="left">
            <asp:Label ID="LabelLastLoginDate" runat="server" CssClass="LabelStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            Last Lockout Date :
        </td>
        <td align="left">
            <asp:Label ID="LastLockoutDate" runat="server" CssClass="LabelStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            Last Activity Date :
        </td>
        <td align="left">
            <asp:Label ID="LabelActivityDate" runat="server" CssClass="LabelStyle" />
        </td>
    </tr>
    <tr>
        <td class="TdLabelStyle" align="right">
            Last Password Changed Date :
        </td>
        <td align="left">
            <asp:Label ID="LabelLastPasswordChangedDate" runat="server" CssClass="LabelStyle" />
        </td>
    </tr>
</table>

<table border="0" width="100%" cellpadding="0">
<tr>
<td align="left">
    <table cellpadding="2" align="center" border="0">
        <tr>
            <td height="40px">
                <asp:Button ID="ButtonSave" runat="server" Text="Sauver" OnClick="ButtonSave_Click" CssClass="ButtonStyle"/>                
            </td>
            <td>
                <asp:Button ID="ButtonSupprimer" runat="server" Text="Supprimer" CssClass="ButtonStyle" ToolTip="Visualiser les objets associés à ce Membre avant suppression" OnClick="ButtonSupprimer_Click"/>                
            </td>
            <td>
                <asp:Button ID="ButtonCancel" runat="server" Text="Retour" CssClass="ButtonStyle" OnClick="ButtonCancel_Click"/>                
            </td>
        </tr>
    </table>
</td>
</tr>
</table>

<table style="border:none" cellpadding="0" cellspacing="0">
    <tr>
        <td height="30">
            <asp:Label ID="ValidationMessage" CssClass="LabelValidationMessageStyle" Runat="server" Visible="false" />
        </td>
    </tr>
</table>

</asp:Content>