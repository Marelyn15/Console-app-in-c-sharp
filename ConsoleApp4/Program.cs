using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection cn = new SqlConnection("Data Source=.;Initial Catalog=SYSTEM;Integrated Security=True");

            cn.Open();

            bool continuar = true;

            while (continuar == true)
            {
                SqlCommand cmd = cn.CreateCommand();

                cmd.CommandText = "SELECT * FROM USERS";
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                    }

                }

                reader.Close();

                //Funciones de CRUD: 
                Console.WriteLine("Por favor Indicar que desea hacer...");
                Console.WriteLine("C. Crear nuevo usuario R. Buscar usuario U. Actualizar usuario D. Borrar usuario");
                var respuesta = Console.ReadLine();

                if (respuesta == "C" || respuesta == "c")
                {
                    //Entrada de usuario
                    Console.WriteLine("Ingresa tu nombre");
                    var nombre = Console.ReadLine();

                    Console.WriteLine("Ingresa tu edad");
                    var edad = Console.ReadLine();

                    if (nombre != null && edad != null)
                    {
                        var newEdad = int.Parse(edad);

                        //modo medio automatico

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "INSERT USER";
                        cmd.Parameters.AddWithValue("@NAME_USER", nombre);
                        cmd.Parameters.AddWithValue("@AGE", newEdad);
                        Console.WriteLine("Datos ingresados");

                        //Muy importante 
                        cmd.ExecuteNonQuery();
                    }

        
                }

                else if (respuesta == "R" || respuesta == "r")
                {
                    Console.WriteLine("Busqueda de usuarios");
                    Console.WriteLine("Ingresa ID");
                    var idUser = Console.ReadLine();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "VER";
                    cmd.Parameters.AddWithValue("@ID", idUser);

                    cmd.ExecuteNonQuery();
                }
                else if (respuesta == "U" || respuesta == "U")
                {
                    Console.WriteLine("Actualizar usuario");
                    Console.WriteLine("Por favor, ingresa tu ID");
                    var Id = Console.ReadLine();

                    Console.WriteLine("Ahora ingresa tus nuevos datos");
                    //Entrada de usuario
                    Console.WriteLine("Ingresa tu nombre");
                    var nombreUpdate = Console.ReadLine();

                    Console.WriteLine("Ingresa tu edad");
                    var edadUpdate = Console.ReadLine();

                    if (nombreUpdate != null && edadUpdate != null)
                    {
                        var newEdad = int.Parse(edadUpdate);

                        //modo medio automatico

                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "EDITAR";
                        cmd.Parameters.AddWithValue("@NAME_USER", nombreUpdate);
                        cmd.Parameters.AddWithValue("@AGE", newEdad);
                        cmd.Parameters.AddWithValue("@ID", Id);
                        Console.WriteLine("Datos actualizados");

                        //Muy importante 
                        cmd.ExecuteNonQuery();

                    }
                }
                else if(respuesta == "D" || respuesta == "d")
                {
                    Console.WriteLine("Eliminar usuario");
                    Console.WriteLine("Por favor, ingresa tu ID");
                    var Id = Console.ReadLine();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "ELIMINAR";
                    cmd.Parameters.AddWithValue("@ID", Id);

                    cmd.ExecuteNonQuery();

                }
                else
                {
                    Console.WriteLine("Mmm parece su respuesta no esta dentro de las opciones");
                }






                Console.WriteLine("Desea continuar? (Y/n) ");
                var aswer = Console.ReadLine();

                if (aswer == "Y")
                {
                    continuar = true;
                }
                else if (aswer == "n")
                {
                    continuar = false;
                }
                else
                {
                    Console.WriteLine("Letra equivocada");
                    continuar = false;
                }

            }

            cn.Close();

        }
    }
}


