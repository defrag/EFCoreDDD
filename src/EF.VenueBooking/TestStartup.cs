using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;

namespace EF.VenueBooking
{
    public class TestStartup : Startup, IDisposable
    {
        private SqliteConnection _connection;

        public TestStartup()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Close();
            _connection = null;
        }

        protected override Action<DbContextOptionsBuilder> GetDbOptions(IConfiguration configuration)
        {
            return options => options.UseSqlite(_connection);
        }
    }
}
