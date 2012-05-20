<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Subscribe.aspx.cs" Inherits="HumanResources.EventsCalendar.Subscription.Layouts.HumanResources.EventsCalendar.Subscribe" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
<SharePoint:CssRegistration ID="CssRegistration1" Name="forms.css" runat="server"/>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <table class="ms-formtable" style="margin-top: 8px;"  border="0" cellspacing="0" width="100%">
        <%-- Email --%>
        <tr>
            <td width="190px" valign="top" class="ms-formlabel">
                <h3 class="ms-standardheader">
                    <nobr>Email<span class="ms-formvalidation"> *</span></nobr>
                </h3>
            </td>
            <td width="400px" valign="top" class="ms-formbody">
                <asp:TextBox ID="email" runat="server"></asp:TextBox>
<%--                <asp:RequiredFieldValidator runat="server" 
                    ControlToValidate="email" 
                    ErrorMessage="Email is required" 
                    EnableClientScript="True" 
                    ValidationGroup="subscribe">                
                </asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator runat="server" 
                    ControlToValidate="email" 
                    EnableClientScript="True"
                    ValidationGroup="subscribe"
                    ValidationExpression="^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$" 
                    ErrorMessage="Invalid email adddress">                    
                </asp:RegularExpressionValidator>--%>
            </td>
        </tr>
        <%-- Target --%>
        <tr>
            <td width="190px" valign="top" class="ms-formlabel">
                <h3 class="ms-standardheader">
                    <nobr>Target</nobr>
                </h3>
            </td>           
            <td width="400px" valign="top" class="ms-formbody">
                <asp:CheckBoxList ID="target" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <%-- Target --%>
        <tr>
            <td width="190px" valign="top" class="ms-formlabel">
                <h3 class="ms-standardheader">
                    <nobr>Notify of new events</nobr>
                </h3>
            </td>           
            <td width="400px" valign="top" class="ms-formbody">
                <asp:CheckBox ID="notifyOfNewEvents" runat="server" />
            </td>
        </tr>
    </table>
    
    <table cellpadding="0" cellspacing="0" width="100%">
        <tbody>
            <tr>
                <td class="ms-formline">
                    <img src="/_layouts/images/blank.gif" width="1" height="1" alt="">
                </td>
            </tr>
        </tbody>
    </table>    

    <table cellpadding="0" cellspacing="0" width="100%" style="padding-top: 7px">
        <tbody>
            <tr>
                <td width="100%">
		            <table class="ms-formtoolbar" cellpadding="2" cellspacing="0" border="0" width="100%">
                        <tbody>
                              <tr>
	                            <td width="99%" class="ms-toolbar" nowrap="nowrap"><img src="/_layouts/images/blank.gif" width="1" height="18" alt=""></td>
		                        <td class="ms-toolbar" nowrap="nowrap">
	
		                            <table cellpadding="0" cellspacing="0" width="100%">
		                                <tbody>
		                                    <tr>
		                                        <td align="right" width="100%" nowrap="nowrap">
                                                    <asp:Button ID="subscribeButton" runat="server" Text="Subscribe" OnClick="Subscribe_Click" ValidationGroup="subscribe"  CssClass="ms-ButtonHeightWidth" />
                                                    <asp:Button ID="saveButton" runat="server" Text="Save" OnClick="Subscribe_Click" Visible="False" ValidationGroup="subscribe" CssClass="ms-ButtonHeightWidth" />
		                                        </td> 
		                                    </tr> 
		                                </tbody>
		                            </table>
	
		                        </td>
	                            <td class="ms-separator">&nbsp;</td>
                                
		                        <td class="ms-toolbar" nowrap="nowrap">
	
		                            <table cellpadding="0" cellspacing="0" width="100%">
		                                <tbody>
		                                    <tr>
		                                        <td align="right" width="100%" nowrap="nowrap">
                                                    <asp:Button ID="unsubscribeButton" runat="server" Text="Unsubscribe" OnClick="Unsubscribe_Click" Visible="False" CssClass="ms-ButtonHeightWidth" />
		                                        </td> 
		                                    </tr> 
		                                </tbody>
		                            </table>
	
		                        </td>

	                            <td class="ms-separator">&nbsp;</td>

		                        <td class="ms-toolbar" nowrap="nowrap">
	
		                            <table cellpadding="0" cellspacing="0" width="100%">
		                                <tbody>
		                                    <tr>
		                                        <td align="right" width="100%" nowrap="nowrap">
		                                            <asp:Button ID="Button1" runat="server" Text="Close" OnClientClick="SP.UI.ModalDialog.commonModalDialogClose(SP.UI.DialogResult.cancel, 'Cancelled'); return false;" CssClass="ms-ButtonHeightWidth"/>
			                                    </td> 
		                                    </tr> 
		                                </tbody>
		                            </table>
	
		                        </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Subscribe to events
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
Subscribe to events
</asp:Content>
