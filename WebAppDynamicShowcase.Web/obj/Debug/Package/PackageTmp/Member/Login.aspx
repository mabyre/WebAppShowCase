<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Member_Login" Title="Untitled Page" Codebehind="Login.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
<div id="body">
    <div id="columnright">
        <div class="rightblock">
            <table>
                <tr>
                    <td height="20px">
                    </td>
                </tr>
                <tr>
                    <td height="40px">
                        <h3>Login</h3>
                    </td>
                </tr>
            </table>
            <asp:Login ID="LoginControl" runat="server" CssClass="LabelStyle">
                <LayoutTemplate>
                    <asp:Panel ID="PanelMessageUtilisateur" runat="server" >
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Label runat="server" ID="LabelMessageUtilisateur" Visible="false" />
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    <asp:Literal runat="server" ID="FailureText" EnableViewState="False"></asp:Literal>
                    <table>
                        <tr>
                            <td align="right">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" Text="Nom d'utilisateur :" CssClass="LabelStyle" />
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="UserName" />
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ValidationGroup="Login1"
                                    ErrorMessage="Nom d'utilisateur requis." ToolTip="Nom d'utilisateur requis." >*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" Text="Mot de Passe :" CssClass="LabelStyle" />
                            </td>
                            <td>
                                <asp:TextBox ID="Password" runat="server" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" ValidationGroup="Login1"
                                    ErrorMessage="Password is required." ToolTip="Password is required." ID="PasswordRequired">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td height="25px">
                                <asp:CheckBox runat="server" ID="RememberMe" Text="Mémoriser ma connexion." CssClass="LabelStyle" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td height="35px">
                                <asp:Button ID="LoginButton" runat="server" Text="Login" CommandName="Login" CssClass="ButtonStyle" OnClick="LoginButton_Click"/>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
            </asp:Login>
        </div>
        <div class="rightblock">
            <h3>Mot de Passe oublié ?</h3>
            <div class="dashedline">
            </div>
            <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" SubmitButtonStyle-CssClass="ButtonStyle" SuccessTextStyle-CssClass="LabelStyle" SuccessTextStyle-Font-Bold="False" ValidatorTextStyle-CssClass="LabelStyle" LabelStyle-CssClass="LabelStyle" CssClass="LabelStyle">
                <UserNameTemplate>
                    <asp:Literal EnableViewState="False" ID="FailureText" runat="server" />
                    <table border="0" cellpadding="0">
                        <tr height="40px" valign="top">
                            <td colspan="2">
                                <asp:Label ID="Label1" runat="server" Text="Entrez votre Nom d'utilisateur pour retrouver votre Mot de Passe." CssClass="LabelStyle" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label AssociatedControlID="UserName" ID="UserNameLabel" runat="server" Text="Nom d'utilisateur :" CssClass="LabelStyle" />
                            </td>
                            <td align="left">
                                <asp:TextBox ID="UserName" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="UserName" ErrorMessage="User Name is required."
                                    ID="UserNameRequired" runat="server" ToolTip="User Name is required." ValidationGroup="PasswordRecovery1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td height="35px" align="left">
                                <asp:Button CommandName="Submit" ID="SubmitButton" runat="server" Text="Valider" ValidationGroup="PasswordRecovery1" CssClass="ButtonStyle" />
                            </td>
                        </tr>
                    </table>
                </UserNameTemplate>
            </asp:PasswordRecovery>
        </div>
        <asp:Panel runat="server" ID="Panel1" Visible="false">
        <div class="rightblock">
            <h3>Enregistrez vous</h3>
            <div class="dashedline"></div>
            <asp:Label ID="Label1" runat="server" Text="Vous n'avez pas de Login ?" CssClass="LabelStyle" /><br />
            <asp:Label ID="Label2" runat="server" Text="Utilisez la page suivante pour nous rejoindre." CssClass="LabelStyle" /><br />
            <table border="0">
                <tr>
                    <td height="50px">
                        <asp:HyperLink ID="OnSenfou2" runat="server" Text="Registration" NavigateURL="Register.aspx" CssClass="ButtonStyle" />
                    </td>
                </tr>
            </table>
        </div>
        </asp:Panel>
    </div>
</div>
</asp:Content>

