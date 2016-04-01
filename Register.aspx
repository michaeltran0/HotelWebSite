<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Content Page" Inherits="Register" CodeFile="~/Register.aspx.cs"%>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">
        <div id="content">
            
            <div>
                <table>
                    <tr>
                        <td><asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label></td>
                        <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*Email Field Required." ForeColor="Red" ControlToValidate="txtEmail"></asp:RequiredFieldValidator></td>
                        <%--<td><asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="*Email is not in correct format." ForeColor="Red" ControlToValidate="txtEmail"></asp:CompareValidator></td>--%>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblFirst" runat="server" Text="First Name: "></asp:Label></td>
                        <td><asp:TextBox ID="txtFirst" runat="server"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*First Name Field Required." ForeColor="Red" ControlToValidate="txtFirst"></asp:RequiredFieldValidator></td>
                        <%--<td><asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="*First Name is invalid." ForeColor="Red" ControlToValidate="txtFirst"></asp:CompareValidator></td>--%>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblLast" runat="server" Text="Last Name: "></asp:Label></td>
                        <td><asp:TextBox ID="txtLast" runat="server"></asp:TextBox></td> 
                        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Last Name Field Required." ForeColor="Red" ControlToValidate="txtLast"></asp:RequiredFieldValidator></td>
                        <%--<td><asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="*Last Name is invalid." ForeColor="Red" ControlToValidate="txtLast"></asp:CompareValidator></td>--%>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label></td>
                        <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Password Field Required." ForeColor="Red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
                        <%--<td><asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="*Password does not fit requirements." ForeColor="Red" ControlToValidate="txtPassword"></asp:CompareValidator></td>--%>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblPassword2" runat="server" Text="Re-type Password: "></asp:Label></td>
                        <td><asp:TextBox ID="txtPassword2" runat="server" TextMode="Password"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*Re-type Password Field Required." ForeColor="Red" ControlToValidate="txtPassword2"></asp:RequiredFieldValidator></td>
                        <td><asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="*Password does not match." ForeColor="Red" ControlToValidate="txtPassword2" ControlToCompare="txtPassword"></asp:CompareValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblPhone" runat="server" Text="Phone Number: "></asp:Label></td>
                        <td><asp:TextBox ID="txtPhone" runat="server" TextMode="Phone"></asp:TextBox></td>
                        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*Phone Number Field Required." ForeColor="Red" ControlToValidate="txtPhone"></asp:RequiredFieldValidator></td>
                        <%--<td><asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="*Phone number is not in correct format." ForeColor="Red" ControlToValidate="txtPhone"></asp:CompareValidator></td>--%>
                    </tr>
                    <tr>
                        <td><asp:Button ID="btnRegister" runat="server" Text="Register" /></td>
                        <td><asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" CausesValidation="False" /></td>
                    </tr>
                </table>
            </div>
            
        </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
</asp:Content>

