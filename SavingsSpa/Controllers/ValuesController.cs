using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using SavingsSpa.Models;

namespace SavingsSpa.Controllers
{
    public class ValuesController : ApiController
    {
        

        private InputSetValidator Validator { get; set; }

        public ValuesController(InputSetValidator validator)
        {
            Validator = validator;
        }

        public string Get(int id)
        {
            return "inputSet";
        }

       
        public HttpResponseMessage Put(InputSet value)
        {
            var result = Validator.Validate(value);
            if (result.IsValid)
            {
                var resultSet = new Calculator().ResultSets(value);
                return Request.CreateResponse(HttpStatusCode.OK, resultSet);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result.Errors); 
        }

        
        public void Delete(int id)
        {
        }
    }

    public class ResultSet
    {
        public int Interval { get; set; }
        public double Value { get; set; }
    }
}
