using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class MotionDesignFes : MonoBehaviour
{
    [SerializeField] private Image borderCircle;
    [SerializeField] private Transform foldTrianglesParent;
    [SerializeField] private Image perticles;
    [SerializeField] private Image centerCircle;
    [SerializeField] private Transform textParent;
    [SerializeField] private Image aroundLine;
    [SerializeField] private Image[] snakes;

    private Image[] foldTriangles;
    private List<Image> textImages;
    private List<Image> textCircleImages;
    
    private List<int> iIndexes = new List<int>(){ 3, 9, 16 };
    private const float SNAKE_MOVE_DIST = 30f;
    
    private void Start()
    {
        PlayAnimation();
    }

    private void Initialize()
    {
        borderCircle.SetAlpha(0);
        borderCircle.rectTransform.localScale = Vector3.one * 0.7f;
        borderCircle.rectTransform.localRotation = Quaternion.Euler(0, 0, -10f);

        foldTriangles = foldTrianglesParent.GetComponentsInChildren<Image>();
        for (var i = 0; i < foldTriangles.Length; i++)
        {
            var foldTriangle = foldTriangles[i];
            foldTriangle.SetAlpha(0);
            foldTriangle.rectTransform.localScale = new Vector3((i + 1) % 2, i % 2, 1);
        }
        
        perticles.rectTransform.localScale = Vector3.zero;
        centerCircle.SetAlpha(0);
        centerCircle.rectTransform.localScale = Vector3.one * 0.7f;

        var textImageAll = textParent.GetComponentsInChildren<Image>().ToList();
        textImages = textImageAll.GetRange(0, textImageAll.Count - 3);
        textCircleImages = textImageAll.GetRange(textImageAll.Count - 3, 3);
        for (var i = 0; i < textImages.Count; i++)
        {
            var textImage = textImages[i];
            var parentName = textImage.transform.parent.name;
            var color = parentName == "Motion" ? textCircleImages[0].color
                : parentName == "Design" ? textCircleImages[1].color
                : parentName == "Festival" ? textCircleImages[2].color
                : Color.white;
            textImage.color = color;
            textImage.SetAlpha(0);
            textImage.rectTransform.localScale = new Vector3(1, 0, 1);
        }
        for (var i = 0; i < textCircleImages.Count; i++)
        {
            var textCircleImage = textCircleImages[i];
            textCircleImage.rectTransform.localScale = new Vector3(1, 0, 1);
        }
        
        aroundLine.SetAlpha(0);
        aroundLine.rectTransform.localRotation = Quaternion.Euler(0, 0, -30f);

        foreach (var snake in snakes) snake.SetAlpha(0);
        snakes[0].rectTransform.anchoredPosition += Vector2.one * SNAKE_MOVE_DIST;
        snakes[1].rectTransform.anchoredPosition -= Vector2.one * SNAKE_MOVE_DIST;
    }

    private void PlayAnimation()
    {
        Initialize();
        DOTween.Sequence()
            .AppendInterval(0.4f)
            
            // しましまの円
            .Append(borderCircle.DOFade(1, 0.4f).SetEase(Ease.InQuad))
            .Join(borderCircle.rectTransform.DOScale(1, 0.4f))
            .Join(borderCircle.rectTransform.DOLocalRotate(Vector3.zero, 0.4f))

            // 折り畳み三角形
            .AppendCallback(() =>
            {
                var groupNum = foldTrianglesParent.childCount;
                var triangleNumInGroup = foldTrianglesParent.GetChild(0).childCount;
                for (var i = 0; i < groupNum; i++)
                {
                    for (var j = 0; j < triangleNumInGroup; j++)
                    {
                        var triangleIdx = i % 2 == 0 ? j : triangleNumInGroup - 1 - j;
                        var target = foldTriangles[i * triangleNumInGroup + triangleIdx];
                        DOTween.Sequence()
                            .Append(target.DOFade(1, 0.08f))
                            .Join(target.rectTransform.DOScale(Vector3.one, 0.08f).SetEase(Ease.Linear))
                            .SetDelay(j * 0.08f);
                    }
                }
            })
            .AppendInterval(0.08f * (foldTrianglesParent.GetChild(0).childCount + 1) + 0.1f)
            
            // パーティクル & 中心円
            .Append(perticles.rectTransform.DOScale(1, 0.2f))
            .Join(centerCircle.DOFade(1, 0.4f))
            .Join(centerCircle.rectTransform.DOScale(1, 0.6f).SetEase(Ease.OutBack, 12))
            
            // 文字
            .AppendCallback(() =>
            {
                for (var i = 0; i < textImages.Count; i++)
                {
                    var target = textImages[i];
                    DOTween.Sequence()
                        .Append(target.DOFade(1, 0.1f))
                        .Join(target.rectTransform.DOScaleY(1, 0.3f).SetEase(Ease.OutBack))
                        .Join(target.rectTransform.DOAnchorPosY(40f, 0.3f).SetLoops(2, LoopType.Yoyo).SetRelative(true))
                        .Append(target.DOColor(Color.white, 0.2f))
                        .SetDelay(i * 0.03f);

                    var iIndex = iIndexes.IndexOf(i);
                    if (iIndex != -1)
                    {
                        var targetCircle = textCircleImages[iIndex];
                        DOTween.Sequence()
                            .Append(targetCircle.rectTransform.DOScaleY(1, 0.1f))
                            .Append(targetCircle.rectTransform.DOScale(new Vector3(0.6f, 1.2f, 1), 0.3f)
                                .SetLoops(2, LoopType.Yoyo))
                            .Join(targetCircle.rectTransform.DOAnchorPosY(60f, 0.3f).SetRelative(true)
                                .SetLoops(2, LoopType.Yoyo))
                            .SetDelay(i * 0.03f + 0.2f);
                    }
                }
            })
            .AppendInterval(textImages.Count * 0.03f + 0.8f)
            
            // 円周の線 & ジグザグ線
            .Append(aroundLine.DOFade(1, 0.4f))
            .Join(aroundLine.rectTransform.DOLocalRotate(Vector3.zero, 0.4f))
            .Join(snakes[0].DOFade(1, 0.4f))
            .Join(snakes[1].DOFade(1, 0.4f))
            .Join(snakes[0].rectTransform.DOAnchorPos(Vector2.one * -SNAKE_MOVE_DIST, 0.4f).SetRelative(true))
            .Join(snakes[1].rectTransform.DOAnchorPos(Vector2.one * SNAKE_MOVE_DIST, 0.4f).SetRelative(true))
           
            // .AppendInterval(1f)
            // .AppendCallback(Initialize)
            // .SetLoops(-1)
            ;
    }
}
