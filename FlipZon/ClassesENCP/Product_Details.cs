using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Web.Http.Results;
using System.Web.Http;
using System.Configuration;
using System.Data;

namespace FlipZon_Backend.ClassesENCP
{
    public class Product_Details
    {
        public int Product_Id { set; get; }
        public string Product_Name { set; get; }
        public string Product_Detail { set; get; }
        public string Product_Specification { set; get; }
        public int Product_Category_Code { set; get; }
        public int Product_Cost { set; get; }
        public byte[] Product_Image { set; get; }
        public bool Isavailable { set; get; }


        public string getProduct(string query)
        {
            string CS = ConfigurationManager.ConnectionStrings["FlipZonCS"].ConnectionString;
            using (
                SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                List<Product_Details> pdList = new List<Product_Details>();

                while (rdr.Read())
                {
                    Product_Details pd = new Product_Details();
                    pd.Product_Id=(int)rdr["Product_Id"];
                    pd.Product_Name = rdr["Product_Name"].ToString();
                    pd.Product_Detail = rdr["Product_Detail"].ToString();
                    pd.Product_Specification = rdr["Product_Specification"].ToString();
                    pd.Product_Category_Code = (int)rdr["Product_Category_Code"];
                    pd.Product_Cost = (int)rdr["Product_Cost"];
                    pd.Product_Image = (byte[])rdr["Product_Image"];
                    pd.Isavailable = (bool)rdr["Isavailable"];

                    pdList.Add(pd);
                }
                var json = JsonConvert.SerializeObject(pdList);
                return json;
            }
        }


        public int PostProduct(Product_Details Pd)
        {
            string CS = ConfigurationManager.ConnectionStrings["FlipZonCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {

                SqlCommand cmd = new SqlCommand("Add_Product_Details", con);
                cmd.CommandType = CommandType.StoredProcedure;

                if (Pd.Product_Id != 0)
                {
                    cmd.Parameters.AddWithValue("@Product_Id", Pd.Product_Id);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Product_Id", 0);
                }
                

               


                cmd.Parameters.AddWithValue("@Product_Name", Pd.Product_Name);
                cmd.Parameters.AddWithValue("@Product_Details", Pd.Product_Detail);
                cmd.Parameters.AddWithValue("@Product_Specification", Pd.Product_Specification);
                cmd.Parameters.AddWithValue("@Product_Category_Code", Pd.Product_Category_Code);
                cmd.Parameters.AddWithValue("@Product_Cost", Pd.Product_Cost);
                cmd.Parameters.AddWithValue("@Product_Image", Pd.Product_Image);
                cmd.Parameters.AddWithValue("@Isavailable", Pd.Isavailable);
                
                SqlParameter param8 = new SqlParameter();
                param8.ParameterName = "@returnValue";
                param8.Direction = ParameterDirection.Output;
                param8.Size = 25;
                cmd.Parameters.Add(param8);

                //***********************************************************************************************

                //SqlParameter param = new SqlParameter();
                //param.ParameterName = "@Product_Name";
                //param.Value = Pd.Product_Name;
                //cmd.Parameters.Add(param);               

                //SqlParameter param1 = new SqlParameter();
                //param1.ParameterName = "@Product_Details";
                //param1.Value = Pd.Product_Detail;
                //cmd.Parameters.Add(param1);

                //SqlParameter param2 = new SqlParameter();
                //param2.ParameterName = "@Product_Specification";
                //param2.Value = Pd.Product_Specification;
                //cmd.Parameters.Add(param2);

                //SqlParameter param4 = new SqlParameter();
                //param4.ParameterName = "@Product_Category_Code";
                //param4.Value = Pd.Product_Category_Code;
                //cmd.Parameters.Add(param4);

                //SqlParameter param5 = new SqlParameter();
                //param5.ParameterName = "@Product_Cost";
                //param5.Value = Pd.Product_Cost;
                //cmd.Parameters.Add(param5);

                //SqlParameter param6 = new SqlParameter();
                //param6.ParameterName = "@Product_Image";
                //param6.Value = Pd.Product_Image;
                //cmd.Parameters.Add(param6);

                //SqlParameter param7 = new SqlParameter();
                //param7.ParameterName = "@Isavailable";
                //param7.Value = Pd.Isavailable == true ? 1 : 0 ;
                //cmd.Parameters.Add(param7);

                //SqlParameter param8 = new SqlParameter();
                //param8.ParameterName = "@returnValue";
                //param8.Direction = ParameterDirection.Output;
                //param8.Size = 25;
                //cmd.Parameters.Add(param8);

                //***********************************************************************************************

                con.Open();
                int IsSuccess = cmd.ExecuteNonQuery();
               
                if (param8.Value.ToString() == "1")
                    return 1;
                else
                    return 0;
            }

        }
    }
}

   
