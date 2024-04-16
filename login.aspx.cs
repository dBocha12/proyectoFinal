using proyectoFinal.CDatos;
using proyectoFinal.CLogica;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace proyectoFinal
{
    public partial class WebForm1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = "a";

            if (!IsPostBack)
            {
                if (Request.QueryString["Code"] != null)
                {
                    code = Request.QueryString["Code"];
                    ScriptManager.RegisterStartupScript(this, GetType(), "CambioPass", $"toastr.success('Inicie sesion con el codigo temporal enviado a su correo');", true);
                }
            }
        }
        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            string username = txtUsuario.Text;
            string password = txtContra.Text;

            int resultado = ClsUsuario.Ingresar(username, password);

            if (resultado == 1)
            {
                
                //ClsVerificarLogin.SetLoginStatus(1);
                int cambioDeContraRequired = ClsUsuario.CambioDeContraRequired(username);

                if (cambioDeContraRequired == 1)
                {
                    Response.Redirect($"/CVistas/CambioContrasena.aspx?Username={username}");
                }
                else {
                    Response.Redirect($"/CVistas/princi.aspx?Username={username}");
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "UsuarioIncorrecto", $"toastr.error('Usuario o contraseña incorrectos, intente de nuevo');", true);
            }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("CVistas/Registrar.aspx");
        }
    }
}
