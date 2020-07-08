using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnOffToggle : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private RectTransform rail;
    [SerializeField] private RectTransform handle;
    [SerializeField] private Image handleFront;
    [SerializeField] private TextMeshProUGUI onText;
    [SerializeField] private TextMeshProUGUI offText;
    [SerializeField] private Image circle;
    

    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeOnColor;
    [SerializeField] private Color activeOffColor;

    private bool isOn;
    private Sequence sequence;
    

    /// <summary>
    /// ハンドルのY座標（中央配置を前提）
    /// </summary>
    private float handlePosY;

    private void Start()
    {
        handlePosY = rail.rect.yMax;

        isOn = true;
        UpdateState();
        sequence?.Complete(true);
    }

    /// <summary>
    /// タップした時に呼び出される
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        isOn = !isOn;
        UpdateState();
    }

    /// <summary>
    /// 状態を更新する
    /// </summary>
    private void UpdateState()
    {
        sequence?.Kill();

        var targetPosY = isOn ? handlePosY : -handlePosY;
        var activeText = isOn ? onText : offText;
        var inactiveText = isOn ? offText : onText;
        var activeColor = isOn ? activeOnColor : activeOffColor;
        var circleSize = isOn ? 800f : 0;

        sequence = DOTween.Sequence()
            .OnStart(() =>
            {
                inactiveText.color = inactiveColor;
                handleFront.color = inactiveColor;
            })
            .Append(handle.DOAnchorPosY(targetPosY, 0.32f).SetEase(Ease.OutQuart))
            .Join(circle.rectTransform.DOSizeDelta(Vector2.one * circleSize, 0.32f))
            .AppendCallback(() =>
            {
                activeText.color = activeColor;
                handleFront.color = activeColor;
            })
            .Append(handle.DOScale(1.2f, 0.04f))
            .Join(activeText.rectTransform.DOScale(1.2f, 0.04f))
            .Append(handle.DOScale(1, 0.1f))
            .Join(activeText.rectTransform.DOScale(1, 0.1f));
    }
}