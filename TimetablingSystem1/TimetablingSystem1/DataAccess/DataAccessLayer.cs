using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using TimetablingSystem1.Models;

namespace TimetablingSystem1.DataAccess
{
    public class DataAccessLayer
    {
       //static SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["team02database"].ConnectionString.ToString());
        static SqlConnection conn = new SqlConnection("Data Source=co-web.lboro.ac.uk;Integrated Security=False;User ID=team02;Password=s9s38vb;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");

        public static string sha256(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
            foreach (byte bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        } 
       

        public static bool UserIsValid(string username, string password)
        {

             
            
                bool authenticated = false;

                string query = string.Format("SELECT * FROM Department WHERE DepartmentCode = '{0}' AND PasswordHash = '{1}'", username, DataAccessLayer.sha256(password) );

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                authenticated = sdr.HasRows;
                conn.Close();
                return (authenticated);

                          
            

        } 

       
    }
}