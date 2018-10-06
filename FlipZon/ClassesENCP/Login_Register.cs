using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FlipZon_Backend.ClassesENCP
{
    public class Login_Register
    {
        public string Email_Id  { set; get; }
        public string Full_Name { set; get; }
        public string Login_Id  { set; get; }
        public double Mobile_No    { set; get; }
        public string Passwords { set; get; }


        public string getUserDetailsToVerify(string query)
        {
            string CS = ConfigurationManager.ConnectionStrings["FlipZonCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                List<Login_Register> pdList = new List<Login_Register>();

                while (rdr.Read())
                {
                    Login_Register InsLogReg = new Login_Register();
                    InsLogReg.Email_Id = rdr["Email_Id"].ToString();
                    InsLogReg.Full_Name = rdr["Full_Name"].ToString();
                    InsLogReg.Login_Id = rdr["Login_Id"].ToString();
                    InsLogReg.Mobile_No = double.Parse( rdr["Mobile_No"].ToString());
                    

                    pdList.Add(InsLogReg);
                }
                var json = JsonConvert.SerializeObject(pdList);
                return json;
            }
        }


        public int RegisterUserAllowLogin(Login_Register UserDetail)
        {
            string CS = ConfigurationManager.ConnectionStrings["FlipZonCS"].ConnectionString;
            string command;
            int IsSuccess;
            if (UserDetail.Mobile_No != 0)
            {
                command = @"Insert into User_Login_Details (Email_Id, Full_Name, Login_Id, Mobile_No, Passwords) values ('"+UserDetail.Email_Id + "','" +UserDetail.Full_Name +"','" +UserDetail.Login_Id + "'," + UserDetail.Mobile_No + ",'" + UserDetail.Passwords+"')";
            }
            else
            {
                command = @"select count(Email_Id) from User_Login_Details where Login_Id='" + UserDetail.Login_Id + "' and Passwords='" + UserDetail.Passwords+"'";
            }

            using (SqlConnection con = new SqlConnection(CS))
            {

                SqlCommand cmd = new SqlCommand(command, con);

                con.Open();
                if (UserDetail.Mobile_No != 0)
                {
                    IsSuccess = cmd.ExecuteNonQuery();
                }
                else
                {
                    IsSuccess = (int)cmd.ExecuteScalar();
                }


                if (IsSuccess != 0)
                    return 1;
                else
                    return 0;
            }
        }  }
}