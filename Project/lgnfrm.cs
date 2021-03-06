using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Project
{
    public partial class lgnfrm : Form
    {
        public lgnfrm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void btn_lgin_Click(object sender, EventArgs e)
        {

            try
            {
                Person p = new Person(Convert.ToUInt32(usernametxt.Text), passwordtxt.Text);
                passwordtxt.Clear();
        
                if (p.loginCheck() )
                {
                    if (p.AdminLvl == 0)
                    {
                        this.Hide();
                        MainForm frm = new MainForm(p.Account_Number);
                        frm.Closed += (s, args) => this.Close();
                        frm.Show();
                    }
                    else
                    {
                        this.Hide();
                        TellerForm frm2 = new TellerForm(p.Account_Number , p.Password);
                        frm2.Closed += (s, args) => this.Close();
                        frm2.Show();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter valid data");
                usernametxt.Clear();
            }
            
        }


        private void label2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void usernametxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
