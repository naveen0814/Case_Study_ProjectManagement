using System;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Data.Sql;
using ProjectManagement.util;
using Microsoft.Data.SqlClient;


namespace ProjectManagement.util 
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Server=NAVEEN;Database=PROJECTMANAGEMENT;Integrated Security=True;TrustServerCertificate=True";
            return new SqlConnection(connectionString);
        }
    }
}