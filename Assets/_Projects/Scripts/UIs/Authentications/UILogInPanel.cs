using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UILogInPanel : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private TMP_InputField inputUserName;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private Button buttonSignUp;
    [SerializeField] private Button buttonLogin;

    private UIAuthentication uiAuthentication;
    public void Initialize(UIAuthentication uiAuthentication)
    {
        this.uiAuthentication = uiAuthentication;
    }

    public override void Show(Action onDone = null)
    {
        base.Show(onDone);
        inputUserName.text = string.Empty;
        inputPassword.text = string.Empty;
    }

    protected override void OnEnable()
    {
        buttonSignUp.onClick.AddListener(OnSignUpClicked);
        buttonLogin.onClick.AddListener(OnLoginClicked);
    }

    protected override void OnDisable()
    {
        buttonSignUp.onClick.RemoveAllListeners();
        buttonLogin.onClick.RemoveAllListeners();
    }

    private void OnSignUpClicked()
    {
        uiAuthentication.ShowSignUpPanel();
    }

    private void OnLoginClicked()
    {
        if (string.IsNullOrEmpty(inputUserName.text) || string.IsNullOrEmpty(inputPassword.text))
        {
            UICanvasManager.Instance.UIMessageBox.Show("Please fill in all fields.");
            return;
        }

        uiAuthentication.Login(inputUserName.text, inputPassword.text);
    }

}
