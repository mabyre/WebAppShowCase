<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Contact_MemberDelete" Title="Supprimer un membre" Codebehind="Delete.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<div id="body">
<div>
    <div>
    <br />
    <h3>Suppression d'un Membre</h3>
    <br />
    </div>
    
    <table style="border:none" cellpadding="0" cellspacing="0">
        <tr>
            <td align="left">
                <asp:Label ID="ValidationMessage" CssClass="LabelValidationMessageStyle" Runat="server" Visible="false" />
            </td>
        </tr>
    </table>

    <table cellpadding="2">
        <tr>
            <td height="60px">
                <asp:Button ID="ButtonSupprimer" runat="server" Text="Supprimer" ToolTip="Confirmer la suppression de ce Membre et des objets associés" OnClick="ButtonSupprimer_Click" CssClass="ButtonStyle"/>                
            </td>
            <td height="60px">
                <asp:Button ID="ButtonCancel" runat="server" Text="Retour" ToolTip="Retour à la liste des Membres" OnClick="ButtonCancel_Click" CssClass="ButtonStyle"/>                
            </td>
        </tr>
    </table>
    
</div>
</div></asp:Content>

