using System;
using System.Linq;
using System.IO;
using System.Data.SqlClient;


namespace Tweets_Sentiment_Analysis
{
    class Program
    {
        public static string[] positive_words = new string[10000]; 
        // for storing positive words
        public static string[] negetive_words = new string[10000];
        // for storing negative words
        public static string[] tweets_tokens = new string[100];
        // for storing tokens 
        public SqlCommand cmd;
        public static string sql_con = "Data Source=PHOENIX-AP\\MSSQLSERVER2016;Initial Catalog=Twitter_election;Integrated Security=True";

        static void Main(string[] args)
        {
            int id=0;
            int count = 0;
            string tweet_time = "", retweet_time = "", retweet_text = "", tweet_text = "";

            int[] sentiments = new int[3];
            //  sentiments 0 -- stores total tokens   
            //  sentiments 1 -- stores positive words count
            //  sentiments 2 -- stores negetive words count

            //loading positive and negetive words into array
            loaidng_lexical_resource();
            Console.WriteLine("lexical resource has been loaded");

            SqlDataReader dr = null; // datareader for reading tweets row by row
            SqlConnection con;
            con = new SqlConnection(sql_con);   //making database connection MS SQL SERVER 2016
            con.Open();
            Console.WriteLine();

            SqlCommand cmd = new SqlCommand("select * from clinton_tweets", con);  
            //selecting tweets for analyzing -- first go with clinton and then trump
            dr = cmd.ExecuteReader();

            //fetching individual tweets and analyzing whether it is positive and negetive
            while (dr.Read())
            {
                id = (int)dr["tweet_id"];
                tweet_text = (string)dr["tweet_text"];
                update_table(id,tweet_text);
                count++;
                Console.WriteLine(count+" tweets analyzed");
            }

            Console.WriteLine();
            Console.WriteLine("successfully analyzed tweets");

            con.Close();
            dr.Close();
        }

        // This function is used to update the table with updated information of analysis
        public static void update_table(int id, string ttext)
        {
            
            SqlConnection con1;
            con1 = new SqlConnection(sql_con);
            con1.Open();

            int[] res = new int[3];
            string result = "tokens not matched";
            res = tweets_analysis(ttext);

            if (res[1] == res[2])
                result = "nutural";
            else if (res[1] < res[2])
                result = "negetive";
            else
                result = "positive";


            //update table in database
            string query = "update clinton_tweets set total_token =" + res[0]+", positive_words="+res[1]+", negetive_words="+res[2]+", result ='"+result+"'  where tweet_id = " + id+"";
            SqlCommand cmd = new SqlCommand(query, con1);
            cmd.ExecuteNonQuery();
            con1.Close();
        }

        //Analysing text, in terms of how many positive or negetive words it has
        public static int[] tweets_analysis(string text)
        {
            int[] analysis = new int[3];
            analysis[1] = 0;
            analysis[2] = 0;
            string[] tweets_tokens = new string[100];
            tweets_tokens = text.Split(' ');
            analysis[0] = tweets_tokens.Length; // for total keywords/tokens in tweet
            
            foreach (string tokens in tweets_tokens)
            {
                bool pos, neg = false;
                pos = positive_words.Contains(tokens);
                neg = negetive_words.Contains(tokens);
                if (pos) analysis[1]++;
                if (neg) analysis[2]++;
                //Console.WriteLine(tokens);
            }
            return analysis;
        }

        //function which loads the keywords from text file of positive and negetive words
        public static void loaidng_lexical_resource()
        {
            string line;
            int i=0;
            //reading positive word list
            StreamReader filereader = new StreamReader(@"../../Lexical Resource/Positive-words-twitter.txt");
            while ((line = filereader.ReadLine()) != null)
            {
                positive_words[i] = line;
                i++;
            }

            //reading negetive word list
            StreamReader fr = new StreamReader(@"../../Lexical Resource/Negetive-words-twitter.txt");
            while ((line = fr.ReadLine()) != null)
            {
                negetive_words[i] = line;
                i++;
            }
        }
    }
}
