using proyectoFinal.CDatos;
using System;
using System.Timers;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace proyectoFinal.CVistas
{
    public partial class WebForm2 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int loginStatus = ClsVerificarLogin.GetLoginStatus();

            string username = "a";
            int idEmpleado = 0;

            if (!IsPostBack)
            {
                if (Request.QueryString["Username"] != null)
                {
                    username = Request.QueryString["Username"];
                    int idUsuario = ClsUsuario.ConsultarIDUsandoUser(username);
                    idEmpleado = ClsEmpleado.ConsultarIDEmpleado(idUsuario);
                    string nombreCompleto = ClsEmpleado.ObtenerNombreCompletoEmpleado(idEmpleado);
                    lblUsuario.Text = nombreCompleto;

                    byte[] pic = ClsUsuario.ConsultarImagen(username);

                    if (pic != null && pic.Length > 0)
                    {
                        string base64String = Convert.ToBase64String(pic, 0, pic.Length);
                        ImagePerfil.ImageUrl = "data:image/jpeg;base64," + base64String;
                    }
                }
                else
                {
                    Response.Redirect("/login.aspx");
                }
            }

            int cargo = ClsEmpleado.VerificarCargo(idEmpleado);

            btnAdministracionUsuarios.Visible = (cargo == 1);
        }

        protected void btnAdministracionUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("/CVistas/adminUsuario.aspx");
        }


    }
}
