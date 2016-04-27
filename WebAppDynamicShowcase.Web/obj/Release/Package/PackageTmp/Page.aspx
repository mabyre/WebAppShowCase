<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" Inherits="Default_Page" Title="Page" Codebehind="Page.aspx.cs" %>
<%@ Register Src="~/Controls/CommentView.ascx" TagName="CommentView" TagPrefix="usrc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<table border="0" cellpadding="15" cellspacing="0" width="100%">
    <tr>
        <td>
            <div id="page">
                <h1 runat="server" id="h1Title" />
                <div runat="server" id="divText" />    
                <%=AdminLinks%>
            </div>
        </td>
    </tr>
</table>
<asp:Panel ID="PanelCommentairesVisibles" runat="server" >
<table border="0" cellpadding="15" cellspacing="0" width="100%">
    <tr>
        <td align="center">
            <asp:Button ID="ButtonCommentaires" runat="server" CssClass="ButtonStyle" OnClick="ButtonCommentaires_Click" Text="Commentaires" ToolTip="Afficher les Commentaires"/>
        </td>
    </tr>
</table>    
<asp:Panel ID="PanelCommentaires" runat="server" >
<table border="0" cellpadding="15" cellspacing="0">
    <tr>
        <td>
            <usrc:CommentView ID="CommentView1" runat="server" />
        </td>
    </tr>
</table>  
</asp:Panel>
</asp:Panel>
</asp:content>
