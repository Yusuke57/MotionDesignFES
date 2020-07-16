using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Space : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image sunImage;
    [SerializeField] private RectTransform moonParent;
    [SerializeField] private Image moonImage;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image flare;
    
    
    private void Start()
    {
        PlayAnimation();
    }
    
    private void PlayAnimation()
    {
        DOTween.Sequence()
            .Append(backgroundImage.DOColor(new Color(0.7f, 0.9f, 1f), 0))
            .Join(sunImage.DOColor(new Color(1f, 0.97f, 0.7f), 0))
            .Join(moonParent.DOLocalRotate(new Vector3(0, 0, 40), 0))
            .Join(moonImage.DOColor(new Color(0.84f, 0.84f, 0.6f), 0))
            .Join(text.DOFade(0, 0))
            .Join(flare.DOFade(0, 0))
            .AppendCallback(() => text.characterSpacing = 40f)
            .Append(backgroundImage.DOColor(Color.black, 2f).SetEase(Ease.InExpo))
            .Join(sunImage.DOColor(Color.white, 2f).SetEase(Ease.InExpo))
            .Join(moonParent.DOLocalRotate(Vector3.zero, 2f).SetEase(Ease.InExpo))
            .Join(moonImage.DOColor(Color.black, 2f).SetEase(Ease.InExpo))
            .Append(text.DOFade(1, 1f).SetLoops(2, LoopType.Yoyo))
            .Join(DOTween.To(() => text.characterSpacing, x => text.characterSpacing = x, 120f, 2.4f).SetEase(Ease.OutCubic))
            .Join(flare.DOFade(0.4f, 0.5f))
            .Join(flare.DOFade(0.26f, 0.5f).SetLoops(3, LoopType.Yoyo).SetDelay(0.5f))
            .Append(backgroundImage.DOColor(new Color(0.7f, 0.9f, 1f), 2f).SetEase(Ease.OutExpo))
            .Join(sunImage.DOColor(new Color(1f, 0.97f, 0.7f), 2f).SetEase(Ease.OutExpo))
            .Join(moonParent.DOLocalRotate(new Vector3(0, 0, -40), 2f).SetEase(Ease.OutExpo))
            .Join(moonImage.DOColor(new Color(0.84f, 0.84f, 0.6f), 2f).SetEase(Ease.OutExpo))
            .Join(flare.DOFade(0, 0.2f))
            .SetLoops(-1);
    }
}
