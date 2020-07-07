using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class Loading2 : MonoBehaviour
{
    [SerializeField] private Image[] charImages;
    [SerializeField] private RectTransform innerCircle;
    [SerializeField] private Color loadingColor;
    [SerializeField] private Color doneColor;

    private Sequence boundSequence;
    private bool isDone;

    private const float BOUND_DURATION = 1f;
    private const float BOUND_DIST = 120f;

    private void Start()
    {
        isDone = false;
        BoundCircle();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) isDone = true;
    }

    /// <summary>
    /// ループ用バウンドアニメーション
    /// </summary>
    private void BoundCircle()
    {
        var circleRect = charImages[1].rectTransform;
        circleRect.SetPivotWithKeepingPosition(new Vector2(0.5f, 0));
        var defaultPosY = circleRect.anchoredPosition.y;
        innerCircle.localScale = Vector3.zero;
        foreach (var charImage in charImages)
        {
            charImage.color = loadingColor;
        }

        boundSequence = DOTween.Sequence()
            .Append(circleRect.DOScaleY(0.7f, BOUND_DURATION / 8))
            .Append(circleRect.DOScaleY(1f, BOUND_DURATION / 8).SetEase(Ease.InQuad))
            .AppendCallback(() =>
            {
                if (isDone)
                {
                    boundSequence.Kill();
                    PlayDoneAnimation();
                }
            })
            .Append(circleRect.DOAnchorPosY(defaultPosY + BOUND_DIST, BOUND_DURATION * 3 / 8).SetEase(Ease.OutSine))
            .Append(circleRect.DOAnchorPosY(defaultPosY, BOUND_DURATION * 3 / 8).SetEase(Ease.InSine))
            .SetLoops(-1);
    }

    /// <summary>
    /// 完了時のアニメーションを再生
    /// </summary>
    private void PlayDoneAnimation()
    {
        // D、O以外の文字
        for (int i = 2; i < charImages.Length; i++)
        {
            DOTween.Sequence()
                .Append(charImages[i].rectTransform.DOAnchorPosX(400f, 0.2f).SetEase(Ease.InQuad))
                .Join(charImages[i].DOFade(0, 0.1f).SetDelay(0.1f));
        }

        // D、O
        var check = charImages[0];
        var circle = charImages[1];
        DOTween.Sequence()
            .Append(circle.rectTransform.DOAnchorPos(new Vector2(0, BOUND_DIST * 2), 0.4f))
            .Join(circle.rectTransform.DOScale(2f, 0.4f))
            .Append(check.rectTransform.DOLocalRotate(new Vector3(0, 180, 45), 0.2f))
            .Append(check.rectTransform.DOAnchorPos(Vector2.zero, 0.2f))
            .AppendCallback(() => circle.rectTransform.SetPivotWithKeepingPosition(new Vector2(0.5f, 0.5f)))
            .Append(circle.rectTransform.DOAnchorPosY(0, 0.04f).SetEase(Ease.InSine))
            .AppendCallback(() =>
            {
                circle.color = doneColor;
                check.color = doneColor;
            })
            .AppendInterval(0.06f)
            .Append(circle.rectTransform.DOScale(2.4f, 0.1f).SetEase(Ease.OutBack))
            .Join(circle.DOColor(doneColor, 0.1f))
            .Join(innerCircle.DOScale(0.84f, 0.1f))
            .Join(check.rectTransform.DOScale(1.2f, 0.1f).SetEase(Ease.OutBack))
            .Join(check.rectTransform.DOAnchorPosY(12f, 0.1f))
            .AppendInterval(0.6f)
            .Append(circle.rectTransform.DOScale(1.2f, 0.3f).SetRelative(true))
            .Join(circle.DOFade(0, 0.3f))
            .Join(check.DOFade(0, 0.3f));
    }
}