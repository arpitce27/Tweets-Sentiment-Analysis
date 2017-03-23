using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tweets_Sentiment_Analysis
{
    class DBConnection
    {
        private string sql_con;
        private string stringcon;
        public SqlConnection con;
        public void OpenCnn()
        {
            sql_con = "Data Source=(local);Initial Catalog=Twitter_election;Integrated Security=True";
            con.ConnectionString = sql_con; 
            con = new SqlConnection();
            con.Open();
        }

        public void CloseCnn()
        {
            con.Close();
        }
    }
}
