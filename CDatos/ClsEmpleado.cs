using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace proyectoFinal.CDatos
{
    public class ClsEmpleado
    {
        public static int CrearEmpleado(int idCargo, string dni, string apellidos, string nombre, string sexo, string direccion, string estadoCivil)
        {
            int nuevoIdEmpleado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "EXEC CrearEmpleadoNuevo @IdCargo, @DNI, @Apellidos, @Nombre, @Sexo, @Direccion, @EstadoCivil";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdCargo", 2);
                    comando.Parameters.AddWithValue("@DNI", dni);
                    comando.Parameters.AddWithValue("@Apellidos", apellidos);
                    comando.Parameters.AddWithValue("@Nombre", nombre);
                    comando.Parameters.AddWithValue("@Sexo", sexo);
                    comando.Parameters.AddWithValue("@Direccion", direccion);
                    comando.Parameters.AddWithValue("@EstadoCivil", estadoCivil);

                    try
                    {
                        conexion.Open();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                nuevoIdEmpleado = reader.GetInt32(0);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }

            return nuevoIdEmpleado;
        }

        public static int ConsultarIDEmpleado(int idUsuario)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "SELECT IdEmpleado FROM Usuario WHERE IdUsuario = @Usuario;";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", idUsuario);

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

        public static string ObtenerNombreCompletoEmpleado(int idEmpleado)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "EXEC ObtenerNombreCompletoEmpleado @IdEmpleado";
            string nombreCompleto = "";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                    try
                    {
                        conexion.Open();
                        nombreCompleto = (string)comando.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }

            return nombreCompleto;
        }

        public static int VerificarCargo(int IdEmpleado)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "SELECT IdCargo FROM Empleado WHERE IdEmpleado = @IdEmpleado";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdEmpleado", IdEmpleado);

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

        public static int verificarDNI(string dni)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "SELECT COUNT(*) FROM Empleado WHERE DNI = @DNI";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@DNI", dni);

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
