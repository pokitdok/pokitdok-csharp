using System;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using ExampleWebApp.Models;
using pokitdokcsharp;
using pokitdokcsharp.Models.Transactions;

namespace ExampleWebApp.Controllers
{
    [RoutePrefix("/")]
    public class PokitdokTestController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Claim/Create
        [HttpGet]
        [Route("CreateClaim")]
        public ActionResult CreateClaim()
        {
            var model = new ClaimViewModel();
            return View(model);
        }

        // POST: Claim/Create
        [HttpPost]
        [Route("CreateClaim")]
        public ActionResult CreateClaim(ClaimViewModel claim)
        {
            PlatformClient client = CreateClient();
            claim.BaseTransaction.Claim.ValueInformation = null;

            claim.BaseTransaction.Claim.ServiceLines?.RemoveAll(sl => string.IsNullOrWhiteSpace(sl.ProcedureCode));
            claim.BaseTransaction.Claim.ServiceLines?.ForEach(sl => sl.DiagnosisCodes.RemoveAll(string.IsNullOrWhiteSpace));

            claim.BaseTransaction.BillingProvider.Address.AddressLines.RemoveAll(string.IsNullOrWhiteSpace);
            if (claim.BaseTransaction.BillingProvider.Address.AddressLines.TrueForAll(a => a == null))
                claim.BaseTransaction.BillingProvider.Address = null;

            if (string.IsNullOrWhiteSpace(claim.BaseTransaction.Patient.FirstName))
                claim.BaseTransaction.Patient = null;
            else
            {
                claim.BaseTransaction.Patient.Address.AddressLines.RemoveAll(string.IsNullOrWhiteSpace);
                if (claim.BaseTransaction.Patient.Address.AddressLines.TrueForAll(a => a == null))
                    claim.BaseTransaction.Patient.Address = null;
            }

            claim.BaseTransaction.Subscriber.Address.AddressLines.RemoveAll(string.IsNullOrWhiteSpace);
            if (claim.BaseTransaction.Subscriber.Address.AddressLines.TrueForAll(a => a == null))
                claim.BaseTransaction.Subscriber.Address = null;

            claim.BaseTransaction.Claim.TotalChargeAmount =
                claim.BaseTransaction.Claim.ServiceLines.Sum(sl => sl.ChargeAmount);

            var response = client.claims(claim.BaseTransaction);
            var newModel = new ClaimViewModel();
            newModel.Response = response;
            return View(newModel);
        }

        private static PlatformClient CreateClient()
        {
            return new PlatformClient(WebConfigurationManager.AppSettings["ApiKey"], WebConfigurationManager.AppSettings["SecretApiKey"]);
        }

        [HttpGet]
        [Route("CreateClaimStatus")]
        public ActionResult CreateClaimStatus()
        {
            var claimStatusViewModel = new ClaimStatusViewModel();
            return View(claimStatusViewModel);
        }

        [HttpPost]
        [Route("CreateClaimStatus")]
        public ActionResult CreateClaimStatus(ClaimStatusViewModel model)
        {
            PlatformClient client = CreateClient();
            var response = client.claimsStatus(model.BaseTransaction);
            var newModel = new ClaimStatusViewModel();
            newModel.Response = response;
            return View(newModel);
        }

        [HttpGet]
        [Route("CreateEligibility")]
        public ActionResult CreateEligibility()
        {
            return View(new EligibilityViewModel());
        }

        [HttpPost]
        [Route("CreateEligibility")]
        public ActionResult CreateEligibility(EligibilityTransaction baseTransaction)
        {
            PlatformClient client = CreateClient();
            var response = client.eligibility(baseTransaction);
            var viewModel = new EligibilityViewModel {Response = response};
            return View(viewModel);
        }
    }
}