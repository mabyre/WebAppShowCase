<%@ Page Language="C#" MasterPageFile="~/Admin/MasterAdminPage.master" ValidateRequest="false" AutoEventWireup="true" Inherits="Admin_SiteMap" Title="Sodevlog - Site Map" Codebehind="SiteMap.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">
    <table border="0" width="890px" cellpadding="0" cellspacing="0">
        <tr>
            <td height="15" width="5">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table border="0" cellpadding="3" cellspacing="2"  bgcolor="#F5F5F4">
                    <tr>
                        <td valign="middle" width="900px">
                            <asp:Label ID="LabelErreurMessage" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelMessage" CssClass="LabelPageStyle" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                            <asp:Label ID="LabelTailleUserFiles" CssClass="LabelPageStyle" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table border="0">
        <tr>
            <td height="5">
            </td>
            <td style="width: 625px">
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td height="460" style="width: 625px">
                <asp:PlaceHolder ID="PlaceHolderSiteMap" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td >
            </td>
            <td style="width: 625px">
            </td>
        </tr>
    </table>

</asp:Content>

