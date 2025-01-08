using FiscalizationService;
using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;

namespace SimpleFiscalizationClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // The URL of the service endpoint
            string serviceUrl = "https://efiskalizimi-test.tatime.gov.al:443/FiscalizationService-v3";

            // Create a binding and endpoint address
            BasicHttpBinding binding = new BasicHttpBinding
            {
                Security = new BasicHttpSecurity
                {
                    Mode = BasicHttpSecurityMode.Transport // HTTPS
                }
            };

            EndpointAddress endpointAddress = new EndpointAddress(serviceUrl);
    
            
           
            // Create the service client
            using (FiscalizationServicePortTypeClient client = new FiscalizationServicePortTypeClient(binding, endpointAddress))
            {
                try
                {
                    // Simple request for demonstration
                    var request = new GetBusinessUnitsRequest
                    {
                        Header = new GetBusinessUnitsRequestHeaderType()
                        {
                            UUID = "a4503170-a16a-4e52-96d0-352452cf9ff6",
                            SendDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK"),
                        },
                        Signature = new SignatureType()
                        {
                            SignedInfo = new SignedInfoType()
                            {
                                CanonicalizationMethod = new CanonicalizationMethodType()
                                {
                                    Algorithm = "http://www.w3.org/2001/10/xml-exc-c14n#"
                                },
                                SignatureMethod = new SignatureMethodType()
                                {
                                    Algorithm = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256",
                                },
                                Reference = new ReferenceType[] {
                                    new ReferenceType()
                                    {
                                        URI="#Response",
                                        Transforms = new TransformType[]
                                        {
                                            new TransformType()
                                            {
                                                Algorithm="http://www.w3.org/2000/09/xmldsig#enveloped-signature"
                                            },
                                            new TransformType()
                                            {
                                                Algorithm="http://www.w3.org/2001/10/xml-exc-c14n#"
                                            },
                                        },
                                        DigestMethod = new DigestMethodType()
                                        {
                                            Algorithm="http://www.w3.org/2001/04/xmlenc#sha256"
                                        },
                                        DigestValue = Convert.FromBase64String("WYXOkHAdSLOIbwDdHCQk")
                                    }
                                },

                            },
                            SignatureValue = new SignatureValueType()
                            {
                                Value = Convert.FromBase64String("WYXOkHAdSLOIbwDdHCQk")
                            },
                            KeyInfo = new KeyInfoType()
                            {
                                Items = new object[] {
                                     new  X509DataType()
                                     {
                                         Items = new object[]
                                         {
                                             new X509Certificate(Convert.FromBase64String("WYXOkHAdSLOIbwDdHCQk"))
                                         }
                                     }
                                }
                            }
                        }
                    };

                    var request1 = new FiscalizationService.getBusinessUnitsRequest1(request);

                    // Send the request
                    var response = client.getBusinessUnits(request1);

                    // Print response details
                    Console.WriteLine($"Response: {response}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
