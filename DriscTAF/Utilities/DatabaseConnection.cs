namespace DriscTAF.Utilities
{
    using System;
    using System.Configuration;
    using System.Data.SqlClient;

    using TechTalk.SpecFlow;

    [Binding]
     public class DatabaseConnection
    {

        private static string connectionString = ConfigurationManager.AppSettings["connectionString"];
        private static string queryString = "";
        private static SqlConnection connection;
        private static SqlCommand command;
        static int bin;
        static string binId = "";

        public static string dbValue = "";


        public void Connection()
        {

            string queryString = "Select BinId,EventType,ValidationState from dbo.BinEvent where Id=66";

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);


                // Open the connection in a try/catch block. 
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine("\t{0}\t{1}\t{2}",
                            reader[0], reader[1], reader[2]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }

        }

        public static Int32 getBinID()
        {

            queryString = "SELECT TOP 1 BinId from [dbo].[BinInventory] where CurrentLocationType='Dc'";


            using (connection =
                new SqlConnection(connectionString))
            {

                command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    var binId = command.ExecuteScalar();
                    Console.WriteLine(binId.ToString());
                    bin = Convert.ToInt32(binId.ToString());

                    return bin;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }

            return bin;

        }

        public static String getBin(String query)
        {

            queryString = query;



            using (connection =
                new SqlConnection(connectionString))
            {

                command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    string binId = Convert.ToString(command.ExecuteScalar());
                    Console.WriteLine(binId.ToString());
                    // bin = Convert.ToInt32(binId.ToString());


                    return binId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }

            return binId;

        }

        public static String getRandBinIdFromInventory(String query)
        {

            queryString = query;

            SqlDataReader rdr = null;

            using (connection =
                new SqlConnection(connectionString))
            {

                command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    rdr=command.ExecuteReader();
          
                    while (rdr.Read())
                    {
                        binId = (string)rdr.GetValue(rdr.GetOrdinal("BinId"));
                        if (binId != null)
                        {
                            break;
                        }
                    }
                
                    Console.WriteLine(binId.ToString());
                    // bin = Convert.ToInt32(binId.ToString());


                    return binId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }

            return binId;

        }

        public static String getBinIdNotExistsInInventory(String query)
        {

            queryString = query;

            SqlDataReader rdr = null;

            using (connection =
                new SqlConnection(connectionString))
            {

                command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    rdr = command.ExecuteReader();

                    while (rdr.Read())
                    {
                        binId = (string)rdr.GetValue(rdr.GetOrdinal("BinId"));
                        if (binId != null)
                        {
                            break;
                        }
                    }

                    Console.WriteLine(binId.ToString());
                    // bin = Convert.ToInt32(binId.ToString());


                    return binId;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }

            return binId;

        }

        public static String doQuery(String query)
        {

            queryString = query;



            using (connection =
                       new SqlConnection(connectionString))
            {

                command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    string dbValue = Convert.ToString(command.ExecuteScalar());
                    Console.WriteLine(dbValue.ToString());
                    // bin = Convert.ToInt32(binId.ToString());


                    return dbValue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }

            return dbValue;

        }

        public static string GetExistingBin()
        {
             return doQuery("SELECT TOP 1 BinId from [dbo].[BinInventory] where CurrentLocationType='Dc'");
        }

    }
}
