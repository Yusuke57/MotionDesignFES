using DG.Tweening;
using DG.Tweening.Plugins.Options;
using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LikeButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image background;
    [SerializeField] private Image inactiveIcon;
    [SerializeField] private Image activeIcon;
    [SerializeField] private TextMeshProUGUI likeText;
    [SerializeField] private Image[] lines;

    private bool isLike;
    private Sequence sequence;
    
    
    private Vector2[] defaultLinePositions;

    private void Start()
    {
        defaultLinePositions = new Vector2[lines.Length];
        for (var i = 0; i < defaultLinePositions.Length; i++)
        {
            defaultLinePositions[i] = lines[i].rectTransform.anchoredPosition;
        }

        isLike = false;
        Inactivate();
        sequence?.Complete(true);
    }

    /// <summary>
    /// タップした時に呼び出される
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        isLike = !isLike;
        if (isLike) Activate();
        else Inactivate();
    }

    /// <summary>
    /// いいねした状態にする
    /// </summary>
    private void Activate()
    {
        sequence?.Complete();

        sequence = DOTween.Sequence()
            .OnStart(() =>
            {
                inactiveIcon.SetAlpha(0);
                activeIcon.SetAlpha(1);
            })
            .Append(background.DOFade(1, 0.1f))
            .Join(transform.DOScale(1.1f, 0.04f))
            .Join(transform.DOScale(1f, 0.1f).SetDelay(0.04f))
            .Join(likeText.DOColor(Color.white, 0.1f))
            .Join(activeIcon.rectTransform.DOLocalRotate(Vector3.forward * 16f, 0.04f))
            .Append(lines[0].rectTransform.DOScaleX(1, 0.04f))
            .Join(lines[1].rectTransform.DOScaleX(1, 0.04f))
            .AppendCallback(() =>
            {
                foreach (var line in lines)
                    line.rectTransform.SetPivotWithKeepingPosition(new Vector2(1, 0.5f));
            })
            .Append(lines[0].rectTransform.DOScaleX(0, 0.2f))
            .Join(lines[1].rectTransform.DOScaleX(0, 0.2f))
            .Join(activeIcon.rectTransform.DOLocalRotate(Vector3.zero, 0.06f));
    }

    /// <summary>
    /// いいねしてない状態にする
    /// </summary>
    private void Inactivate()
    {
        sequence?.Complete();

        var inactiveColor = inactiveIcon.color;
        inactiveColor.a = 1;
        
        sequence = DOTween.Sequence()
            .OnStart(() =>
            {
                inactiveIcon.SetAlpha(1);
                for (var i = 0; i < lines.Length; i++)
                {
                    var lineRectTrans = lines[i].rectTransform;
                    lineRectTrans.pivot = new Vector2(0, 0.5f);
                    lineRectTrans.anchoredPosition = defaultLinePositions[i];
                    lineRectTrans.localScale = new Vector3(0, 1, 1);
                }
            })
            .Append(background.DOFade(0, 0.28f))
            .Join(background.rectTransform.DOScale(1f, 0.1f))
            .Join(likeText.DOColor(inactiveColor, 0.2f))
            .Join(activeIcon.DOFade(0, 0.2f));
    }
}