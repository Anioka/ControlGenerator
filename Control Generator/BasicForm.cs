using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.IO;
using System.Configuration;

namespace Control_Generator
{
    public partial class BasicForm : Form
    {
        FillInForm ff;
        
        object[] clamps = { "WH1 - 15V", "WH1 - 24V", "WH2 - 15V", "WH2 - 24V", "PC" };
        
        Clamp clamp;
        Clamp basicData;
        Clamp errorClamp;

        bool resultDB, resultEX;

        string save;
        string direktorijum = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        string filepath;

        public BasicForm()
        {
            InitializeComponent();
            //butStat.Hide();
            //save = @"\\10.0.140.11\share\NCPR";

            save = ConfigurationManager.AppSettings["DirectoryFilepath"];

            cbClampType.Items.AddRange(clamps);

            ff = new FillInForm();
            clamp = new Clamp();
            basicData = new Clamp();
            errorClamp = new Clamp();

            filepath = "";

            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {
            if (HiddenButtons)
            {
                butAdvanced.Hide();
                butDel.Dock = DockStyle.Top;
            }
            clamp = ClampOperations.ReturnClamp();
            tbSerial.Text = clamp.Serialnum;
            tbRado.Text = clamp.Rado;
            tbRN.Text = clamp.Rn;

            textBox3.Text = clamp.Serialnum;
            textBox4.Text = clamp.Rado;

            NewForm(ff);
        }

        private void butExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void butMinimise_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void butFC_Click(object sender, EventArgs e)
        {
            NewForm(ff);
            //panel5.Show();
            butAdd.Enabled = true;
            butAdd.BackColor = Color.LightGreen;
        }

        private void butAdvanced_Click(object sender, EventArgs e)
        {
            ////panel5.Hide();
            NewForm(new AdvancedForm(lblStatus));
            butAdd.Enabled = false;
            butAdd.BackColor = Color.Maroon;
        }

        private void butDel_Click(object sender, EventArgs e)
        {
            Clamp clamp = new Clamp()
            {
                Serialnum = tbSerial.Text
            };
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete?", "Delete last added", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                bool result = ClampOperations.DeleteClamp(clamp);

                if (result)
                {
                    MessageBox.Show("Success");
                }
                else
                {
                    MessageBox.Show("Could not delete clamp");
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }

        private void butLog_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm lf = new LoginForm();
            lf.ShowDialog();
            this.Close();
        }
        
        private void butAdd_Click(object sender, EventArgs e)
        {
            errorClamp = ff.NewUpdate(BasicClampData());

            //progressBar.Maximum = 100;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Visible = true;
            lblStatus.Text = "Please wait...";
            bgWorker.WorkerReportsProgress = true;
            bgWorker.RunWorkerAsync();
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (!errorClamp.Clamptype.Contains("PC"))
                {
                    if (!Directory.Exists(save + @"\Final Control")) Directory.CreateDirectory(save + @"\Final Control");
                    string dest = save + @"\Final Control";
                    string destfile = "FC " + DateTime.Now.ToString("dd-MM-yyyy") + " " + textBox3.Text + " " + textBox4.Text + ".xlsx";

                    File.Copy(Path.Combine(direktorijum, "final control.xlsx"), Path.Combine(dest, destfile), true);

                    string editfile = dest + "\\" + destfile;

                    resultDB = ClampOperations.InsertClamp(errorClamp);
                    //ExcellClass.crateFinalControl(@"C:\Users\AleksandraTos\Documents\Visual Studio 2008\Projects\Control Generator\bin\Debug\final control.xlsx", @"Desktop\final control.xlsx", errorClamp);
                    resultEX = ExcellClass.crateFinalControl(editfile, errorClamp);
                }
                else 
                {
                    if (!Directory.Exists(save + @"\Final Control")) Directory.CreateDirectory(save + @"\Final Control");
                    string dest = save + @"\Final Control";
                    string destfile = "FC " + DateTime.Now.ToString("dd-MM-yyyy") + " " + textBox3.Text + " " + textBox4.Text + ".xlsx";

                    File.Copy(Path.Combine(direktorijum, "final control PC.xlsx"), Path.Combine(dest, destfile), true);

                    string editfile = dest + "\\" + destfile;

                    resultDB = ClampOperations.InsertClamp(errorClamp);
                    //ExcellClass.crateFinalControl(@"C:\Users\AleksandraTos\Documents\Visual Studio 2008\Projects\Control Generator\bin\Debug\final control.xlsx", @"Desktop\final control.xlsx", errorClamp);
                    resultEX = ExcellClass.crateFinalControl(editfile, errorClamp);
                }
            }
            catch(Exception ex)
            {
                string newfolder = direktorijum + "\\Logs";
                if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                newfolder += "\\log1" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                StreamWriter sw = new StreamWriter(newfolder);
                sw.Write(ex.ToString());
                sw.Close();
            }
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (resultDB && resultEX)
            {
                MessageBox.Show("Success");
                Autoincrement();
                tbSerial.Text = basicData.Serialnum;
                tbRado.Text = basicData.Rado;
                tbRN.Text = basicData.Rn;

                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Done";
            }
            else
            {
                if (!String.IsNullOrEmpty(errorClamp.Clampimputerror))
                    MessageBox.Show("Please fill in the full form before continuing. The following values are missing: " + errorClamp.Clampimputerror);

                else if (!resultDB) MessageBox.Show("Database error: Could not insert clamp.");
                else if (!resultEX) MessageBox.Show("Office error: Could not create excel file.");

                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Error";
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //progressBar.Value = e.ProgressPercentage;
            lblStatus.Text = "Please wait...";
            lblStatus.ForeColor = Color.Red;
        }

 
        private void NewForm(object form)
        {
            if (this.panelMain.Controls.Count > 0)
                this.panelMain.Controls.RemoveAt(0);
            Form fh = form as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panelMain.Controls.Add(fh);
            this.panelMain.Tag = fh;
            fh.Show();
        }

        private Clamp BasicClampData()
        {
            basicData.Clampimputerror = "";

            if (!String.IsNullOrEmpty(filepath) && !String.IsNullOrEmpty(textBox3.Text) && !String.IsNullOrEmpty(textBox4.Text) && !String.IsNullOrEmpty(textBox2.Text) && /*!String.IsNullOrEmpty(label31.Text) &&*/ cbClampType.SelectedIndex != -1)
            {
                basicData.Clamptype = cbClampType.SelectedItem.ToString();
                basicData.Serialnum = textBox3.Text;
                basicData.Rado = textBox4.Text;
                basicData.Rn = textBox2.Text;
                basicData.Barcode = filepath;
                basicData.Controldate = dtDate.Value.ToString("yyyy-MM-dd");
            }
            else basicData.Clampimputerror += " basic data";

            return basicData;
        }

        public bool HiddenButtons
        { get; set; }

        private void cbClampType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ff.ItemDisabled(cbClampType.SelectedIndex);
        }

        private void Autoincrement()
        {
            int num = 0;

            if (checkBoxAI.Checked)
            {
                try
                {
                    Int32.TryParse(textBox4.Text, out num);
                    num += 1;
                    textBox4.Text = num.ToString();
                    
                    num = 0;
                    string substr = textBox3.Text.Substring(textBox3.Text.Length - 3); //uzmi zadnja tri broja
                    string subnum = textBox3.Text.Remove(textBox3.Text.Length - 3); //skloni zadnja tri brija da mogu da ih zamenim na kraju
                    Int32.TryParse(substr, out num); //pretvori string u Int
                    num += 1; //inkrementiraj za jedan

                    if (num < 10)
                    {
                        textBox3.Text = subnum + 0 + 0 + num.ToString();
                    }
                    else if (num < 100)
                    {
                        textBox3.Text = subnum + 0 + num.ToString();

                    }
                    else
                    {
                        textBox3.Text = subnum + num.ToString();
                    }
                }
                catch (Exception ex)
                {
                    string newfolder = direktorijum + "\\Logs";
                    if (!Directory.Exists(newfolder)) Directory.CreateDirectory(newfolder);
                    newfolder += "\\log2" + DateTime.Now.ToString("dd_MM_yyyy hh_mm_ss") + ".txt";
                    StreamWriter sw = new StreamWriter(newfolder);
                    sw.Write(ex.ToString());
                    sw.Close();
                }
            }
        }

        private void butUpload_Click(object sender, EventArgs e)
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.bmp; *.png)|*.jpg; *.jpeg; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pictureBox1.Image = new Bitmap(open.FileName);
                //display name of the file
                label31.Text = open.SafeFileName;

                filepath = open.FileName;

                //Debug.Write(filepath);
            }
        }
    }
}
