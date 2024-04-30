using System;
using BestHTTP;
using Utility;

public static class AuthenticationController
{
    public static void Authenticate(Action<BearerTokenDto> OnAuthenticationSuccess)
    {
        string requestUri = UriUtil.FormatUri(Config.OauthToken);
        var request = new HTTPRequest(new Uri(requestUri), methodType: HTTPMethods.Post, (request, response) =>
        {
            if (response.StatusCode == 200)
            {
                BearerTokenDto bearerTokenDto = JsonUtil.CreateFromJSON<BearerTokenDto>(response.DataAsText);
                OnAuthenticationSuccess?.Invoke(bearerTokenDto);
            }
        });

        request.AddField("username", UserCredentials.Username);
        request.AddField("password", UserCredentials.Password);
        request.AddField("grant_type", "password");
        request.AddField("scope", "");
        request.AddField("client_id", "2");
        request.AddField("client_secret", "GmchUV5ztqIIlXQg1ODdjWK1eDy4WtQCUdmV13bM");

        request.AddHeader("Content-Type", "application/json");
        
        request.Send();
    }
}