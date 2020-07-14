using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chart : MonoBehaviour
{
    [SerializeField] private Image wave;
    [SerializeField] private Image chartBack;
    [SerializeField] private Image chart;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private GameObject percentObj;

    private void Start()
    {
        PlayAnimation(72);
    }

    private void PlayAnimation(int percent)
    {
        DOTween.Sequence()
            .Append(wave.rectTransform.DOAnchorPosY(-1320f, 0))
            .Join(chartBack.DOFade(0, 0))
            .Join(chartBack.rectTransform.DOScale(0.7f, 0))
            .Join(chart.DOFillAmount(0, 0))
            .AppendCallback(() =>
            {
                valueText.text = "";
                percentObj.SetActive(false);
            })
            .AppendInterval(1f)
            .Append(chartBack.DOFade(1, 0.3f))
            .Join(chartBack.rectTransform.DOScale(1, 0.3f).SetEase(Ease.OutBack))
            .AppendInterval(0.4f)
            .AppendCallback(() =>
            {
                var value = 0;
                percentObj.SetActive(true);
                DOTween.To(() => value, x => valueText.text = x.ToString("00"), percent, 1f);
                wave.material.DOOffset(Vector2.left, 300f).SetLoops(-1).SetEase(Ease.Linear);
            })
            .Join(chart.DOFillAmount(percent / 100f, 1f))
            .Append(wave.rectTransform.DOAnchorPosY(Mathf.Lerp(-1320f, -60, percent / 100f), 1f).SetEase(Ease.OutBack));
    }
    
    
}
