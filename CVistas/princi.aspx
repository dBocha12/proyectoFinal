<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="princi.aspx.cs" Inherits="proyectoFinal.CVistas.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pagina principal</title>
    <meta charset="UTF-8" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=Kanit:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;0,800;0,900;1,100;1,200;1,300;1,400;1,500;1,600;1,700;1,800;1,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../lib/css/principal1.css" />
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <nav>
                <ul>
                    <li id="liCatalogos" runat="server"><a href="#">Administración de catálogos</a></li>
                    <li id="reporte"><a href="#">Reporte</a></li>
                    <li id="facturacion"><a href="#">Facturación</a></li>
                    <li id="liAdministracionUsuarios" runat="server">
                        <asp:Button ID="btnAdministracionUsuarios" runat="server" Text="Administración de usuarios" OnClick="btnAdministracionUsuarios_Click" CssClass="nav-button" />
                    </li>
                </ul>
            </nav>
        </header>

        <div class="container">
            <asp:Label ID="lblReloj" runat="server"></asp:Label>
            <asp:Label ID="bienvenida" runat="server" Text="Botón 4">Bienvenido, </asp:Label>
            <asp:Label ID="lblUsuario" runat="server" Text="Botón 4"></asp:Label>
            <asp:Image ID="ImagePerfil" runat="server" />
        </div>


    </form>
</body>
</html>
