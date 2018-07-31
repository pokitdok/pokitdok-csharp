using System;
using System.IO;
using pokitdokcsharp;
using pokitdokcsharp.tests.data;
using NUnit.Framework;

/// <summary>
/// Platform client test.
/// </summary>
[TestFixture]
public class PlatformApiTests
{
    private PlatformClient _client = new PlatformClient("", "");
    private string _oopLoadedPriceUuid = null; 
    private void Assert200(ResponseData res)
    {
        Assert.AreEqual(200, res.status_code); 
    }

    [Test]
    public void TestActivities()
    {
        // https://platform.pokitdok.com/documentation/v4/#activities
        Assert200(_client.activities());
        Assert200(_client.activities(ExampleRequests.ActivitiesRequest));
    }

    [Test]
    public void TestAuthorizations()
    {
        // https://platform.pokitdok.com/documentation/v4/#authorizations
        Assert200(_client.authorizations(ExampleRequests.AuthorizationsRequet)); 
    }

    [Test]
    public void TestCashPrices()
    {
        // https://platform.pokitdok.com/documentation/v4/#cash-prices
        Assert200(_client.cashPrices(ExampleRequests.PricesRequestExample)); 
    }

    [Test]
    public void TestClaims()
    {
        // https://platform.pokitdok.com/documentation/v4/#claims
        Assert200(_client.claims(ExampleRequests.ClaimsRequest)); 
    }

    [Test]
    public void TestClaimsConvert()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#claims-convert
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "files/secondary_claim_model_first_payer.837");
        Assert200(_client.claimsConvert(path));
    }

    [Test]
    public void TestClaimsStatus()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#claims-status
        Assert200(_client.claimsStatus(ExampleRequests.ClaimsStatusRequest)); 
    }

    [Test]
    public void TestEligibility()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#eligibility
        Assert200(_client.eligibility(ExampleRequests.EligibilityRequest)); 
    }

    [Test]
    public void TestIcdConvert()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#icd-convert
        Assert200(_client.icdConvert(ExampleRequests.IcdConvertRequest));
    }

    [Test]
    public void TestIdentityProofGenerate()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#identity-proof
        Assert200(_client.createProofQuestion(ExampleRequests.CreateProofQuestionExample)); 
    }

    [Test]
    public void TestIdentityValidProof()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#identity-proof
        Assert200(_client.validateIdentity(ExampleRequests.ValidateIdentityExample)); 
    }

    [Test]
    public void TestInsurancePrices()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#insurance-prices
        Assert200(_client.insurancePrices(ExampleRequests.PricesRequestExample)); 
    }

    [Test]
    public void TestMpc()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#medical-procedure-code
        Assert200(_client.mpc(ExampleRequests.MpcRequest)); 
    }

    [Test]
    public void TestOopLoadPrice()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#oop-price-estimate
        Assert200(_client.oopInsuranceLoadPrice(ExampleRequests.OopLoadPriceExample)); 
    }

    [Test]
    public void TestOopGetPrice()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#oop-price-estimate
        Assert200(_client.oopInsuranceEstimate(ExampleRequests.OopInsuranceEstimateExample)); 
    }

    [Test]
    public void TestPharmacyFormulary()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#pharmacy-formulary
        Assert200(_client.pharmacyFormulary(ExampleRequests.PharmacyFormularyExample)); 
    }


    [Test]
    public void TestPharmacyNpiNetwork()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#in-network-pharmacies
        Assert200(_client.pharmacyNetwork("1427382266", ExampleRequests.PharmacyNetworkNpiExample)); 
    }

    [Test]
    public void TestPharmacyPlansNetwork()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#pharmacy-formulary
        Assert200(_client.pharmacyNetwork(ExampleRequests.PharmacyNetworkPlansExample)); 
    }

    [Test]
    public void TestPharmacyPlans()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#pharmacy-plans
        Assert200(_client.pharmacyPlans(ExampleRequests.PharmacyPlansExample)); 
    }

    [Test]
    public void TestPlans()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#plans
        Assert200(_client.plans(ExampleRequests.PlansRequestExample)); 
    }

    [Test]
    public void TestProviders()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#providers
        Assert200(_client.providers(ExampleRequests.ProvidersRequestExample));
        Assert200(_client.providers("1467560003")); 
    }

    [Test]
    public void TestReferrals()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#referrals
        Assert200(_client.referrals(ExampleRequests.ReferralsRequestExample)); 
    }

    [Test]
    public void TestSchedulingGet()
    {
        // https://platform.pokitdok.com/documentation/v4/?csharp#scheduling
        Assert200(_client.schedulers());
        Assert200(_client.schedulers("967d207f-b024-41cc-8cac-89575a1f6fef")); 
    }

    [Test]
    public void TestTradingPartners()
    {
        Assert200(_client.tradingPartners());
        Assert200(_client.tradingPartners("MOCKPAYER")); 
    }

    [Test]
    public void TestAppRegistrations() 
    {
        Assert200(_client.appRegistrations());
    }

}
