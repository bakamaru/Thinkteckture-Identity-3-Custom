public static class Constants
{
    // public const string BaseAddress = "https://localhost:44300/oauth";
    //public const string BaseAddress = "https://172.18.29.125/oauth";
    public const string BaseAddress = "https://localhost:44300/oauth";

    public const string AuthorizeEndpoint = BaseAddress + "/connect/authorize";
    public const string LogoutEndpoint = BaseAddress + "/connect/endsession";
    public const string TokenEndpoint = BaseAddress + "/connect/token";
    public const string UserInfoEndpoint = BaseAddress + "/connect/userinfo";
    public const string IdentityTokenValidationEndpoint = BaseAddress + "/connect/identitytokenvalidation";
    public const string TokenRevocationEndpoint = BaseAddress + "/connect/revocation";
}