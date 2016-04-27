<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="True" Inherits="PageErreur" Title="Page d'erreur" Codebehind="PageErreur.aspx.cs" %>
<%@ Register Src="~/Controls/CommentView.ascx" TagName="CommentView" TagPrefix="usrc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
    <div id="body">
        <div class="fullwidth">
        <table cellpadding="5px">
            <tr>
                <td height="15px">
                </td>
            </tr>
            <tr>
                <td>
                    <h3>Message de l'application</h3>
                </td>
            </tr>
            <tr>
                <td>
			        <asp:Label ID="LabelValidationMessage" CssClass="LabelValidationMessageStyle" Runat="server" />
                </td>
            </tr>
            <tr>
                <td height="15px">
                </td>
            </tr>
        </table>
        </div>
    </div>
</asp:Content>