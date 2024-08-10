using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIMessageBox : BaseUIBehaviourCanvasGroup
{
    [SerializeField] private RectTransform _rectMessage;
    [SerializeField] private TMP_Text _textMessage;
    [SerializeField] private Button _buttonOk;
    private Action _onButtonOkClick;
    private Tween _msgTween;

    public void Show(string message, Action onClickOk = null, Action onDone = null)
    {
        _textMessage.text = message;
        _onButtonOkClick = onClickOk;

        _msgTween?.Kill();
        _msgTween = _rectMessage.transform.DOPunchScale(Vector3.one * 1.05f, 0.25f)
                    .SetEase(Ease.Linear)
                    .OnKill(() => _rectMessage.transform.localScale = Vector3.one);

        base.Show(onDone);
    }
    public override void Hide(Action onDone = null)
    {
        base.Hide(onDone);
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
