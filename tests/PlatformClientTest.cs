using System;
using System.Collections.Generic;
using pokitdokcsharp;
using NUnit.Framework;
using System.Net;
using pokitdokcsharp.tests.data;

/// <summary>
/// Platform client test.
/// </summary>
[TestFixture]
public class PlatformClientTest
{

    // Dummy class definition -- not connecting to PokitDok Platform
    OauthApplicationClient oac = new OauthApplicationClient("a_client_id", "a_client_secret");

    public string host = "http://localhost";

    /// <summary>
    /// For every request, there are things we expect to remain constant. Verify that they do.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="method"></param>
    public void AssertRequestDefaults(HttpWebRequest request, string method)
    {
        Assert.IsTrue(request.Address == new Uri(host));
        Assert.IsTrue(request.Headers.ToString() == "Authorization: Bearer\r\nContent-Type: application/json\r\n\r\n");
        Assert.IsTrue(request.Host == "localhost");
        Assert.IsTrue(request.KeepAlive == true);
        Assert.IsTrue(request.Method == method);
        Assert.IsTrue(request.Timeout == 90000);
    }

    [Test]
    public void TestCreateRequests()
    {
        oac.CreateRequest(null, "http://localhost", "POST");
        oac.CreateRequest(new Dictionary<string, object>(), "http://localhost", "POST");
        oac.CreateRequest(new Dictionary<string, object>(), "http://localhost", "POST", content_type: "test");
    }

    [Test]
    public void TestGET()
    {
        var request = oac.CreateRequest(null, "http://localhost", "GET");
        AssertRequestDefaults(request, "GET");
    }

    [Test]
    public void TestPOST()
    {
        var request = oac.CreateRequest(ExampleRequests.EligibilityRequest, "http://localhost", "POST");
        AssertRequestDefaults(request, "POST");
    }

    [Test]
    public void TestPUT()
    {
        var request = oac.CreateRequest(ExampleRequests.EligibilityRequest, "http://localhost", "PUT");
        AssertRequestDefaults(request, "PUT");
    }

    [Test]
    public void TestDELETE()
    {
        var request = oac.CreateRequest(ExampleRequests.EligibilityRequest, "http://localhost", "DELETE");
        AssertRequestDefaults(request, "DELETE");
    }
}
