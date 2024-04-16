using proyectoFinal.CDatos;
using proyectoFinal.CLogica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyectoFinal.CVistas
{
    public partial class CambioContrasena : System.Web.UI.Page
    {
        ClsCorreos correos = new ClsCorreos();
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = "a";

            if (!IsPostBack)
            {
                if (Request.QueryString["username"] != null)
                {
                    code = Request.QueryString["username"];
                    txtUsuarioTraido.Text = code;
                    ScriptManager.RegisterStartupScript(this, GetType(), "UsuarioIncorrecto", $"toastr.info('Por favor, cambie su contrasena temporal aca!');", true);
                }
            }
        }

        protected void btnCambiar_Click(object sender, EventArgs e)
        {
            string username = txtUsuarioTraido.Text;
            string password = txtNuevaContrasena.Text;
            string passwordConfirmar = txtConfirmarNuevaContrasena.Text;

            if (password != passwordConfirmar)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "UsuarioIncorrecto", $"toastr.error('Las contrasenas no son iguales, intente de nuevo');", true);
            }
            else {

                string salt;
                string nuevaContraseñaEncriptada = ClsEncriptador.CrearContrasena(password.ToString(), out salt);

                int resultado = ClsUsuario.CambiarContrasena(nuevaContraseñaEncriptada, salt, username);
                if (resultado == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "UsuarioIncorrecto", $"toastr.info('Su cambio de contrasena fue un exito, iniciando sesion...');", true);

                    Response.Redirect($"/CVistas/princi.aspx?Username={username}");
                }
            }
        }

    }
}