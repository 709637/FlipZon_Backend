using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http.Results;
using System.Web.Mvc;
using FlipZon_Backend.ClassesENCP;
using Newtonsoft.Json;

namespace FlipZon_Backend.Controllers
{
    //[System.Web.Mvc.Authorize]
    public class ProductDetailController : ApiController
    {

        public string GetProductDetails()
        {
            string query = "select * from Product_Details";
            Product_Details pd = new Product_Details();
             return pd.getProduct(query);
        }


        public string GetProductDetails(int id)
        {
            string query = "select * from Product_Details where Product_Id="+ id.ToString();
            Product_Details pd = new Product_Details();
            return pd.getProduct(query);
        }

        // POST api/values
        public HttpStatusCode Post([FromBody]Product_Details value)
        {
            Product_Details Pd = new Product_Details();
            int IsSuccess=Pd.PostProduct(value);
            if (IsSuccess == 1)
                return HttpStatusCode.Created;
            else
                return HttpStatusCode.BadRequest;
        }

        // PUT api/values/5
        public HttpStatusCode Put(int id, [FromBody]Product_Details value)
        {
            Product_Details Pd = new Product_Details();
            value.Product_Id = id;
            int IsSuccess = Pd.PostProduct(value);
            if (IsSuccess == 1)
                return HttpStatusCode.Created;
            else
                return HttpStatusCode.BadRequest;
        }

        // DELETE api/values/5
        public HttpStatusCode Delete(Product_Details id)
        {
            Product_Details Pd = new Product_Details();
            
            int IsSuccess = Pd.PostProduct(id);
            if (IsSuccess == 1)
                return HttpStatusCode.Created;
            else
                return HttpStatusCode.BadRequest;
        }

    }
}
