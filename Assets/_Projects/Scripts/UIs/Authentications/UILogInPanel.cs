using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILogInPanel : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private TMP_InputField _inputUserName;
    [SerializeField] private TMP_InputField _inputPassword;
    [SerializeField] private Button _buttonSignUp;
    [SerializeField] private Button _buttonLogin;

    private UIAuthentication _uiAuthentication;

    public void Initialize(UIAuthentication uiAuthentication)
    {
        this._uiAuthentication = uiAuthentication;
    }

    public override void Show(Action onDone = null)
    {
        base.Show(onDone);
        _inputUserName.text = string.Empty;
        _inputPassword.text = string.Empty;
    }

    protected override void OnEnable()
    {
        _buttonSignUp.onClick.AddListener(OnSignUpClicked);
        _buttonLogin.onClick.AddListener(OnLoginClicked);
    }

    protected override void OnDisable()
    {
        _buttonSignUp.onClick.RemoveAllListeners();
        _buttonLogin.onClick.RemoveAllListeners();
    }

    private void OnSignUpClicked()
    {
        _uiAuthentication.ShowSignUpPanel();
    }

    private void OnLoginClicked()
    {
        if (string.IsNullOrEmpty(_inputUserName.text) || string.IsNullOrEmpty(_inputPassword.text))
        {
            UICanvasManager.Instance.UIMessageBox.Show("Please fill in all fields.");
            return;
        }

        _uiAuthentication.Login(_inputUserName.text, _inputPassword.text);
    }

}
