using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Page404 : MonoBehaviour
{
    [SerializeField] private RectTransform compass;
    [SerializeField] private Image[] images404;
    [SerializeField] private RectTransform background;

    float angle = 0f;
    float duration = 0f;
    int seed = Environment.TickCount;

    private void Awake()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        DOTween.Sequence()
            .OnStart(() =>
            {
                foreach (var image in images404) image.SetAlpha(0);
                background.localScale = Vector3.one * 2f;
                DOTween.Sequence()
                    .Append(background.DOScale(1, 1f))
                    .Append(images404[0].DOFade(1, 0.4f))
                    .Join(images404[1].DOFade(1, 0.4f))
                    .Join(images404[2].DOFade(1, 0.4f))
                    .OnComplete(() =>
                    {
                        background.DOScale(1.04f, 1f)
                            .SetEase(Ease.InOutQuad)
                            .SetLoops(-1, LoopType.Yoyo);
                    });
            })
            .AppendCallback(() =>
            {
                compass.DOKill();
                compass.DOLocalRotate(Vector3.forward * Random.Range(-1200, 1200), 1.4f)
                    .SetEase(Ease.Linear)
                    .SetRelative(true);
            })
            .AppendInterval(Random.Range(0.4f, 1.4f))
            .SetLoops(-1);
    }
}
