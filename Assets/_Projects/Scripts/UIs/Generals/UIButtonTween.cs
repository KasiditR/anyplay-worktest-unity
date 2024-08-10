using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class UIButtonTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    private Tween _buttonTween;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button == null)
        {
            return;
        }

        _buttonTween?.Kill();
        _buttonTween = _button.transform.DOScale(Vector3.one * 1.05f, 0.25f).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_button == null)
        {
            return;
        }

        _buttonTween?.Kill();
        _buttonTween = _button.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InBack).OnKill(() => _button.transform.localScale = Vector3.one);
    }
}
