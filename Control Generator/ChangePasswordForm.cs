using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Control_Generator
{
    public partial class ChangePasswordForm : Form
    {
        List<User> list;
        bool result;

        public ChangePasswordForm()
        {
            InitializeComponent();

            textBox1.ForeColor = SystemColors.GrayText;
            textBox1.Text = "Username";

            textBox2.ForeColor = SystemColors.GrayText;
            textBox2.Text = "Old password";

            textBox3.ForeColor = SystemColors.GrayText;
            textBox3.Text = "New password";

            SlidePanel2.Hide();
            SlidePanel3.Hide();

            list = new List<User>();

            result = false;

            bgWorker.DoWork += bgWorker_DoWork;
            bgWorker.ProgressChanged += bgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += bgWorker_RunWorkerCompleted;
        }
                                
        private void butExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            SlidePanel2.Hide();
            SlidePanel3.Hide();
            panel6.BackColor = Color.WhiteSmoke;
            panel2.BackColor = Color.WhiteSmoke;
            textBox1.Clear();
            
            panel4.BackColor = Color.Gainsboro;
            SlidePanel.Show();

            textBox1.Clear();
            textBox1.ForeColor = SystemColors.WindowText;
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = "Old password";
            }
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.ForeColor = SystemColors.GrayText;
                textBox3.Text = "New password";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            SlidePanel.Hide();
            SlidePanel3.Hide();
            panel4.BackColor = Color.WhiteSmoke;
            panel2.BackColor = Color.WhiteSmoke;
            textBox2.Clear();
            
            panel6.BackColor = Color.Gainsboro;
            SlidePanel2.Show();

            textBox2.Clear();
            textBox2.ForeColor = SystemColors.WindowText;
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = "Username";
            }
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                textBox3.ForeColor = SystemColors.GrayText;
                textBox3.Text = "New password";
            }
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            SlidePanel.Hide();
            SlidePanel2.Hide();
            panel4.BackColor = Color.WhiteSmoke;
            panel6.BackColor = Color.WhiteSmoke;
            textBox3.Clear();
            
            panel2.BackColor = Color.Gainsboro;
            SlidePanel3.Show();
            
            textBox3.Clear();
            textBox3.ForeColor = SystemColors.WindowText;
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = "Old password";
            }
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = "Username";
            }
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (result)
            {
                MessageBox.Show("Success");

                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = "Username";

                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = "Old password";

                textBox3.ForeColor = SystemColors.GrayText;
                textBox3.Text = "New password";

                SlidePanel.Show();
                SlidePanel2.Hide();
                SlidePanel3.Hide();
                panel4.BackColor = Color.Gainsboro;
                panel6.BackColor = Color.WhiteSmoke;
                panel2.BackColor = Color.WhiteSmoke;

                lblStatus.ForeColor = Color.Green;
                lblStatus.Visible = true;
                lblStatus.Text = "Done";
            }
            else
            {
                MessageBox.Show("Could not update your password");

                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = "Username";

                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = "Old password";

                textBox3.ForeColor = SystemColors.GrayText;
                textBox3.Text = "New password";

                SlidePanel.Show();
                SlidePanel2.Hide();
                SlidePanel3.Hide();
                panel4.BackColor = Color.Gainsboro;
                panel6.BackColor = Color.WhiteSmoke;
                panel2.BackColor = Color.WhiteSmoke;

                lblStatus.ForeColor = Color.Red;
                lblStatus.Visible = true;
                lblStatus.Text = "Error";
            }
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblStatus.ForeColor = Color.Red;
            lblStatus.Visible = true;
            lblStatus.Text = "Please wait...";
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            User user = new User()
            {
                Username = textBox1.Text,
                Password = textBox2.Text
            };

            result = UserOperations.UpdatePassword(user, textBox3.Text);
        }

        private void butChange_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text) && !String.IsNullOrEmpty(textBox3.Text))
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Visible = true;
                lblStatus.Text = "Please wait...";
                bgWorker.WorkerReportsProgress = true;
                bgWorker.RunWorkerAsync();
            }
            else MessageBox.Show("Please fill in your credentials");
        }

        private void butBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm lf = new LoginForm();
            lf.ShowDialog();
            this.Close();
        }
    }
}
