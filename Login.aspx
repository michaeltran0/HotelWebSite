<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" Title="Content Page" Inherits="Login" CodeFile="~/Login.aspx.cs"%>

<asp:Content ID="Content1" ContentPlaceHolderId="ContentPlaceHolder1" runat="server">
        <div id="content">
            <asp:Login ID="Login1" runat="server" BackColor="#F7F7DE" BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" OnAuthenticate="Login1_Authenticate" DisplayRememberMe="False" UserNameLabelText="Username:">
                <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
            </asp:Login>
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
</asp:Content>
