using System;
using Microsoft.Extensions.Configuration;

namespace Examen.Models
{
    public class DbContext
    {
        public string connectionString { get; }
        private readonly IConfiguration configuration;
        public DbContext(IConfiguration _configuration)
        {
            configuration = _configuration;
            connectionString = configuration.GetConnectionString("MySqlConnection");
        }
    }
}