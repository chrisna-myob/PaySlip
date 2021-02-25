using System;
using Xunit;

namespace BasicPaySlip.Tests
{
    public class PaySlipTests
    {
        [Fact]
        public void ValidNameTest()
        {
            string fullName = PaySlip.GetName("John", "Doe");
            Assert.Equal("John Doe", fullName);
        }

        [Fact]
        public void TrimTest()
        {
            string fullName = PaySlip.GetName("John ", " Doe   ");
            Assert.Equal("John Doe", fullName);
        }

        [Fact]
        public void EmptyNameTest()
        {
            string fullName = PaySlip.GetName(" ", "");
            Assert.Equal("Invalid Name", fullName);
        }

        [Fact]
        public void UnassignedDateTest() {
            DateTime newDate = new DateTime();
            string date = PaySlip.GetPayPeriod(newDate, newDate);
            Assert.Equal("Invalid Date", date);
        }

        [Fact]
        public void ValidDateTest() {
            string date = PaySlip.GetPayPeriod(DateTime.Parse("1 March"), DateTime.Parse("30 March"));
            Assert.Equal("01 March - 30 March", date);
        }
        
        [Fact]
        public void UnassignedSalaryTest() {
            decimal income = PaySlip.GetGrossIncome(0.0m);
            Assert.Equal(0, income);
        }

        [Fact]
        public void GrossIncomeTest() {
            decimal income = PaySlip.GetGrossIncome(60050m);
            Assert.Equal(5004, income);
        }

        [Fact]
        public void UnassignedSuperTest() {
            decimal income = PaySlip.GetGrossIncome(0.0m);
            decimal super = PaySlip.GetSuper(income, -8);
            Assert.Equal(0, super);
        }
        
        [Fact]
        public void SuperTest() {
            decimal income = PaySlip.GetGrossIncome(60050m);
            decimal super = PaySlip.GetSuper(income, 9);
            Assert.Equal(450, super);
        }
        
        [Fact]
        public void IncomeTaxTest() {
            decimal tax = PaySlip.GetIncomeTax(60050m);
            Assert.Equal(922, tax);
        }

        [Fact]
        public void NetIncome() {
            decimal income = PaySlip.GetGrossIncome(60050m);
            decimal tax = PaySlip.GetIncomeTax(60050m);
            decimal net = PaySlip.GetNetIncome(income, tax);
            Assert.Equal(4082, net);
        }

    }
}
