<%@ Control Language="C#" AutoEventWireup="true" Inherits="User_controls_CommentView" Codebehind="CommentView.ascx.cs" %>
<%@ Import Namespace="PageEngine" %>

<% if (Post.Comments.Count > 0){ %>
<h3 id="comment">Commentaires</h3>
<%} %>

<div id="commentlist">
    <asp:PlaceHolder runat="server" ID="PlaceHolderComments" >
        <asp:Label ID="LabelPasDeCommentaire" runat="server" Text="Pas de commentaire" Visible="false" CssClass="LabelStyle" />
    </asp:PlaceHolder>
</div>

<img src="<%=Utils.RelativeWebRoot %>Images/ajax-loader.gif" alt="Saving the comment" style="display:none" id="ajaxLoader" />  
<span id="status"></span>

<asp:PlaceHolder runat="Server" ID="PlaceHolderAddComment">

<div class="commentForm">
<table border="0" cellpadding="3px" cellspacing="2px" width="100%">
<tr>
    <td colspan="3" valign="middle">
        <h3 id="addcomment">Ajoutez un commentaire</h3>
    </td>
</tr>
<tr>
    <td align="right" width="130px">
        <asp:Label ID="Label10" runat="server" Text="* " CssClass="LabelRedStyle" />
        <asp:Label ID="Label9" runat="server" Text="Nom :" CssClass="LabelStyle"  />
    </td>
    <td align="left">
        <asp:TextBox runat="Server" ID="TextBoxName" TabIndex="1" ValidationGroup="AddComment" Width="250px" />
        <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="TextBoxName" ErrorMessage="Please choose another name" Display="dynamic" ClientValidationFunction="CheckAuthorName" EnableClientScript="true" ValidationGroup="AddComment" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" CssClass="RequiredFieldValidatorStyle" ControlToValidate="TextBoxName" ErrorMessage="Requis" Display="dynamic" ValidationGroup="AddComment" />
    </td>
    <td align="left" width="5px">
    </td>
</tr>
<tr>
    <td align="right">
        <asp:Label ID="Label4" runat="server" Text="* " CssClass="LabelRedStyle"/>
        <asp:Label ID="Label3" runat="server" Text="Email :" CssClass="LabelStyle" />
    </td>
    <td align="left" width="410px">
        <asp:TextBox runat="Server" ID="txtEmail" TabIndex="2" ValidationGroup="AddComment" Width="250px" />
        <asp:Label ID="LabelAvatar" runat="server" CssClass="LabelStyle"/>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="RequiredFieldValidatorStyle" ControlToValidate="txtEmail" ErrorMessage="Requis" Display="dynamic" ValidationGroup="AddComment" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" CssClass="RegularExpressionValidatorStyle" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email non valide" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="AddComment" />
    </td>
    <td align="left">
    </td>
</tr>
<tr>
    <td align="right">
        <asp:Label ID="Label5" runat="server" Text="Site Web :" CssClass="LabelStyle" />
    </td>
    <td align="left">
        <asp:TextBox runat="Server" ID="txtWebsite" TabIndex="3" ValidationGroup="AddComment" Width="250px" />
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="RegularExpressionValidatorStyle" runat="Server" ControlToValidate="txtWebsite" ValidationExpression="(http://|https://|)([\w-]+\.)+[\w-]+(/[\w- ./?%&=;~]*)?" ErrorMessage="Please enter a valid URL" Display="Dynamic" ValidationGroup="AddComment" />
    </td>
    <td align="left">
    </td>
</tr>
<tr>
    <td align="right">
        <asp:Label ID="Label1" runat="server" Text="Pays :" CssClass="LabelStyle" />
    </td>
    <td align="left">
        <asp:DropDownList runat="server" ID="ddlCountry" onchange="SetFlag(this.value)" TabIndex="4" EnableViewState="false" ValidationGroup="AddComment" />&nbsp;
        <asp:Image runat="server" ImageUrl="~/Images/flags/fr.png" ID="imgFlag" AlternateText="Country flag" Width="16" Height="11" EnableViewState="false" />
    </td>
    <td align="left">
    </td>
</tr>
<tr>
    <td valign="top" align="left" colspan="3">
        <asp:Label ID="Label7" runat="server" Text="* " CssClass="LabelRedStyle"/>
        <asp:Label ID="Label6" runat="server" Text="Commentaire :" CssClass="LabelStyle"/><br />
        <asp:TextBox runat="server" ID="txtContent" TextMode="multiLine" Rows="10" TabIndex="5" ValidationGroup="AddComment" />
    </td>
</tr>
<tr>
    <td valign="top" align="left" colspan="3" height="18px">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="RequiredFieldValidatorStyle" runat="server" ControlToValidate="txtContent" ErrorMessage="Requis" Display="dynamic" ValidationGroup="AddComment" />
    </td>
</tr>
<tr>
    <td colspan="3">
        <input type="checkbox" id="cbNotify" style="width: auto" />
        <asp:Label ID="Label8" runat="server" Text="Me prévenir pour de nouveaux commentaires" CssClass="LabelStyle"/>
    </td>
</tr>
<tr>
    <td colspan="3" height="35px">
        <input type="button" id="btnSave" value="Sauvez commentaire" class="ButtonStyle" onclick="if(Page_ClientValidate()){AddComment()}" tabindex="6" />  
        <asp:Button runat="server" ID="btnSave" style="display:none" Text="Ajoutez commentaire" CssClass="ButtonStyle" UseSubmitBehavior="false" TabIndex="6" ValidationGroup="AddComment" OnClick="btnSave_Click" />
    </td>
</tr>
</table>
</div>

<script type="text/javascript">
<!--//
var isAjaxSupported = (window.ActiveXObject != "undefined" || window.XMLHttpRequest != "undefined");
if (!isAjaxSupported)
{
  document.getElementById('<%=btnSave.ClientID %>').style.display = "inline";
  document.getElementById('btnSave').style.display = "none";
}

function AddComment()
{
  document.getElementById("btnSave").disabled = true;
  document.getElementById("ajaxLoader").style.display = "inline";
  document.getElementById("status").className = "";
  document.getElementById("status").innerHTML = "Saving the comment...";
  
  var author = document.getElementById("<%=TextBoxName.ClientID %>").value;
  var email = document.getElementById("<%=txtEmail.ClientID %>").value;
  var website = document.getElementById("<%=txtWebsite.ClientID %>").value;
  var country = "";
  if (document.getElementById("<%=ddlCountry.ClientID %>"))
    country = document.getElementById("<%=ddlCountry.ClientID %>").value;
  var content = document.getElementById("<%=txtContent.ClientID %>").value;
  var notify = document.getElementById("cbNotify").checked;
  var argument = author + "-|-" + email + "-|-" + website + "-|-" + country + "-|-" + content + "-|-" + notify;
  <%=Page.ClientScript.GetCallbackEventReference(this, "argument", "AppendComment", "'comment'") %>
  
  if (typeof OnComment != "undefined")
    OnComment(author, email, website, country, content);
}

function AppendComment(args, context)
{
  if (context == "comment")
  {
    if (document.getElementById("commentlist").innerHTML == "")
      document.getElementById("commentlist").innerHTML = "<h3 id='comment'>Comments</h3>"
    document.getElementById("commentlist").innerHTML += args;
    document.getElementById("<%=txtContent.ClientID %>").value = "";
    document.getElementById("ajaxLoader").style.display = "none";
    document.getElementById("status").className = "success";
    document.getElementById("status").innerHTML = "Le commentaire a été enregistré. Merci de votre feed-back";
  }
  
  document.getElementById("btnSave").disabled = false;
}

var flagImage = document.getElementById("<%= imgFlag.ClientID %>");

function SetFlag(iso)
{  
  if (iso.length > 0)
    flagImage.src = "<%=VirtualPathUtility.ToAbsolute("~/") %>Images/flags/" + iso + ".png";
  else
    flagImage.src = "<%=VirtualPathUtility.ToAbsolute("~/") %>Images/pixel.gif";
}

function CheckAuthorName(sender, args)
{
  args.IsValid = true;
  
  <% if (!Page.User.Identity.IsAuthenticated){ %>
  var author = "<%=Post.Author %>";
  var visitor = document.getElementById("<%=TextBoxName.ClientID %>").value;
  args.IsValid = author.toLowerCase() != visitor.toLowerCase();
  <%} %>
}
//-->
</script>
</asp:PlaceHolder>

<asp:label runat="server" id="LabelCommentsDisabled" visible="false" CssClass="LabelStyle">Les commentaires sont clos</asp:label>