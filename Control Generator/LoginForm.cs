using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Control_Generator
{
    public partial class LoginForm : Form
    {
        //List<User> list;
        bool login;
        bool credentials;
        bool empty;
        bool connection;
        bool advanced;
        bool basic;

        public LoginForm()
        {
            InitializeComponent();
            SlidePanel2.Hide();

            //list = UserConnectionClass.SelectUser(Program.userLoginQuery);

            textBox1.ForeColor = SystemColors.GrayText;
            textBox1.Text = "Username";

            textBox2.ForeColor = SystemColors.GrayText;
            textBox2.Text = "Password";

            //foreach (User l in list) Debug.Write(l);

            login = false;
            credentials = false;
            empty = false;
            connection = false;
            advanced = false;
            basic = false;

            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += BgWorker_Finished;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ChangePasswordForm fpf = new ChangePasswordForm();
            fpf.ShowDialog();
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            ForgotPassForm fpf = new ForgotPassForm();
            fpf.ShowDialog();
            this.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            SlidePanel2.Hide();
            panel5.BackColor = Color.WhiteSmoke;

            panel2.BackColor = Color.Gainsboro;
            SlidePanel.Show();

            textBox1.Clear();
            textBox1.ForeColor = SystemColors.WindowText;
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = "Password";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            SlidePanel.Hide();
            panel2.BackColor = Color.WhiteSmoke;

            panel5.BackColor = Color.Gainsboro;
            SlidePanel2.Show();

            textBox2.Clear();
            textBox2.ForeColor = SystemColors.WindowText;
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = "Username";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblStatus.ForeColor = Color.Red;
            lblStatus.Visible = true;
            lblStatus.Text = "Please wait...";
            bgWorker.WorkerReportsProgress = true;
            bgWorker.RunWorkerAsync();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Visible = true;
                lblStatus.Text = "Please wait...";
                bgWorker.WorkerReportsProgress = true;
                bgWorker.RunWorkerAsync();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Visible = true;
                lblStatus.Text = "Please wait...";
                bgWorker.WorkerReportsProgress = true;
                bgWorker.RunWorkerAsync();
            }
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            login = Login();
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStatus.Text = "Please wait...";
            lblStatus.ForeColor = Color.Red;
        }

        private void BgWorker_Finished(object sender, RunWorkerCompletedEventArgs e)
        {
            if (login)
            {
                if (advanced)
                {
                    this.Hide();
                    BasicForm bf = new BasicForm();
                    bf.HiddenButtons = false;
                    bf.ShowDialog();
                    this.Close();

                    advanced = false;
                    credentials = false;
                    connection = false;
                    empty = false;
                }
                else if (basic)
                {
                    this.Hide();
                    BasicForm bf = new BasicForm();
                    bf.HiddenButtons = true;
                    bf.ShowDialog();
                    this.Close();

                    advanced = false;
                    credentials = false;
                    connection = false;
                    empty = false;
                }
                lblStatus.ForeColor = Color.Green;
                lblStatus.Text = "Done";
            }
            else
            {
                if (connection)
                {
                    MessageBox.Show("Connection problems: Unable to log in");

                    advanced = false;
                    credentials = false;
                    connection = false;
                    empty = false;
                }
                if (credentials)
                {
                    textBox1.Clear();
                    textBox2.Clear();

                    MessageBox.Show("Incorrect credentials");

                    textBox2.ForeColor = SystemColors.GrayText;
                    textBox2.Text = "Password";

                    textBox1.ForeColor = SystemColors.GrayText;
                    textBox1.Text = "Username";

                    advanced = false;
                    credentials = false;
                    connection = false;
                    empty = false;
                }
                if (empty)
                {
                    MessageBox.Show("Please fill in your credentials");

                    advanced = false;
                    credentials = false;
                    connection = false;
                    empty = false;
                }

                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "Error";
            }
        }

        //hendlovanje kada nema interneta
        private bool Login()
        {
            bool result = false;

            try
            {
                if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
                {
                    User newuser = new User()
                    {
                        Username = textBox1.Text,
                        Password = textBox2.Text
                    };

                    User login = UserOperations.SelectUser(newuser);

                    if (login.DataV)
                    {
                        if (login.Role.Equals(2))
                        {
                            advanced = true;
                            basic = false;
                            credentials = false;
                            connection = false;
                            empty = false;
                            result = true;
                        }
                        else if (login.Role.Equals(1))
                        {
                            basic = true;
                            advanced = false;
                            credentials = false;
                            connection = false;
                            empty = false;
                            result = true;
                        }
                    }
                    else
                    {
                        basic = false;
                        advanced = false;
                        credentials = true;
                        connection = false;
                        empty = false;
                        result = false;
                    }
                }

                else
                {
                    basic = false;
                    advanced = false;
                    credentials = false;
                    connection = false;
                    empty = true;
                    result = false;
                }
            }
            catch (Exception ex)
            {
                basic = false;
                advanced = false;
                credentials = false;
                connection = true;
                empty = false;
                result = false;
            }

            return result;
        }
    }
}
