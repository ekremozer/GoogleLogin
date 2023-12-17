using Microsoft.AspNetCore.Mvc;
using GoogleLogin.Infrastructure;

namespace GoogleLogin.Controllers;

public class HomeController : Controller
{
    private readonly GoogleAuthService _googleAuthService;

    public HomeController(GoogleAuthService googleAuthService)
    {
        _googleAuthService = googleAuthService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> GoogleLoginRedirect()
    {
        var redirectUrl = await _googleAuthService.GetRedirectUrl();
        return Redirect(redirectUrl);
    }

    public async Task<IActionResult> GoogleLogin(string code)
    {
        var tokenModel = await _googleAuthService.GetTokenModel(code);
        if (tokenModel == null)
        {
            //TODO:Token alınamadı, hata yönetimi
        }
        else
        {
            var userInfo = await _googleAuthService.GetUserInfo(tokenModel.AccessToken);
            if (userInfo == null)
            {
                //TODO:Kullanıcı bilgisi alınamadı, hata yönetimi
            }
            else
            {
                ViewBag.UserInfo = userInfo;
                //TODO:Kullanıcı kayıt ve giriş işlemleri...
            }
        }

        return View("Index");
    }
}