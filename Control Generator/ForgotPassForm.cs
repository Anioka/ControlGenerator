using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Control_Generator
{
    public partial class ForgotPassForm : Form
    {
        //its not going to work like this
        bool mailStatus;
        //List<User> list;
        public ForgotPassForm()
        {
            InitializeComponent();

            textBox1.ForeColor = SystemColors.GrayText;
            textBox1.Text = "E-mail";

            textBox2.ForeColor = SystemColors.GrayText;
            textBox2.Text = "Username";

            SlidePanel2.Hide();

            mailStatus = false;

            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += BgWorker_Finished;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            SlidePanel2.Hide();
            panel6.BackColor = Color.WhiteSmoke;

            panel4.BackColor = Color.Gainsboro;
            SlidePanel.Show();

            textBox1.Clear();
            textBox1.ForeColor = SystemColors.WindowText;
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = "Username";
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            SlidePanel.Hide();
            panel4.BackColor = Color.WhiteSmoke;

            panel6.BackColor = Color.Gainsboro;
            SlidePanel2.Show();

            textBox2.Clear();
            textBox2.ForeColor = SystemColors.WindowText;
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = "E-mail";
            }
        }

        private void butSend_Click(object sender, EventArgs e)
        {
            lblStatus.ForeColor = Color.Red;
            lblStatus.Visible = true;
            lblStatus.Text = "Please wait...";
            bgWorker.WorkerReportsProgress = true;
            bgWorker.RunWorkerAsync();
        }

        private void BgWorker_DoWork(object s, DoWorkEventArgs e)
        {
            mailStatus = SendMail(textBox1.Text);
        }

        private void BgWorker_ProgressChanged(object s, ProgressChangedEventArgs e)
        {
            lblStatus.ForeColor = Color.Red;
            lblStatus.Visible = true;
            lblStatus.Text = "Please wait...";
        }

        private void BgWorker_Finished(object s, RunWorkerCompletedEventArgs e)
        {
            //MessageBox.Show("mail Send");
            if (mailStatus)
            {
                MessageBox.Show("E-mail with your password has been sent");
                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = "E-mail";

                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = "Username";

                SlidePanel.Show();
                SlidePanel2.Hide();

                panel4.BackColor = Color.Gainsboro;
                panel6.BackColor = Color.WhiteSmoke;


                lblStatus.ForeColor = Color.Green;
                lblStatus.Visible = true;
                lblStatus.Text = "Mail sent";
            }
            else
            {
                MessageBox.Show("Something went wrong while recovering the password");
                textBox1.ForeColor = SystemColors.GrayText;
                textBox1.Text = "E-mail";

                textBox2.ForeColor = SystemColors.GrayText;
                textBox2.Text = "Username";

                SlidePanel.Show();
                SlidePanel2.Hide();

                panel4.BackColor = Color.Gainsboro;
                panel6.BackColor = Color.WhiteSmoke;

                lblStatus.ForeColor = Color.Red;
                lblStatus.Visible = true;
                lblStatus.Text = "Error";
            }
        }

        private bool SendMail(string recipient)
        {
            //list = UserConnectionClass.SelectUser(Program.userLoginQuery);

            bool status = false;
            try
            {
                if (!String.IsNullOrEmpty(textBox1.Text) && !String.IsNullOrEmpty(textBox2.Text))
                {
                    User newuser = new User()
                    {
                        Username = textBox2.Text,
                        Mail = textBox1.Text
                    };

                    User u = UserOperations.SelectUserMail(newuser);

                    SmtpClient SmtpServer = new SmtpClient("mail.lmbsoft.com");
                    MailMessage mail = new MailMessage();

                    mail.From = new MailAddress("aleksandra.tosic@lmbsoft.com");
                    mail.To.Add(recipient);
                    mail.Subject = "Password recovery";
                    mail.Body = "Your password is: " + u.Password;

                    SmtpServer.Port = 465;
                    ServicePointManager.ServerCertificateValidationCallback = delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                    SmtpServer.Credentials = new System.Net.NetworkCredential("aleksandra.tosic@lmbsoft.com", "Lmb#123");
                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);

                    //MessageBox.Show("E-mail with your password has been sent");
                    status = true;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
                //MessageBox.Show("Something went wrong while recovering the password");
                status = false;
            }

            return status;
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
