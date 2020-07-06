using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class ClickViewer : MonoBehaviour
{
    private Color color;
    [SerializeField] private Image circle;
    [SerializeField] private Image ripple;

    private const float FADE_DURATION = 0.4f;
    
    private void Start()
    {
        circle.color = color;
        ripple.color = color;

        var sequence = DOTween.Sequence();
        sequence.Append(circle.DOFade(0, FADE_DURATION / 2))
            .Join(ripple.DOFade(0, FADE_DURATION))
            .Join(ripple.rectTransform.DOScale(2f, FADE_DURATION))
            .SetLink(gameObject)
            .OnComplete(() => Destroy(gameObject));
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }
}
