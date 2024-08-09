using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISignUpPanel : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private TMP_InputField inputUserName;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputConfirmPassword;
    [SerializeField] private Button buttonSignUp;

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
        inputConfirmPassword.text = string.Empty;
    }

    protected override void OnEnable()
    {
        buttonSignUp.onClick.AddListener(OnSignUpClicked);
    }

    protected override void OnDisable()
    {
        buttonSignUp.onClick.RemoveAllListeners();
    }

    private void OnSignUpClicked()
    {
        if (string.IsNullOrEmpty(inputUserName.text) || string.IsNullOrEmpty(inputPassword.text) || string.IsNullOrEmpty(inputConfirmPassword.text))
        {
            UICanvasManager.Instance.UIMessageBox.Show("Please fill in all fields.");
            return;
        }

        if (inputPassword.text != inputConfirmPassword.text)
        {
            UICanvasManager.Instance.UIMessageBox.Show("Passwords do not match. Please try again.");
            return;
        }

        uiAuthentication.SignUp(inputUserName.text, inputPassword.text, inputConfirmPassword.text);
    }
}
