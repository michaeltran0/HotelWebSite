<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Content Page" Inherits="RoomBooking" CodeFile="~/RoomBooking.aspx.cs" %>

<%--<asp:Content ID="Head" ContentPlaceHolderId="head" runat="server">
    <link id="Link1" href="~/Styles/MasterPage.css" rel="stylesheet" runat="server"/>
</asp:Content>--%>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">

        <div id="content">
        <p>ROOM BOOKING PAGE</p>
            <%-- Gridviews to see database tables. WILL BE REMOVED --%>
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
            <br />
            <%-- end of test views --%>
            <asp:Label ID="lblSearch" runat="server" Visible="False"></asp:Label>
            <asp:GridView ID="GridView3" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <asp:Label ID="lblError" runat="server"></asp:Label>
            
        <%-- Make Reservation Form --%>
            <h1>Make reservation</h1>
            <table>
                <tr><td>
                        <asp:Label ID="lblCheckIn" runat="server" Text="Check In Date:"></asp:Label>
                    </td><td class="auto-style1">
                        <asp:TextBox ID="txtCheckIn" runat="server" TextMode="Date"></asp:TextBox>
                    </td><td>
                        <asp:CompareValidator ID="cvCheckIn" runat="server" ControlToValidate="txtCheckIn" ErrorMessage="Valid date not entered (mm/dd/yyyy)" Operator="DataTypeCheck" Type="Date" ForeColor="Red"></asp:CompareValidator>
                    </td><td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCheckIn" ErrorMessage="Field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td></tr>
                <tr><td>
                        <asp:Label ID="lblCheckOut" runat="server" Text="Check Out Date:"></asp:Label>
                    </td><td class="auto-style1">
                        <asp:TextBox ID="txtCheckOut" runat="server" TextMode="Date"></asp:TextBox>
                    </td><td>
                        <asp:CompareValidator ID="cvCheckOut" runat="server" ControlToValidate="txtCheckOut" ErrorMessage="Valid date not entered (mm/dd/yyyy)" Operator="DataTypeCheck" Type="Date" ForeColor="Red"></asp:CompareValidator>
                    </td><td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCheckOut" ErrorMessage="Field is required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td></tr>
                <tr><td>
                        <asp:Label ID="lblRoom" runat="server" Text="Room No."></asp:Label>
                    </td><td class="auto-style1">
                        <asp:DropDownList ID="ddlRooms" runat="server"></asp:DropDownList>
                </td></tr>
                <tr><td>
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="btnSubmit_Click" />&nbsp;
                </td><td>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="button" OnClick="btnSearch_Click" />&nbsp;
                </td></tr>
            </table>
            <asp:Label ID="lblResult" runat="server"></asp:Label>

        </div><%-- End Make Reservation Form --%>
    
</asp:Content>



