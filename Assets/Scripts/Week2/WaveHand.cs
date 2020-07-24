using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class WaveHand : MonoBehaviour
{
    [SerializeField] private RectTransform hole;
    [SerializeField] private RectTransform hand;
    [SerializeField] private RectTransform byebyeParent;
    [SerializeField] private Image byebye;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        DOTween.Sequence()
            .Append(hole.DOScale(0, 0))
            .Join(hand.DOAnchorPos(new Vector2(-560f, -1000f), 0))
            .Join(hand.DOLocalRotate(new Vector3(0, 0, -40f), 0))
            .Join(byebyeParent.DOLocalRotate(new Vector3(0, 0, 60f),0 ))
            .Join(byebyeParent.DOScale(0, 0))
            .AppendInterval(1f)
            .Append(hole.DOScale(1, 0.6f).SetEase(Ease.OutBack))
            .AppendInterval(0.4f)
            .Append(hand.DOAnchorPos(new Vector2(-240f, -440f), 0.3f))
            .Join(hand.DOLocalRotate(new Vector3(0, 0, -24f), 0.2f).SetDelay(0.1f))
            .Join(byebyeParent.DOScale(1, 0.3f))
            .Append(hand.DOLocalRotate(new Vector3(0, 0, -36f), 0.2f).SetLoops(9, LoopType.Yoyo).SetEase(Ease.InOutQuad))
            .Join(byebyeParent.DOLocalRotate(new Vector3(0, 0, -60f), 1.8f).SetEase(Ease.InQuad))
            .AppendInterval(0.2f)
            .Append(hand.DOAnchorPos(new Vector2(-560f, -1000f), 0.3f))
            .Join(byebyeParent.DOScale(0, 0.3f))
            .Append(hole.DOScale(0, 0.4f).SetEase(Ease.InBack));
    }
}
