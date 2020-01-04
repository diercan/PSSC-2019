using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace APPicola.Models
{
    public class ConnImplement
    {
        
        public List<ConnProp> GetSqlRowsUsers()
        {
                List<ConnProp> emplist = new List<ConnProp>();
                string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
                MySqlConnection mysqlconn = new MySqlConnection(mainconn);
                string sqlquery = "select * from users";
                MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
                mysqlconn.Open();
                MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
                DataTable dt = new DataTable();
                mda.Fill(dt);
                mysqlconn.Close();
                foreach (DataRow dr in dt.Rows)
                {
                    emplist.Add(new ConnProp
                    {
                        user = dr["user"].ToString(),
                        password = dr["password"].ToString()
                    });
                }
                return emplist;
        }

        public string InsertSqlRow(string user, string password)
        {
            ConnImplement sqllist = new ConnImplement();
            string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlinsert = "INSERT INTO `users`(`user`, `password`) VALUES ('" + user + "','" + password + "')";

            for (int i = 0; i < sqllist.GetSqlRowsUsers().Count; i++)
            {
                if (sqllist.GetSqlRowsUsers()[i].user == user && sqllist.GetSqlRowsUsers()[i].password == password)
                {
                    return "User name already exists!";
                }
            }
            MySqlCommand sqlcomm = new MySqlCommand(sqlinsert, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();
            return "Account created!";
        }

        public List<Articole> GetSqlRowsArticole()
        {
            List<Articole> artlist = new List<Articole>();
            string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlquery = "select * from articole";
            MySqlCommand sqlcomm = new MySqlCommand(sqlquery, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();
            foreach (DataRow dr in dt.Rows)
            {
                artlist.Add(new Articole
                {
                    index = Convert.ToInt32(dr["index"]),
                    numearticol = dr["numearticol"].ToString()
                });
            }
            return artlist;
        }
        public string InsertSqlRowsArticole(Articole articole)
        {
            ConnImplement sqllist = new ConnImplement();
            string mainconn = ConfigurationManager.ConnectionStrings["Mysqlconnection"].ConnectionString;
            MySqlConnection mysqlconn = new MySqlConnection(mainconn);
            string sqlinsert = "INSERT INTO `articole`(`index`, `numearticol`) VALUES ('" + articole.index + "','" + articole.numearticol + "')";

            for (int i = 0; i < sqllist.GetSqlRowsArticole().Count; i++)
            {
                if (sqllist.GetSqlRowsArticole()[i].index == articole.index && sqllist.GetSqlRowsArticole()[i].numearticol == articole.numearticol)
                {
                    return "Articolul exista deja!";
                }
            }
            MySqlCommand sqlcomm = new MySqlCommand(sqlinsert, mysqlconn);
            mysqlconn.Open();
            MySqlDataAdapter mda = new MySqlDataAdapter(sqlcomm);
            DataTable dt = new DataTable();
            mda.Fill(dt);
            mysqlconn.Close();
            return "Articol adaugat!";
        }
    }
}