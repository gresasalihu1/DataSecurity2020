using System;
using System.Data;

using MySql.Data.MySqlClient;

namespace ds
{
    public class Connection
    {
        public static MySqlDataReader databaza(string sql)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection("server=localhost;database=siguria;userid=root;password=password123.");
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader reader;
                reader = cmd.ExecuteReader();
                return reader;

            }


            catch (Exception exeption)
            {
                throw new Exception(exeption.Message);
            }
        }
        public static DataSet DataSet(string sql)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;database=siguria;userid=root;password=password123.");
            DataSet DS = new DataSet();
            var SqlCommnad = new MySqlCommand(sql, conn);
            var DA = new MySqlDataAdapter(SqlCommnad);
            try
            {
                DA.Fill(DS);
                return DS;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
