<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Member_Register" Title="Enregistrement d'un nouveau membre" Codebehind="Register.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="Server">

<script type="text/javascript">
function validateLengthPassWord( oSrc, args )
{
    args.IsValid = ( args.Value.length >= '<%=Membership.MinRequiredPasswordLength%>' );
}
</script>                

<div id="body">
    <div class="fullwidth">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <br />
        <h3>Enregistrez un nouveau membre</h3>
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table border="0" cellpadding="3" cellspacing="0">
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label1" runat="server" Text="Nom d'utilisateur :" CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxNomUtilisateur" runat="server" 
                    CssClass="TextBoxRegisterStyle"></asp:TextBox>
                </td>
                <td class="TdRequiredFieldValidatorStyle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxNomUtilisateur"
                        ErrorMessage="Entrez un nom d'utilisateur." 
                        CssClass="RequiredFieldValidatorStyle" 
                        ValidationGroup="reg">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label2" runat="server" Text="Mot de Passe : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxPassWord" TextMode="Password" runat="server" 
                        CssClass="TextBoxRegisterStyle" />
                </td>
                <td class="TdRequiredFieldValidatorStyle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                        runat="server" ControlToValidate="TextBoxPassWord"
                        ErrorMessage="Entrez un mot de Passe." 
                        ValidationGroup="reg" 
                        CssClass="RequiredFieldValidatorStyle" >*</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorPassWord" runat="server" 
                        ControlToValidate = "TextBoxPassWord"
                        ClientValidationFunction="validateLengthPassWord" >
                    </asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label3" runat="server" 
                        Text="Confirmez le Mot de Passe : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxConfirmPassword" TextMode="Password" runat="server" CssClass="TextBoxRegisterStyle"></asp:TextBox></td>
                <td class="TdRequiredFieldValidatorStyle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxConfirmPassword"
                        ErrorMessage="Merci de confirmer votre Mot de Passe." 
                        ValidationGroup="reg" CssClass="RequiredFieldValidatorStyle" >*</asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxPassWord" ControlToValidate="TextBoxConfirmPassword"
                        ErrorMessage="La confirmation du Mot de Passe ne correspond pas." 
                        ValidationGroup="reg">*</asp:CompareValidator></td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label4" runat="server" Text="Email : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxEmail" runat="server" CssClass="TextBoxRegisterStyle" />
                <td class="TdRequiredFieldValidatorStyle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="TextBoxEmail"
                        ErrorMessage="Merci d'entrer votre adresse email." 
                        ValidationGroup="reg" CssClass="RequiredFieldValidatorStyle" >*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="TextBoxEmail" ErrorMessage="Merci d'entrer une adresse email valide."
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                        ValidationGroup="reg">*</asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label5" runat="server" Text="Question de Sécurité : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxQuestion" runat="server" CssClass="TextBoxRegisterStyle"></asp:TextBox>
                </td>
                <td class="TdRequiredFieldValidatorStyle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TextBoxQuestion"
                        ErrorMessage="Entrez une question de sécurité." ValidationGroup="reg" CssClass="RequiredFieldValidatorStyle" >*</asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label6" runat="server" Text="Réponse de Sécurité : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxAnswer" runat="server" CssClass="TextBoxRegisterStyle"></asp:TextBox>
                </td>
                <td class="TdRequiredFieldValidatorStyle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxAnswer"
                        ErrorMessage="Merci d'entrer une réponse de sécurité." ValidationGroup="reg" CssClass="RequiredFieldValidatorStyle" >*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label7" runat="server" Text="Prénom : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxFisrtName" runat="server" CssClass="TextBoxRegisterStyle"></asp:TextBox>
                </td>
                <td class="TdRequiredFieldValidatorStyle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBoxFisrtName"
                        ErrorMessage="Merci d'entrer votre prénom." ValidationGroup="reg" CssClass="RequiredFieldValidatorStyle" >*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label8" runat="server" Text="Nom : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxLastName" runat="server" CssClass="TextBoxRegisterStyle"></asp:TextBox>
                </td>
                <td class="TdRequiredFieldValidatorStyle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TextBoxLastName"
                        ErrorMessage="Merci d'entrer votre nom." ValidationGroup="reg" CssClass="RequiredFieldValidatorStyle">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label9" runat="server" Text="Société : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxSociete" runat="server" CssClass="TextBoxRegisterStyle"></asp:TextBox>
                </td>
                <td class="TdRequiredFieldValidatorStyle">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TextBoxSociete"
                        ErrorMessage="Merci d'entrer votre société." ValidationGroup="reg" CssClass="RequiredFieldValidatorStyle">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right" valign="top">
                    <asp:Label ID="Label10" runat="server" Text="Adresse : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxAdresse" runat="server" CssClass="TextBoxRegisterStyle" Height="29px"></asp:TextBox>
                </td>
                <td class="TdRequiredFieldValidatorStyle">
                </td>
            </tr>
            <tr>
                <td class="TdLabelStyle" align="right">
                    <asp:Label ID="Label11" runat="server" Text="Téléphone : " CssClass="LabelStyle" />
                </td>
                <td class="TdRegisterTextBoxStyle">
                    <asp:TextBox ID="TextBoxTelephone" runat="server" CssClass="TextBoxRegisterStyle"></asp:TextBox>
                </td>
                <td class="TdRequiredFieldValidatorStyle">
                </td>
            </tr>
        </table>

        <table border="0" cellpadding="15" cellspacing="0">
            <tr>
                <td class="TdValidationSummaryStyle">
                    <asp:ValidationSummary ID="ValidationSummary1" CssClass="RequiredFieldValidatorStyle" runat="server" ValidationGroup="reg"/>
                </td>
            </tr>
        </table>
        
        <table border="0" width="100%" height="60">
        <tr>
            <td align="center">
                <asp:Button ID="ButtonEnregistrer" runat="server" Text="Enregistrer" ValidationGroup="reg" CssClass="ButtonStyle" OnClick="ButtonEnregistrer_Click" />
                <asp:Button ID="ButtonRetour" runat="server" Text="Retour" ToolTip="Retourner à la liste des Membres" CssClass="ButtonStyle" OnClick="ButtonRetour_Click" />
            </td>
        </tr>
        </table>
        
        <table style="border:none" cellpadding="0" cellspacing="0">
            <tr>
                <td height="30">
                    <asp:Label ID="ValidationMessage" CssClass="LabelValidationMessageStyle" Runat="server" Visible="false"/>
                </td>
            </tr>
        </table>    
        
        </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="200">
        <ProgressTemplate>
            <asp:Image ID="loadingimg" runat="server" ImageUrl="~/Images/ajax-loader.gif"/>
        </ProgressTemplate>
        </asp:UpdateProgress>
        
        <br />
        <br />
    </div>
    <div class="clear2column">
    </div>
</div>
</asp:Content>

