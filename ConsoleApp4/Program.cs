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

            while(continuar == true)
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

                

                Console.WriteLine("Desea continuar? (Y/n) ");
                var aswer = Console.ReadLine();

                if(aswer == "Y")
                {
                    continuar = true;
                }
                else if(aswer == "n")
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


