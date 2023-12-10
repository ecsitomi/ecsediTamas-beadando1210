using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls;

namespace ecsediTamas_beadando1210
{
    internal class Program
    {
        static List<Tag> lista = new List<Tag>();
        static void Main(string[] args)
        {
            MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
            sb.Clear();
            sb.Server = "localhost";
            sb.UserID = "root";
            sb.Password = "";
            sb.Database = "tagdij";
            sb.CharacterSet = "utf8";
            MySqlConnection connection = new MySqlConnection(sb.ConnectionString);
            try 
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM `ugyfel`";
                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Tag tag = new Tag(dr.GetInt32("azon"),dr.GetString("nev"),dr.GetInt32("szulev"),dr.GetInt32("irszam"),dr.GetString("orsz"));
                        //Console.WriteLine(tag);
                        HozzaAd(tag);

                    }
                }
                connection.Close();
            }
            catch (MySqlException ex) 
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }

            Megmutat();
            Tag ujtag = new Tag(999, "Ecsedi Tamás", 1990, 3300, "Magyarország");

            HozzaAd(ujtag);
            Megmutat();

            Torol(ujtag);
            Megmutat();

        }
        static void HozzaAd(Tag tag)
        { 
            lista.Add(tag);
        }
        static void Megmutat()
        {
            foreach (var tag in lista)
            {
                Console.WriteLine(tag.nev);
            }
        }
        static void Torol(Tag tag)
        { 
            lista.Remove(tag);
        }
    }
}
