using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentityModel;
using IdentityModel.Client;
using IdentityModel.Extensions;
using Newtonsoft.Json.Linq;

namespace DirectAccessApp.Controllers
{
    //public static class TokenClientExtensions
    //{

    //    public static Task<TokenResponse> RequestResourceOwnerPasswordAsync(this DirectAccessApp.Controllers.TokenClient client, string userName, string password, string scope = null, object extra = null, CancellationToken cancellationToken = null)
    //    {
    //        Dictionary<string, string> explicitValues = new Dictionary<string, string>()
    //  {
    //    {
    //      "grant_type",
    //      "password"
    //    },
    //    {
    //      "username",
    //      userName
    //    },
    //    {
    //      "password",
    //      password
    //    }
    //  };
    //        if (!string.IsNullOrWhiteSpace(scope))
    //            explicitValues.Add("scope", scope);
    //        return client.RequestAsync((IDictionary<string, string>)IdentityModel.Client.TokenClientExtensions.Merge(client, explicitValues, extra), cancellationToken);
    //    }
    //    private static Dictionary<string, string> Merge(TokenClient client, Dictionary<string, string> explicitValues, object extra = null)
    //    {
    //        Dictionary<string, string> dictionary1 = explicitValues;
    //        if (client.AuthenticationStyle == AuthenticationStyle.PostValues)
    //        {
    //            dictionary1.Add("client_id", client.ClientId);
    //            if (!string.IsNullOrEmpty(client.ClientSecret))
    //                dictionary1.Add("client_secret", client.ClientSecret);
    //        }
    //        Dictionary<string, string> dictionary2 = TokenClientExtensions.ObjectToDictionary(extra);
    //        if (dictionary2 != null)
    //            dictionary1 = Enumerable.ToDictionary<KeyValuePair<string, string>, string, string>(Enumerable.Concat<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>)explicitValues, Enumerable.Where<KeyValuePair<string, string>>((IEnumerable<KeyValuePair<string, string>>)dictionary2, (Func<KeyValuePair<string, string>, bool>)(add => !explicitValues.ContainsKey(add.Key)))), (Func<KeyValuePair<string, string>, string>)(final => final.Key), (Func<KeyValuePair<string, string>, string>)(final => final.Value));
    //        return dictionary1;
    //    }
    //}

    //public class TokenClient
    //{

    //    protected HttpClient _client;

    //    public AuthenticationStyle AuthenticationStyle { get; set; }
    //    public string ClientId { get; set; }
    //    public string ClientSecret { get; set; }

    //    public TokenClient(string address)
    //        : this(address, new HttpClientHandler())
    //    { }

    //    public TokenClient(string address, HttpMessageHandler innerHttpMessageHandler)
    //    {
    //        if (address == null) throw new ArgumentNullException("address");
    //        if (innerHttpMessageHandler == null) throw new ArgumentNullException("innerHttpMessageHandler");

    //        _client = new HttpClient(innerHttpMessageHandler)
    //        {
    //            BaseAddress = new Uri(address)
    //        };

    //        _client.DefaultRequestHeaders.Accept.Clear();
    //        _client.DefaultRequestHeaders.Accept.Add(
    //            new MediaTypeWithQualityHeaderValue("application/json"));

    //        AuthenticationStyle = AuthenticationStyle.None;
    //    }

    //    public TokenClient(string address, string clientId, string clientSecret, AuthenticationStyle style = AuthenticationStyle.BasicAuthentication)
    //        : this(address, clientId, clientSecret, new HttpClientHandler(), style)
    //    { }

    //    public TokenClient(string address, string clientId, AuthenticationStyle style = AuthenticationStyle.BasicAuthentication)
    //        : this(address, clientId, string.Empty, new HttpClientHandler(), style)
    //    { }

    //    public TokenClient(string address, string clientId, HttpMessageHandler innerHttpMessageHandler)
    //        : this(address, clientId, string.Empty, innerHttpMessageHandler, AuthenticationStyle.PostValues)
    //    { }

    //    public TokenClient(string address, string clientId, string clientSecret, HttpMessageHandler innerHttpMessageHandler, AuthenticationStyle style = AuthenticationStyle.BasicAuthentication)
    //        : this(address, innerHttpMessageHandler)
    //    {
    //        if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException("ClientId");

    //        AuthenticationStyle = style;
    //        ClientId = clientId;
    //        ClientSecret = clientSecret;

    //        if (style == AuthenticationStyle.BasicAuthentication)
    //        {
    //            _client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(clientId, clientSecret);
    //        }
    //    }

    //    public TimeSpan Timeout
    //    {
    //        set
    //        {
    //            _client.Timeout = value;
    //        }
    //    }

    //    public virtual async Task<TokenResponse> RequestAsync(IDictionary<string, string> form, CancellationToken cancellationToken = default(CancellationToken))
    //    {
    //        var response = await _client.PostAsync(string.Empty, new FormUrlEncodedContent(form), cancellationToken).ConfigureAwait(false);

    //        if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
    //        {
    //            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
    //            return new TokenResponse(content);
    //        }
    //        else
    //        {
    //            return new TokenResponse(response.StatusCode, response.ReasonPhrase);
    //        }
    //    }
    //}
    public class SpecialController : Controller
    {
        // GET: Special
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Urldemo(string Id)
        {
            var response = RequestToken(Id).Result;
            var claims = ShowResponse(response);
           // ShowResponse(response);
            return View(claims);
        }
     

        private async Task<TokenResponse> RequestToken(string username)
        {
            try
            {

                var client = new TokenClient(
                    Constants.TokenEndpoint,
                    "cashrewards.rofc",
                    "secret");

                //HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri(Constants.TokenEndpoint);
                //client.DefaultRequestHeaders
                //      .Accept
                //      .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,);
                //request.Content = new StringContent("{\"username\":\"John Doe\",\"password\":33}",
                //                                    Encoding.UTF8,
                //                                    "application/json");

                // idsrv supports additional non-standard parameters 
                // that get passed through to the user service
                var optional = new
                {
                    acr_values = "tenant:custom_account_store1 SkipEula dpa"
                };
                //Dictionary<string, string> explicitValues = new Dictionary<string, string>()
                //{
                //    {
                //        "grant_type",
                //        "password"
                //    },
                //    {
                //        "username",
                //        "9803325408"
                //    },
                //    {
                //        "password",
                //        "123456"
                //    }
                //    ,
                //    {
                //        "scope",
                //        "openid profile"
                //    }
                //    ,
                //    {
                //        "client_id",
                //        "cashrewards.rofc"
                //    }
                //    ,
                //    {
                //        "client_secret",
                //        "secret"
                //    }
                //    ,
                //    {
                //        "response_type",
                //        "id_token"
                //    }
                //};
               // return await client.RequestAsync(explicitValues, new CancellationToken());

                  return client.RequestResourceOwnerPasswordAsync(username, username , "openid profile", optional).Result;

            }
            catch (Exception)
            {

                throw;
            }
        }

        void CallService(string token)
        {
            //var baseAddress = Constants.AspNetWebApiSampleApi;

            //var client = new HttpClient
            //{
            //    BaseAddress = new Uri(baseAddress)
            //};

            //client.SetBearerToken(token);
            //var response = client.GetStringAsync("identity").Result;

            //"\n\nService claims:".ConsoleGreen();
            //Console.WriteLine(JArray.Parse(response));
        }

        private JObject ShowResponse(TokenResponse response)
        {
            if (!response.IsError)
            {
            
                Console.WriteLine(response.Json);

                if (response.AccessToken.Contains("."))
                {

                    var parts = response.AccessToken.Split('.');
                    var header = parts[0];
                    var claims = parts[1];


                   var headObject= JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(header)));

                    var claimsObj= JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(claims)));
                    return claimsObj;
                    //return new {Header = headObject, Claims = claimsObj};
                }
            }
            return null;
          
        }

        private async Task<IEnumerable<Claim>> ValidateIdentityTokenAsync(string token)
        {
            var certString = "MIIDBTCCAfGgAwIBAgIQNQb+T2ncIrNA6cKvUA1GWTAJBgUrDgMCHQUAMBIxEDAOBgNVBAMTB0RldlJvb3QwHhcNMTAwMTIwMjIwMDAwWhcNMjAwMTIwMjIwMDAwWjAVMRMwEQYDVQQDEwppZHNydjN0ZXN0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqnTksBdxOiOlsmRNd+mMS2M3o1IDpK4uAr0T4/YqO3zYHAGAWTwsq4ms+NWynqY5HaB4EThNxuq2GWC5JKpO1YirOrwS97B5x9LJyHXPsdJcSikEI9BxOkl6WLQ0UzPxHdYTLpR4/O+0ILAlXw8NU4+jB4AP8Sn9YGYJ5w0fLw5YmWioXeWvocz1wHrZdJPxS8XnqHXwMUozVzQj+x6daOv5FmrHU1r9/bbp0a1GLv4BbTtSh4kMyz1hXylho0EvPg5p9YIKStbNAW9eNWvv5R8HN7PPei21AsUqxekK0oW9jnEdHewckToX7x5zULWKwwZIksll0XnVczVgy7fCFwIDAQABo1wwWjATBgNVHSUEDDAKBggrBgEFBQcDATBDBgNVHQEEPDA6gBDSFgDaV+Q2d2191r6A38tBoRQwEjEQMA4GA1UEAxMHRGV2Um9vdIIQLFk7exPNg41NRNaeNu0I9jAJBgUrDgMCHQUAA4IBAQBUnMSZxY5xosMEW6Mz4WEAjNoNv2QvqNmk23RMZGMgr516ROeWS5D3RlTNyU8FkstNCC4maDM3E0Bi4bbzW3AwrpbluqtcyMN3Pivqdxx+zKWKiORJqqLIvN8CT1fVPxxXb/e9GOdaR8eXSmB0PgNUhM4IjgNkwBbvWC9F/lzvwjlQgciR7d4GfXPYsE1vf8tmdQaY8/PtdAkExmbrb9MihdggSoGXlELrPA91Yce+fiRcKY3rQlNWVd4DOoJ/cPXsXwry8pWjNCo5JD8Q+RQ5yZEy7YPoifwemLhTdsBz3hlZr28oCGJ3kbnpW0xGvQb3VHSTVVbeei0CfXoW6iz1";
            var cert = new X509Certificate2(Convert.FromBase64String(certString));
            
            var parameters = new TokenValidationParameters
            {
                ValidAudience = "cashrewards.rofc",
                ValidIssuer = Constants.BaseAddress,
                IssuerSigningToken = new X509SecurityToken(cert)
            };

            var handler = new JwtSecurityTokenHandler();
            SecurityToken jwt;
            var id = handler.ValidateToken(token, parameters, out jwt);

            //if (id.FindFirst("nonce").Value !=
            //    result.Identity.FindFirst("nonce").Value)
            //{
            //    throw new InvalidOperationException("Invalid nonce");
            //}


            return id.Claims;
        }
    }
}