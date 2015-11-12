using System;
using pokitdokcsharp;
using pokitdokcsharp.Models.Transactions;

namespace ExampleWebApp.Models
{
    public class EligibilityViewModel
    {
        public EligibilityViewModel()
        {
            Response = new ResponseData();
            BaseTransaction = new EligibilityTransaction();

            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            BaseTransaction.TradingPartnerId = "MOCKPAYER";
            BaseTransaction.Provider.FirstName = "JEROME";
            BaseTransaction.Provider.LastName = "AYA-AY";
            BaseTransaction.Provider.NPI = "1467560003";

            BaseTransaction.Member.Id = "W000000000";
            BaseTransaction.Member.FirstName = "Jane";
            BaseTransaction.Member.LastName = "Doe";
            BaseTransaction.Member.BirthDate = DateTime.Parse("1970-01-01");
        }

        public EligibilityTransaction BaseTransaction { get; set; }
        public ResponseData Response { get; set; }
    }
}