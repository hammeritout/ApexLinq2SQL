<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebInvoice.aspx.cs" Inherits="WebLINQtoSQL.WebInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <asp:Label ID="lblStartDate" runat="server" Text="Start Date: "></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
          <asp:ImageButton ID="imgButton" runat="server" ImageUrl="~/Images/b_calendar.png" OnClick="imgButton_Click" />
        <asp:Calendar ID="calStart" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px" OnSelectionChanged="calStart_SelectionChanged">
            <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
            <OtherMonthDayStyle ForeColor="#CC9966" />
            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
            <SelectorStyle BackColor="#FFCC66" />
            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
        </asp:Calendar>
         <br>
        </div>
        <div>
           <asp:Label ID="lblEndDate" runat="server" Text="End Date:  "></asp:Label>
        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
        
        <asp:ImageButton ID="imgeButton" runat="server" ImageUrl="~/Images/b_calendar.png" OnClick="imgeButton_Click" />
        
        <br>
        <asp:Calendar ID="calEnd" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px" OnSelectionChanged="calEnd_SelectionChanged">
            <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
            <OtherMonthDayStyle ForeColor="#CC9966" />
            <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
            <SelectorStyle BackColor="#FFCC66" />
            <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
            <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
        </asp:Calendar>
        <div>
           <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
           <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />

        </div>
        </div>
        <div>
        <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    </form>
    
</body>
</html>
