<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" Inherits="WebContent_Edit" Title="Untitled Page" Codebehind="Edit.aspx.cs" %>
<%@ Register Assembly="FredCK.FCKeditorV2" tagPrefix="FCKeditorV2" namespace="FredCK.FCKeditorV2"%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<table border="0" cellpadding="10px" cellpadding="5px" width="100%">
<tr>
    <td>
        <h3>Edition d'une Page</h3>
    </td>
</tr>
</table>
        
    <div class="DivEditeurStyle">
        
        <table border="0" cellpadding="2" width="100%">
        <tr>
            <td align="right" width="260px">
                <strong>Section : </strong>
            </td>
            <td align="left">
                <asp:Label ID="LableSection" CssClass="LabelStyle" runat="server" />                
            </td>
        </tr>
        </table>
       
        <table><tr><td height="10px"></td></tr></table>
        
        <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="500px" Width="100%"
            ToolbarSet="Default" SkinPath="skins/office2003/" BasePath="~/FCKeditor/" UseBROnCarriageReturn="true" />

        <table border="0">
        <tr>
              <td class="LabelStyle" width="160px" align="right">Transférer une image : </td>
              <td>
                <asp:FileUpload runat="server" ID="txtUploadImage" Width="400" />
                <asp:Button runat="server" ID="ButtonUploadImage" Text="Upload" OnClick="ButtonUploadImage_Click" />
              </td>
        </tr>
        <tr>
              <td class="LabelStyle"  width="160px" align="right">Transmettre un fichier : </td>
              <td>
                <asp:FileUpload runat="server" ID="txtUploadFile" Width="400" />        
                <asp:Button runat="server" ID="ButtonUploadFile" Text="Upload" OnClick="ButtonUploadFile_Click" />
              </td>
        </tr>
        </table>     
        <table style="border:none" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td height="30px" align="center">
                    <asp:Label ID="ValidationMessage" CssClass="LabelValidationMessageStyle" Runat="server" Visible="false" />
                </td>
            </tr>
        </table>        
    </div>
    <table border="0" width="100%" height="80px">
        <tr>
            <td align="center">
                <asp:Button ID="ButtonSauver" runat="server" Visible="true" CssClass="ButtonStyle" OnClick="ButtonSauver_Click" Text="Sauver" />
                <asp:HyperLink Visible="false" ID="HyperLinkRetour" runat="server" CssClass="ButtonStyle" NavigateURL="~/WebContent/Manage.aspx" Text="Retour" ToolTip="Retourner à la gestion des pages" />
            </td>
        </tr>
    </table>
</asp:Content>

