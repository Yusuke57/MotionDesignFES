using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class MindPost : MonoBehaviour
{
    [SerializeField] private Color textColor;
    
    [SerializeField] private Image shapeParent;
    [SerializeField] private Transform textParent;
    private Image[] circles;
    private Image[] lines;
    private Image[] texts;

    private const float OFFSET_TIME = 0.2f;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        // 初期化
        shapeParent.SetAlpha(0);
        var circleParent = shapeParent.transform.GetChild(1);
        circles = new Image[circleParent.childCount];
        for (var i = 0; i < circleParent.childCount; i++)
        {
            circles[i] = circleParent.GetChild(i).GetComponent<Image>();
        }
        lines = shapeParent.transform.GetChild(0).GetComponentsInChildren<Image>();
        texts = textParent.GetComponentsInChildren<Image>();
        foreach (var text in texts)
        {
            text.color = circles[0].color;
            text.SetAlpha(0);
        }

        var oPrePosY = texts[5].rectTransform.anchoredPosition.y;
        texts[5].rectTransform.anchoredPosition += Vector2.up * 100f;

        // 再生
        for (var i = 0; i < circles.Length; i++)
        {
            var circle = circles[i];
            circle.SetAlpha(0);
            DOTween.Sequence()
                .AppendInterval(OFFSET_TIME)
                .Append(circle.DOFade(1, 0.08f))
                .Join(circle.rectTransform.DOScale(1.4f, 0.08f))
                .Append(circle.rectTransform.DOScale(1f, 0.16f))
                .SetDelay(0.24f * i);
        }

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var rectTransform = line.rectTransform;
            var size = rectTransform.sizeDelta;
            rectTransform.sizeDelta = new Vector2(0, size.y);

            DOTween.Sequence()
                .AppendInterval(OFFSET_TIME)
                .Append(rectTransform.DOSizeDelta(size, 0.1f))
                .SetDelay(0.24f * i + 0.16f);
        }

        DOTween.Sequence()
            .AppendInterval(OFFSET_TIME)
            .AppendInterval(0.24f * circles.Length)
            .Append(shapeParent.DOFade(1, 0))
            .Join(texts[5].DOFade(1, 0.2f))
            .Join(texts[5].rectTransform.DOAnchorPosY(oPrePosY, 0.32f).SetEase(Ease.OutBounce))
            .AppendCallback(() =>
            {
                for (var i = 0; i < texts.Length; i++)
                {
                    DOTween.Sequence()
                        .Append(texts[i].DOFade(1, 0.14f))
                        .Append(texts[i].DOColor(textColor, 0.2f))
                        .SetDelay(Mathf.Abs(5 - i) * 0.03f);
                }
            });

    }
}
