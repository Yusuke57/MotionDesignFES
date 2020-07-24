using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Kiitos : MonoBehaviour
{
    [SerializeField] private Transform shapeParent;
    [SerializeField] private Image textImage;

    private RectTransform[] shapes;
    private Vector2 shapePos;
    private Vector2 textPos;
    
    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        shapes = shapeParent.GetComponentsInChildren<RectTransform>();
        shapePos = shapes[0].anchoredPosition;
        textPos = textImage.rectTransform.anchoredPosition;
        for (var i = 1; i < shapes.Length; i++)
        {
            shapes[i].gameObject.SetActive(false);
        }

        DOTween.Sequence()
            .Append(shapes[0].DOAnchorPos(Vector2.zero, 0))
            .Join(shapes[0].DOScale(0, 0))
            .Join(textImage.DOFade(0, 0))
            .Join(textImage.rectTransform.DOAnchorPosX(400f, 0))
            .AppendCallback(() => shapes[1].gameObject.SetActive(true))
            
            .AppendInterval(0.1f)
            
            .Append(shapes[0].DOScale(2, 0.4f).SetEase(Ease.OutBack))
            .AppendInterval(0.2f)
            .AppendCallback(() =>
            {
                shapes[1].gameObject.SetActive(false);
                shapes[2].gameObject.SetActive(true);
                shapes[3].gameObject.SetActive(true);
            })
            .Append(shapes[2].DOAnchorPos(new Vector2(-10f, -10f), 0.1f).SetLoops(2, LoopType.Yoyo).SetRelative(true))
            .Join(shapes[3].DOAnchorPos(new Vector2(10f, 10f), 0.1f).SetLoops(2, LoopType.Yoyo).SetRelative(true))
            .AppendInterval(0.1f)
            .AppendCallback(() =>
            {
                shapes[3].gameObject.SetActive(false);
                shapes[4].gameObject.SetActive(true);
                shapes[5].gameObject.SetActive(true);
            })
            .Append(shapes[4].DOAnchorPos(new Vector2(10f, -10f), 0.1f).SetLoops(2, LoopType.Yoyo).SetRelative(true))
            .Join(shapes[5].DOAnchorPos(new Vector2(-10f, 10f), 0.1f).SetLoops(2, LoopType.Yoyo).SetRelative(true))

            .AppendInterval(0.2f)
            .Append(shapes[0].DOAnchorPos(shapePos, 0.4f))
            .Join(shapes[0].DOScale(1, 0.4f))
            .Append(textImage.DOFade(1, 0.2f))
            .Join(textImage.rectTransform.DOAnchorPosX(textPos.x, 0.3f));

    }
}
