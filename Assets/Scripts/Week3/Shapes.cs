using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class Shapes : MonoBehaviour
{
    [SerializeField] private Image background;
    
    [SerializeField] private RectTransform centerSquare;
    [SerializeField] private RectTransform[] aroundSquares;
    [SerializeField] private RectTransform centerCircle;
    [SerializeField] private Image[] circleFrames;
    [SerializeField] private RectTransform[] triangles;

    [SerializeField] private Color[] colors;

    private Vector2[] trianglePoses;
    private const float BEAT = 0.3f;
    
    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        trianglePoses = new Vector2[triangles.Length];
        for (var i = 0; i < triangles.Length; i++)
        {
            trianglePoses[i] = triangles[i].anchoredPosition;
        }
        
        DOTween.Sequence()
            .AppendCallback(() =>
            {
                background.color = colors[0];
                centerSquare.localScale = Vector3.zero;
                centerSquare.localRotation = Quaternion.Euler(Vector3.zero);
                foreach (var square in aroundSquares)
                {
                    square.localScale = Vector3.zero;
                    square.localRotation = Quaternion.Euler(Vector3.zero);
                }
                
                centerCircle.localScale = Vector3.zero;
                foreach (var circleFrame in circleFrames)
                {
                    circleFrame.fillAmount = 0;
                }

                foreach (var triangle in triangles)
                {
                    triangle.localScale = Vector3.zero;
                    triangle.anchoredPosition = Vector2.zero;
                    triangle.localRotation = Quaternion.Euler(Vector3.zero);
                }
            })
            .Append(centerSquare.DOScale(1, BEAT).SetEase(Ease.OutCirc))
            .Join(centerSquare.DOLocalRotate(Vector3.forward * 72f, BEAT).SetEase(Ease.OutCirc))
            .Append(centerSquare.DOScale(0, BEAT).SetEase(Ease.InCirc))
            .Join(centerSquare.DOLocalRotate(Vector3.forward * 144f, BEAT).SetEase(Ease.InCirc))
            .AppendCallback(() =>
            {
                foreach (var square in aroundSquares)
                {
                    DOTween.Sequence()
                        .Append(square.DOScale(1, BEAT).SetEase(Ease.OutCirc))
                        .Join(square.DOLocalRotate(Vector3.forward * 72f, BEAT).SetEase(Ease.OutCirc))
                        .Append(square.DOScale(0, BEAT / 2).SetEase(Ease.InCirc))
                        .Join(square.DOLocalRotate(Vector3.forward * 144f, BEAT / 2).SetEase(Ease.InCirc));
                }
            })
            .AppendInterval(BEAT)
            .AppendCallback(() =>
            {
                foreach (var circleFrame in circleFrames)
                {
                    circleFrame.DOFillAmount(1, BEAT * 3 / 2)
                        .SetEase(Ease.InOutCubic)
                        .SetDelay(BEAT / 2);
                }
            })
            .Append(centerCircle.DOScale(1, BEAT * 2).SetEase(Ease.OutCirc))
            .Append(centerCircle.DOScale(0, BEAT).SetEase(Ease.InCirc))
            
            .AppendCallback(() =>
            {
                for (var i = 0; i < triangles.Length; i++)
                {
                    DOTween.Sequence()
                        .Append(triangles[i].DOScale(1, BEAT * 3 / 2))
                        .Join(triangles[i].DOAnchorPos(trianglePoses[i], BEAT * 3 / 2).SetEase(Ease.OutCirc))
                        .Join(triangles[i].DOLocalRotate(Vector3.forward * 180, BEAT * 3 / 2, RotateMode.FastBeyond360).SetRelative(true))
                        .Append(triangles[i].DOScale(0, BEAT / 2));
                }
            })
            .AppendInterval(BEAT * 2)
            .SetLoops(-1);

    }
}
