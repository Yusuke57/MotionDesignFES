using DG.Tweening;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

public class Hello : MonoBehaviour
{
    [SerializeField] private Transform charParent;
    
    private Image[][] squares;

    private const float CHAR_INTERVAL = 0.16f;
    private const float SQUARE_INTERVAL = 0.005f;
    private const float SQUARE_DURATION = 0.2f;

    private void Start()
    {
        squares = new Image[charParent.childCount][];
        for (var i = 0; i < squares.Length; i++)
        {
            squares[i] = charParent.GetChild(i).GetComponentsInChildren<Image>(true);
        }
        
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        for (var i = 0; i < squares.Length; i++)
        {
            var charSquares = squares[i];
            for (var j = 0; j < charSquares.Length; j++)
            {
                var square = charSquares[charSquares.Length - j - 1];
                var rectTransform = square.rectTransform;
                var pos = rectTransform.anchoredPosition;
                var alpha = square.color.a;
                DOTween.Sequence()
                    .AppendCallback(() =>
                    {
                        square.color = new Color(Random.Range(0.6f, 0.7f), 1, Random.Range(0.8f, 1f), alpha);
                        square = charSquares[charSquares.Length - j - 1];
                        square.rectTransform.SetPivotWithKeepingPosition(Vector2.right);
                    })
                    .Append(square.rectTransform.DOAnchorPosX(1600f, 0))
                    .Join(square.rectTransform.DOScaleX(12f, 0))
                    .Join(square.DOFade(1, 0))
                    .AppendInterval(0.2f)
                    .AppendInterval(CHAR_INTERVAL * i + SQUARE_INTERVAL * j)
                    .Append(square.rectTransform.DOAnchorPosX(pos.x, SQUARE_DURATION).SetEase(Ease.OutQuart))
                    .Join(square.rectTransform.DOScaleX(1, SQUARE_DURATION).SetEase(Ease.OutQuart))
                    .Join(square.DOFade(alpha, SQUARE_DURATION).SetEase(Ease.OutQuart))

                    .AppendInterval(0.4f)
                    .Append(square.DOColor(new Color(1, 1, 1, alpha + 0.2f), 0.05f).SetLoops(2, LoopType.Yoyo))
                    .AppendInterval(0.4f)
                    .AppendInterval(SQUARE_INTERVAL * (charSquares.Length - j))

                    .AppendInterval(Random.Range(0f, 0.2f))
                    .Append(square.rectTransform.DOAnchorPosX(-100f, SQUARE_DURATION / 2))
                    .Join(square.DOFade(0, SQUARE_DURATION / 2));
            }
        }
    }
}
