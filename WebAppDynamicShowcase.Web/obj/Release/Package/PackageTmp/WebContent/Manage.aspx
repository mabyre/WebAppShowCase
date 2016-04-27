<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" Inherits="Page_WebContentManage" Codebehind="Manage.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">
<div id="body">

        <asp:SqlDataSource ID="SqlDataSourceAdmin" runat="server" ConnectionString="<%$ ConnectionStrings:QuestionnaireDB %>"
                SelectCommand="GetWebContentVisualisateur" 
                SelectCommandType="StoredProcedure" 
                DataSourceMode="DataSet">
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSourceMembre" runat="server" ConnectionString="<%$ ConnectionStrings:QuestionnaireDB %>"
                SelectCommand="GetWebContentByUser" 
                SelectCommandType="StoredProcedure" 
                DataSourceMode="DataSet">
                <SelectParameters>
                    <asp:Parameter Name="Utilisateur" Type="String" />
                </SelectParameters>
        </asp:SqlDataSource>

        <div class="fullwidth">
            <h3>Les pages visualisées</h3>
            
            <table border="0" width="80%" cellpadding="10px">
                <tr>
                    <td align="left">
                        <a href="../Aide.aspx#Pages" title="Aide sur la gestion des Pages">
                        <img src="../App_Themes/Sodevlog/Images/help_16.ico" border="0" />
                        </a>
                    </td>
                </tr>
            </table>
            
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                DataKeyNames="WebContentID" 
                Width="80%" 
                DataSourceID="SqlDataSourceAdmin"
                AllowSorting="True">
                <HeaderStyle CssClass="GridViewHeaderStyle"/>
                <Columns>
                    <asp:TemplateField HeaderText="Section" SortExpression="Section">
                        <ItemStyle CssClass="BoundFieldStyle" />
                        <ItemTemplate>
                            <asp:Label ID="LabelSection" runat="server" Text='<%# Eval("Section") %>' CssClass='<%# ColorSection(Eval("Section").ToString()) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Utilisateur" HeaderText="Intervieweur" SortExpression="Utilisateur" Visible="false"/>
                    <asp:BoundField DataField="Visualisateur" HeaderText="Visualiseur" SortExpression="Visualisateur"/>
                    <asp:BoundField DataField="Description" HeaderText="Questionnnaire" SortExpression="Description" Visible="false"/>
                    <asp:BoundField DataField="WebContentDate" HeaderText="Date" SortExpression="WebContentDate" Visible="false" />
                    <asp:TemplateField HeaderText="Editer" SortExpression="">
                        <ItemStyle CssClass="BoundFieldEmailStyle" />
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLinkEdit" runat="server" Text="&#187;&#187;&#187;" NavigateUrl='<%#"~/WebContent/Edit.aspx?id=" +  Eval("WebContentID").ToString() %>' ToolTip='<%#Eval("Description").ToString()%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <table border="0" cellpadding="10px"><tr><td><b>Pas de Pages pour cet utilisateur</b></td></tr></table>                
                </EmptyDataTemplate>
            </asp:GridView>
        </div>

        <table border="0" cellpadding="3px" width="80%">
        <tr>
            <td align="left" width="120px">
                <asp:CheckBox ID="CheckBoxDescription" runat="server" Text="Description" AutoPostBack="true" OnCheckedChanged="CheckBoxDescription_CheckedChanged" />
            </td>
            <td align="left">
                <asp:CheckBox ID="CheckBoxDate" runat="server" Text="Date" AutoPostBack="true" OnCheckedChanged="CheckBoxDate_CheckedChanged" />
            </td>
        </tr>        
        </table>             
        
        <table border="0" cellpadding="10px" height="75px" >
        <tr>
            <td align="left">
                <asp:HyperLink ID="HyperLinkNouveau" runat="server" NavigateURL="~/WebContent/Edit.aspx" Text="Nouveau" ToolTip="Créer une Nouvelle Page" />
            </td>
        </tr>        
        </table>            
    </div>
</asp:Content>