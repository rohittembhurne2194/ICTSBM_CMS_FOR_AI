<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayReports.aspx.cs" Inherits="SwachhBharatAbhiyan.CMS.DisplayReports" %>
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
   <form id="form1" runat="server">  
     
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
          
    <div style="vertical-align:top">  
         
    <%--    <asp:ImageButton ID="back" ImageUrl="~/Images/Reports/Reportback.jpg" width="15px" height="9px"  runat="server"  OnClick="back_Click"
                                 ToolTip="Back" />--%>
        <rsweb:reportviewer id="rptViewer" runat="server" height="9000px" SizeToReportContent="True"   AsyncRendering ="false"     width="5000px" ShowParameterPrompts="False"  OnDrillthrough="repViewer_Drillthrough"> 
        </rsweb:reportviewer>  


    </div>  
     
</form> 
</body>
</html>
