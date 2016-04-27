<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Member_Email" Title="Untitled Page" Codebehind="Email.aspx.cs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="Server">
    <div id="body">
        <div id="columnleft">
            <a title="content_start" id="content_start"></a>
            <div class="leftblock">
                <h2>
                    Email</h2>
                <p>
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh
                    euismod tincidunt ut laoreet dolore magna aliquam erat volutpat. Ut wisi enim ad
                    minim veniam, quis nostrud exercitation ulliam corper suscipit lobortis nisl ut
                    aliquip ex ea commodo consequat. Duis autem veleum iriure dolor in hendrerit in
                    vulputate velit esse molestie consequat, vel willum lunombro dolore eu feugiat nulla
                    facilisis at vero eros et accumsan et iusto odio dignissim qui blandit praesent
                    luptatum zzril delenit augue duis dolore te feugait nulla facilisi.</p>
            </div>
        </div>

        <div id="columnright">
            <div class="rightblock">
                <table>
                    <tr>
                        <td class="formlabel">
                            Subject:
                        </td>
                        <td align="left" style="width: 384px">
                            <asp:TextBox ID="txtSubject" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" ToolbarSet="Default" SkinPath="skins/office2003/"
                    Value='<%# Bind("description") %>' BasePath="~/FCKeditor/" UseBROnCarriageReturn="true">
                </FCKeditorV2:FCKeditor>
                <br />
                <table>
                    <tr>
                        <td class="formlabel">
                            Attachments:
                        </td>
                        <td align="left" style="width: 384px">
                            <asp:TextBox ID="txtAttachments" runat="server" Height="56px" TextMode="MultiLine"
                                Visible="False" Width="100%" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:FileUpload ID="AttachmentFile" runat="server" />
                            <asp:Button ID="fileUpload" runat="server" Text="Upload" /></td>
                    </tr>
                    <tr>
                        <td class="formlabel">
                            Recipients:
                        </td>
                        <td>
                            <asp:CheckBoxList ID="chkRecipients" runat="server" Width="100%">
                            </asp:CheckBoxList>
                            <asp:CheckBox ID="chkAllMembers" runat="server" Text="All Members" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSend" runat="server" Text="Send Email" />
                        </td>
                        <td>
                            <asp:Label ID="lblError" runat="server" Visible="False" Width="100%" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

