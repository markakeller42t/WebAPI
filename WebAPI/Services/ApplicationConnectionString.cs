using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public class ApplicationConnectionString : IApplicationConnectionString
    {

        public ApplicationConnectionString(string connectionString)
        {
            this.ConnectionString = connectionString;

        }

        public string ConnectionString { get; }
    }
}
