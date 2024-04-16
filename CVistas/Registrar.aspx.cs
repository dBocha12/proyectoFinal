using proyectoFinal.CDatos;
using proyectoFinal.CLogica;
using System;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyectoFinal
{
    public partial class Registrar : Page
    {
        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            ClsCorreos correos = new ClsCorreos();
            if (string.IsNullOrWhiteSpace(txtNuevoUsuario.Text) ||
                string.IsNullOrWhiteSpace(txtDNI.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) ||
                string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CamposIncompletos", $"toastr.error('Por favor, complete todos los campos.');", true);
                return;
            }

            string nuevoUsuario = txtNuevoUsuario.Text;
            string dni = txtDNI.Text;
            string apellidos = txtApellidos.Text;
            string nombre = txtNombre.Text;
            string sexo = listSexo.Text;
            string direccion = txtDireccion.Text;
            string estadoCivil = listEstadoCivil.Text;
            string correo = txtCorreo.Text;

            byte[] imagenPerfil = null;
            if (FileUpload.HasFile && FileUpload.PostedFile.ContentLength > 0)
            {
                HttpPostedFile postedFile = FileUpload.PostedFile;
                imagenPerfil = new byte[postedFile.ContentLength];
                postedFile.InputStream.Read(imagenPerfil, 0, postedFile.ContentLength);
            }

            int password = new Random().Next(100000, 999999);

            string salt;
            string nuevaContraseñaEncriptada = ClsEncriptador.CrearContrasena(password.ToString(), out salt);

            int resultado = ClsUsuario.verificarUsuario(nuevoUsuario);
            if (resultado == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "UsuarioExistente", $"toastr.error('El usuario {nuevoUsuario} ya existe, intente con otro usuario');", true);
            }
            else
            {
                int nuevoIdEmpleado = ClsEmpleado.CrearEmpleado(2, dni, apellidos, nombre, sexo, direccion, estadoCivil);
                if (nuevoIdEmpleado > 0)
                {
                    int resultadoCrearUsuario = ClsUsuario.CrearUsuario(nuevoIdEmpleado, nuevoUsuario, nuevaContraseñaEncriptada, correo, 1, salt, imagenPerfil);
                    if (resultadoCrearUsuario > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "UsuarioIncorrecto", $"toastr.error('Usuario o contraseña incorrectos, intente de nuevo');", true);
                        correos.EnviarCorreo("PROYECTO4 >> Codigo de acceso", $"Gracias por registrarse al programa, su clave temporal es: {password}, usela para iniciar sesion", correo);
                        Thread.Sleep(3000);
                        Response.Redirect($"../login.aspx?Code={resultadoCrearUsuario}");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "UsuarioNoCreado", $"toastr.error('El usuario {nuevoUsuario} no pudo ser creado, img {imagenPerfil}');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "EmpleadoNoCreado", $"toastr.error('El empleado no pudo ser creado');", true);
                }
            }
        }
    }
}
