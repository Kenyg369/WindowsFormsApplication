using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class frmMenu : UIForm
    {
        private string currentUserName;
        private string currentAccountName;
        private double accountBalance = 1; // Global Balance
        private double conversionNum; // Exchange rate
        private List<AccountInformation> userAccounts;

        public frmMenu()
        {
            InitializeComponent();
        }

        public frmMenu(List<AccountInformation> userAccounts)
        {
            this.userAccounts = userAccounts;
            currentUserName = userAccounts.First().UserName;

            InitializeComponent();
        }


        private void frmMenu_Load(object sender, EventArgs e)
        {
            this.uiLabel1.Text = "Welcome，" + currentUserName + "!";
            foreach(AccountInformation account in userAccounts)
            {
                this.uiComboBox1.Items.Add(account.AccountName);
            }
            this.uiComboBox1.SelectedIndex = 0;
            foreach (AccountInformation account in userAccounts)
            {
                if(account.AccountName == uiComboBox1.Items[uiComboBox1.SelectedIndex].ToString())
                {
                    accountBalance = account.AccountBalance;
                    currentAccountName = account.AccountName;
                    this.uiLabel4.Text = "$" + account.AccountBalance.ToString().Trim() + "(CAD)";
                    this.uiTextBox1.Text = account.AccountBalance.ToString().Trim();
                }
            }
            this.uiComboBox2.Items.Add("CAD");
            this.uiComboBox2.Items.Add("USD");
            this.uiComboBox2.Items.Add("MXN");
            this.uiComboBox2.Items.Add("EURO");
            this.uiComboBox2.SelectedIndex = 0;
        }

        private void uiComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (AccountInformation account in userAccounts)
            {
                if (account.AccountName == uiComboBox1.Items[uiComboBox1.SelectedIndex].ToString())
                {
                    this.uiLabel4.Text = "$" + account.AccountBalance.ToString().Trim() + "(CAD)";
                    this.uiTextBox1.Text = account.AccountBalance.ToString().Trim();
                    accountBalance = account.AccountBalance;
                    currentAccountName = account.AccountName;
                }
            }
        }

        /// <summary>
        /// Process currency exchange
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void uiComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            double balance = 0;
            if(this.uiComboBox2.SelectedIndex == 0)
            {
                conversionNum = 1;
                balance = accountBalance;
                this.uiTextBox1.Text = balance.ToString();
            }
            if (this.uiComboBox2.SelectedIndex == 1)
            {
                conversionNum = 2;
                balance = accountBalance * 0.5;
                this.uiTextBox1.Text = balance.ToString();
            }
            if (this.uiComboBox2.SelectedIndex == 2)
            {
                conversionNum = 0.1;
                balance = accountBalance * 10.0;
                this.uiTextBox1.Text = balance.ToString();
            }
            if (this.uiComboBox2.SelectedIndex == 3)
            {
                conversionNum = 4;
                balance = accountBalance * 0.25;
                this.uiTextBox1.Text = balance.ToString();
            }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            double withDrawMoney = double.Parse(this.uiTextBox1.Text) * conversionNum;
            if (withDrawMoney > accountBalance)
            {
                UIMessageBox.Show("Withdraw can't be greater than balance！", "Error", UIStyle.Red, UIMessageBoxButtons.OK, true);
                return;
            }
            accountBalance = accountBalance - withDrawMoney;
            this.uiTextBox1.Text = accountBalance.ToString();
            this.uiComboBox2.SelectedIndex = 0;
            this.uiLabel4.Text = "$" + accountBalance.ToString().Trim() + "(CAD)";


            // Update txt balance
            UpdateAccountBalanceInFile();

            UIMessageBox.Show("Withdraw Successfully！", "Infor", UIStyle.Green, UIMessageBoxButtons.OK, true);
        }

        private void UpdateAccountBalanceInFile()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Account Information.txt");
            string[] lines = File.ReadAllLines(filePath);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(',');

                if (parts.Length == 4)
                {
                    string userName = parts[0];
                    string accountName = parts[1];
                    string password = parts[2];

                    // Find current user and account
                    if (userName == currentUserName && accountName == currentAccountName)
                    {
                        // Update balance
                        lines[i] = $"{userName},{accountName},{password},{accountBalance}";
                        break;
                    }
                }
            }

            // Write updated balance back to file
            File.WriteAllLines(filePath, lines);
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            double storeMoney = double.Parse(this.uiTextBox1.Text) * conversionNum;
            accountBalance = accountBalance + storeMoney;
            this.uiTextBox1.Text = accountBalance.ToString();
            this.uiComboBox2.SelectedIndex = 0;
            this.uiLabel4.Text = "$" + accountBalance.ToString().Trim() + "(CAD)";


            // Update balance in file
            UpdateAccountBalanceInFile();

            UIMessageBox.Show("Save Successfully！", "Infor", UIStyle.Green, UIMessageBoxButtons.OK, true);
        }

        private void uiTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void uiLabel2_Click(object sender, EventArgs e)
        {

        }

        private void uiPanel1_Click(object sender, EventArgs e)
        {

        }
    }
}
