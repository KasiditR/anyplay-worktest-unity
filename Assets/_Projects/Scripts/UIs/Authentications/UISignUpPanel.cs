using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISignUpPanel : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private TMP_InputField _inputUserName;
    [SerializeField] private TMP_InputField _inputPassword;
    [SerializeField] private TMP_InputField _inputConfirmPassword;
    [SerializeField] private Button _buttonSignUp;

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
        _inputConfirmPassword.text = string.Empty;
    }

    protected override void OnEnable()
    {
        _buttonSignUp.onClick.AddListener(OnSignUpClicked);
    }

    protected override void OnDisable()
    {
        _buttonSignUp.onClick.RemoveAllListeners();
    }

    private void OnSignUpClicked()
    {
        if (string.IsNullOrEmpty(_inputUserName.text) || string.IsNullOrEmpty(_inputPassword.text) || string.IsNullOrEmpty(_inputConfirmPassword.text))
        {
            UICanvasManager.Instance.UIMessageBox.Show("Please fill in all fields.");
            return;
        }

        if (_inputPassword.text != _inputConfirmPassword.text)
        {
            UICanvasManager.Instance.UIMessageBox.Show("Passwords do not match. Please try again.");
            return;
        }

        _uiAuthentication.SignUp(_inputUserName.text, _inputPassword.text, _inputConfirmPassword.text);
    }
}
