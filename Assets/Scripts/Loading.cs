using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private Image[] firstPoints;
    [SerializeField] private Image[] secondPoints;
    [SerializeField] private Image[] ripples;
    
    private Vector2 size;
    private const float POINT_SIZE = 24f;

    private void Start()
    {
        size = GetComponent<RectTransform>().sizeDelta;
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        for (var i = 0; i < 2; i++)
        {
            var firstPoint = firstPoints[i];
            var secondPoint = secondPoints[i];
            var ripple = ripples[i];

            DOTween.Sequence()
                .OnStart(() =>
                {
                    Initialize(firstPoint, secondPoint, ripple);
                })
                .Append(firstPoint.rectTransform.DOSizeDelta(new Vector2(size.x + POINT_SIZE, POINT_SIZE), 0.2f).SetEase(Ease.InQuad))
                .AppendCallback(() => secondPoint.gameObject.SetActive(true))
                .Append(secondPoint.rectTransform.DOSizeDelta(new Vector2(size.y + POINT_SIZE, POINT_SIZE), 0.2f).SetEase(Ease.OutQuad))
                .AppendInterval(0.2f)
                .AppendCallback(() =>
                {
                    firstPoint.rectTransform.SetPivotWithKeepingPosition(new Vector2(1, 0.5f));
                    secondPoint.rectTransform.SetPivotWithKeepingPosition(new Vector2(0, 0.5f));
                })
                .Append(firstPoint.rectTransform.DOSizeDelta(new Vector2(POINT_SIZE, POINT_SIZE), 0.12f).SetEase(Ease.InQuad))
                .AppendCallback(() => firstPoint.gameObject.SetActive(false))
                .Append(secondPoint.rectTransform.DOSizeDelta(new Vector2(POINT_SIZE, POINT_SIZE), 0.12f).SetEase(Ease.OutQuad))
                .AppendInterval(0.4f)
                .AppendCallback(() => secondPoint.rectTransform.SetPivotWithKeepingPosition(Vector2.one * 0.5f))
                .Append(ripple.rectTransform.DOScale(8f, 0.8f))
                .Join(secondPoint.rectTransform.DOScale(1.6f, 0.1f))
                .Join(secondPoint.rectTransform.DOScale(1, 0.3f).SetDelay(0.1f))
                .Join(ripple.DOFade(0, 0.4f).SetDelay(0.4f))
                .OnStepComplete(() => Initialize(firstPoint, secondPoint, ripple))
                .SetLoops(-1);
        }
    }

    private void Initialize(Image firstPoint, Image secondPoint, Image ripple)
    {
        firstPoint.gameObject.SetActive(true);
        secondPoint.gameObject.SetActive(false);

        var firstRect = firstPoint.rectTransform;
        var secondRect = secondPoint.rectTransform;
        firstPoint.rectTransform.SetPivotWithKeepingPosition(new Vector2(0, 0.5f));
        secondPoint.rectTransform.SetPivotWithKeepingPosition(new Vector2(1, 0.5f));
        firstRect.pivot = new Vector2(0, 0.5f);
        secondRect.pivot = new Vector2(1, 0.5f);
        firstRect.sizeDelta = Vector2.one * POINT_SIZE;
        secondRect.sizeDelta = Vector2.one * POINT_SIZE;
        firstRect.anchoredPosition = new Vector2(-POINT_SIZE / 2, 0);
        secondRect.anchoredPosition = new Vector2(0, POINT_SIZE / 2);
        
        ripple.rectTransform.localScale = Vector3.zero;
        ripple.SetAlpha(1);
    }
}
