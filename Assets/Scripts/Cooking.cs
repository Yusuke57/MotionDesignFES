using DG.Tweening;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    [SerializeField] private RectTransform panParent;
    [SerializeField] private RectTransform rice;
    [SerializeField] private RectTransform[] fires;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        DOTween.Sequence()
            .Append(panParent.DOLocalRotate(Vector3.zero, 0))
            .Join(rice.DOLocalRotate(Vector3.zero, 0))
            .Append(panParent.DOLocalRotate(Vector3.back * -20f, 0.2f))
            .Join(panParent.DOLocalRotate(Vector3.back * 40f, 0.4f).SetDelay(0.2f))
            .Join(rice.DOLocalRotate(Vector3.back * 180, 1f))
            .Join(panParent.DOLocalRotate(Vector3.back * -10f, 0.8f).SetDelay(0.4f))
            .Join(rice.DOLocalRotate(Vector3.back * 360, 0.8f).SetEase(Ease.InQuad))
            .Join(panParent.DOLocalRotate(Vector3.zero, 0.2f).SetDelay(0.8f))
            .Append(fires[0].DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo).SetDelay(0.1f))
            .Join(fires[1].DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo).SetDelay(0.1f))
            .Join(fires[2].DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo).SetDelay(0.1f))
            .SetDelay(0.2f)
            .SetLoops(-1);
    }
}
