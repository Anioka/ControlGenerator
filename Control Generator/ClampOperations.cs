using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Control_Generator
{
    public static class ClampOperations
    {

        public static string direktorijum = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static bool InsertClamp(Clamp clamp)//Clamp clamp
        {
            bool result = false;

            try
            {
                if (String.IsNullOrEmpty(clamp.Clampimputerror))
                {
                    result = ClampConnectionClass.CreateClamp(Program.clampInsertIntoQuery, clamp);
                }
                else result = false;
            }
            catch(Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log20" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            
                //Debug.Write(ex);
                result = false;
            }

            return result;
        }

        public static bool DeleteClamp(Clamp clamp)
        {
            bool result = false;

            try
            {
                if (ClampConnectionClass.ClampExists(Program.clampSelectValidity, clamp))
                {
                    result = ClampConnectionClass.DeleteClamp(Program.clampDeleteQuery);
                }
                else result = false;
            }
           catch(Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log14" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            
                //Debug.Write(ex);
                result = false;
            }

            return result;
        }

        public static bool UpdateClampBarcode(Clamp clamp)
        {
            bool result = false;

            try
            {
                if (ClampConnectionClass.ClampExists(Program.clampSelectValidity, clamp))
                {
                    result = ClampConnectionClass.UpdateClampBarCode(Program.clampUpdateQueryBarcode, clamp);
                }
                else result = false;
            }
            catch(Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log15" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            
                //Debug.Write(ex);
                result = false;
            }

            return result;
        }


        public static Clamp ReturnClamp()
        {
            Clamp clamp = new Clamp(); 
            try
            {
                clamp = ClampConnectionClass.SelectClamp(Program.clampSelectQuery);
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log16" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }
            return clamp;
        }

        public static List<Clamp> ReturnClampPackingList(string firstSerial, string lastSerial)
        {
            List<Clamp> result = new List<Clamp>();
            int count = ClampConnectionClass.ClampCount(Program.clampSelectPackingListCount, firstSerial, lastSerial);

            try
            {
                if (count < 20 && count > -1)
                    result = ClampConnectionClass.SelectClampValuesPL(Program.clampSelectPackingList, firstSerial, lastSerial);
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log17" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }

            return result;
        }

        public static string ReturnClampRN(string firstSerial, string lastSerial)
        {
            List<Clamp> result = new List<Clamp>();
            string str = "";

            try
            {
                result = ClampConnectionClass.SelectClampValuesRN(Program.clampSelectRN, firstSerial, lastSerial);
                foreach (Clamp c in result)
                {
                    if (!String.IsNullOrEmpty(str))
                    {
                        if (!str.Contains(c.Rn))
                            str += ", " + c.Rn;
                    }
                    else
                    {
                        if (!str.Contains(c.Rn))
                            str += c.Rn;
                    }
                }
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log18" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }

            return str;
        }

        public static List<Clamp> ReturnClampFCR(string firstSerial, string lastSerial)
        {
            List<Clamp> result = new List<Clamp>();

            try
            {
                result = ClampConnectionClass.SelectClampValuesFCR(Program.clampSelectFinalControlReport, firstSerial, lastSerial);
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log19" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }

            return result;
        }

        /*public static List<string> ReturnClampStatistic(string param)
        {
            List<string> result = new List<string>();
            try
            {
                result = ClampConnectionClass.SelectClampStatistic(Program.clampStatistics, param);
            }
            catch (Exception ex) { }

            return result;
        }

        public static List<string> ReturnClampValuesStatistic(string param)
        {
            List<string> result = new List<string>();
            try
            {
                result = ClampConnectionClass.SelectClampValuesStatistic(Program.clampStatisticsValues, param);
            }
            catch (Exception ex) { }

            return result;
        }

        Clamp newclamp = new Clamp()
                {
                    Clamptype = clampdata[1],
                    Serialnum = clampdata[2],
                    Rado = clampdata[3],
                    Rn = clampdata[4],
                    Controldate = clampdata[5],
                    Barcode = clampdata[6],
                    Rfcable = clampdata[7],
                    Rfcablerror = clampdata[8],
                    Solenoidcable = clampdata[9],
                    Solenoidcablerror = clampdata[10],
                    Smb = clampdata[11],
                    Smberror = clampdata[12],
                    Armature = clampdata[13],
                    Armaturerror = clampdata[14],
                    Edges = clampdata[15],
                    Edgeserror = clampdata[16],
                    Paralelity = clampdata[17],
                    Paralelityerror = clampdata[18],
                    Distance = clampdata[19],
                    Distancerror = clampdata[20],
                    Rflenght = clampdata[21],
                    Rflenghterror = clampdata[22],
                    Solenoidlenght = clampdata[23],
                    Solenoidlenghterror = clampdata[24],
                    Electrodes = clampdata[25],
                    Electrodeserror = clampdata[26],
                    Bigresistance = clampdata[27],
                    Bigresistancerror = clampdata[28],
                    Smallresistance = clampdata[29],
                    Smallresistancerror = clampdata[30],
                    Shortcircuit = clampdata[31],
                    Shortcircuiterror = clampdata[32],
                    Emptythick = clampdata[33],
                    Emptythickerror = clampdata[34],
                    Fullthick = clampdata[35],
                    Fullthickerror = clampdata[36],
                    Emptythin = clampdata[37],
                    Emptythinerror = clampdata[38],
                    Fullthin = clampdata[39],
                    Fullthinerror = clampdata[40]
                };*/
    }
}
