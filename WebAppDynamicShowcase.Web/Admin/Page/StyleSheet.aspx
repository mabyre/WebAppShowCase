<%@ Page Language="C#" MasterPageFile="~/Admin/MasterAdminPage.master" ValidateRequest="false" AutoEventWireup="true" Inherits="Admin_StyleSheet" Title="Sodevlog - Style Sheet" Codebehind="StyleSheet.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphAdmin" Runat="Server">
    <table border="0px" width="890px" cellpadding="0" cellspacing="0">
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
                <table border="0px" cellpadding="3" cellspacing="2"  bgcolor="#F5F5F4">
                    <tr>
                        <td valign="middle" width="900px">
                            <asp:Label ID="Label2" runat="server" CssClass="LabelStyle" Text="Transférer une image :" Width="160px"></asp:Label>
                            &nbsp;
                            <asp:FileUpload runat="server" ID="FileUploadImage" Width="400" />
                            <asp:Button runat="server" ID="ButtonUploadImage" Text="Upload" CausesValidation="False" OnClick="ButtonUploadImage_Click" />
                            &nbsp;
                            <asp:Button runat="server" ID="ButtonDir" Text="Dir" OnClick="ButtonDir_Click" />
                            &nbsp;
                            <asp:Label ID="LabelErreurMessage" runat="server" Visible="false"></asp:Label>
                            <br />
                            <asp:Label ID="Label1" runat="server" CssClass="LabelStyle" Text="Fichier à supprimer :" Width="160px" ></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="TextBoxFichier" runat="server"></asp:TextBox>
                            &nbsp;
                            <asp:Button ID="ButtonSupprimer" runat="server" OnClick="ButtonSupprimer_Click" Text="Supprimer" ToolTip="Supprimer le fichier" />
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
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td height="460">
                <asp:PlaceHolder ID="PlaceHolderCss" runat="server"></asp:PlaceHolder>
            </td>
        </tr>
        <tr>
            <td >
            </td>
            <td>
            </td>
        </tr>
    </table>

</asp:Content>

