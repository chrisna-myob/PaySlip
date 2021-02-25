using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Globalization;
using System;

namespace BasicPaySlip
{
    public class PaySlip
    {
        public static string GetName(string firstName, string lastName) 
        {
            if (String.IsNullOrEmpty(firstName) || String.IsNullOrEmpty(lastName)) 
            {
                return "Invalid Name";
            } else {
                return firstName.Trim()+" "+lastName.Trim();
            }
        }

        public static string GetPayPeriod(DateTime startDate, DateTime endDate) 
        {
            if (startDate == DateTime.MinValue || endDate == DateTime.MinValue)
            {
                return "Invalid Date";
            } else {
                return startDate.ToString("dd MMMM") + " - " + endDate.ToString("dd MMMM");
            }
        }

        public static decimal GetGrossIncome(decimal salary) 
        {
            if (salary == 0.0m) return salary;
            else return Math.Floor(salary/12);
        }

        public static decimal GetSuper(decimal grossIncome, int rate) 
        {
            if (grossIncome == 0.0m || rate < 0) {
                return grossIncome;
            }
            else return Math.Floor(grossIncome*rate/100);
        }

        public static decimal GetIncomeTax(decimal salary) {
            if (salary >= 0 && salary <= 18200) {
                return 0;
            }

            decimal rate = 0; 
            decimal amount = 0;
            decimal taxBracket = 0; 
            if (salary >= 18201 && salary <= 37000) {
                taxBracket = 18200;
                amount = 0;
                rate = 19/100;
            }

            if (salary >= 37001 && salary <= 87000) {
                taxBracket = 37000;
                amount = 3572;
                rate = 32.5m/100;
            }

            if (salary >= 87001 && salary <= 180000) {
                taxBracket = 87000;
                amount = 19822;
                rate = 37/100;
            }

            if (salary >= 180001) {
                taxBracket = 180000;
                amount = 54232;
                rate = 45/100;
            }

            return Math.Ceiling((amount + (salary - taxBracket)*rate)/12);
        }

        public static decimal GetNetIncome(decimal grossIncome, decimal incomeTax) {
            return grossIncome - incomeTax;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the payslip generator!\n");

            // get inputs
            Console.Write("Please input your name: ");
            string firstName = Console.ReadLine();
            if (firstName == "") 
            {
                Console.WriteLine("Please provide your first name");
                return;
            }

            Console.Write("Please input your surname: ");
            string lastName = Console.ReadLine();
            if (lastName == "") 
            {
                Console.WriteLine("Please provide your last name");
                return;
            }

            Console.Write("Please enter your annual salary: ");
            string salaryString = Console.ReadLine();
            decimal salary;
            bool salaryOutcome = Decimal.TryParse(salaryString, out salary);
            if (!salaryOutcome || salary <= 0) 
            {
                Console.WriteLine("Invalid Salary");
                return;
            }

            Console.Write("Please enter your super rate: ");
            string superRateString = Console.ReadLine();
            int superRate;
            bool outcome = Int32.TryParse(superRateString, out superRate);
            if (!outcome || superRate < 0 || superRate > 50) 
            {
                Console.WriteLine("Super rate must be between 0 - 50%");
                return;
            }

            Console.Write("Please enter your payment start date: ");
            string start = Console.ReadLine();
            DateTime startDate;
            if (!DateTime.TryParse(start, out startDate))
            {
                Console.WriteLine("Invalid Date");
                return;
            }

            Console.Write("Please enter your payment end date: ");
            string end = Console.ReadLine();
            DateTime endDate;
            if (!DateTime.TryParse(start, out endDate))
            {
                Console.WriteLine("Invalid Date");
                return;
            }

            Console.Write("\n");

            decimal grossIncome = GetGrossIncome(salary);

            decimal incomeTax = GetIncomeTax(salary);

            Console.Write("Your payslip has been generated: \n");

            Console.Write("Name: "+GetName(firstName, lastName)+"\nPay Period: "+GetPayPeriod(startDate, endDate)+"\nGross Income: "+grossIncome+"\nIncome Tax: "+incomeTax+"\nNet Income: "+GetNetIncome(grossIncome, incomeTax)+"\nSuper: "+GetSuper(grossIncome, superRate)+"\n\n");

            Console.Write("Thank you for using MYOB!\n");

        }
    }
}