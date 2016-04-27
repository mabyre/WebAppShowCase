<%@ Page Language="C#" MasterPageFile="~/Admin/MasterAdminPage.master" ValidateRequest="false" AutoEventWireup="true" Inherits="Admin_Pages_SettingsSmtp" Title="Settings Smtp" Codebehind="SmtpServeur.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" runat="Server">
    <br />
    <div style="text-align: right">
        <asp:Button runat="server" ID="ButtonSaveTop" Text="Sauver" OnClick="ButtonSave_OnClick" />
    </div>
    <br />
    
    <div class="settings">
        <h1>Email de l'administrateur</h1>
        <asp:Label Width="80px" ID="Label12" runat="server" ToolTip="Pour envoyer les nouveaux commentaires">E-mail :</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxEmailAdmin" Width="200px" ToolTip="Email-To" />
        <br />
        <asp:Label Width="80px" ID="Label1" runat="server" ToolTip="Pour particulariser les emails envoyé à l'administrateur">Sujet :</asp:Label>
        <asp:TextBox runat="server" ID="TextBoxEmailSujet" Width="200px" />
        <br />
    </div>

    <div class="settings">
    
        <h1>Configuration du Serveur Smtp</h1>

        <label>Nom d'utilisateur :</label>
        <asp:TextBox runat="server" ID="TextBoxUserName" Width="300" />                
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxUserName" ErrorMessage="Requis" />
        <br />

        <label>Mot de Passe :</label>
        <asp:TextBox runat="server" ID="TextBoxPassWord" Width="300" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxPassWord" ErrorMessage="Requis" />
        <br />
        
        <label>Nom du Serveur :</label>
        <asp:TextBox runat="server" ID="TextBoxServerName" Width="300" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBoxServerName" ErrorMessage="Requis" />
        <br />

        <label>Numéro de Port :</label>
        <asp:TextBox runat="server" ID="TextBoxServerPort" Width="300" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBoxServerPort" ErrorMessage="Requis" />
        <br />

        <label title="Email-From">Adresse E-mail :</label>
        <asp:TextBox runat="server" ID="TextBoxEmail" Width="300" ToolTip="Email-From" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Requis" />
        <br />

        <label>Enable SSL : </label>
        <asp:CheckBox runat="Server" ID="CheckBoxEnableSSL" />
        <br />
                
        <asp:Button runat="server" ID="ButtonTestSmtp" CausesValidation="False" Text="Test mail settings" OnClick="ButtonTestSmtp_Click" />
        <asp:Label runat="Server" ID="LabelSmtpStatus" />            
    </div>
    <div align="right">
        <asp:Button runat="server" ID="ButtonSave" Text="Sauver" OnClick="ButtonSave_OnClick" />
    </div>
    <br />
</asp:Content>