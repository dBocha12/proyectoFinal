using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace proyectoFinal.CDatos
{
    public class ClsAdmin
    {

        public static Dictionary<string, object> RetornarInformacion(string DNI)
        {
            Dictionary<string, object> informacionUsuario = new Dictionary<string, object>();

            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "Exec ConsultarTodaInformacion @DNI";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@DNI", DNI);

                    try
                    {
                        conexion.Open();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                informacionUsuario["IdEmpleado"] = reader.GetInt32(reader.GetOrdinal("IdEmpleado"));
                                informacionUsuario["IdUsuario"] = reader.GetInt32(reader.GetOrdinal("IdUsuario"));
                                informacionUsuario["Nombre"] = reader.GetString(reader.GetOrdinal("Nombre"));
                                informacionUsuario["Apellidos"] = reader.GetString(reader.GetOrdinal("Apellidos"));
                                informacionUsuario["DNI"] = reader.GetString(reader.GetOrdinal("DNI"));
                                informacionUsuario["Sexo"] = reader.GetString(reader.GetOrdinal("Sexo"));
                                informacionUsuario["Direccion"] = reader.GetString(reader.GetOrdinal("Direccion"));
                                informacionUsuario["EstadoCivil"] = reader.GetString(reader.GetOrdinal("EstadoCivil"));
                                informacionUsuario["Usuario"] = reader.GetString(reader.GetOrdinal("Usuario"));
                                informacionUsuario["Contraseña"] = reader.GetString(reader.GetOrdinal("Contraseña"));
                                informacionUsuario["Correo"] = reader.GetString(reader.GetOrdinal("Correo"));
                                informacionUsuario["IdCargo"] = reader.GetInt32(reader.GetOrdinal("IdCargo"));
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }

            return informacionUsuario;
        }

        public static int VerificarCargo(string nuevoNombre, string nuevoApellidos, string nuevoDNI, string nuevoSexo, string nuevoDireccion, string nuevoEstadoCivil, string nuevoUsuario, string nuevoContrasena, string nuevoCorreo, string nuevoPuesto, string DNIExistente)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "Exec GuardarTodaInformacion @NuevoCorreo, @NuevoNombre, @NuevoApellidos, @NuevoDNI, @NuevoSexo, @NuevoDireccion, @NuevoEstadoCivil, @NuevoUsuario, @NuevoContrasena, @NuevoPuesto, @DNIExistente";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@NuevoCorreo", nuevoCorreo);
                    comando.Parameters.AddWithValue("@NuevoNombre", nuevoNombre);
                    comando.Parameters.AddWithValue("@NuevoApellidos", nuevoApellidos);
                    comando.Parameters.AddWithValue("@NuevoDNI", nuevoDNI);
                    comando.Parameters.AddWithValue("@NuevoSexo", nuevoSexo);
                    comando.Parameters.AddWithValue("@NuevoDireccion", nuevoDireccion);
                    comando.Parameters.AddWithValue("@NuevoEstadoCivil", nuevoEstadoCivil);
                    comando.Parameters.AddWithValue("@NuevoUsuario", nuevoUsuario);
                    comando.Parameters.AddWithValue("@NuevoContrasena", nuevoContrasena);
                    comando.Parameters.AddWithValue("@NuevoPuesto", nuevoPuesto);
                    comando.Parameters.AddWithValue("@DNIExistente", DNIExistente);

                    try
                    {
                        conexion.Open();
                        resultado = (int)comando.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }

                }
            }

            return resultado;
        }


    }
}