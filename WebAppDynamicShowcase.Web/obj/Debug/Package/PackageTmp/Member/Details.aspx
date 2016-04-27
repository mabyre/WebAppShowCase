<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Page_MemberDetails" Title="Edition d'un membre" Codebehind="Details.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

<table border="0" cellpadding="3" cellspacing="0">
<tr>
    <td colspan="2">
        <br />
        <h3>Edition de votre compte d'utilisateur</h3>
    </td>
</tr><tr>
    <td class="TdLabelStyle" align="right">
    </td>
    <td class="TdTextBoxStyle" align="left">
        <asp:Label CssClass="LabelStyle"  ForeColor="#0742AF" ID="LabelUserName" runat="server"></asp:Label>
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

<asp:Panel ID="PanelChangeMotPasse" runat="server" Visible="true">
<h3>Changez votre Mot de Passe</h3>
<asp:ChangePassword id="ChangePassword1" runat="server" OnChangingPassword="ChangePassword1_ChangingPassword" >
<ChangePasswordTemplate>
<table cellPadding="1" border="0">
<tr>
    <td>
    <table cellPadding="0" border="0">
    <tr>
        <td align="right">
            <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword" CssClass="LabelStyle" Text="Mot de Passe :" />
        </td>
        <td>
            <asp:TextBox id="CurrentPassword" runat="server" TextMode="Password" />
            <asp:RequiredFieldValidator id="CurrentPasswordRequired" runat="server" ValidationGroup="ChangePassword1" ToolTip="Password is required." ErrorMessage="Password is required." ControlToValidate="CurrentPassword">*</asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword" CssClass="LabelStyle" Text="Nouveau Mot de Passe :" />
        </td>
        <td>
            <asp:TextBox ID="NewPassword" runat="server" TextMode="Password" />
            <asp:RequiredFieldValidator id="NewPasswordRequired" runat="server" ValidationGroup="ChangePassword1" ToolTip="New Password is required." ErrorMessage="New Password is required." ControlToValidate="NewPassword">*</asp:RequiredFieldValidator> 
        </td>
    </tr>
    <tr>
        <td align=right>
            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword" CssClass="LabelStyle" Text="Confirmez Nouveau Mot de Passe :" />
        </td>
    <td>
        <asp:TextBox id="ConfirmNewPassword" runat="server" TextMode="Password" />
        <asp:RequiredFieldValidator id="ConfirmNewPasswordRequired" runat="server" ValidationGroup="ChangePassword1" ToolTip="Confirm New Password is required." ErrorMessage="Confirm New Password is required." ControlToValidate="ConfirmNewPassword">*</asp:RequiredFieldValidator> 
    </td>
    </tr>
    <tr>
        <td align="center" colSpan="2" width="410">
            <asp:CompareValidator id="NewPasswordCompare" runat="server" ValidationGroup="ChangePassword1" ErrorMessage="La confirmation du nouveau mot de passe ne correspond pas au nouveau mot de passe." ControlToValidate="ConfirmNewPassword" Display="Dynamic" ControlToCompare="NewPassword" CssClass="LabelValidationMessageErrorStyle" />
        </td>
    </tr>
    <tr>
        <td align=center colSpan=2 width="410">
            <asp:Label ID="FailureText" runat="server" EnableViewState="False" CssClass="LabelValidationMessageErrorStyle" />
        </td>
    </tr>
    <tr>
        <td align="center" colSpan=2 height="40px">
            <asp:Button ID="ButtonChangePassword" runat="server" Text="Modifier" ValidationGroup="ChangePassword1" CommandName="ChangePassword" CssClass="ButtonStyle" />
        </td>
    </tr>
    </table>
</td>
</tr>
</table>
</ChangePasswordTemplate>
<SuccessTemplate>
<table cellSpacing="0" cellPadding="1" border="0">
<tr>
    <td>
        <table cellPadding="0" border="0">
        <tr>
            <td align="center" colSpan="2">
            <label class="LabelStyle">
            Changement de Mot de Passe Terminé.
            </label>
            </td>
        </tr>
        <tr>
            <td>
            <label class="LabelStyle">
            Votre Mot de Passe a été changé avec succès.
            </label>
            </td>
        </tr>
        <tr>
        <td align=right colSpan=2>&nbsp;</td></tr>
        </table>
    </td>
</tr>
</table>
</SuccessTemplate>
</asp:ChangePassword>
</asp:Panel>

<asp:Panel ID="PanelSupprimerCompte" runat="server" Visible="true">
<h3>Supprimer votre compte d'utilisateur</h3>
<table border="0" width="100%" cellpadding="0">
    <tr>
        <td>
            <label class="LabelStyle">
            Vous pouvez supprimer définitivement votre compte.
            Attention vous n'aurez plus accès à cette plateforme.
            </label>
        </td>
    </tr>
    <tr>
        <td height="40px">
            <asp:Button ID="ButtonSupprimerCompte" runat="server" Text="Supprimer" CssClass="ButtonStyle" ToolTip="Supprimer définitivement votre compte" OnClick="ButtonSupprimerCompte_Click"/>                
        </td>
    </tr>
</table>
</asp:Panel>

</asp:Content>