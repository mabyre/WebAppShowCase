<%@ Control Language="C#" AutoEventWireup="true" Inherits="UserControl_WebContent" Codebehind="WebContent.ascx.cs" %>
<asp:HiddenField ID="HiddenFieldSectionName" runat="server" />
<asp:Hyperlink ID="HyperlinkEdit" runat="server" Visible="false">
    <asp:Image ID="ImageEdit" runat="server" ImageUrl="~/Images/Edit.gif" />
</asp:Hyperlink>
<asp:Label ID="LabelContent" runat="server" />
