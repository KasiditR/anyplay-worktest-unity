using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BaseUIBehaviourCanvasGroup : UIBehaviour
{
    [Header("Canvas Group Setting")]
    [SerializeField] private CanvasGroup _canvasGroup = default;

    private Tweener _showTween;
    private Tweener _hideTween;

    private Coroutine _showCoroutine;
    private Coroutine _hideCoroutine;

    public event Action OnShow;
    public event Action OnHide;

    private const float ALPHA_FADE_DURATION = 0.25f;

    protected float alpha
    {
        get => _canvasGroup.alpha;
        set => _canvasGroup.alpha = value;
    }

    protected bool interactable
    {
        get => _canvasGroup.interactable;
        set => _canvasGroup.interactable = value;
    }

    protected bool blocksRaycasts
    {
        get => _canvasGroup.blocksRaycasts;
        set => _canvasGroup.blocksRaycasts = value;
    }

    protected bool ignoreParentGroups
    {
        get => _canvasGroup.ignoreParentGroups;
        set => _canvasGroup.ignoreParentGroups = value;
    }

    public virtual void Show(Action onDone = null)
    {
        interactable = true;
        blocksRaycasts = true;
        ignoreParentGroups = false;

        if (_showCoroutine != null)
        {
            StopCoroutine(_showCoroutine);
        }

        if (_showTween != null)
        {
            _showTween.Kill();
        }

        _showCoroutine = StartCoroutine(ShowRoutine(onDone));
    }

    public virtual void Hide(Action onDone = null)
    {
        interactable = false;
        blocksRaycasts = false;
        ignoreParentGroups = false;

        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
        }

        if (_hideTween != null)
        {
            _hideTween.Kill();
        }

        _hideCoroutine = StartCoroutine(HideRoutine(onDone));
    }

    protected virtual IEnumerator ShowRoutine(Action onDone = null)
    {
        _showTween = DOVirtual.Float(alpha, 1, ALPHA_FADE_DURATION, value => alpha = value).SetUpdate(UpdateType.Late, true);
        yield return new WaitUntil(() => alpha == 1);
        yield return new WaitForEndOfFrame();
        onDone?.Invoke();
        OnShow?.Invoke();
    }

    protected virtual IEnumerator HideRoutine(Action onDone = null)
    {
        _hideTween = DOVirtual.Float(alpha, 0, ALPHA_FADE_DURATION, value => alpha = value).SetUpdate(UpdateType.Late, true);
        yield return new WaitUntil(() => alpha == 0);
        yield return new WaitForEndOfFrame();
        onDone?.Invoke();
        OnHide?.Invoke();
    }

#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        _canvasGroup ??= GetComponent<CanvasGroup>();
    }
#endif
}
