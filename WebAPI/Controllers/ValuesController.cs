using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using Dapper;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly TelemetryClient telemetry;
        private readonly string _applicationConnectionString;
        private readonly IEnumerable<string> _databasenames;


        public ValuesController(IApplicationConnectionString applicationConnectionString,
                                TelemetryClient telemetry)
        {
            this.telemetry = telemetry;

            _applicationConnectionString = applicationConnectionString.ConnectionString;

            // Test for a good DB Connection String and ability to connect to the DB
                using (IDbConnection cnn = new SqlConnection(_applicationConnectionString))
                {
                        _databasenames = cnn.Query<string>("select name from sys.databases");
                }
        }


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            telemetry.TrackEvent("Get Request in Values Controller");
            List<string> returnvalues = new List<string>();
            returnvalues.Add("Value1");
            returnvalues.Add("Value2");
            returnvalues.Add(_applicationConnectionString);
            foreach (string dbname in _databasenames)
            {
                returnvalues.Add(dbname);
            } 
            return returnvalues;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
