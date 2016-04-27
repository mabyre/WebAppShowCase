<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_LoginBanner" Codebehind="LoginBanner.ascx.cs" %>
<asp:LoginView ID="LoginView1" runat="server">
    <LoggedInTemplate>
        <table border="0" cellpadding="2" cellspacing="3">
            <tr>
                <td>
                    <asp:LoginName ID="LoginName1" runat="server" CssClass="LabelStyle" />
                    <asp:Label ID="LabelUserInRoles" runat="server" Text="Label" CssClass="LabelStyle" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="LinkButtonLogout" runat="server" Text="Logout" CssClass="ButtonStyle" OnClick="LinkButtonLogout_Click" />
                </td>
            </tr>
        </table>
    </LoggedInTemplate>
    <AnonymousTemplate>
        <table border="0" cellpadding="8" cellspacing="3">
            <tr>
                <td>
                    <asp:HyperLink ID="HyperLinkLogin" runat="server" CssClass="ButtonStyle" Text="Admin" NavigateURL="~/Member/Login.aspx?ReturnURL=" />
                    &nbsp;
                    <asp:HyperLink ID="HyperLinkRegister" runat="server" CssClass="ButtonStyle" Text="Register" NavigateURL="~/Member/Register.aspx" />
                </td>
            </tr>
        </table>
    </AnonymousTemplate>
</asp:LoginView>
