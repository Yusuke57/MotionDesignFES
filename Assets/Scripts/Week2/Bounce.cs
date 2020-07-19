using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Bounce : MonoBehaviour
{
    [SerializeField] private Image ball;
    [SerializeField] private Image shadow;
    [SerializeField] private Image background;
    
    [SerializeField] private Color[] ballColors;
    [SerializeField] private Color[] bgColors;
    
    private int currentIdx;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        currentIdx = 0;
        
        DOTween.Sequence()
            // 初期化
            .Append(ball.rectTransform.DOScale(new Vector3(1.8f, 0.6f, 1), 0))
            .Join(ball.rectTransform.DOAnchorPosY(-300f, 0))
            .Join(shadow.rectTransform.DOScale(1.4f, 0))
            .Join(shadow.DOFade(0.32f, 0))
            .AppendCallback(() =>
            {
                if (++currentIdx % 2 == 1) return;
                var idx = (currentIdx / 2) % bgColors.Length;
                ball.color = ballColors[idx];
                background.color = bgColors[idx];
            })
            
            .AppendInterval(0.02f)

            // アニメーション開始
            .Append(ball.rectTransform.DOScale(1, 0.1f))
            .Join(shadow.DOFade(0.4f, 0.1f))
            .Join(shadow.rectTransform.DOScale(1f, 0.1f))
            .Append(ball.rectTransform.DOAnchorPosY(200f, 0.4f))
            .Join(shadow.rectTransform.DOScale(1.4f, 0.4f))
            .Join(shadow.DOFade(0.1f, 0.4f))
            .Join(ball.rectTransform.DOScaleX(0.8f, 0.2f))
            .Join(ball.rectTransform.DOScale(new Vector3(1, 0.8f, 1), 0.2f).SetDelay(0.2f))
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Yoyo);
    }
    
}
