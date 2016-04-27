<%@ Control Language="C#" ClassName="MenuStyle"  EnableTheming="true" AutoEventWireup="true" EnableViewState="true" Inherits="Control_MenuPanel" Codebehind="MenuPanel.ascx.cs" %>
<asp:Menu ID="MenuArticle" runat="server" 
    CssClass="MenuStyle" 
    Orientation="Horizontal" 
    DisappearAfter="3000" 
    BorderStyle="None" 
    StaticBottomSeparatorImageUrl="~/App_Themes/Sodevlog/Images/menu_static_bottom_separator.gif" 
    StaticPopOutImageUrl="~/App_Themes/Sodevlog/Images/arrow_right.gif" 
    StaticTopSeparatorImageUrl="~/App_Themes/Sodevlog/Images/menu_static_bottom_separator.gif" 
    DynamicPopOutImageUrl="~/App_Themes/Sodevlog/Images/arrow_right.gif"
    DynamicEnableDefaultPopOutImage="True" 
    MaximumDynamicDisplayLevels="10">
    <StaticMenuStyle CssClass="StaticMenuStyle" />
    <StaticMenuItemStyle CssClass="StaticMenuItemStyle" />
    <StaticSelectedStyle CssClass="StaticSelectedStyle" />
    <StaticHoverStyle CssClass="StaticHoverStyle" />
    <DynamicMenuStyle CssClass="DynamicMenuStyle" />
    <DynamicMenuItemStyle CssClass="DynamicMenuItemStyle"/>
    <DynamicSelectedStyle CssClass="DynamicSelectedStyle" />
    <DynamicHoverStyle CssClass="DynamicHoverStyle" />
</asp:Menu>
