using System;
using System.Collections.Generic;
using pokitdokcsharp;
using NUnit.Framework;
using System.Net;
using pokitdokcsharp;
using pokitdokcsharp.tests.data;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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

    /// <summary>
    /// Verify base constructor
    /// </summary>
    [Test]
    public void TestPokitDokException_DefaultConstructor()
    {
        PokitDokException exception = new PokitDokException();
        Assert.AreEqual("Exception of type 'pokitdokcsharp.PokitDokException' was thrown.", exception.Message); 
    }

    /// <summary>
    /// Verify "String" Constructor
    /// </summary>
    [Test]
    public void TestPokitDokException_StringConstructor()
    {
        PokitDokException exception = new PokitDokException("Test Message.");
        Assert.AreEqual("Test Message.", exception.Message); 
    }


    /// <summary>
    /// Verify "String-Exception" Constructor
    /// </summary>
    [Test]
    public void TestPokitDokException_StringExceptionConstructor()
    {
        WebException innerException = new WebException(); 

        PokitDokException exception = new PokitDokException("Test Message.", innerException);
        Assert.AreEqual("Test Message.", exception.Message); 
    }


    /// <summary>
    /// Verify "String-Exception" Constructor
    /// </summary>
    [Test]
    public void TestPokitDokException_VerifyInnerException()
    {
        WebException innerException = new WebException("Alternative Test Message"); 

        PokitDokException exception = new PokitDokException("Test Message.", innerException);
        Assert.AreEqual("Test Message.", exception.Message);
        Assert.AreEqual(exception.InnerException, innerException); 
    }

    /// <summary>
    /// Verify Default Constructor can be deserialized
    /// </summary>
    [Test]
    public void TestPokitDokException_DefaultConstructorDeSerializable()
    {
        // Arrange
        var originalException = new PokitDokException(); 
        var buffer = new byte[4096];
        var ms = new MemoryStream(buffer);
        var ms2 = new MemoryStream(buffer);
        var formatter = new BinaryFormatter();

        // Act
        formatter.Serialize(ms, originalException);
        var deserializedException = (PokitDokException)formatter.Deserialize(ms2);

        // Assert
        Assert.AreEqual(originalException.ResourceReferenceProperty, deserializedException.ResourceReferenceProperty);
        Assert.AreEqual(originalException.Message, deserializedException.Message);
    }


    /// <summary>
    /// Verify String Constructor can be Deserialized
    /// </summary>
    [Test]
    public void TestPokitDokException_StringConstructorDeSerializable()
    {
        // Arrange
        var originalException = new PokitDokException("Test Message."); 
        var buffer = new byte[4096];
        var ms = new MemoryStream(buffer);
        var ms2 = new MemoryStream(buffer);
        var formatter = new BinaryFormatter();

        // Act
        formatter.Serialize(ms, originalException);
        var deserializedException = (PokitDokException)formatter.Deserialize(ms2);

        // Assert
        Assert.AreEqual(originalException.ResourceReferenceProperty, deserializedException.ResourceReferenceProperty);
        Assert.AreEqual(originalException.Message, deserializedException.Message);
    }


    /// <summary>
    /// Verify String, Exception Constructor can be Deserialized
    /// </summary>
    [Test]
    public void TestPokitDokException_StringExceptionConstructorDeSerializable()
    {
        // Arrange
        var innerEx = new Exception("foo");
        var originalException = new PokitDokException("Test Message.", innerEx); 
        var buffer = new byte[4096];
        var ms = new MemoryStream(buffer);
        var ms2 = new MemoryStream(buffer);
        var formatter = new BinaryFormatter();

        // Act
        formatter.Serialize(ms, originalException);
        var deserializedException = (PokitDokException)formatter.Deserialize(ms2);

        // Assert
        Assert.AreEqual(originalException.ResourceReferenceProperty, deserializedException.ResourceReferenceProperty);
        Assert.AreEqual(originalException.InnerException.Message, deserializedException.InnerException.Message);
        Assert.AreEqual(originalException.Message, deserializedException.Message);
    }
}
