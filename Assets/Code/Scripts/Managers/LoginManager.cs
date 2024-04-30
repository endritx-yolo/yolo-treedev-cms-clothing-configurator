using System;
using UnityEngine;

public class LoginManager : SceneSingleton<LoginManager>
{
    public event Action<string> OnAuthenticate;
    
    [field: SerializeField] public string AccessToken { get; private set; }

    private bool _isAuthenticated;

    protected override void Awake()
    {
        base.Awake();
        Authenticate();
    }

    private void Authenticate()
    {
        if (_isAuthenticated) return;
        AuthenticationController.Authenticate((bearerToken) =>
        {
            AccessToken = bearerToken.AccessToken;
            _isAuthenticated = true;
            OnAuthenticate?.Invoke(AccessToken);
            ShowroomManager.Instance.UpdateShowroomAssets();
        });
    }
}