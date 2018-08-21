using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Collections;
using System.Diagnostics;

namespace Control_Generator
{
    //komunikacija sa bazom
    public static class UserConnectionClass
    {
        //MySqlAdapter to fill DataSet, MySqlCommand to do insert, delete, update, MySqlReader to read row by row
        private static string con = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        //public static List<User> SelectUser(string query) 
        //{
        //    List<User> users = new List<User>();
        //    users.Capacity = 100;
        //    try
        //    {
        //        using (MySqlConnection conn = new MySqlConnection(con))
        //        {
        //            conn.Open();
        //            using (MySqlCommand command = new MySqlCommand(query, conn))
        //            {
        //                using (MySqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        var user = new User()
        //                        {
        //                            Username = reader.GetString("username"),
        //                            Password = reader.GetString("password"),
        //                            Role = reader.GetInt16("role"),
        //                            Mail = reader.GetString("email")
        //                        };
        //                        users.Add(user);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex) {
        //        //Debug.Write(ex.ToString());
        //    }
        //   return users;
        //}

        public static User SelectUser(string query, User user)
        {
            //bool executed = false;
            User results = new User();
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", user.Username);
                        cmd.Parameters.AddWithValue("?param2", user.Password);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Username = reader.GetString("username");
                                results.Password = reader.GetString("password");
                                results.Role = reader.GetInt16("role");
                                results.Mail = reader.GetString("email");
                                results.DataV = true;
                                //results.Add(readuser);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.Write(ex.ToString());
                results.DataV = true;
            }

            //results.Add(executed);

            return results;
        }

        public static User SelectUserbyEmail(string query, User user)
        {
            //bool executed = false;
            User results = new User();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", user.Mail);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                results.Username = reader.GetString("username");
                                results.Password = reader.GetString("password");
                                results.Role = reader.GetInt16("role");
                                results.Mail = reader.GetString("email");
                                results.DataV = true;
                                //results.Add(readuser);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.Write(ex.ToString());
                results.DataV = true;
            }

            //results.Add(executed);

            return results;
        }

        public static bool UpdateUserPassword(string query, User user)
        {
            bool executed = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", user.Password);
                        cmd.Parameters.AddWithValue("?param2", user.Username);
                        cmd.ExecuteNonQuery();
                        executed = true;
                        Debug.Write(query);
                    }
                }
            }
            catch (Exception ex) {
                //Debug.Write(ex.ToString());
                executed = false;
            }
            return executed;
        }

        public static bool UpdateUserUsername(string query, User user)
        {
            bool executed = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", user.Username);
                        cmd.Parameters.AddWithValue("?param2", user.Mail);
                        cmd.ExecuteNonQuery();
                        executed = true;
                        Debug.Write(query);
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.Write(ex.ToString());
                executed = false;
            }
            return executed;
        }

        public static bool DeleteUser(string query, User user)
        {
            bool executed = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", user.Username);
                        cmd.Parameters.AddWithValue("?param2", user.Mail);
                        cmd.ExecuteNonQuery();
                        executed = true;
                        Debug.Write(query);
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.Write(ex.ToString());
                executed = false;
            }
            return executed;
        }

        public static bool CreateUser(string query, User user)
        {
            bool executed = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", user.Username);
                        cmd.Parameters.AddWithValue("?param2", user.Password);
                        cmd.Parameters.AddWithValue("?param3", user.Role);
                        cmd.Parameters.AddWithValue("?param4", user.Mail);
                        cmd.ExecuteNonQuery();
                        executed = true;
                        //Debug.Write(query);
                    }
                }
            }
            catch (Exception ex)
            {
                //Debug.Write(ex.ToString());
                executed = false;
            }
            return executed;
        }
    }
}
