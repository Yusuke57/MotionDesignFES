using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Crown : MonoBehaviour
{
    [SerializeField] private RectTransform stand;
    [SerializeField] private RectTransform crown;
    [SerializeField] private GameObject starParent;
    [SerializeField] private RectTransform lights;
    [SerializeField] private Image shadow;

    private Image[] stars;

    private const float UP_POS = 1000f;

    private void Start()
    {
        stars = starParent.GetComponentsInChildren<Image>();
        foreach (var star in stars)
        {
            star.rectTransform.localScale = Vector3.zero;
            star.rectTransform.anchoredPosition = Vector2.zero;
        }
        PlayAnimation();
    }

    /// <summary>
    /// アニメーションを再生
    /// </summary>
    private void PlayAnimation()
    {
        var param = new TweenParams().SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        
        DOTween.Sequence()
            .Append(stand.DOAnchorPosY(UP_POS, 0))
            .Join(crown.DOAnchorPosY(UP_POS, 0))
            .Join(lights.DOScale(0, 0))
            .Join(shadow.DOFade(0, 0))
            .Join(shadow.rectTransform.DOScale(0, 0))
            .Append(stand.DOAnchorPosY(-200f, 0.4f))
            .Join(stand.DOLocalRotate(Vector3.forward * -8f, 0.4f))
            .Append(stand.DOLocalRotate(Vector3.forward * 3f, 0.06f))
            .Append(stand.DOLocalRotate(Vector3.forward * -1f, 0.03f))
            .Append(stand.DOLocalRotate(Vector3.forward * 0, 0.02f))
            .AppendInterval(0.1f)
            .Append(crown.DOAnchorPosY(0, 0.4f))
            .Join(crown.DOLocalRotate(Vector3.forward * -2f, 0.4f))
            .Join(shadow.DOFade(0.16f, 0.4f))
            .Join(shadow.rectTransform.DOScale(1f, 0.4f))
            .AppendCallback(() =>
            {
                DOTween.Sequence()
                    .Append(crown.DOAnchorPosY(30f, 0.8f))
                    .Join(shadow.DOFade(0, 0.8f))
                    .Join(shadow.rectTransform.DOScale(1.12f, 0.8f))
                    .SetAs(param);
                crown.DOLocalRotate(Vector3.forward * 2f, 0.55f).SetAs(param);
            })
            .Append(lights.DOScale(1, 0.4f))
            .AppendCallback(() =>
            {
                lights.DOLocalRotate(Vector3.back * 180f, 5f)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1);
            })
            
            .AppendCallback(() =>
            {
                for (var i = 0; i < stars.Length; i++)
                {
                    var angle = (1 - (float)i / (stars.Length - 1)) * Mathf.PI;
                    var dest = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 300f;
                    var star = stars[i];
                    DOTween.Sequence()
                        .Append(star.rectTransform.DOScale(1, 0.2f))
                        .Join(star.rectTransform.DOAnchorPos(dest, 0.2f))
                        .SetDelay(0.03f * i);

                    DOVirtual.DelayedCall(0.1f * i, () =>
                        {
                            DOTween.Sequence()
                                .Append(star.DOFade(1, 0.1f))
                                .AppendInterval(2f)
                                .Append(star.DOFade(0, 0.1f))
                                .SetLoops(-1);
                        });
                }
            })
            ;
    }
    
}
