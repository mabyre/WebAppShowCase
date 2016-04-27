<%@ Control Language="C#" AutoEventWireup="true" Inherits="Controls_AdminMenu" Codebehind="AdminMenu.ascx.cs" %>
<%@ Register TagPrefix="usrc" TagName="WebContent" Src="~/Controls/WebContent.ascx" %>
<table border="0" cellpadding="0" cellspacing="0">
    <tr class="BoiteGaucheHeaderStyle" >
        <td width="450px" valign="top">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="20px" height="40px"></td>
                    <td>
                    <h3>Administration</h3>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="BoiteGaucheCentreStyle" id="TrBoiteGaucheCentre" runat="server">
        <td>
            <table>
               <tr>
               <td width="20px"></td>
               <td>
                    <asp:LoginView ID="LoginViewAdminMenu" runat="server">
                        <AnonymousTemplate>
                        Anonyme
                        </AnonymousTemplate>
                        <LoggedInTemplate>
                            <h3>Hello <asp:LoginName ID="LoginName1" runat="server" /></h3>
                            <p>
                            <usrc:WebContent ID="WebContent1" Section="WelcomeBack" runat="server" />
                            <div class="dashedline">
                            </div>
                            </p>
                            <asp:LinkButton ID="LinkButtonUserDetails" runat="server" CssClass="ButtonStyle" PostBackUrl="~/Member/Details.aspx" Width="122px" Text="Mes détails" />
                            <br style="font-size:3px" /> 
                            <asp:LinkButton ID="LinkButtonDeconnexion" runat="server" CssClass="ButtonStyle" OnClick="LinkButtonDeconnexion_Click" Width="122px" Text="Déconnexion" />
                            <br style="font-size:3px" /> 
                            <asp:LinkButton ID="LinkButtonManage" runat="server" CssClass="ButtonStyle" PostBackUrl="~/Member/Manage.aspx" Width="122px" Text="Gérez Utilisateurs" Visible="false"/>
                            <br style="font-size:3px" />     
                            <asp:LinkButton ID="LinkButtonManageRoles" runat="server" CssClass="ButtonStyle" PostBackUrl="~/Member/ManageRoles.aspx" Width="122px" Text="Gérez les rôles" Visible="false"/>
                            <br style="font-size:3px" />     
                            <asp:LinkButton ID="LinkButtonPage" runat="server" CssClass="ButtonStyle" PostBackUrl="~/Admin/Page/Pages.aspx" Width="122px" Text="Pages" Visible="false"/>
                            <br style="font-size:3px" /> 
                            <asp:LinkButton ID="LinkButtonStyleSheet" runat="server" CssClass="ButtonStyle" PostBackUrl="~/Admin/Page/StyleSheet.aspx" Width="122px" Text="StyleSheet" Visible="false"/>
                            <br style="font-size:3px" /> 
                            <asp:LinkButton ID="LinkButtonSiteMap" runat="server" CssClass="ButtonStyle" PostBackUrl="~/Admin/Page/SiteMap.aspx" Width="122px" Text="SiteMap" Visible="false"/>
                            <br style="font-size:3px" /> 
                            <asp:LinkButton ID="LinkButtonSiteSettings" runat="server" CssClass="ButtonStyle" PostBackUrl="~/Admin/Page/Settings.aspx" Width="122px" Text="Paramètres" Visible="false"/>
                            <br style="font-size:3px" /> 
                            <asp:LinkButton ID="LinkButtonLogUser" runat="server" CssClass="ButtonStyle" PostBackUrl="~/Admin/Page/LogUser.aspx" Width="122px" Text="Log" Visible="false"/>
                            <br style="font-size:3px" /> 
                            <asp:LinkButton ID="LinkButtonSmtpServeur" runat="server" CssClass="ButtonStyle" PostBackUrl="~/Admin/Page/SmtpServeur.aspx" Width="122px" Text="Smtp Serveur" Visible="false"/>
                            <br style="font-size:3px" /> 
                        </LoggedInTemplate>
                    </asp:LoginView>
               </td>
               </tr>
            </table>
        </td>
    </tr>
    <tr class="BoiteGaucheCorpsStyle" height="20px">
        <td>
        </td>
    </tr>
    <tr class="BoiteGaucheFooterStyle">
        <td>
        </td>
    </tr>
</table>


