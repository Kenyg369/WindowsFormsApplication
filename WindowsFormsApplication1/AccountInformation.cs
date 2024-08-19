using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class AccountInformation
    {
        public string UserName { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public double AccountBalance { get; set; }

        public AccountInformation(string userName, string accountName, string password, double accountBalance)
        {
            UserName = userName;
            AccountName = accountName;
            Password = password;
            AccountBalance = accountBalance;
        }

        public override string ToString()
        {
            return $"Username: {UserName}, Account: {AccountName}, Balance: {AccountBalance:C}";
        }
    }

}
