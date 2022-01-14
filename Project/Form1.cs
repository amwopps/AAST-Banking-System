﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        User user1;

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(UInt32 num)
        {
            user1 = new User(num);
            if (user1.AccountType == 1)
            {
                user1 = new FixedAccount(num);
            }
            else
            {
                user1 = new SavingAccount(num);
            }
            
            InitializeComponent();
            UserNameLab.Text = user1.Name;
            UserBalanceLab.Text = Convert.ToString(user1.Balance);
            lbl_debt.Text = Convert.ToString(user1.Debt);
            lbl_expBal.Text = Convert.ToString(user1.GetBalance());
            lbl_accType.Text = user1.GetType();
        }
        
        

        private void Form1_Load(object sender, EventArgs e)
        {
            DateLab.Text = DateTime.Now.ToLongDateString();
        }

        private void BtnChangePass_Click(object sender, EventArgs e)
        {
            ChangePass chng = new ChangePass(user1.Account_Number);
            chng.Show();
        }

        private void BtnSignOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            lgnfrm newlgn = new lgnfrm();
            newlgn.Closed += (s, args) => this.Close();
            newlgn.Show();
        }


        private void BtnDeposit_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(textDeposit.Text) && textDeposit.Text.All(char.IsNumber))
                {
                    double val = Double.Parse(textDeposit.Text);

                    if (user1.Deposit(val))
                    {
                        dep_with_stat.Text = "Succesful transaction";
                        UserBalanceLab.Text = Convert.ToString(user1.Balance);
                        textDeposit.Clear();
                    }
                    else
                    {
                        textDeposit.Clear();
                        dep_with_stat.Text = "Please enter a valid amount";
                    }
                }
                else
                {
                    textDeposit.Clear();
                    dep_with_stat.Text = "Please enter a valid amount";
                }
                lbl_expBal.Text = Convert.ToString(user1.GetBalance());
            }
            catch (Exception)
            {
                textDeposit.Clear();
                dep_with_stat.Text = "Please enter a valid amount";
            }
        }


        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textWithdraw.Text) && textWithdraw.Text.All(char.IsNumber))
                {
                    double val = Double.Parse(textWithdraw.Text);
                    if (user1.Withdraw(val))
                    {
                        dep_with_stat.Text = "Succesful transaction";
                        UserBalanceLab.Text = Convert.ToString(user1.Balance);

                    }
                    else
                        dep_with_stat.Text = "Please enter a valid amount";

                }
                else
                {
                    dep_with_stat.Text = "Please enter a valid amount";
                }
                textWithdraw.Clear();
                lbl_expBal.Text = Convert.ToString(user1.GetBalance());
            }
            catch (Exception)
            {
                dep_with_stat.Text = "Please enter a valid amount";
                textWithdraw.Clear();
            }
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtTranAmount.Text) && !string.IsNullOrEmpty(TxtTranUser.Text)
                    && TxtTranAmount.Text.All(char.IsNumber) && TxtTranUser.Text.All(char.IsNumber))
                {
                    double val = Double.Parse(TxtTranAmount.Text);
                    UInt32 user2_acc_num = UInt32.Parse(TxtTranUser.Text);
                    User user2 = new User(user2_acc_num);
                    if (user1.Account_Number == user2.Account_Number)
                    {
                        MessageBox.Show("Can not transfer money to your own account");
                        TxtTranUser.Clear();
                    }
                    else if (user1.Transfer(user2, val))
                    {
                        transferstat.Text = string.Format("Transfered ${0} to: {1}", val, user2.Name);
                        UserBalanceLab.Text = Convert.ToString(user1.Balance);
                    }
                    else
                    {
                        transferstat.Text = "Insufficient or account doesn't exist";
                        TxtTranUser.Clear();
                        TxtTranAmount.Clear();
                        lbl_expBal.Text = Convert.ToString(user1.GetBalance());
                    }
                }
                else
                {
                    TxtTranUser.Clear();
                    TxtTranAmount.Clear();
                    MessageBox.Show("Pleanser Enter valid data");
                }
            }
            catch (Exception)
            {
                TxtTranUser.Clear();
                TxtTranAmount.Clear();
                MessageBox.Show("Pleanser Enter valid data");
            }
           
        }

        private void btn_askLoan_Click(object sender, EventArgs e)
        {
            try 
            {
                if (!string.IsNullOrEmpty(txtbox_askLoan.Text) && txtbox_askLoan.Text.All(char.IsNumber))
                {
                    double val = Double.Parse(txtbox_askLoan.Text);
                    if (user1.AskLoan(val))
                    {
                        UserBalanceLab.Text = Convert.ToString(user1.Balance);
                        lbl_debt.Text = Convert.ToString(user1.Debt);
                        askLoan_stat.Text = string.Format("Succesfully added ${0} to your account", val);
                    }
                    else
                    {
                        askLoan_stat.Text = "You are not eligble for this loan";
                        txtbox_askLoan.Clear();
                        lbl_expBal.Text = Convert.ToString(user1.GetBalance());
                    }
                }
                else
                {
                    MessageBox.Show("Pleas enter a valid amount");
                    txtbox_askLoan.Clear();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Pleas enter a valid amount");
                txtbox_askLoan.Clear();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(payDebt.Text) && payDebt.Text.All(char.IsNumber))
                {
                    double val = Double.Parse(payDebt.Text);
                    if (user1.Debt == 0)
                    {
                        label4.Text = "Your debt amout is 0$";
                        payDebt.Clear();
                    }

                    else if (user1.paydebt(val))
                    {
                        UserBalanceLab.Text = Convert.ToString(user1.Balance);
                        lbl_debt.Text = Convert.ToString(user1.Debt);
                        label4.Text = "Completed Successfully";
                    }
                    else
                    {
                        label4.Text = "Please Enter valid amout";
                        payDebt.Clear();
                    }

                }
                else
                {
                    label4.Text = "Please Enter valid amout";
                    payDebt.Clear();
                }
                lbl_expBal.Text = Convert.ToString(user1.GetBalance());
            }
            catch(Exception)
            {
                label4.Text = "Please Enter valid amout";
                payDebt.Clear();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
