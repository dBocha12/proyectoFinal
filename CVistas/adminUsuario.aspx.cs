using proyectoFinal.CDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace proyectoFinal.CVistas
{
    public partial class adminUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CambioPass", $"toastr.info('Busque al usuario mediante el DNI');", true);
                ModificarVisibilidad(false);
            }
        }

        protected void Dni_TextChanged(object sender, EventArgs e)
        {
            LimpiarCampos();
            ConsultarInformacion();
        }


        private void ConsultarInformacion()
        {
            string valorDni = dni.Text;
            if (String.IsNullOrEmpty(valorDni))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CambioPass", $"toastr.warning('Ingrese un valor válido como DNI');", true);
            }
            else
            {
                Dictionary<string, object> informacionUsuario = ClsAdmin.RetornarInformacion(valorDni);
                ScriptManager.RegisterStartupScript(this, GetType(), "CambioPass", $"toastr.warning('Consultando con DNI {valorDni}');", true);

                if (informacionUsuario != null && informacionUsuario.Count > 0)
                {
                    ModificarVisibilidad(true);

                    int sqlIdEmpleado = Convert.ToInt32(informacionUsuario["IdEmpleado"]);
                    int sqlIdUsuario = Convert.ToInt32(informacionUsuario["IdUsuario"]);
                    string sqlNombre = informacionUsuario["Nombre"].ToString();
                    string sqlApellidos = informacionUsuario["Apellidos"].ToString();
                    string sqlDni = informacionUsuario["DNI"].ToString();
                    string sqlSexo = informacionUsuario["Sexo"].ToString();
                    string sqlDireccion = informacionUsuario["Direccion"].ToString();
                    string sqlEstadoCivil = informacionUsuario["EstadoCivil"].ToString();
                    string sqlUsuario = informacionUsuario["Usuario"].ToString();
                    string sqlContrasena = informacionUsuario["Contraseña"].ToString();
                    string sqlCorreo = informacionUsuario["Correo"].ToString();
                    int sqlIdCargo = Convert.ToInt32(informacionUsuario["IdCargo"]);

                    nombre.Text = sqlNombre;
                    apellidos.Text = sqlApellidos;
                    direccion.Text = sqlDireccion;
                    usuario.Text = sqlUsuario;
                    contrasena.Text = sqlContrasena;
                    correo.Text = sqlCorreo;

                    sexo.ClearSelection();
                    ListItem sexoItem = sexo.Items.FindByValue(sqlSexo);
                    if (sexoItem != null)
                    {
                        sexoItem.Selected = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CambioPass", $"toastr.warning('No se encontró el sexo {sqlSexo}');", true);
                    }

                    estadoCivil.ClearSelection();
                    ListItem estadoCivilItem = estadoCivil.Items.FindByText(sqlEstadoCivil);
                    if (estadoCivilItem != null)
                    {
                        estadoCivilItem.Selected = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CambioPass", $"toastr.warning('No se encontró el estado civil {sqlEstadoCivil}');", true);
                    }

                    puesto.ClearSelection();
                    ListItem puestoItem = puesto.Items.FindByValue(sqlIdCargo.ToString());
                    if (puestoItem != null)
                    {
                        puestoItem.Selected = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CambioPass", $"toastr.warning('No se encontró el puesto con ID {sqlIdCargo}');", true);
                    }
                }
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            string nuevoNombre = nombre.Text.Trim();
            string nuevoApellidos = apellidos.Text.Trim();
            string nuevoDNI = dni.Text.Trim();
            string nuevoSexo = sexo.Text.Trim();
            string nuevoDireccion = direccion.Text.Trim();
            string nuevoEstadoCivil = estadoCivil.Text.Trim();
            string nuevoUsuario = usuario.Text.Trim();
            string nuevoContrasena = contrasena.Text.Trim();
            string nuevoCorreo = correo.Text.Trim();
            string nuevoPuesto = puesto.Text.Trim();
            string DNIExistente = dni.Text.Trim();

            if (string.IsNullOrWhiteSpace(nuevoNombre) ||
                string.IsNullOrWhiteSpace(nuevoApellidos) ||
                string.IsNullOrWhiteSpace(nuevoDNI) ||
                string.IsNullOrWhiteSpace(nuevoSexo) ||
                string.IsNullOrWhiteSpace(nuevoEstadoCivil) ||
                string.IsNullOrWhiteSpace(nuevoUsuario) ||
                string.IsNullOrWhiteSpace(nuevoContrasena) ||
                string.IsNullOrWhiteSpace(nuevoCorreo) ||
                string.IsNullOrWhiteSpace(nuevoPuesto) ||
                string.IsNullOrWhiteSpace(DNIExistente))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "CamposVacios", $"toastr.error('Por favor, complete todos los campos.');", true);
            }
            else
            {
                int usuarioExiste = ClsUsuario.verificarUsuario(nuevoUsuario);
                int correoExiste = ClsUsuario.verificarCorreo(nuevoCorreo);
                int dniExiste = ClsEmpleado.verificarDNI(nuevoDNI);

                if ((dniExiste > 0 && nuevoDNI != DNIExistente) || (correoExiste > 0 && nuevoCorreo != correo.Text.Trim()) || (usuarioExiste > 0 && nuevoUsuario != usuario.Text.Trim()))
                {
                    if (dniExiste > 0 && nuevoDNI != DNIExistente)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "DNIExistente", $"toastr.error('El DNI {nuevoDNI} ya está vinculado a otro usuario.');", true);
                    }
                    if (correoExiste > 0 && nuevoCorreo != correo.Text.Trim())
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CorreoExistente", $"toastr.error('El correo {nuevoCorreo} ya está vinculado a otro usuario.');", true);
                    }
                    if (usuarioExiste > 0 && nuevoUsuario != usuario.Text.Trim())
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "UsuarioExistente", $"toastr.error('El usuario {nuevoUsuario} ya está vinculado a otro usuario.');", true);
                    }
                }
                else
                {
                    int resultado = ClsAdmin.VerificarCargo(nuevoNombre, nuevoApellidos, nuevoDNI, nuevoSexo, nuevoDireccion, nuevoEstadoCivil, nuevoUsuario, nuevoContrasena, nuevoCorreo, nuevoPuesto, DNIExistente);
                    if (resultado == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CambioPass", $"toastr.success('Cambios realizados para el DNI {DNIExistente}');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "CambioPass", $"toastr.error('Los cambios no se pudieron realizar... ');", true);
                    }
                }
            }
        }






        public void ModificarVisibilidad(bool estado)
        {
            nombre.Enabled = estado;
            apellidos.Enabled = estado;
            sexo.Enabled = estado;
            direccion.Enabled = estado;
            estadoCivil.Enabled = estado;
            usuario.Enabled = estado;
            contrasena.Enabled = estado;
            correo.Enabled = estado;
            puesto.Enabled = estado;
            btnGuardarCambios.Visible = estado;
        }

        public void LimpiarCampos()
        {
            nombre.Text = "";
            apellidos.Text = "";
            sexo.ClearSelection();
            direccion.Text = "";
            estadoCivil.ClearSelection();
            usuario.Text = "";
            contrasena.Text = "";
            correo.Text = "";
            puesto.ClearSelection();
        }
    }
}
