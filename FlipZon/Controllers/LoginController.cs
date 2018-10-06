using FlipZon_Backend.ClassesENCP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FlipZon_Backend.Controllers
{
    public class LoginController : ApiController
    {
        public string GetUserDetails()
        {
            string query = "select Email_Id, Full_Name, Login_Id, Mobile_No from User_Login_Details";
            Login_Register insLogReg = new Login_Register();
            return insLogReg.getUserDetailsToVerify(query);
        }

        public string GetUserDetails(string id)
        {
            string query = @"select * from User_Login_Details where Login_Id='" + id +"'";
            Login_Register insLogReg = new Login_Register();
            return insLogReg.getUserDetailsToVerify(query);
        }

        public HttpStatusCode Post([FromBody]Login_Register value)
        {
            Login_Register insLogReg = new Login_Register();
            int IsSuccess = insLogReg.RegisterUserAllowLogin(value);
            if (IsSuccess == 1)
                return HttpStatusCode.Created;
            else
                return HttpStatusCode.BadRequest;
        }
    }
}
