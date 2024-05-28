<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PutetAPITest.aspx.cs" Inherits="WebApp.PutetAPITest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="butPINRequest" runat="server" Text="PINReq" OnClick="butPINRequest_Click"  />
                </td>
            </tr>
            <tr>
                <td>
                    PIN Type :
                </td>
                <td>
                    <asp:TextBox ID="txtPINType" runat="server" Width="270px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Amount :
                </td>
                <td>
                    <asp:TextBox ID="txtAmount" runat="server" Width="269px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;Request PUTET:
                </td>
                <td>
                    Response PUTET:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtRequest" runat="server" TextMode="MultiLine" Width="500px" Rows="10" Wrap="false" spellcheck="false" Font-Size="Large"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtResponse" runat="server" TextMode="MultiLine" Width="500px" Rows="10" Wrap="false" spellcheck="false" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtCompress" runat="server" TextMode="MultiLine" Width="500px" Rows="3" Wrap="false" spellcheck="false" Font-Size="Large"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TextBox ID="txtURL" runat="server" Width="490px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>

                    <asp:Button ID="butSubmit" runat="server" text="Submit" OnClick="butSubmit_Click"/>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
