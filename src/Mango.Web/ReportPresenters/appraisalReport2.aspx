<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="appraisalReport2.aspx.cs" Inherits="Mango.Web.ReportPresenters.appraisalReport2" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            
            width: 100%;
        }
        .style3
        {
            width: 100%;
            height: 3px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <center >
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                         </asp:ScriptManager>
            <asp:Label ID="lblMessage" runat="server" Font-Names="Verdana" Font-Size="9pt" 
                ForeColor="Red"></asp:Label>
             <table style="width:100%;">
                 <tr>
                     <td align="center" class="style2">
                         <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="Period" 
                             Font-Names="Arial" Font-Size="9pt" style="font-style: italic"></asp:Label>
                         <asp:DropDownList ID="ddlPeriod" runat="server" 
                            >
                         </asp:DropDownList>
                         <asp:Button ID="btnDisplay" runat="server" 
                             onclick="btnDisplayReport_Click" Text="Display" 
                             Font-Bold="True" Font-Names="Arial" Font-Size="9pt" />
                     </td>
                 </tr>
                 <tr>
                     <td align="center" class="style3">
                         </td>
                 </tr>
                 </table>

            <rsweb:ReportViewer ID="rv" runat="server" Height="" Width="">
            </rsweb:ReportViewer>
        </center>
    </center>
    </form>
</body>
</html>
