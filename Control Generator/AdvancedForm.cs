using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace Control_Generator
{
    public partial class AdvancedForm : Form
    {
        List<User> list;
        List<string> paramPL;
        List<string> paramFCR;

        bool resultPL;
        bool resultFCR;
        bool buttonpressedPL;
        bool buttonpressedFCR;

        string save;
        string direktorijum = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        Label status;

        public AdvancedForm(Label lbl)
        {
            InitializeComponent();

            //save = @"\\10.0.140.11\share\NCPR";

            save = ConfigurationManager.AppSettings["DirectoryFilepath"];

            paramPL = new List<string>();
            paramFCR = new List<string>();
            list = new List<User>();

            resultFCR = false;
            resultPL = false;
            buttonpressedPL = false;
            buttonpressedFCR = false;

            status = lbl;

            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        private void butCreatePL_Click(object sender, EventArgs e)
        {
            paramPL.Add(dateTimePicker2.Value.ToString("dd-MM-yyyy"));
            paramPL.Add(dateTimePicker3.Value.ToString("dd-MM-yyyy"));
            paramPL.Add(textBox5.Text);

            buttonpressedPL = true;

            status.ForeColor = Color.Red;
            status.Visible = true;
            status.Text = "Please wait...";
            
            bgWorker.WorkerReportsProgress = true;
            bgWorker.RunWorkerAsync();

            //ovo ce morati da se napravi u background workeru...procackaj u ponedeljak
        }

        private void butCreateFCR_Click(object sender, EventArgs e)
        {
            buttonpressedFCR = true;

            status.ForeColor = Color.Red;
            status.Visible = true;
            status.Text = "Please wait...";

            bgWorker.WorkerReportsProgress = true;
            bgWorker.RunWorkerAsync();
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<Clamp> counter = new List<Clamp>();
            if (buttonpressedPL && !buttonpressedFCR)
            {
                counter = ClampOperations.ReturnClampPackingList(textBox3.Text, textBox4.Text);
                if (counter.Count > 0)
                {
                    if (!Directory.Exists(save + @"\Packing List")) Directory.CreateDirectory(save + @"\Packing List");
                    string dest = save + @"\Packing List";
                    string destfile = "PL " + DateTime.Now.ToString("dd-MM-yyyy -- HH-mm-ss") + ".xlsx";

                    File.Copy(Path.Combine(direktorijum, "packing list.xlsx"), Path.Combine(dest, destfile), true);

                    string editfile = dest + "\\" + destfile;

                    resultPL = ExcellClass.createPackingList(editfile, counter, paramPL, ClampOperations.ReturnClampRN(textBox3.Text, textBox4.Text));

                    buttonpressedPL = false;

                    Process process = new Process();
                    process = Process.Start(editfile);
                }
                else resultPL = false;
            }
            if (buttonpressedFCR && !buttonpressedPL)
            {
                counter = ClampOperations.ReturnClampFCR(textBox1.Text, textBox2.Text);
                if (counter.Count > 0)
                {
                    if (!Directory.Exists(save + @"\Final Control Report")) Directory.CreateDirectory(save + @"\Final Control Report");
                    string dest = save + @"\Final Control Report";
                    string destfile = "FCR " + DateTime.Now.ToString("dd-MM-yyyy -- HH-mm-ss") + ".xlsx";

                    File.Copy(Path.Combine(direktorijum, "final control report.xlsx"), Path.Combine(dest, destfile), true);

                    string editfile = dest + "\\" + destfile;

                    resultFCR = ExcellClass.createFinalControlReport(editfile, counter, dateTimePicker1.Value.ToString("dd-MM-yyyy"), ClampOperations.ReturnClampRN(textBox1.Text, textBox2.Text));

                    buttonpressedFCR = false;

                    Process process = new Process();
                    process = Process.Start(editfile);
                }
                else resultFCR = false;
            }
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (resultPL)
            {
                MessageBox.Show("Success");

                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                 
                status.ForeColor = Color.Green;
                status.Text = "Done";

                resultPL = false;
            }
            else if (resultFCR)
            {
                MessageBox.Show("Success");
                
                textBox1.Clear();
                textBox2.Clear();

                status.ForeColor = Color.Green;
                status.Text = "Done";

                resultFCR = false;
            }
            else
            {
                MessageBox.Show("Error");

                status.ForeColor = Color.Red;
                status.Text = "Error";
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            status.Text = "Please wait...";
            status.ForeColor = Color.Red;
        }

        private void butSerialUpdate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox6.Text) && !String.IsNullOrEmpty(textBox7.Text))
            {
                status.Text = "Please wait...";
                status.ForeColor = Color.Red;
                status.Visible = true;

                Clamp clamp = new Clamp()
                {
                    Rado = textBox7.Text,
                    Serialnum = textBox6.Text
                };
                bool result = ClampOperations.UpdateClampBarcode(clamp);
                
                if (result)
                {
                    MessageBox.Show("Success");
                    textBox6.Clear();
                    textBox7.Clear();

                    status.ForeColor = Color.Green;
                    status.Text = "Done";
                }
                else
                {
                    MessageBox.Show("Could not update your barcode");

                    status.ForeColor = Color.Red;
                    status.Text = "Error";
                }
            }
            else MessageBox.Show("Please fill in all data");
        }

        private void butUserUpdate_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox10.Text) && !String.IsNullOrEmpty(textBox9.Text))
            {
                status.Text = "Please wait...";
                status.ForeColor = Color.Red;
                status.Visible = true;

                User user = new User()
                {
                    Mail = textBox10.Text,
                    Username = textBox9.Text
                };
                bool result = UserOperations.UpdateUsername(user);

                if (result)
                {
                    MessageBox.Show("Success");
                    textBox10.Clear();
                    textBox9.Clear();
                    textBox9.Clear();

                    status.ForeColor = Color.Green;
                    status.Text = "Done";
                }
                else
                {
                    MessageBox.Show("Could not update your username");

                    status.ForeColor = Color.Red;
                    status.Text = "Error";
                }
            }
            else MessageBox.Show("Please fill in your credentials");
        }

        private void butAddUser_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox10.Text) && !String.IsNullOrEmpty(textBox9.Text) && !String.IsNullOrEmpty(textBox8.Text))
            {
                status.Text = "Please wait...";
                status.ForeColor = Color.Red;
                status.Visible = true; 

                int rules = 1;
                if (cbRights.Checked) rules = 2;

                User user = new User
                {
                    Username = textBox9.Text,
                    Password = textBox8.Text,
                    Mail = textBox10.Text,
                    Role = rules
                };

                bool success = UserOperations.InsertUser(user);
                if (success)
                {
                    MessageBox.Show("Success");
                    textBox10.Clear();
                    textBox9.Clear();
                    textBox8.Clear();
                    cbRights.Checked = false;

                    status.ForeColor = Color.Green;
                    status.Text = "Done";
                }
                else
                {
                    MessageBox.Show("Unable to create user");

                    status.ForeColor = Color.Red;
                    status.Text = "Error";
                }
            }
            else MessageBox.Show("Please fill in credentials");
        }

        private void butDelUser_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox9.Text) && !String.IsNullOrEmpty(textBox10.Text))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this user?", "Delete user", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    status.Text = "Please wait...";
                    status.ForeColor = Color.Red;
                    status.Visible = true;

                    User user = new User()
                    {
                        Username = textBox9.Text,
                        Mail = textBox10.Text,
                        Password = textBox8.Text
                    };
                    bool result = UserOperations.DeleteUser(user);

                    if (result)
                    {
                        MessageBox.Show("Success");

                        textBox10.Clear();
                        textBox9.Clear();
                        textBox8.Clear();

                        status.ForeColor = Color.Green;
                        status.Text = "Done";
                    }
                    else
                    {
                        MessageBox.Show("Could not delete user");

                        status.ForeColor = Color.Red;
                        status.Text = "Error";
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
            else MessageBox.Show("Please fill in your credentials");
        }
    }
}
