using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System.Security.Principal;


namespace DirectAccessApp
{
    public class ManualFlowClient
    {
       
        public const string BaseAddress = "https://localhost:44300/core";

        public const string AuthorizeEndpoint = BaseAddress + "/connect/authorize";
        public const string LogoutEndpoint = BaseAddress + "/connect/endsession";
        public const string TokenEndpoint = BaseAddress + "/connect/token";
        public const string UserInfoEndpoint = BaseAddress + "/connect/userinfo";
        public const string IdentityTokenValidationEndpoint = BaseAddress + "/connect/identitytokenvalidation";
        public const string TokenRevocationEndpoint = BaseAddress + "/connect/revocation";


        public string Connect(string scopes)
        {
            var state = Guid.NewGuid().ToString("N");
            var nonce = Guid.NewGuid().ToString("N");
            SetTempState(state, nonce);

            var request = new AuthorizeRequest(AuthorizeEndpoint);

            var url = request.CreateAuthorizeUrl(
                clientId: "codeclient",
                responseType: "code",
                scope: scopes,
                redirectUri: "http://localhost:55820/ManualCodeFlowDemo/callback",
                state: state,
                nonce: nonce);

            return url;
        }
        private void SetTempState(string state, string nonce)
        {
            var tempId = new ClaimsIdentity("TempState");
            tempId.AddClaim(new Claim("state", state));
            tempId.AddClaim(new Claim("nonce", nonce));

           HttpContext.Current.Request.GetOwinContext().Authentication.SignIn(tempId);
        }

        public async Task<TokenResponse> GetToken()
        {
            var client = new TokenClient(
          TokenEndpoint,
            "codeclient",
            "secret");

            var code =HttpContext.Current.Request.QueryString["code"];
            var tempState = await GetTempStateAsync();
           HttpContext.Current.Request.GetOwinContext().Authentication.SignOut("TempState");

            var response = await client.RequestAuthorizationCodeAsync(
                code,
                "https://localhost:44312/callback");

            await ValidateResponseAndSignInAsync(response, tempState.Item2);

            //if (!string.IsNullOrEmpty(response.IdentityToken))
            //{
            //    ViewBag.IdentityTokenParsed = ParseJwt(response.IdentityToken);
            //}
            //if (!string.IsNullOrEmpty(response.AccessToken))
            //{
            //    ViewBag.AccessTokenParsed = ParseJwt(response.AccessToken);
            //}

            return response;
        }
        private async Task ValidateResponseAndSignInAsync(TokenResponse response, string nonce)
        {
            if (!string.IsNullOrWhiteSpace(response.IdentityToken))
            {
                var tokenClaims = ValidateToken(response.IdentityToken, nonce);
                var claims = new List<Claim>();

                if (!string.IsNullOrWhiteSpace(response.AccessToken))
                {
                    claims.AddRange(await GetUserInfoClaimsAsync(response.AccessToken));

                    claims.Add(new Claim("access_token", response.AccessToken));
                    claims.Add(new Claim("expires_at", (DateTime.UtcNow.ToEpochTime() + response.ExpiresIn).ToDateTimeFromEpoch().ToString()));
                }

                if (!string.IsNullOrWhiteSpace(response.RefreshToken))
                {
                    claims.Add(new Claim("refresh_token", response.RefreshToken));
                }

                var id = new ClaimsIdentity(claims, "Cookies");
               HttpContext.Current.Request.GetOwinContext().Authentication.SignIn(id);
            }
        }

        private List<Claim> ValidateToken(string token, string nonce)
        {
            var certString = "MIIDBTCCAfGgAwIBAgIQNQb+T2ncIrNA6cKvUA1GWTAJBgUrDgMCHQUAMBIxEDAOBgNVBAMTB0RldlJvb3QwHhcNMTAwMTIwMjIwMDAwWhcNMjAwMTIwMjIwMDAwWjAVMRMwEQYDVQQDEwppZHNydjN0ZXN0MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAqnTksBdxOiOlsmRNd+mMS2M3o1IDpK4uAr0T4/YqO3zYHAGAWTwsq4ms+NWynqY5HaB4EThNxuq2GWC5JKpO1YirOrwS97B5x9LJyHXPsdJcSikEI9BxOkl6WLQ0UzPxHdYTLpR4/O+0ILAlXw8NU4+jB4AP8Sn9YGYJ5w0fLw5YmWioXeWvocz1wHrZdJPxS8XnqHXwMUozVzQj+x6daOv5FmrHU1r9/bbp0a1GLv4BbTtSh4kMyz1hXylho0EvPg5p9YIKStbNAW9eNWvv5R8HN7PPei21AsUqxekK0oW9jnEdHewckToX7x5zULWKwwZIksll0XnVczVgy7fCFwIDAQABo1wwWjATBgNVHSUEDDAKBggrBgEFBQcDATBDBgNVHQEEPDA6gBDSFgDaV+Q2d2191r6A38tBoRQwEjEQMA4GA1UEAxMHRGV2Um9vdIIQLFk7exPNg41NRNaeNu0I9jAJBgUrDgMCHQUAA4IBAQBUnMSZxY5xosMEW6Mz4WEAjNoNv2QvqNmk23RMZGMgr516ROeWS5D3RlTNyU8FkstNCC4maDM3E0Bi4bbzW3AwrpbluqtcyMN3Pivqdxx+zKWKiORJqqLIvN8CT1fVPxxXb/e9GOdaR8eXSmB0PgNUhM4IjgNkwBbvWC9F/lzvwjlQgciR7d4GfXPYsE1vf8tmdQaY8/PtdAkExmbrb9MihdggSoGXlELrPA91Yce+fiRcKY3rQlNWVd4DOoJ/cPXsXwry8pWjNCo5JD8Q+RQ5yZEy7YPoifwemLhTdsBz3hlZr28oCGJ3kbnpW0xGvQb3VHSTVVbeei0CfXoW6iz1";
            var cert = new X509Certificate2(Convert.FromBase64String(certString));

            var parameters = new TokenValidationParameters
            {
                ValidAudience = "codeclient",
                ValidIssuer = BaseAddress,
                IssuerSigningToken = new X509SecurityToken(cert)
            };

            SecurityToken jwt;
            var principal = new JwtSecurityTokenHandler().ValidateToken(token, parameters, out jwt);

            // validate nonce
            var nonceClaim = principal.FindFirst("nonce");

            if (!string.Equals(nonceClaim.Value, nonce, StringComparison.Ordinal))
            {
                throw new Exception("invalid nonce");
            }

            return principal.Claims.ToList();
        }

        private async Task<IEnumerable<Claim>> GetUserInfoClaimsAsync(string accessToken)
        {
            var userInfoClient = new UserInfoClient(new Uri(UserInfoEndpoint), accessToken);

            var userInfo = await userInfoClient.GetAsync();

            var claims = new List<Claim>();
            userInfo.Claims.ToList().ForEach(ui => claims.Add(new Claim(ui.Item1, ui.Item2)));

            return claims;
        }

        private string ParseJwt(string token)
        {
            if (!token.Contains("."))
            {
                return token;
            }

            var parts = token.Split('.');
            var part = Encoding.UTF8.GetString(Base64Url.Decode(parts[1]));

            var jwt = JObject.Parse(part);
            return jwt.ToString();
        }

        public async Task<Tuple<string, string>> GetTempStateAsync()
        {
            var data = await  HttpContext.Current.Request.GetOwinContext().Authentication.AuthenticateAsync("TempState");

            var state = data.Identity.FindFirst("state").Value;
            var nonce = data.Identity.FindFirst("nonce").Value;

            return Tuple.Create(state, nonce);
        }

        public async Task<JArray> CallService(string apiurl)
        {
            var principal = HttpContext.Current.User as ClaimsPrincipal;

            var client = new HttpClient();
            client.SetBearerToken(principal.FindFirst("access_token").Value);

            var result = await client.GetStringAsync(apiurl);

            return JArray.Parse(result);
        }

        public async Task<TokenResponse> RefreshToken()
        {
            var client = new TokenClient(
                TokenEndpoint,
                "codeclient",
                "secret");

            var principal = HttpContext.Current.User as ClaimsPrincipal;
            var refreshToken = principal.FindFirst("refresh_token").Value;

            var response = await client.RequestRefreshTokenAsync(refreshToken);
            UpdateCookie(response);

            return response;
        }

        public async Task<HttpResponseMessage> RevokeAccessToken()
        {
            var accessToken = (HttpContext.Current.User as ClaimsPrincipal).FindFirst("access_token").Value;
            var client = new HttpClient();
            client.SetBasicAuthentication("codeclient", "secret");

            var postBody = new Dictionary<string, string>
            {
                { "token", accessToken },
                { "token_type_hint", "access_token" }
            };

            var result = await client.PostAsync(TokenRevocationEndpoint, new FormUrlEncodedContent(postBody));

            return result;
        }

        public async Task<HttpResponseMessage> RevokeRefreshToken()
        {
            var refreshToken = (HttpContext.Current.User as ClaimsPrincipal).FindFirst("refresh_token").Value;
            var client = new HttpClient();
            client.SetBasicAuthentication("codeclient", "secret");

            var postBody = new Dictionary<string, string>
            {
                { "token", refreshToken },
                { "token_type_hint", "refresh_token" }
            };

            var result = await client.PostAsync(TokenRevocationEndpoint, new FormUrlEncodedContent(postBody));

            return result;
        }

        private void UpdateCookie(TokenResponse response)
        {
            if (response.IsError)
            {
                throw new Exception(response.Error);
            }

            var identity = (HttpContext.Current.User as ClaimsPrincipal).Identities.First();
            var result = from c in identity.Claims
                         where c.Type != "access_token" &&
                               c.Type != "refresh_token" &&
                               c.Type != "expires_at"
                         select c;

            var claims = result.ToList();

            claims.Add(new Claim("access_token", response.AccessToken));
            claims.Add(new Claim("expires_at", (DateTime.UtcNow.ToEpochTime() + response.ExpiresIn).ToDateTimeFromEpoch().ToString()));
            claims.Add(new Claim("refresh_token", response.RefreshToken));

            var newId = new ClaimsIdentity(claims, "Cookies");
            HttpContext.Current.Request.GetOwinContext().Authentication.SignIn(newId);
        }

    }
}
