using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Diagnostics;
using MySql.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.IO;
using System.Reflection;

namespace Control_Generator
{
    public static class ClampConnectionClass
    {
        public static string con = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        public static string direktorijum = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static bool CreateClamp(string query, Clamp clamp)
        {
            bool executed = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", clamp.Clamptype);
                        cmd.Parameters.AddWithValue("?param2", clamp.Serialnum);
                        cmd.Parameters.AddWithValue("?param3", clamp.Rado);
                        cmd.Parameters.AddWithValue("?param4", clamp.Rn);
                        cmd.Parameters.AddWithValue("?param5", clamp.Controldate);
                        cmd.Parameters.AddWithValue("?param6", clamp.Barcode);
                        cmd.Parameters.AddWithValue("?param7", clamp.Rfcable);
                        cmd.Parameters.AddWithValue("?param8", clamp.Rfcablerror);
                        cmd.Parameters.AddWithValue("?param9", clamp.Solenoidcable);
                        cmd.Parameters.AddWithValue("?param10", clamp.Solenoidcablerror);
                        cmd.Parameters.AddWithValue("?param11", clamp.Smb);
                        cmd.Parameters.AddWithValue("?param12", clamp.Smberror);
                        cmd.Parameters.AddWithValue("?param13", clamp.Armature);
                        cmd.Parameters.AddWithValue("?param14", clamp.Armaturerror);
                        cmd.Parameters.AddWithValue("?param15", clamp.Edges);
                        cmd.Parameters.AddWithValue("?param16", clamp.Edgeserror);
                        cmd.Parameters.AddWithValue("?param17", clamp.Paralelity);
                        cmd.Parameters.AddWithValue("?param18", clamp.Paralelityerror);
                        cmd.Parameters.AddWithValue("?param19", clamp.Distance);
                        cmd.Parameters.AddWithValue("?param20", clamp.Distancerror);
                        cmd.Parameters.AddWithValue("?param21", clamp.Rflenght);
                        cmd.Parameters.AddWithValue("?param22", clamp.Rflenghterror);
                        cmd.Parameters.AddWithValue("?param23", clamp.Solenoidlenght);
                        cmd.Parameters.AddWithValue("?param24", clamp.Solenoidlenghterror);
                        cmd.Parameters.AddWithValue("?param25", clamp.Electrodes);
                        cmd.Parameters.AddWithValue("?param26", clamp.Electrodeserror);
                        cmd.Parameters.AddWithValue("?param27", clamp.Bigresistance);
                        cmd.Parameters.AddWithValue("?param28", clamp.Bigresistancerror);
                        cmd.Parameters.AddWithValue("?param29", clamp.Smallresistance);
                        cmd.Parameters.AddWithValue("?param30", clamp.Smallresistancerror);
                        cmd.Parameters.AddWithValue("?param31", clamp.Shortcircuit);
                        cmd.Parameters.AddWithValue("?param32", clamp.Shortcircuiterror);
                        cmd.Parameters.AddWithValue("?param33", clamp.Emptythick);
                        cmd.Parameters.AddWithValue("?param34", clamp.Emptythickerror);
                        cmd.Parameters.AddWithValue("?param35", clamp.Fullthick);
                        cmd.Parameters.AddWithValue("?param36", clamp.Fullthickerror);
                        cmd.Parameters.AddWithValue("?param37", clamp.Emptythin);
                        cmd.Parameters.AddWithValue("?param38", clamp.Emptythinerror);
                        cmd.Parameters.AddWithValue("?param39", clamp.Fullthin);
                        cmd.Parameters.AddWithValue("?param40", clamp.Fullthinerror);
                        cmd.ExecuteNonQuery();
                        executed = true;
                        //Debug.Write(query);
                    }
                }
            }
            catch(Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log13" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            
                //Debug.Write(ex.ToString());
                executed = false;
            }
            return executed;
        }

        public static bool DeleteClamp(string query)
        {
            bool executed = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                        executed = true;
                        Debug.Write(query);
                    }
                }
            }
            catch (Exception ex)
            {
             
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log3" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            
                //Debug.Write(ex.ToString());
                executed = false;
            }
            return executed;
        }

        public static Clamp SelectClamp(string query) 
        {
            Clamp clamp = new Clamp();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clamp.Serialnum = reader.GetString("serialnum");
                                clamp.Rado = reader.GetString("rado");
                                clamp.Rn = reader.GetString("rn");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log4" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }
            return clamp;
        }

        public static bool UpdateClampBarCode(string query, Clamp clamp)
        {
            bool executed = false;
            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", clamp.Rado);
                        cmd.Parameters.AddWithValue("?param2", clamp.Serialnum);
                        cmd.ExecuteNonQuery();
                        executed = true;
                        Debug.Write(query);
                    }
                }
            }
            catch(Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log5" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            
                //Debug.Write(ex.ToString());
                executed = false;
            }
            return executed;
        }

        public static List<string> SelectClampStatistic(string query, string param)
        {
            List<string> result = new List<string>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", param);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(reader.GetString("ammount"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log6" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }

            return result;
        }

        public static List<string> SelectClampValuesStatistic(string query, string param)
        {
            List<string> result = new List<string>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", param);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(reader.GetString("ammount") + "; " + reader.GetString("name"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log7" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }

            return result;
        }

        public static List<Clamp> SelectClampValuesPL(string query, string param1, string param2)
        {
            List<Clamp> result = new List<Clamp>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", param1);
                        cmd.Parameters.AddWithValue("?param2", param2);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Clamp clamp = new Clamp()
                                {
                                    Serialnum = reader.GetString("serialnum"),
                                    Rado = reader.GetString("rado"),
                                    Clamptype = reader.GetString("clamptype")
                                };
                                
                                result.Add(clamp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log8" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }

            return result;
        }

        public static List<Clamp> SelectClampValuesRN(string query, string param1, string param2)
        {
            List<Clamp> result = new List<Clamp>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", param1);
                        cmd.Parameters.AddWithValue("?param2", param2);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Clamp clamp = new Clamp()
                                {
                                    Rn = reader.GetString("rn")
                                };

                                result.Add(clamp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log9" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }

            return result;
        }

        public static bool ClampExists(string query, Clamp clamp)
        {
            bool result = false;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", clamp.Serialnum);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log10" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            
                result = false;
            }

            return result;
        }

        public static List<Clamp> SelectClampValuesFCR(string query, string param1, string param2)
        {
            List<Clamp> result = new List<Clamp>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", param1);
                        cmd.Parameters.AddWithValue("?param2", param2);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Clamp clamp = new Clamp()
                                {
                                    Serialnum = reader.GetString("serialnum"),
                                    Rado = reader.GetString("rado"),
                                    Clamptype = reader.GetString("clamptype"),
                                    Smallresistance = reader.GetString("smallresistance"),
                                    Bigresistance = reader.GetString("bigresistance")
                                };

                                result.Add(clamp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log11" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }

            return result;
        }

        public static int ClampCount(string query, string param1, string param2)
        {
            Int32 result = -1;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(con))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("?param1", param1);
                        cmd.Parameters.AddWithValue("?param2", param2);
                        result = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                }
            }
            catch(Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log12" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            
                result = -1;
            }

            return result;
        }
    }
}
