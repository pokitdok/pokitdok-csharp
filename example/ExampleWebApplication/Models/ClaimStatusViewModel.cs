using System;
using pokitdokcsharp;
using pokitdokcsharp.Models;
using pokitdokcsharp.Models.Transactions;

namespace ExampleWebApp.Models
{
    public class ClaimStatusViewModel
    {
        public ClaimStatusTransaction BaseTransaction { get; set; }
        public ResponseData Response { get; set; }

        public ClaimStatusViewModel()
        {
            Response = new ResponseData();
            BaseTransaction = new ClaimStatusTransaction
            {
                Patient = new BasicEntity(),
                Provider = new Provider(),
                Subscriber = new BasicEntity()
            };
            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            BaseTransaction.TradingPartnerId = "MOCKPAYER";
            BaseTransaction.Provider.FirstName = "Jerome";
            BaseTransaction.Provider.LastName = "Aya-Ay";
            BaseTransaction.Provider.NPI = "1467560003";
            BaseTransaction.Subscriber.FirstName = "Jane";
            BaseTransaction.Subscriber.LastName = "Doe";
            BaseTransaction.Subscriber.BirthDate = DateTime.Parse("1970-01-01");
            BaseTransaction.Subscriber.Id = "123456789";
            BaseTransaction.Patient.Id = "123456789";
            BaseTransaction.Patient.BirthDate = DateTime.Parse("2000-01-01");
            BaseTransaction.Patient.FirstName = "JOHN";
            BaseTransaction.Patient.LastName = "DOE";
            BaseTransaction.ServiceDate = DateTime.Now;
            BaseTransaction.ServiceEndDate = DateTime.Now;
        }
    }
}