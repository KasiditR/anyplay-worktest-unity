using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMessageBox : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private TMP_Text _textMessage;
    [SerializeField] private Button _buttonOk;
    private Action _onButtonOkClick;

    public void Show(string message, Action onClickOk = null, Action onDone = null)
    {
        _textMessage.text = message;
        _onButtonOkClick = onClickOk;
        base.Show(onDone);
    }
    public override void Hide(Action onDone = null)
    {
        base.Hide(onDone);
        _textMessage.text = string.Empty;
    }
    protected override void OnEnable()
    {
        _buttonOk.onClick.AddListener(OnButtonOkClicked);
    }

    protected override void OnDisable()
    {
        _buttonOk.onClick.RemoveAllListeners();
    }

    private void OnButtonOkClicked()
    {
        _onButtonOkClick?.Invoke();
        this.Hide();
    }
}
