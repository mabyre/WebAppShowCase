<%@ Page Language="C#" 
    MasterPageFile="~/MasterPage.master" 
    AutoEventWireup="true" 
    Inherits="_Default" 
    Title="Page par défaut"
    Trace="false" Codebehind="Default.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<table border="0" cellpadding="15px" cellspacing="5px">
<tr>
    <td>
        <asp:Label ID="LabelDefault" runat="server" CssClass="LabelStyle" />
    </td>
</tr>
</table>
</asp:Content>

