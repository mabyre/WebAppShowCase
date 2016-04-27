<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" Inherits="Member_ManageRoles" Title="Gérer les rôles utilisateurs" Codebehind="ManageRoles.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<table class="TableStyle" border="0">
    <tr>
        <td width="450" class="TitleTextStyle" height="45px" valign="middle">
            <br />
            <h3>
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:labels, CreateRoleTitre %>" />
            <asp:Label CssClass="LabelStyleNoHeight" ForeColor="#0742AF" ID="LabelUserName" runat="server"></asp:Label>
            </h3>
        </td>
    </tr>
    <tr>
        <td align="center" valign="middle" width="450">
        <table>
            <tr>
                <td align="center" width="150">
                    <asp:Label ID="Label3" runat="server" CssClass="LabelStyle" Text="<%$ Resources:labels, CreateRoleUtilisateur %>" Width="150px"></asp:Label></td>
                <td width="150" align="center">
                    <asp:Label CssClass="LabelStyle" ID="Label1" runat="server" Text="<%$ Resources:labels, CreateRoleApplication %>" Width="150px"></asp:Label></td>
                <td align="center" width="60">
                    &nbsp;</td>
                <td width="150" align="center">
                    <asp:Label CssClass="LabelStyle" ID="Label2" runat="server" Text="<%$ Resources:labels, CreateRoleRoleUtilisateur %>" Width="150px"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" width="150" valign="top">
                    <asp:ListBox ID="ListBoxUtilisateurs" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListBoxUtilisateurs_SelectedIndexChanged" Rows="5">
                    </asp:ListBox></td>
                <td width="150" align="center" valign="top">
                    <asp:ListBox ID="ListBoxRolesApplication" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ListBoxRolesApplication_SelectedIndexChanged" Rows="5"></asp:ListBox></td>
                <td align="center" valign="top" width="60">
                    <asp:Button ID="ButtonAddRole" runat="server" Text="->" Width="40px" OnClick="ButtonAddRole_Click" CssClass="ButtonStyle" /><br /><br />
                    <asp:Button ID="ButtonRemoveRole" runat="server" Text="<-" Width="40px" OnClick="ButtonRemoveRole_Click" CssClass="ButtonStyle" />
                </td>
                <td width="150" align="center" valign="top">
                    <asp:ListBox ID="ListBoxRolesUtilisateur" runat="server" Rows="5"></asp:ListBox></td>
            </tr>
            <tr>
                <td align="center" width="150" height="40">
                    <asp:Button ID="EditerUtilisateur" runat="server" CssClass="ButtonStyle" OnClick="EditerUtilisateur_Click" Text="<%$ Resources:labels, CreateRoleBoutonEditer %>" Width="80px" ToolTip="<%$ Resources:labels, CreateRoleBoutonEditerToolTip %>" />
                    <br /><br style="font-size:3px" />
                    <asp:Button ID="SupprimerUtilisateur" runat="server" CssClass="ButtonStyle" OnClick="SupprimerUtilisateur_Click" Text="<%$ Resources:labels, CreateRoleBoutonSupprimer %>" Width="80px" ToolTip="<%$ Resources:labels, CreateRoleBoutonSupprimerToolTip %>" Visible="false" />
                </td>
                <td align="center" width="150" height="40">
                    <asp:Button ID="SupprimerRole" runat="server" CssClass="ButtonStyle" OnClick="SupprimerRole_Click" Text="<%$ Resources:labels, CreateRoleBoutonSupprimerRole %>" Width="80px" ToolTip="<%$ Resources:labels, CreateRoleBoutonSupprimerRoleToolTip %>" /></td>
                <td align="center" valign="top" width="60" height="40">
                </td>
                <td align="center" width="150" height="40">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Label ID="Label4" runat="server" CssClass="LabelStyle" Text="<%$ Resources:labels, CreateRoleLabelRoleApplication %>" Width="140px"></asp:Label>
                    <asp:Label ID="LabelRoleApplication" runat="server" CssClass="LabelStyleNoHeight" ForeColor="#0742AF"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" width="150">
                </td>
                <td align="center" width="150">
            <asp:ListBox ID="ListBoxUtilisateurRoles" runat="server" AutoPostBack="True" Rows="5"></asp:ListBox></td>
                <td align="center" valign="top" width="60">
                </td>
                <td align="center" width="150">
                </td>
            </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td width="450" height="70" align="center">
            <p class="LabelStyle"><asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:labels, CreateRoleLableCreerRole %>" />
            <asp:TextBox ID="RoleTextBox" runat="server" />
            <asp:Button Text="<%$ Resources:labels, CreateRoleButton%>" ID="CreateRoleButton" runat="server" OnClick="CreateNewRole_OnClick" Width="100px" CssClass="ButtonStyle" />
            </p>
        </td>
    </tr>
    <tr>
        <td align="center" height="50" width="450">
            <asp:Button ID="ButtonAnnuler" runat="server" Text="<%$ Resources:labels, CreateRoleBoutonAnnuler %>" OnClick="ButtonAnnuler_Click" CssClass="ButtonStyle" />
        </td>
    </tr>
    <tr>
        <td width="450" height="35" class="HyperLinkStyle" >
            <font color="red"><asp:Label ID="Message" runat="server" /></font>
        </td>
    </tr>
    <tr>
        <td width="450" height="35" class="HyperLinkStyle" >
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Member/Register.aspx" Text="<%$ Resources:labels, CreateRoleHyperLinkCreerUtilisateur %>" CssClass="ButtonStyle" />
        </td>
    </tr>
</table>
    <!-- C'est objet sont la pour la demmo
    <table>
        <tr>
            <td style="width:auto">
              <asp:GridView  ID="RolesGrid" runat="server" 
                CellPadding="2"
                Gridlines="Both" 
                CellSpacing="2" 
                AutoGenerateColumns="false" >
                <HeaderStyle BackColor="#0742AF" ForeColor="white" />
                <Columns>
                  <asp:TemplateField HeaderText="Les rôles utilisateurs" runat="server" >
                    <ItemTemplate>
                      <%# Container.DataItem.ToString() %>
                    </ItemTemplate>
                  </asp:TemplateField>
                </Columns>
               </asp:GridView>
            </td>
            <td style="width: 10px">
            </td>
            <td style="width:auto">
                <asp:DropDownList ID="DropDownListRoles" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    -->
</asp:Content>