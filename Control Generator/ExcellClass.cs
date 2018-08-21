using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Diagnostics;
using System.Drawing;

namespace Control_Generator
{
    public static class ExcellClass
    {
        public static string direktorijum = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static bool crateFinalControl(string save, Clamp clamp)
        {
            bool open = false;

            Microsoft.Office.Interop.Excel.Application oXL = null;
            Microsoft.Office.Interop.Excel._Workbook oWB = null;
            Microsoft.Office.Interop.Excel._Worksheet oSheet = null;
            //Microsoft.Office.Interop.Excel.Range oRng;

            object misvalue = System.Reflection.Missing.Value;
            
            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                //oXL.Visible = true;

                // oWB = (Excel.Workbook)(oXL.Workbooks._Open(@"C:\Users\AleksandraTos\Documents\Visual Studio 2008\Projects\Control Generator\bin\Debug\final control.xlsx", Missing.Value,
                //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));

                oWB = (Excel.Workbook)(oXL.Workbooks._Open(save, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));

                //Get a new workbook.
                //oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                if (!clamp.Clamptype.Contains("PC"))
                {
                    RegularClamp(oSheet, clamp);
                }
                else
                {
                    PinchClamp(oSheet, clamp);
                }

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs(save, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                //oWB.Close(true, open, Missing.Value);
                open = true;

                oWB.Close(true, Missing.Value, Missing.Value);
                oXL.Quit();

                Marshal.ReleaseComObject(oSheet);
                Marshal.ReleaseComObject(oWB);
                Marshal.ReleaseComObject(oXL);
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log21" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
                //MessageBox.Show("Something went wrong during the creation of the control file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //if (!Directory.Exists(direktorijum + @"\Log Files")) Directory.CreateDirectory(direktorijum + @"\Log Files");

                //StreamWriter sw = new StreamWriter(direktorijum + "\\Log Files\\Log" + DateTime.Now.ToString("dd-MM-yyyy -- HH-mm-ss") + ".txt");
                //sw.Write(ex);
                //sw.Close();
                open = false;
            }
            finally
            { 
            }

            return open;
        }

        //public static void addPictureExcel(string value, int cellname, int cellnum)
        //{
        //    foreach (Excel.Shape sh in myExcelWorkSheet.Shapes)
        //    {
        //        if (sh.Name != "Picture 1")
        //            sh.Delete(]=1;
        //    }
        //    myExcelWorkSheet.Shapes.AddPicture(value, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoTrue, cellname, cellnum, 131, 40]=1;
        //}

        public static bool createFinalControlReport(string save, List<Clamp> clamps, string date, string RN)
        {
            bool result = false;

            int counter = 5;

            object misValue = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Excel.Application oXL = null;
            Microsoft.Office.Interop.Excel._Workbook oWB = null;
            Microsoft.Office.Interop.Excel._Worksheet oSheet = null;

            try
            {
                oXL = new Microsoft.Office.Interop.Excel.Application();
                //oXL.Visible = true;

                // oWB = (Excel.Workbook)(oXL.Workbooks._Open(@"C:\Users\AleksandraTos\Documents\Visual Studio 2008\Projects\Control Generator\bin\Debug\final control.xlsx", Missing.Value,
                //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));

                oWB = (Excel.Workbook)(oXL.Workbooks._Open(save, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));

                //Get a new workbook.
                //oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                if (oXL == null)
                {
                    //MessageBox.Show("Excel is not properly installed!!");
                    //return;
                }
                else
                {
                    oSheet.Cells[5, 1] = date;
                    oSheet.Cells[15, 1] = RN;

                    foreach (Clamp c in clamps)
                    {
                        oSheet.Cells[counter, 2] = c.Serialnum;
                        oSheet.Cells[counter, 3] = c.Rado;
                        oSheet.Cells[12, 1] = c.Clamptype;

                        if (c.Clamptype.Contains("WH1 - 15V") || c.Clamptype.Contains("WH2 - 15V"))
                        {
                            oSheet.Cells[9, 1] = "15V";
                            oSheet.Cells[counter, 8] = c.Smallresistance;
                        }
                        else
                        {
                            oSheet.Cells[9, 1] = "24V";
                            oSheet.Cells[counter, 8] = c.Bigresistance;
                        }

                        oSheet.Cells[counter, 6] = "OK";
                        oSheet.Cells[counter, 5] = "MM, NS, MM";
                        oSheet.Cells[counter, 4] = "1";

                        counter++;
                    }

                    oXL.Visible = false;
                    oXL.UserControl = false;
                    oWB.SaveAs(save, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                        false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                    //oWB.Close(true, open, Missing.Value);
                    result = true;

                    oWB.Close(true, Missing.Value, Missing.Value);
                    oXL.Quit();

                    Marshal.ReleaseComObject(oSheet);
                    Marshal.ReleaseComObject(oWB);
                    Marshal.ReleaseComObject(oXL);
                }
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log22" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
                //MessageBox.Show("Something went wrong during the creation of the control file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //if (!Directory.Exists(direktorijum + @"\Log Files")) Directory.CreateDirectory(direktorijum + @"\Log Files");

                //StreamWriter sw = new StreamWriter(direktorijum + "\\Log Files\\Log" + DateTime.Now.ToString("dd-MM-yyyy -- HH-mm-ss") + ".txt");
                //sw.Write(ex);
                //sw.Close();
                result = false;
            }
            
            return result;
        }

        public static bool createPackingList(string save, List<Clamp> clamps, List<string> onetimeparams, string RN)
        {
            bool open = false;

            int counter = 16;

            object misValue = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Excel.Application oXL = null;
            Microsoft.Office.Interop.Excel._Workbook oWB = null;
            Microsoft.Office.Interop.Excel._Worksheet oSheet = null;
            //Microsoft.Office.Interop.Excel.Range oRng;
            try
            {
                //if (xlApp == null)
                //{
                //    //MessageBox.Show("Excel is not properly installed!!");
                //    //return;
                //}
                //else
                //{
                    oXL = new Microsoft.Office.Interop.Excel.Application();
                    //oXL.Visible = true;

                    // oWB = (Excel.Workbook)(oXL.Workbooks._Open(@"C:\Users\AleksandraTos\Documents\Visual Studio 2008\Projects\Control Generator\bin\Debug\final control.xlsx", Missing.Value,
                    //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    //Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));

                    oWB = (Excel.Workbook)(oXL.Workbooks._Open(save, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                    Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));

                    //Get a new workbook.
                    //oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                    oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                    oSheet.Cells[4, 8] = onetimeparams[0];
                    oSheet.Cells[13, 1] = onetimeparams[1];
                    oSheet.Cells[10, 1] = RN;
                    oSheet.Cells[13, 4] = onetimeparams[2];

                    foreach (Clamp c in clamps)
                    {
                        oSheet.Cells[counter, 7] = c.Serialnum;
                        oSheet.Cells[counter, 8] = c.Rado;
                        oSheet.Cells[counter, 9] = "1";
                        oSheet.Cells[16, 6] = c.Clamptype;

                        counter++;
                    }


                    oXL.Visible = false;
                    oXL.UserControl = false;
                    oWB.SaveAs(save, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                        false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


                    //oWB.Close(true, open, Missing.Value);
                    open = true;

                    oWB.Close(true, Missing.Value, Missing.Value);
                    oXL.Quit();

                    Marshal.ReleaseComObject(oSheet);
                    Marshal.ReleaseComObject(oWB);
                    Marshal.ReleaseComObject(oXL);
                //}
            }
            catch (Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log23" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
                //MessageBox.Show("Something went wrong during the creation of the control file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //if (!Directory.Exists(direktorijum + @"\Log Files")) Directory.CreateDirectory(direktorijum + @"\Log Files");

                //StreamWriter sw = new StreamWriter(direktorijum + "\\Log Files\\Log" + DateTime.Now.ToString("dd-MM-yyyy -- HH-mm-ss") + ".txt");
                //sw.Write(ex);
                //sw.Close();
                open = false;
            }
           
            return open;
        }

        private static void RegularClamp(Microsoft.Office.Interop.Excel._Worksheet oSheet, Clamp clamp)
        {
            foreach (Excel.Shape sh in oSheet.Shapes)
            {
                if (sh.Name != "Picture 2")
                    sh.Delete();
                else if (sh.Name != "Picture 1")
                    sh.Delete();
            }

            oSheet.Shapes.AddPicture(clamp.Barcode, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoTrue, 500, 172, 131, 40);


            /*Image oImage = Image.FromFile(clamp.Barcode);
            System.Windows.Forms.Clipboard.SetDataObject(oImage, true);

            Excel.Range oRange = (Excel.Range)oSheet.Cells[12, 5];
            oSheet.Paste(oRange, oImage);*/

            oSheet.Cells[9, 3] = clamp.Controldate;
            oSheet.Cells[10, 3] = clamp.Controldate;

            oSheet.Cells[9, 7] = clamp.Rn;

            if (clamp.Clamptype.Contains("WH1 - 15V") || clamp.Clamptype.Contains("WH2 - 15V")) oSheet.Cells[14, 3] = "15V";
            else oSheet.Cells[14, 2] = "24V";

            oSheet.Cells[12, 2] = clamp.Serialnum;
            oSheet.Cells[12, 3] = clamp.Rado;

            if (clamp.Rfcablerror.Contains("-"))
            {
                oSheet.Cells[19, 3] = clamp.Rfcable;
                oSheet.Cells[19, 4] = "-";
                oSheet.Cells[19, 5] = "-";
            }
            else
            {
                oSheet.Cells[19, 3] = "-";
                oSheet.Cells[19, 4] = clamp.Rfcable;
                oSheet.Cells[19, 5] = clamp.Rfcablerror;
            }

            if (clamp.Solenoidcablerror.Contains("-"))
            {
                oSheet.Cells[20, 3] = clamp.Solenoidcable;
                oSheet.Cells[20, 4] = "-";
                oSheet.Cells[20, 5] = "-";
            }
            else
            {
                oSheet.Cells[20, 3] = "-";
                oSheet.Cells[20, 4] = clamp.Solenoidcable;
                oSheet.Cells[20, 5] = clamp.Solenoidcablerror;
            }

            if (clamp.Smberror.Contains("-"))
            {
                oSheet.Cells[21, 3] = clamp.Smb;
                oSheet.Cells[21, 4] = "-";
                oSheet.Cells[21, 5] = "-";
            }
            else
            {
                oSheet.Cells[21, 3] = "-";
                oSheet.Cells[21, 4] = clamp.Smb;
                oSheet.Cells[21, 5] = clamp.Smberror;
            }

            if (clamp.Armaturerror.Contains("-"))
            {
                oSheet.Cells[22, 3] = clamp.Armature;
                oSheet.Cells[22, 4] = "-";
                oSheet.Cells[22, 5] = "-";
            }
            else
            {
                oSheet.Cells[22, 3] = "-";
                oSheet.Cells[22, 4] = clamp.Armature;
                oSheet.Cells[22, 5] = clamp.Armaturerror;
            }

            if (clamp.Edgeserror.Contains("-"))
            {
                oSheet.Cells[23, 3] = clamp.Edges;
                oSheet.Cells[23, 4] = "-";
                oSheet.Cells[23, 5] = "-";
            }
            else
            {
                oSheet.Cells[23, 3] = "-";
                oSheet.Cells[23, 4] = clamp.Edges;
                oSheet.Cells[23, 5] = clamp.Edgeserror;
            }

            oSheet.Cells[27, 3] = clamp.Paralelity;
            oSheet.Cells[27, 5] = clamp.Paralelityerror;

            oSheet.Cells[28, 3] = clamp.Distance;
            oSheet.Cells[28, 5] = clamp.Distancerror;

            oSheet.Cells[29, 3] = clamp.Rflenght;
            oSheet.Cells[29, 5] = clamp.Rflenghterror;

            oSheet.Cells[30, 3] = clamp.Solenoidlenght;
            oSheet.Cells[30, 5] = clamp.Solenoidlenghterror;

            oSheet.Cells[31, 3] = clamp.Electrodes;
            oSheet.Cells[31, 5] = clamp.Electrodeserror;

            oSheet.Cells[35, 3] = clamp.Smallresistance;
            oSheet.Cells[35, 5] = clamp.Smallresistancerror;

            oSheet.Cells[36, 3] = clamp.Bigresistance;
            oSheet.Cells[36, 5] = clamp.Bigresistancerror;

            oSheet.Cells[37, 3] = clamp.Shortcircuit;
            oSheet.Cells[37, 5] = clamp.Shortcircuiterror;

            oSheet.Cells[41, 2] = clamp.Emptythick;
            oSheet.Cells[41, 5] = clamp.Emptythickerror;

            oSheet.Cells[42, 2] = clamp.Fullthick;
            oSheet.Cells[42, 5] = clamp.Fullthickerror;

            oSheet.Cells[43, 2] = clamp.Emptythin;
            oSheet.Cells[43, 5] = clamp.Emptythinerror;

            oSheet.Cells[44, 2] = clamp.Fullthin;
            oSheet.Cells[44, 5] = clamp.Fullthinerror;
        }

        private static void PinchClamp(Microsoft.Office.Interop.Excel._Worksheet oSheet, Clamp clamp)
        {
            foreach (Excel.Shape sh in oSheet.Shapes)
            {
                if (sh.Name != "Picture 2")
                    sh.Delete();
                else if (sh.Name != "Picture 1")
                    sh.Delete();
            }

            oSheet.Shapes.AddPicture(clamp.Barcode, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoTrue, 500, 172, 131, 40);

            /*Image oImage = Image.FromFile(clamp.Barcode);
            System.Windows.Forms.Clipboard.SetDataObject(oImage, true);

            Excel.Range oRange = (Excel.Range)oSheet.Cells[12, 5];
            oSheet.Paste(oRange, oImage);*/

            //oSheet.Shapes.AddPicture(clamp.Barcode, MsoTriState.msoTrue, MsoTriState.msoTrue, 5, 12, 131, 40);

            oSheet.Cells[9, 3] = clamp.Controldate;
            oSheet.Cells[10, 3] = clamp.Controldate;

            oSheet.Cells[9, 7] = clamp.Rn;

            //if (clamp.Clamptype.Contains("WH1 - 15V") || clamp.Clamptype.Contains("WH2 - 15V")) oSheet.Cells[14, 3] = "15V";
            //else oSheet.Cells[14, 2] = "24V";

            oSheet.Cells[12, 2] = clamp.Serialnum;
            oSheet.Cells[12, 3] = clamp.Rado;

            //if (clamp.Rfcablerror.Contains("-"))
            //{
            //    oSheet.Cells[19, 3] = clamp.Rfcable;
            //    oSheet.Cells[19, 4] = "-";
            //    oSheet.Cells[19, 5] = "-";
            //}
            //else
            //{
            //    oSheet.Cells[19, 3] = "-";
            //    oSheet.Cells[19, 4] = clamp.Rfcable;
            //    oSheet.Cells[19, 5] = clamp.Rfcablerror;
            //}

            if (clamp.Solenoidcablerror.Contains("-"))
            {
                oSheet.Cells[19, 3] = clamp.Solenoidcable;
                oSheet.Cells[19, 4] = "-";
                oSheet.Cells[19, 5] = "-";
            }
            else
            {
                oSheet.Cells[19, 3] = "-";
                oSheet.Cells[19, 4] = clamp.Solenoidcable;
                oSheet.Cells[19, 5] = clamp.Solenoidcablerror;
            }

            //if (clamp.Smberror.Contains("-"))
            //{
            //    oSheet.Cells[21, 3] = clamp.Smb;
            //    oSheet.Cells[21, 4] = "-";
            //    oSheet.Cells[21, 5] = "-";
            //}
            //else
            //{
            //    oSheet.Cells[21, 3] = "-";
            //    oSheet.Cells[21, 4] = clamp.Smb;
            //    oSheet.Cells[21, 5] = clamp.Smberror;
            //}

            if (clamp.Armaturerror.Contains("-"))
            {
                oSheet.Cells[20, 3] = clamp.Armature;
                oSheet.Cells[20, 4] = "-";
                oSheet.Cells[20, 5] = "-";
            }
            else
            {
                oSheet.Cells[20, 3] = "-";
                oSheet.Cells[20, 4] = clamp.Armature;
                oSheet.Cells[20, 5] = clamp.Armaturerror;
            }

            if (clamp.Edgeserror.Contains("-"))
            {
                oSheet.Cells[21, 3] = clamp.Edges;
                oSheet.Cells[21, 4] = "-";
                oSheet.Cells[21, 5] = "-";
            }
            else
            {
                oSheet.Cells[21, 3] = "-";
                oSheet.Cells[21, 4] = clamp.Edges;
                oSheet.Cells[21, 5] = clamp.Edgeserror;
            }

            if (clamp.Paralelityerror.Contains("-"))
            {
                oSheet.Cells[25, 3] = clamp.Paralelity;
                oSheet.Cells[25, 4] = "-";
                oSheet.Cells[25, 5] = clamp.Paralelityerror;
            }
            else
            {
                oSheet.Cells[25, 3] = "-";
                oSheet.Cells[25, 4] = clamp.Paralelity;
                oSheet.Cells[25, 5] = clamp.Paralelityerror;
            }

            if (clamp.Distancerror.Contains("-"))
            {
                oSheet.Cells[26, 3] = clamp.Distance;
                oSheet.Cells[26, 4] = "-";
                oSheet.Cells[26, 5] = clamp.Distancerror;
            }
            else
            {
                oSheet.Cells[26, 3] = "-";
                oSheet.Cells[26, 4] = clamp.Distance;
                oSheet.Cells[26, 5] = clamp.Distancerror;
            }
            //oSheet.Cells[29, 3] = clamp.Rflenght;
            //oSheet.Cells[29, 5] = clamp.Rflenghterror;

            oSheet.Cells[27, 3] = clamp.Solenoidlenght;
            oSheet.Cells[27, 5] = clamp.Solenoidlenghterror;

            oSheet.Cells[28, 3] = clamp.Electrodes;
            oSheet.Cells[28, 5] = clamp.Electrodeserror;

            oSheet.Cells[32, 3] = clamp.Smallresistance;
            oSheet.Cells[32, 5] = clamp.Smallresistancerror;

            oSheet.Cells[33, 3] = clamp.Bigresistance;
            oSheet.Cells[33, 5] = clamp.Bigresistancerror;

            oSheet.Cells[34, 3] = clamp.Shortcircuit;
            oSheet.Cells[34, 5] = clamp.Shortcircuiterror;

            /*oSheet.Cells[41, 2] = clamp.Emptythick;
            oSheet.Cells[41, 5] = clamp.Emptythickerror;

            oSheet.Cells[42, 2] = clamp.Fullthick;
            oSheet.Cells[42, 5] = clamp.Fullthickerror;

            oSheet.Cells[43, 2] = clamp.Emptythin;
            oSheet.Cells[43, 5] = clamp.Emptythinerror;

            oSheet.Cells[44, 2] = clamp.Fullthin;
            oSheet.Cells[44, 5] = clamp.Fullthinerror;*/
        }
    }
}
