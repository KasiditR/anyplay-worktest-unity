using System;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class UIAuthentication : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private UILogInPanel uiLogInPanel;
    [SerializeField] private UISignUpPanel uiSignUpPanel;

    protected override void Awake()
    {
        base.Awake();
        uiLogInPanel.Initialize(this);
        uiSignUpPanel.Initialize(this);
    }

    public override void Show(Action onDone = null)
    {
        base.Show(onDone);
        ShowLoginPanel();
    }

    public override void Hide(Action onDone = null)
    {
        base.Hide(onDone);
        uiLogInPanel.Hide();
        uiSignUpPanel.Hide();
    }

    public void ShowLoginPanel()
    {
        uiLogInPanel.Show();
        uiSignUpPanel.Hide();
    }
    public void ShowSignUpPanel()
    {
        uiLogInPanel.Hide();
        uiSignUpPanel.Show();
    }

    public void Login(string userName, string password)
    {
        JObject loginObject = new JObject();
        loginObject["username"] = userName;
        loginObject["password"] = password;
        APIManager.Instance.Post("login", loginObject.ToString(), OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(string response)
    {
        JObject responseObject = JObject.Parse(response);
        UserData userData = JsonConvert.DeserializeObject<UserData>(responseObject["user"].ToString());
        CoreDataManager.Instance.UserData = userData;
        UICanvasManager.Instance.UIMessageBox.Show(responseObject["message"].ToString(), () =>
        {
            this.Hide();
            UICanvasManager.Instance.UILobby.Show();
        });
    }

    private void OnLoginFailure(string response)
    {
        JObject responseObject = JObject.Parse(response);
        UICanvasManager.Instance.UIMessageBox.Show(responseObject["message"].ToString());
    }

    public void SignUp(string userName, string password, string confirmPassword)
    {
        JObject signUpObject = new JObject();
        signUpObject["username"] = userName;
        signUpObject["password"] = password;
        signUpObject["confirmPassword"] = confirmPassword;
        APIManager.Instance.Post("signup", signUpObject.ToString(), OnSignUpSuccess, OnSignUpFailure);
    }

    private void OnSignUpSuccess(string response)
    {
        JObject responseObject = JObject.Parse(response);
        UICanvasManager.Instance.UIMessageBox.Show(responseObject["message"].ToString());
        ShowLoginPanel();
    }

    private void OnSignUpFailure(string response)
    {
        JObject responseObject = JObject.Parse(response);
        UICanvasManager.Instance.UIMessageBox.Show(responseObject["message"].ToString());
    }

}
