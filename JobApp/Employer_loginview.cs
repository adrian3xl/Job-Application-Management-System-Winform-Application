﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace JobApp
{
    public partial class Employer_loginview : Form
    {
        private readonly Jobapp_dbEntities _db;
        public Employer_loginview()
        {
            InitializeComponent();
            _db = new Jobapp_dbEntities();
        }



        private void Enter_emp_bt_Click(object sender, EventArgs e)
        {
            
            try
            {
                SHA256 sha = SHA256.Create();

                string company_name = Company_name_tb.Text.Trim();
                string password = company_pass_tb.Text;
                bool IsValid = true;

                byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[1].ToString("x2"));
                }

                var hashed_password = sBuilder.ToString();


                var Employer_details = _db.Employer_details.FirstOrDefault(q => q.Company_name == company_name && q.Password == hashed_password);
               
                if (Employer_details == null)
                {
                    IsValid = false;
                    MessageBox.Show("Invalid user name and password entered");
                    
         
                    
                }

                if (IsValid)
                {
                    
                    MessageBox.Show("Welcome");
                    
                    

                   

                    var EmployerID = Employer_details.id;
                    
                   var Employer_accountview = new Employer_accountview(EmployerID);

                  
                    Employer_accountview.ShowDialog();


                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please re-enter username and password");
                //throw;
            }
        }

        private void cancel_bt_Click(object sender, EventArgs e)
        {
            var Employer_loginview = new Employer_loginview();
            Close();
        }
    }
}
