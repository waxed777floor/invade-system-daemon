using Newtonsoft.Json.Linq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace QuoteLineBot1
{
    public class TObj
    {
        public string create_date;
        public string value;

        public TObj(
         string create_date,
         string value)
        {
            this.create_date = create_date;
            this.value = value;
        }
        public TObj()
        {
           
        }
    }


    public static class InvadeLog
    {
        static readonly string connstr = @"Server=localhost;Database=USDT_CAP;User Id=postgres;Password=tnfd5503";
        
        public static TObj GetLastValue(string zoneCode)
        {
            TObj tObj = new TObj();

            using (NpgsqlConnection connection = new NpgsqlConnection(connstr))
            {
                connection.Open();
                string sql = $@"WITH second_digit_1 AS (
									SELECT * FROM _invade_log
									WHERE SUBSTRING(value, 2, 1) = '{zoneCode}'
									ORDER BY create_date DESC
									LIMIT 1	
                                )

                                SELECT create_date as CreateDate, value FROM second_digit_1
                                ORDER BY CreateDate DESC
	                        ";               

                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string create_date = reader["CreateDate"].ToString();
                        string value = reader["value"].ToString();
                        tObj = new TObj(create_date, value);
                    }
                }

                connection.Close();
            }
            return tObj;
        }

        public static int ClearData3DaysAgo()
        {
            DateTime dtNow = DateTime.UtcNow.AddHours(8);
            DateTime dt3DaysAgo = dtNow.AddDays(-3);
            string dtNowString = dt3DaysAgo.ToString("yyyy-MM-dd HH:mm:ss");

            int result = 0;
            using (NpgsqlConnection connection = new NpgsqlConnection(connstr))
            {
                connection.Open();
                string cmdStr = $"delete from _invade_log where create_date <= '{dtNowString}'";

                NpgsqlCommand cmd = new NpgsqlCommand(cmdStr, connection);

                result = cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
            }

            return result;
        }

    }
}