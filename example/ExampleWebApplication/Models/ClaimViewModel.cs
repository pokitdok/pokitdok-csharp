using System;
using System.Collections.Generic;
using pokitdokcsharp;
using pokitdokcsharp.Models;
using pokitdokcsharp.Models.Transactions;

namespace ExampleWebApp.Models
{
    public class ClaimViewModel
    {
        public ClaimsTransaction BaseTransaction { get; set; }
        public ResponseData Response { get; set; }

        public ClaimViewModel()
        {
            Response = new ResponseData();
            BaseTransaction = new ClaimsTransaction();

            SetDefaultValues();
        }

        private void SetDefaultValues()
        {
            BaseTransaction.BillingProvider = new BillingProvider();
            BaseTransaction.Claim = new Claim();
            BaseTransaction.Patient = new Patient();
            BaseTransaction.Subscriber = new Subscriber();
            BaseTransaction.Patient.Address = new Address();
            BaseTransaction.Subscriber.Address = new Address();
            BaseTransaction.BillingProvider.Address = new Address();
            BaseTransaction.TradingPartnerId = "MOCKPAYER";
            BaseTransaction.TransactionCode = TransactionCode.Chargeable;
            BaseTransaction.BillingProvider.TaxId = "123456789";
            BaseTransaction.BillingProvider.TaxonomyCode = "207Q00000X";
            BaseTransaction.BillingProvider.Address.AddressLines.Add("8311 WARREN H ABERNATHY HWY");
            BaseTransaction.BillingProvider.Address.AddressLines.Add(String.Empty);
            BaseTransaction.BillingProvider.Address.AddressLines.Add(String.Empty);
            BaseTransaction.BillingProvider.Address.City = "SPARTANBURG";
            BaseTransaction.BillingProvider.Address.State = "SC";
            BaseTransaction.BillingProvider.Address.Zipcode = "29301";
            BaseTransaction.BillingProvider.FirstName = "Jerome";
            BaseTransaction.BillingProvider.LastName = "Aya-Ay";
            BaseTransaction.BillingProvider.NPI = "1467560003";
            BaseTransaction.Claim.AdmissionDate = DateTime.Now;
            BaseTransaction.Claim.OnsetDate = DateTime.Now;
            BaseTransaction.Claim.StatementDate = DateTime.Now;
            BaseTransaction.Claim.StatementEndDate = DateTime.Now;
            var serviceLine = new ServiceLine();
            serviceLine.ChargeAmount = 60;
            serviceLine.ProcedureCode = "99213";
            serviceLine.RevenueCode = "0651";
            serviceLine.UnitCount = 1;
            serviceLine.DiagnosisCodes.Add("487.1");
            serviceLine.DiagnosisCodes.Add(string.Empty);
            serviceLine.DiagnosisCodes.Add(string.Empty);
            serviceLine.ProcedureModifierCodes = new List<string>();
            serviceLine.ProcedureModifierCodes.Add(string.Empty);
            serviceLine.ProcedureModifierCodes.Add(string.Empty);
            serviceLine.ProcedureModifierCodes.Add(string.Empty);
            serviceLine.ServiceDate = DateTime.Now;
            BaseTransaction.Claim.ServiceLines.Add(serviceLine);
            BaseTransaction.Claim.ServiceLines.Add(new ServiceLine
            {
                DiagnosisCodes = new List<string> {string.Empty, string.Empty, string.Empty},
                ProcedureModifierCodes = new List<string> {string.Empty, string.Empty, string.Empty}
            });
            BaseTransaction.Claim.ServiceLines.Add(new ServiceLine
            {
                DiagnosisCodes = new List<string> {string.Empty, string.Empty, string.Empty},
                ProcedureModifierCodes = new List<string> {string.Empty, string.Empty, string.Empty}
            });
            BaseTransaction.Subscriber.FirstName = "Jane";
            BaseTransaction.Subscriber.LastName = "Doe";
            BaseTransaction.Subscriber.MemberId = "W000000000";
            BaseTransaction.Subscriber.Address.AddressLines.Add("123 N MAIN ST");
            BaseTransaction.Subscriber.Address.AddressLines.Add(String.Empty);
            BaseTransaction.Subscriber.Address.AddressLines.Add(String.Empty);
            BaseTransaction.Subscriber.Address.City = "SPARTANBURG";
            BaseTransaction.Subscriber.Address.State = "SC";
            BaseTransaction.Subscriber.Address.Zipcode = "29301";
            BaseTransaction.Subscriber.BirthDate = DateTime.Parse("1970-01-01");
            BaseTransaction.Subscriber.Gender = Gender.Female;
            BaseTransaction.Patient.Address.AddressLines.Add(string.Empty);
            BaseTransaction.Patient.Address.AddressLines.Add(string.Empty);
            BaseTransaction.Patient.Address.AddressLines.Add(string.Empty);
        }
    }
}