namespace GoogleLogin.Infrastructure;

public static class GoogleApiSetting
{
    public static string AuthUrl { get; set; } = "https://accounts.google.com/o/oauth2/v2/auth/oauthchooseaccount?redirect_uri={RedirectUrl}&prompt={Prompt}&response_type={ResponseType}&client_id={ClientId}&scope={Scope}&access_type={AccessType}&service={Service}&o2v={O2V}&flowName={FlowName}";
    public static string Prompt { get; set; } = "consent";
    public static string ResponseType { get; set; } = "code";
    public static string Scope { get; set; } = "https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/userinfo.profile";
    public static string AccessType { get; set; } = "offline";
    public static string Service { get; set; } = "lso";
    public static string O2V { get; set; } = "2";
    public static string FlowName { get; set; } = "GeneralOAuthFlow";
    public static string GrandType { get; set; } = "authorization_code";
    public static string GetTokenPostUrl { get; set; } = "https://oauth2.googleapis.com/token";
    public static string GetUserInfoPostUrl { get; set; } = "https://www.googleapis.com/oauth2/v1/userinfo";
    //Dinamik alanlar
    public static string RedirectUrl { get; set; } = "https://localhost:7075/home/GoogleLogin";
    public static string ClientId { get; set; } = "579995378904-gsj77t03k8fvt6gvjertvhphe7ld1afa.apps.googleusercontent.com";
    public static string ClientSecret { get; set; } = "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";
}