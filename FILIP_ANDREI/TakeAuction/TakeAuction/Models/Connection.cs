using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace TakeAuction.Models
{
    public class Connection
    {

        //metoda de  a prelua toti userii
        public List<SignIn> GetSqlRowsUsers()
        {
            List<SignIn> returnList = new List<SignIn>();
            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "select * from Users";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();
            foreach (DataRow dr in dt.Rows)
            {
                returnList.Add(new SignIn
                {
                    User = dr["User"].ToString(),
                    Password = dr["Password"].ToString()
                });
            }
            return returnList;
        }


        public List<Licitatii> GetLicitatii(string command) //preia licitatiile din PhpMyAdm -DB Local
            //folosim string command pt a nu face mai multe metode de get
        {
            List<Licitatii> returnList = new List<Licitatii>();
            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = command;
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();
            foreach (DataRow dr in dt.Rows)
            {
                returnList.Add(new Licitatii
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    NumeProdus = dr["NumeProdus"].ToString(),
                    Pret = Convert.ToDouble(dr["Pret"]),
                    User = dr["User"].ToString()

                });
            }
            return returnList;
        }

        public string InsertLicitatii(Licitatii licitatii, string username) //Din perspectiva de Useri & Admini in DB
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "INSERT INTO `licitatii`(`NumeProdus`, `Pret`, `User`) VALUES ('" + licitatii.NumeProdus + "', '" + licitatii.Pret + "', '" + username + "')";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt); //ne aduce tabela din PHP in C# (mda -SQL adapter)
            mysqlconn.Close();// inchide tabela pt a o putea refolosii in alte servicii
            return "Licitatia a fost adaugata";
        }
        public string InsertUser(Enroll user) //Din perspectiva de Useri & Admini in DB
        {
            Connection conn = new Connection();
            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "INSERT INTO `users`(`User`, `Password`) VALUES ('" + user.User + "', '" + user.Password + "')";
            for(int i = 0; i < conn.GetSqlRowsUsers().Count; i++)
            {
                if(conn.GetSqlRowsUsers()[i].User == user.User && conn.GetSqlRowsUsers()[i].Password == user.Password)
                {
                    return "This username is taken, please find a new one!";
                }
            }
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();
            return "Welcome to TakeAuction! Post yours anytime...";
        }
        public string EditLicitatii(Licitatii licitatii) //Din perspectiva de Useri & Admini in DB
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "UPDATE `licitatii` SET `NumeProdus`='" + licitatii.NumeProdus + "', `Pret`='" + licitatii.Pret + "' where `Id`='" + licitatii.Id + "'";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt); //ne aduce tabela din PHP in C# (mda -SQL adapter)
            mysqlconn.Close();// inchide tabela pt a o putea refolosii in alte servicii
            return "Licitatia a fost editata";
        }
        public string DeleteLicitatii(Licitatii licitatii) //Din perspectiva de Useri & Admini in DB
        {
            string mainconn = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "DELETE FROM `licitatii` WHERE Id='" + licitatii.Id + "'"; // se face delete doar dupa PK
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt); //ne aduce tabela din PHP in C# (mda -SQL adapter)
            mysqlconn.Close();// inchide tabela pt a o putea refolosii in alte servicii
            return "Licitatia a fost stearsa";
        }
    }
}