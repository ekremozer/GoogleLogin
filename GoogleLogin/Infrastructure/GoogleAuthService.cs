using GoogleLogin.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;
using System.Text;

namespace GoogleLogin.Infrastructure;

public class GoogleAuthService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GoogleAuthService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetRedirectUrl()
    {
        var redirectUrl = GoogleApiSetting.AuthUrl
            .Replace("{RedirectUrl}", GoogleApiSetting.RedirectUrl)
            .Replace("{Prompt}", GoogleApiSetting.Prompt)
            .Replace("{ResponseType}", GoogleApiSetting.ResponseType)
            .Replace("{ClientId}", GoogleApiSetting.ClientId)
            .Replace("{Scope}", GoogleApiSetting.Scope)
            .Replace("{AccessType}", GoogleApiSetting.AccessType)
            .Replace("{Service}", GoogleApiSetting.Service)
            .Replace("{O2V}", GoogleApiSetting.O2V)
            .Replace("{FlowName}", GoogleApiSetting.FlowName);

        return await Task.FromResult(redirectUrl);
    }

    public async Task<GoogleTokenModel> GetTokenModel(string code)
    {
        var tokenPostModel = new
        {
            code,
            redirect_uri = GoogleApiSetting.RedirectUrl,
            client_id = GoogleApiSetting.ClientId,
            client_secret = GoogleApiSetting.ClientSecret,
            scope = GoogleApiSetting.Scope,
            grant_type = GoogleApiSetting.GrandType,
            access_type = GoogleApiSetting.AccessType,
        };
        var tokenPostJson = JsonConvert.SerializeObject(tokenPostModel);

        var httpClient = _httpClientFactory.CreateClient();
        var stringContent = new StringContent(tokenPostJson, Encoding.UTF8, "application/json");
        var tokenResponseMessage = await httpClient.PostAsync(GoogleApiSetting.GetTokenPostUrl, stringContent);

        var tokenResponseContent = await tokenResponseMessage.Content.ReadAsStringAsync();
        if (tokenResponseMessage.StatusCode != HttpStatusCode.OK) return null;

        var tokenModel = JsonConvert.DeserializeObject<GoogleTokenModel>(tokenResponseContent);
        return tokenModel;
    }

    public async Task<GoogleUserInfo> GetUserInfo(string accessToken)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var userInfoResponseMessage = await httpClient.GetAsync(GoogleApiSetting.GetUserInfoPostUrl);

        var userInfoResponseContent = await userInfoResponseMessage.Content.ReadAsStringAsync();
        if (userInfoResponseMessage.StatusCode != HttpStatusCode.OK) return null;

        var userInfo = JsonConvert.DeserializeObject<GoogleUserInfo>(userInfoResponseContent);
        return userInfo;
    }
}