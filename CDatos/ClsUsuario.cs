using proyectoFinal.CLogica;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace proyectoFinal.CDatos
{

    public class ClsUsuario
    {
        public static int Ingresar(string username, string password)
        {
            int resultado = 0;

            string hash = ConsultarHash(username);
            string sal = ConsultarSal(username);

            bool contraEncriptada = ClsEncriptador.VerificarContrasena(password, hash, sal);

            if (contraEncriptada == true)
            {
                resultado = 1;
            }
            else { 
                resultado = 0;
            }
            return resultado;
        }

        public static int verificarUsuario(string username)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "EXEC verificarUsuario @Usuario";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", username);

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

        public static int verificarCorreo(string correo)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "SELECT COUNT(*) FROM Usuario WHERE Correo = @Correo";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Correo", correo);

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

        public static int CambioDeContraRequired(string username)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "Exec VerificarCambioRequerido @Usuario";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", username);

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

        public static int CrearUsuario(int idEmpleado, string usuario, string contraseña, string Correo, int CambioDeContrasenaRequerido, string Sal, byte[] imagenPerfil)
        {
            int idNuevoUsuario = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "EXEC CrearUsuarioNuevo @IdEmpleado, @Usuario, @Contraseña, @Correo, @CambioDeContrasenaRequerido, @Sal, @ImagenPerfil";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                    comando.Parameters.AddWithValue("@Usuario", usuario);
                    comando.Parameters.AddWithValue("@Contraseña", contraseña);
                    comando.Parameters.AddWithValue("@Correo", Correo);
                    comando.Parameters.AddWithValue("@CambioDeContrasenaRequerido", CambioDeContrasenaRequerido);
                    comando.Parameters.AddWithValue("@Sal", Sal);
                    comando.Parameters.AddWithValue("@ImagenPerfil", imagenPerfil); 

                    try
                    {
                        conexion.Open();
                        using (SqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idNuevoUsuario = reader.GetInt32(0);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }

            return idNuevoUsuario;
        }


        public static int ConsultarIDUsandoUser(string username)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "SELECT idUsuario FROM Usuario WHERE Usuario = @Usuario;";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", username);

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

        public static int CambiarContrasena(string password, string Sal, string username)
        {
            int resultado = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "Exec CambiarContrasena @Usuario, @Contrasena, @Sal";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", username);
                    comando.Parameters.AddWithValue("@Contrasena", password);
                    comando.Parameters.AddWithValue("@Sal", Sal);

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

        public static string ConsultarHash(string username)
        {
            string resultado = "";
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "SELECT Contraseña FROM Usuario WHERE Usuario = @Usuario";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", username);

                    try
                    {
                        conexion.Open();
                        resultado = (string)comando.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }

                }
            }

            return resultado;
        }

        public static string ConsultarSal(string username)
        {
            string resultado = "";
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "SELECT Sal FROM Usuario WHERE Usuario = @Usuario";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", username);

                    try
                    {
                        conexion.Open();
                        resultado = (string)comando.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }

                }
            }

            return resultado;
        }

        public static byte[] ConsultarImagen(string username)
        {
            byte[] resultado = null;
            string connectionString = ConfigurationManager.ConnectionStrings["proyectoConexion"].ConnectionString;
            string consulta = "SELECT ImagenPerfil FROM Usuario WHERE Usuario = @Usuario";

            using (SqlConnection conexion = new SqlConnection(connectionString))
            {
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Usuario", username);

                    try
                    {
                        conexion.Open();
                        resultado = (byte[])comando.ExecuteScalar();
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

