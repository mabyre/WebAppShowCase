<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Member_Manage" Title="Administrer les membres" Codebehind="Manage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="sm" runat="server" />
<div id="body">
    <div class="DivTitreStyle">
        <h3>Membres</h3>
    </div>
    
    <div class="DivGridViewStyle">
    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
        <ContentTemplate>
        <asp:GridView ID="GridViewMembers" runat="server" Width="80%"
            AllowPaging="False"
            AllowSorting="True" 
            AutoGenerateColumns="False" 
            DataKeyNames="MemberGUID" 
            OnRowCreated="GridViewMembers_RowCreated" 
            OnSorted="GridViewMembers_OnSorted" 
            OnSorting="GridViewMembers_OnSorting"
            OnRowCommand="GridViewMembers_RowCommand"
            OnLoad="GridViewMembers_OnLoad" 
            OnPageIndexChanged="GridViewMembers_PageIndexChanged"
            PagerSettings-Mode="NumericFirstLast" 
            PagerSettings-Position="TopAndBottom" 
            PagerStyle-HorizontalAlign="Right" 
            PagerStyle-Font-Bold="true"
            PagerSettings-PageButtonCount="5"
            CssClass="GridViewStyle">
            <HeaderStyle CssClass="GridViewHeaderStyle"/>
            <SelectedRowStyle BackColor="#F1F1F1" />
            <Columns>
            
                <asp:BoundField HeaderText="NomUtilisateur" DataField="NomUtilisateur" SortExpression="NomUtilisateur" ReadOnly="true">
                    <ItemStyle CssClass="BoundFieldStyle" />
                    <HeaderStyle CssClass="BoundFieldHeaderStyle"/>
                </asp:BoundField>

                <asp:BoundField HeaderText="MotDePasse" DataField="MotDePasse" SortExpression="MotDePasse" ReadOnly="true">
                    <ItemStyle CssClass="BoundFieldStyle" />
                    <HeaderStyle CssClass="BoundFieldHeaderStyle"/>
                </asp:BoundField>

                <asp:BoundField HeaderText="Nom" DataField="Nom" SortExpression="Nom">
                    <ItemStyle CssClass="BoundFieldStyle" />
                    <HeaderStyle CssClass="BoundFieldHeaderStyle"/>
                </asp:BoundField>
                
                <asp:BoundField HeaderText="Prénom" DataField="Prenom" SortExpression="Prenom">
                    <ItemStyle CssClass="BoundFieldStyle" />
                    <HeaderStyle CssClass="BoundFieldHeaderStyle"/>
                </asp:BoundField>
                
                <asp:BoundField HeaderText="Société" DataField="Societe" SortExpression="Societe">
                    <ItemStyle CssClass="BoundFieldStyle" Width="150px" />
                    <HeaderStyle CssClass="BoundFieldHeaderStyle"/>
                </asp:BoundField>

                <asp:TemplateField HeaderText="Approved">
                    <ItemStyle CssClass="BoundFieldStyle" />
                    <ItemTemplate>
                        <asp:Label ID="LabelIsApproved" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Locked">
                    <ItemStyle CssClass="BoundFieldStyle" />
                    <ItemTemplate>
                        <asp:Label ID="LabelLocked" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Online">
                    <ItemStyle CssClass="BoundFieldStyle" />
                    <ItemTemplate>
                        <asp:Label ID="LabelOnline" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Date">
                    <ItemStyle CssClass="BoundFieldDateStyle" />
                    <ItemTemplate>
                        <asp:Label ID="LabelLastLoginDate" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:HyperLinkField DataNavigateUrlFields="MemberGUID" DataNavigateUrlFormatString="~/Member/Edit.aspx?MembreGUID={0}" HeaderText="Editer" Text="&#187;&#187;&#187;" ItemStyle-CssClass="ItemStyle" />
                
            </Columns>
            <EmptyDataTemplate>
                <table border="0" cellpadding="10px"><tr><td><b>Pas de contacts pour ce critère</b></td></tr></table>
            </EmptyDataTemplate>
            <PagerSettings Mode="NumericFirstLast" PageButtonCount="5" Position="TopAndBottom" />
            <PagerStyle Font-Bold="True" HorizontalAlign="Right" />
            
        </asp:GridView>

        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <label class="PageIndexStyle">Page <%=GridViewMembers.PageIndex + 1%> sur <%=GridViewMembers.PageCount%></label>
                </td>
            </tr>
        </table>
           
        </ContentTemplate>
        
    </asp:UpdatePanel>
    
    <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel22">
        <progresstemplate>
            <asp:Image ID="loading2" runat="server" SkinID="ImageLoading" />
        </progresstemplate>
    </asp:UpdateProgress>
    
    <table border="0" cellpadding="10px" height="75px" >
    <tr>
        <td align="left">
            <asp:HyperLink ID="HyperLinkRegister" runat="server" NavigateURL="~/Member/Register.aspx" Text="Nouveau" CssClass="ButtonStyle" ToolTip="Enregistrez un nouvel utilisateur" />
        </td>
    </tr>        
    </table>            
</div>
</div>
</asp:Content>

