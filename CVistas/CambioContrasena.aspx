<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambioContrasena.aspx.cs" Inherits="proyectoFinal.CVistas.CambioContrasena" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../lib/css/cambiocontrasena.css" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.css" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
    <title></title>
</head>
<body>

    <form id="login" runat="server">
        <div class="body"></div>
        <div class="grad"></div>
        <div class="header">
            <div>Cambiar <span>Contrasena</span></div>
        </div>
        <br />
        <div class="login">
            <asp:TextBox ID="txtUsuarioTraido" runat="server" ReadOnly="true"></asp:TextBox><br />
            <asp:TextBox ID="txtNuevaContrasena" runat="server" TextMode="Password" placeholder="Contrasena"></asp:TextBox><br />
            <asp:TextBox ID="txtConfirmarNuevaContrasena" runat="server" TextMode="Password" placeholder="Confirmar Contraseña"></asp:TextBox><br />
            <asp:Button ID="btnCambiar" runat="server" CssClass="btn-submit" Text="Cambiar Contrasena" OnClick="btnCambiar_Click" />
        </div>
    </form>

</body>
</html>
