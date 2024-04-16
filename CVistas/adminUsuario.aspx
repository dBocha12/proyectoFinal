<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminUsuario.aspx.cs" Inherits="proyectoFinal.CVistas.adminUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../lib/css/adminUsuarios.css" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.css" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="column">
                <h2>Información Personal</h2>
                <div class="form-group">
                    <label for="nombre">Nombre:</label>
                    <asp:TextBox ID="nombre" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="apellidos">Apellidos:</label>
                    <asp:TextBox ID="apellidos" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="dni">DNI:</label>
                    <asp:TextBox ID="dni" runat="server" OnTextChanged="Dni_TextChanged" AutoPostBack="true" maxlength="9"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="sexo">Sexo:</label>
                    <asp:DropDownList ID="sexo" runat="server">
                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                        <asp:ListItem Value="F">Femenino</asp:ListItem>
                        <asp:ListItem Value="O">No binario</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label for="direccion">Dirección:</label>
                    <asp:TextBox ID="direccion" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="estadoCivil">Estado Civil:</label>
                    <asp:DropDownList ID="estadoCivil" runat="server">
                        <asp:ListItem Value="soltero">Soltero</asp:ListItem>
                        <asp:ListItem Value="casado">Casado</asp:ListItem>
                        <asp:ListItem Value="divorciado">Divorciado</asp:ListItem>
                        <asp:ListItem Value="complicado">Complicado</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="column">
                <h2>Información de Usuario</h2>
                <div class="form-group">
                    <label for="usuario">Usuario:</label>
                    <asp:TextBox ID="usuario" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="contrasena">Contraseña:</label>
                    <asp:TextBox ID="contrasena" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="correo">Correo:</label>
                    <asp:TextBox ID="correo" TextMode="Email" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="puesto">Puesto:</label>
                    <asp:DropDownList ID="puesto" runat="server">
                        <asp:ListItem Value="2">Empleado</asp:ListItem>
                        <asp:ListItem Value="1">Administrador</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:Button ID="btnGuardarCambios" runat="server" CssClass="btn-submit" Text="Guardar cambios" OnClick="btnGuardarCambios_Click" />
            </div>
        </div>
    </form>
</body>
</html>
