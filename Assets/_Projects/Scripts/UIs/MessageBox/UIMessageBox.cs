using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMessageBox : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private TMP_Text textMessage;
    [SerializeField] private Button buttonOk;
    private Action onButtonOkClick;

    public void Show(string message, Action onClickOk = null, Action onDone = null)
    {
        textMessage.text = message;
        onButtonOkClick = onClickOk;
        base.Show(onDone);
    }
    public override void Hide(Action onDone = null)
    {
        base.Hide(onDone);
        textMessage.text = string.Empty;
    }
    protected override void OnEnable()
    {
        buttonOk.onClick.AddListener(OnButtonOkClicked);
    }

    protected override void OnDisable()
    {
        buttonOk.onClick.RemoveAllListeners();
    }

    private void OnButtonOkClicked()
    {
        onButtonOkClick?.Invoke();
        this.Hide();
    }
}
