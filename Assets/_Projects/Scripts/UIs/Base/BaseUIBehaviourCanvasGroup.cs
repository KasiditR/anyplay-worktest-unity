using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public abstract class BaseUIBehaviourCanvasGroup : UIBehaviour
{
    [Header("Canvas Group Setting")]
    [SerializeField] private CanvasGroup canvasGroup = default;

    private Tweener showTween;
    private Tweener hideTween;

    private Coroutine showCoroutine;
    private Coroutine hideCoroutine;

    public event Action OnShow;
    public event Action OnHide;

    private const float ALPHA_FADE_DURATION = 0.25f;
    protected float alpha
    {
        get => canvasGroup.alpha;
        set => canvasGroup.alpha = value;
    }

    protected bool interactable
    {
        get => canvasGroup.interactable;
        set => canvasGroup.interactable = value;
    }

    protected bool blocksRaycasts
    {
        get => canvasGroup.blocksRaycasts;
        set => canvasGroup.blocksRaycasts = value;
    }

    protected bool ignoreParentGroups
    {
        get => canvasGroup.ignoreParentGroups;
        set => canvasGroup.ignoreParentGroups = value;
    }

    public virtual void Show(Action onDone = null)
    {
        interactable = true;
        blocksRaycasts = true;
        ignoreParentGroups = false;

        if (showCoroutine != null)
        {
            StopCoroutine(showCoroutine);
        }

        if (showTween != null)
        {
            showTween.Kill();
        }

        showCoroutine = StartCoroutine(ShowRoutine(onDone));
    }

    public virtual void Hide(Action onDone = null)
    {
        interactable = false;
        blocksRaycasts = false;
        ignoreParentGroups = false;

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        if (hideTween != null)
        {
            hideTween.Kill();
        }

        hideCoroutine = StartCoroutine(HideRoutine(onDone));
    }

    protected virtual IEnumerator ShowRoutine(Action onDone = null)
    {
        showTween = DOVirtual.Float(alpha, 1, ALPHA_FADE_DURATION, value => alpha = value).SetUpdate(UpdateType.Late, true);
        yield return new WaitUntil(() => alpha == 1);
        yield return new WaitForEndOfFrame();
        onDone?.Invoke();
        OnShow?.Invoke();
    }

    protected virtual IEnumerator HideRoutine(Action onDone = null)
    {
        hideTween = DOVirtual.Float(alpha, 0, ALPHA_FADE_DURATION, value => alpha = value).SetUpdate(UpdateType.Late, true);
        yield return new WaitUntil(() => alpha == 0);
        yield return new WaitForEndOfFrame();
        onDone?.Invoke();
        OnHide?.Invoke();
    }

#if UNITY_EDITOR
    protected override void Reset()
    {
        base.Reset();
        canvasGroup ??= GetComponent<CanvasGroup>();
    }
#endif
}
