﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Project
{
    internal class Person
    {

        protected UInt32 Account_number = 0;
        protected string password = "";
        private int adminLvl;
        private SqlConnection con;
        public bool found = false;

        public Person(UInt32 number, string password)
        {
            Account_number = number;
            this.password = password;

            string path = System.Environment.CurrentDirectory;
            string path2 = path.Substring(0, path.LastIndexOf("bin")) + "DataBase" + "\\DB.mdf";


            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + path2 + ";Integrated Security=True");
            try
            {
                con.Open();
            }
            catch (SqlException)
            {
                Console.WriteLine("Error while connection to SQL server");
            }

        }


        public UInt32 Account_Number
        {
            get { return Account_number;}
        }
        public string Password
        {
            get { return password;}
            set { password = value; }   
        }
        public int AdminLvl
        {
            get { return adminLvl;}
            set { adminLvl = value; }
        }
        public void loginCheck()
        {
            string query = "select * from Users where AccountNumber = " + Account_Number;
            SqlCommand command = new SqlCommand(query, con);
            SqlDataReader dataReader = command.ExecuteReader();

            if (dataReader.Read())
            {
                if (Password != (string)dataReader.GetValue(1))
                {
                    MessageBox.Show("Wrong Password");
                }
                else
                {
                    AdminLvl = Convert.ToInt32(dataReader.GetValue(2));
                    found = true;
                }
                
            }
            else
            {
                MessageBox.Show("Account Number not found");
            }
        }
        
    }
}
