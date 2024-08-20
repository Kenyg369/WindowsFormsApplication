using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace WindowsFormsApplication1
{
    public partial class frmLogin : UIForm
    {
        private List<AccountInformation> accounts;

        public frmLogin()
        {
            InitializeComponent();
            UILocalizeHelper.SetEN();
            LoadAccounts();
        }
        private void LoadAccounts()
        {
            #region this is a test
            accounts = new List<AccountInformation>();

            // Use correct file directory
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Account Information.txt");

            try
            {
                // Read all the data from the "2nd line", 1st line saved for data name 
                string[] lines = File.ReadAllLines(filePath).Skip(1).ToArray();

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length == 4)
                    {
                        string userName = parts[0];
                        string accountName = parts[1];
                        string password = parts[2];
                        double accountBalance = double.Parse(parts[3]);

                        AccountInformation account = new AccountInformation(userName, accountName, password, accountBalance);
                        accounts.Add(account);
                    }
                }
            }
            catch (Exception ex)
            {
                UIMessageBox.Show("Error occur when reading the file：" + ex.Message, "Error", UIStyle.Red, UIMessageBoxButtons.OK, true);
            }
            #endregion
        }
        private void uiButton1_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Seaching for right user
            var userAccounts = accounts.Where(a => a.UserName == username && a.Password == password).ToList();

            if (userAccounts.Count > 0)
            {
                UIMessageBox.Show("Log in successfully!", "Infor", UIStyle.Green, UIMessageBoxButtons.OK, true);

                frmMenu frm = new frmMenu(userAccounts);
                frm.ShowDialog();

            }
            else
            {
                UIMessageBox.Show("Wrong user name or password!", "Error", UIStyle.Red, UIMessageBoxButtons.OK, true);
            }


        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void uiLabel1_Click(object sender, EventArgs e)
        {

        }

        private void uiLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
