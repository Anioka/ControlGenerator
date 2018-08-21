using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Control_Generator
{
    public partial class FillInForm : Form
    {
        public FillInForm()
        {
            InitializeComponent();

            tabPage5.Enabled = false;
        }

        public void ItemDisabled(int index)
        {
            if (index == 1 || index == 3)
            {
                tabPage5.Enabled = false;
                tabPage4.Enabled = true;

                richTextBox24.Enabled = true;
                richTextBox19.Enabled = true;
                richTextBox23.Enabled = false;
                richTextBox18.Enabled = false;

                label19.Enabled = true;
                checkBox1.Enabled = true;
                checkBox6.Enabled = true;

                label13.Enabled = true;
                checkBox3.Enabled = true;
                checkBox8.Enabled = true;

                label28.Enabled = true;
                checkBox20.Enabled = true;
                checkBox19.Enabled = true;

                label24.Enabled = true;
                richTextBox10.Enabled = true;
                richTextBox15.Enabled = true;

                label21.Enabled = true;
                checkBox21.Enabled = true;
                checkBox23.Enabled = true;

                label22.Enabled = true;
                checkBox22.Enabled = true;
                checkBox24.Enabled = true;
            }
            else if (index == 0 || index == 2)
            {
                tabPage5.Enabled = false;
                tabPage4.Enabled = true;

                richTextBox23.Enabled = true;
                richTextBox18.Enabled = true;
                richTextBox24.Enabled = false;
                richTextBox19.Enabled = false;

                label19.Enabled = true;
                checkBox1.Enabled = true;
                checkBox6.Enabled = true;

                label13.Enabled = true;
                checkBox3.Enabled = true;
                checkBox8.Enabled = true;

                label28.Enabled = true;
                checkBox20.Enabled = true;
                checkBox19.Enabled = true;

                label24.Enabled = true;
                richTextBox10.Enabled = true;
                richTextBox15.Enabled = true;

                label21.Enabled = true;
                checkBox21.Enabled = true;
                checkBox23.Enabled = true;

                label22.Enabled = true;
                checkBox22.Enabled = true;
                checkBox24.Enabled = true;
            }
            else
            {
                tabPage5.Enabled = true;
                tabPage4.Enabled = false;

                label19.Enabled = false;
                checkBox1.Enabled = false;
                checkBox6.Enabled = false;
                richTextBox1.Enabled = false;

                label13.Enabled = false;
                checkBox3.Enabled = false;
                checkBox8.Enabled = false;
                richTextBox6.Enabled = false;

                label28.Enabled = false;
                checkBox20.Enabled = false;
                checkBox19.Enabled = false;
                richTextBox11.Enabled = false;

                label24.Enabled = false;
                richTextBox10.Enabled = false;
                richTextBox15.Enabled = false;

                label21.Enabled = false;
                checkBox21.Enabled = false;
                checkBox23.Enabled = false;
                richTextBox12.Enabled = false;

                label22.Enabled = false;
                checkBox22.Enabled = false;
                checkBox24.Enabled = false;
                richTextBox13.Enabled = false;

                richTextBox24.Enabled = true;
                richTextBox19.Enabled = true;
                richTextBox23.Enabled = true;
                richTextBox18.Enabled = true;
            }
        }

        public Color CBIndex { get; set; }

        public Clamp NewUpdate(Clamp clamp) //TODO: uzmi u obzir da svako polje treba da ti bude popunjeno, nemoj da zaboravis da ispitas to
        {
            if (checkBox1.Enabled)
            {
                //Debug.Write(clamp);
                if (checkBox1.Checked)
                {
                    clamp.Rfcable = "OK";
                    clamp.Rfcablerror = "-";
                }
                else if (checkBox6.Checked)
                {
                    clamp.Rfcable = "NOT OK";
                    clamp.Rfcablerror = richTextBox1.Text;
                }
                else clamp.Clampimputerror += " RF cable";
            }
            else
            {
                clamp.Rfcable = "-";
                clamp.Rfcablerror = "-";
            }
            if (checkBox2.Checked)
            {
                clamp.Solenoidcable = "OK";
                clamp.Solenoidcablerror = "-";
            }
            else if (checkBox7.Checked)
            {
                clamp.Solenoidcable = "NOT OK";
                clamp.Solenoidcablerror = richTextBox2.Text;
            }
            else clamp.Clampimputerror += " solenoid cable";
            if (checkBox3.Enabled)
            {
                if (checkBox3.Checked)
                {
                    clamp.Smb = "OK";
                    clamp.Smberror = "-";
                }
                else if (checkBox8.Checked)
                {
                    clamp.Smb = "NOT OK";
                    clamp.Smberror = richTextBox6.Text;
                }
                else clamp.Clampimputerror += " SMB";
            }
            else
            {
                clamp.Smb = "-";
                clamp.Smberror = "-";
            }
            if (checkBox4.Checked)
            {
                clamp.Armature = "OK";
                clamp.Armaturerror = "-";
            }
            else if (checkBox9.Checked)
            {
                clamp.Armature = "NOT OK";
                clamp.Armaturerror = richTextBox3.Text;
            }
            else clamp.Clampimputerror += " armature";
            if (checkBox5.Checked)
            {
                clamp.Edges = "OK";
                clamp.Edgeserror = "-";
            }
            else if (checkBox10.Checked)
            {
                clamp.Edges = "NOT OK";
                clamp.Edgeserror = richTextBox4.Text;
            }
            else clamp.Clampimputerror += " edges";
            if (checkBox20.Enabled)
            {
                if (checkBox20.Checked) //if (!clamp.Clamtype = "PC")
                {
                    clamp.Paralelity = "OK";
                    clamp.Paralelityerror = "-";
                }
                else if (checkBox19.Checked)
                {
                    clamp.Paralelity = "NOT OK";
                    clamp.Paralelityerror = richTextBox11.Text;
                }
                else clamp.Clampimputerror += " paralelity check";
            }
            else
            {
                if (checkBox28.Checked) //if (clamp.Clamtype = "PC")
                {
                    clamp.Paralelity = "OK";
                    clamp.Paralelityerror = "-";
                }
                else if (checkBox27.Checked)
                {
                    clamp.Paralelity = "NOT OK";
                    clamp.Paralelityerror = richTextBox11.Text;
                }
                else clamp.Clampimputerror += " paralelity check";
            }
            if (checkBox21.Enabled)
            {
                if (checkBox21.Checked) //if (!clamp.Clamtype = "PC")
                {
                    clamp.Distance = "OK";
                    clamp.Distancerror = "-";
                }
                else if (checkBox23.Checked)
                {
                    clamp.Distance = "NOT OK";
                    clamp.Distancerror = richTextBox12.Text;
                }
                else clamp.Clampimputerror += " distance check";
            }
            else
            {
                if (checkBox26.Checked) //if (clamp.Clamtype = "PC")
                {
                    clamp.Distance = "OK";
                    clamp.Distancerror = "-";
                }
                else if (checkBox25.Checked)
                {
                    clamp.Distance = "NOT OK";
                    clamp.Distancerror = richTextBox12.Text;
                }
                else clamp.Clampimputerror += " distance check";
            }
            if (checkBox22.Enabled)
            {
                if (checkBox22.Checked)
                {
                    clamp.Rflenght = "OK";
                    clamp.Rflenghterror = "-";
                }
                else if (checkBox24.Checked)
                {
                    clamp.Rflenght = "NOT OK";
                    clamp.Rflenghterror = richTextBox13.Text;
                }
                else clamp.Clampimputerror += " RF lenght";
            }
            else
            {
                clamp.Rflenght = "-";
                clamp.Rflenghterror = "-";
            }
            clamp.Solenoidlenght = richTextBox9.Text;
            clamp.Solenoidlenghterror = richTextBox14.Text;

            if (String.IsNullOrEmpty(richTextBox10.Text))
            {
                clamp.Electrodes = richTextBox10.Text;
                clamp.Electrodeserror = richTextBox15.Text;
            }
            else
            {
                clamp.Electrodes = richTextBox16.Text;
                clamp.Electrodeserror = richTextBox8.Text;
            }
            if (richTextBox24.Enabled)
            {
                if (!String.IsNullOrEmpty(richTextBox24.Text))
                {
                    clamp.Bigresistance = richTextBox24.Text;
                    //Debug.Write(richTextBox24.Text + "bleh ");
                    clamp.Bigresistancerror = richTextBox19.Text;

                    clamp.Smallresistance = richTextBox23.Text;
                    //Debug.Write(richTextBox23.Text + "bleh");
                    clamp.Smallresistancerror = richTextBox18.Text;
                }
                else clamp.Clampimputerror += " resistance";
            }
            if (richTextBox23.Enabled)
            {
                if (!String.IsNullOrEmpty(richTextBox23.Text))
                {
                    clamp.Bigresistance = richTextBox24.Text;
                    //Debug.Write(richTextBox24.Text + "bleh ");
                    clamp.Bigresistancerror = richTextBox19.Text;

                    clamp.Smallresistance = richTextBox23.Text;
                    //Debug.Write(richTextBox23.Text + "bleh");
                    clamp.Smallresistancerror = richTextBox18.Text;
                }
                else clamp.Clampimputerror += " resistance";
            }
            clamp.Shortcircuit = richTextBox22.Text;
            clamp.Shortcircuiterror = richTextBox17.Text;
            if (tabPage4.Enabled)
            {
                if (checkBox11.Checked)
                {
                    clamp.Emptythick = "OK";
                    clamp.Emptythickerror = "-";
                }
                else if (checkBox15.Checked)
                {
                    clamp.Emptythick = "NOT OK";
                    clamp.Emptythickerror = richTextBox23.Text;
                }
                else clamp.Clampimputerror += " empty thick";
                if (checkBox12.Checked)
                {
                    clamp.Fullthick = "OK";
                    clamp.Fullthickerror = "-";
                }
                else if (checkBox16.Checked)
                {
                    clamp.Fullthick = "NOT OK";
                    clamp.Fullthickerror = richTextBox29.Text;
                }
                else clamp.Clampimputerror += " full thick";
                if (checkBox13.Checked)
                {
                    clamp.Emptythin = "OK";
                    clamp.Emptythinerror = "-";
                }
                else if (checkBox17.Checked)
                {
                    clamp.Emptythin = "NOT OK";
                    clamp.Emptythinerror = richTextBox28.Text;
                }
                else clamp.Clampimputerror += " empty thin";
                if (checkBox14.Checked)
                {
                    clamp.Fullthin = "OK";
                    clamp.Fullthinerror = "-";
                }
                else if (checkBox18.Checked)
                {
                    clamp.Fullthin = "NOT OK";
                    clamp.Fullthinerror = richTextBox27.Text;
                }
                else clamp.Clampimputerror += " full thin";
            }
            else
            {
                clamp.Emptythick = "-";
                clamp.Emptythickerror = "-";
                clamp.Fullthick = "-";
                clamp.Fullthickerror = "-";
                clamp.Emptythin = "-";
                clamp.Emptythinerror = "-";
                clamp.Fullthin = "-";
                clamp.Fullthinerror = "-";
            }

            return clamp;
        }

        private void label16_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && checkBox2.Checked && checkBox3.Checked && checkBox4.Checked && checkBox5.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
                checkBox5.Checked = true;
            }
        }

        private void label47_Click(object sender, EventArgs e)
        {
            if (checkBox20.Checked && checkBox21.Checked && checkBox22.Checked)
            {
                checkBox20.Checked = false;
                checkBox21.Checked = false;
                checkBox22.Checked = false;
            }
            else
            {
                checkBox20.Checked = true;
                checkBox21.Checked = true;
                checkBox22.Checked = true;
            }
        }

        private void label45_Click(object sender, EventArgs e)
        {
            if (checkBox11.Checked && checkBox12.Checked && checkBox13.Checked && checkBox14.Checked)
            {
                checkBox11.Checked = false;
                checkBox12.Checked = false;
                checkBox13.Checked = false;
                checkBox14.Checked = false;
            }
            else
            {
                checkBox11.Checked = true;
                checkBox12.Checked = true;
                checkBox13.Checked = true;
                checkBox14.Checked = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox6.Checked = false;
                richTextBox1.Enabled = false;
                richTextBox1.ReadOnly = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox7.Checked = false;
                richTextBox2.Enabled = false;
                richTextBox2.ReadOnly = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox8.Checked = false;
                richTextBox6.Enabled = false;
                richTextBox6.ReadOnly = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox9.Checked = false;
                richTextBox3.Enabled = false;
                richTextBox3.ReadOnly = true;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox10.Checked = false;
                richTextBox4.Enabled = false;
                richTextBox4.ReadOnly = true; 
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                checkBox1.Checked = false;
                richTextBox1.Enabled = true;
                richTextBox1.ReadOnly = false;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                checkBox2.Checked = false;
                richTextBox2.Enabled = true;
                richTextBox2.ReadOnly = false;
            }
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox8.Checked)
            {
                checkBox3.Checked = false;
                richTextBox6.Enabled = true;
                richTextBox6.ReadOnly = false;
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox9.Checked)
            {
                checkBox4.Checked = false;
                richTextBox3.Enabled = true;
                richTextBox3.ReadOnly = false;
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox10.Checked)
            {
                checkBox5.Checked = false;
                richTextBox4.Enabled = true;
                richTextBox4.ReadOnly = false;
            }
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox20.Checked)
            {
                checkBox19.Checked = false;
                richTextBox11.Enabled = false;
                richTextBox11.ReadOnly = true;
            }
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
            {
                checkBox23.Checked = false;
                richTextBox12.Enabled = false;
                richTextBox12.ReadOnly = true;
            }
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked)
            {
                checkBox24.Checked = false;
                richTextBox13.Enabled = false;
                richTextBox13.ReadOnly = true;
            }
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked)
            {
                checkBox20.Checked = false;
                richTextBox11.Enabled = true;
                richTextBox11.ReadOnly = false;
            }
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked)
            {
                checkBox21.Checked = false;
                richTextBox12.Enabled = true;
                richTextBox12.ReadOnly = false;
            }
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox24.Checked)
            {
                checkBox22.Checked = false;
                richTextBox13.Enabled = true;
                richTextBox13.ReadOnly = false;
            }
        }

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                checkBox15.Checked = false;
                richTextBox30.Enabled = false;
                richTextBox30.ReadOnly = true;
            }
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                checkBox16.Checked = false;
                richTextBox29.Enabled = false;
                richTextBox29.ReadOnly = true;
            }
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {
                checkBox17.Checked = false;
                richTextBox28.Enabled = false;
                richTextBox28.ReadOnly = true;
            }
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {
                checkBox18.Checked = false;
                richTextBox27.Enabled = false;
                richTextBox27.ReadOnly = true;
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox15.Checked)
            {
                checkBox11.Checked = false;
                richTextBox30.Enabled = true;
                richTextBox30.ReadOnly = false;
            }
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked)
            {
                checkBox12.Checked = false;
                richTextBox29.Enabled = true;
                richTextBox29.ReadOnly = false;
            }
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked)
            {
                checkBox13.Checked = false;
                richTextBox28.Enabled = true;
                richTextBox28.ReadOnly = false;
            }
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox18.Checked)
            {
                checkBox14.Checked = false;
                richTextBox27.Enabled = true;
                richTextBox27.ReadOnly = false;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (checkBox28.Checked && checkBox26.Checked)
            {
                checkBox28.Checked = false;
                checkBox26.Checked = false;
            }
            else
            {
                checkBox28.Checked = false;
                checkBox26.Checked = false;
            }
        }

        private void checkBox28_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox28.Checked)
            {
                checkBox27.Checked = false;
                richTextBox7.Enabled = false;
                richTextBox7.ReadOnly = true;
            }
        }

        private void checkBox26_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox26.Checked)
            {
                checkBox25.Checked = false;
                richTextBox5.Enabled = false;
                richTextBox5.ReadOnly = true;
            }
        }

        private void checkBox27_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox27.Checked)
            {
                checkBox28.Checked = false;
                richTextBox7.Enabled = true;
                richTextBox7.ReadOnly = false;
            }
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox25.Checked)
            {
                checkBox26.Checked = false;
                richTextBox5.Enabled = true;
                richTextBox5.ReadOnly = false;
            }
        }
    }
}
