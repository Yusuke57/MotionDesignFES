using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Skateboard : MonoBehaviour
{
    [SerializeField] private RectTransform mask;
    [SerializeField] private Image roadImage;
    [SerializeField] private RectTransform skateboard;
    [SerializeField] private RectTransform foot;
    
    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        DOTween.Sequence()
            .Append(mask.DOSizeDelta(Vector2.zero, 0))
            .Join(skateboard.DOAnchorPosY(800f, 0))
            .Join(foot.DOAnchorPosY(800f, 0))
            .AppendInterval(1f)
            .AppendCallback(() =>
            {
                roadImage.materialForRendering.mainTextureOffset = Vector2.zero;
                roadImage.materialForRendering
                    .DOOffset(new Vector2(0, -1), 1f)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1, LoopType.Incremental);
            })
            .Append(mask.DOSizeDelta(Vector2.one * 800f, 0.4f).SetEase(Ease.OutBack))
            .AppendInterval(0.4f)
            .Append(skateboard.DOAnchorPosY(60f, 0.4f).SetEase(Ease.InCubic))
            .Join(foot.DOAnchorPosY(0, 0.4f).SetEase(Ease.InCubic).SetDelay(0.2f))
            .Join(skateboard.DOAnchorPosY(0, 0.2f).SetEase(Ease.InCubic).SetDelay(0.2f))
            .AppendCallback(() =>
            {
                DOTween.Sequence()
                    .Append(skateboard.DOAnchorPosY(40f, 0.1f))
                    .Join(foot.DOAnchorPosY(40f, 0.1f))
                    .Append(skateboard.DOAnchorPosY(0, 0.2f))
                    .Join(foot.DOAnchorPosY(0, 0.2f).SetDelay(0.1f))
                    .SetDelay(1f)
                    .SetLoops(-1);
            })
            .AppendInterval(5f)
            .Append(mask.DOSizeDelta(Vector2.zero, 0.4f).SetEase(Ease.OutQuint));
    }

    private void OnDisable()
    {
        roadImage.materialForRendering.mainTextureOffset = Vector2.zero;
    }
}
