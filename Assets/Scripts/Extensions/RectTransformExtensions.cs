// https://gist.github.com/nkjzm/1b31512c00aee93403427f14ebfb4db8 (2020-7-6 参照)
// Copyright (c) 2019 Nakaji Kohki nkjzm
// Released under the MIT license
// https: //opensource.org/licenses/mit-license.php

using UnityEngine;

namespace Extensions
{
    public static class RectTransformExtensions
    {
        /// <summary>
        /// 座標を保ったままPivotを変更する
        /// </summary>
        /// <param name="rectTransform">自身の参照</param>
        /// <param name="targetPivot">変更先のPivot座標</param>
        public static void SetPivotWithKeepingPosition(this RectTransform rectTransform, Vector2 targetPivot)
        {
            var diffPivot = targetPivot - rectTransform.pivot;
            rectTransform.pivot = targetPivot;
            var diffPos = new Vector2(rectTransform.sizeDelta.x * diffPivot.x, rectTransform.sizeDelta.y * diffPivot.y);
            diffPos = rectTransform.rotation * diffPos;
            rectTransform.anchoredPosition += diffPos;
        }
        /// <summary>
        /// 座標を保ったままPivotを変更する
        /// </summary>
        /// <param name="rectTransform">自身の参照</param>
        /// <param name="x">変更先のPivotのx座標</param>
        /// <param name="y">変更先のPivotのy座標</param>
        public static void SetPivotWithKeepingPosition(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.SetPivotWithKeepingPosition(new Vector2(x, y));
        }
        /// <summary>
        /// 座標を保ったままAnchorを変更する
        /// </summary>
        /// <param name="rectTransform">自身の参照</param>
        /// <param name="targetAnchor">変更先のAnchor座標 (min,maxが共通の場合)</param>
        public static void SetAnchorWithKeepingPosition(this RectTransform rectTransform, Vector2 targetAnchor)
        {
            rectTransform.SetAnchorWithKeepingPosition(targetAnchor, targetAnchor);
        }
        /// <summary>
        /// 座標を保ったままAnchorを変更する
        /// </summary>
        /// <param name="rectTransform">自身の参照</param>
        /// <param name="x">変更先のAnchorのx座標 (min,maxが共通の場合)</param>
        /// <param name="y">変更先のAnchorのy座標 (min,maxが共通の場合)</param>
        public static void SetAnchorWithKeepingPosition(this RectTransform rectTransform, float x, float y)
        {
            rectTransform.SetAnchorWithKeepingPosition(new Vector2(x, y));
        }
        /// <summary>
        /// 座標を保ったままAnchorを変更する
        /// </summary>
        /// <param name="rectTransform">自身の参照</param>
        /// <param name="targetMinAnchor">変更先のAnchorMin座標</param>
        /// <param name="targetMaxAnchor">変更先のAnchorMax座標</param>
        public static void SetAnchorWithKeepingPosition(this RectTransform rectTransform, Vector2 targetMinAnchor, Vector2 targetMaxAnchor)
        {
            var parent = rectTransform.parent as RectTransform;
            if (parent == null) { Debug.LogError("Parent cannot find."); }

            var diffMin = targetMinAnchor - rectTransform.anchorMin;
            var diffMax = targetMaxAnchor - rectTransform.anchorMax;
            // anchorの更新
            rectTransform.anchorMin = targetMinAnchor;
            rectTransform.anchorMax = targetMaxAnchor;
            // 上下左右の距離の差分を計算
            var diffLeft = parent.rect.width * diffMin.x;
            var diffRight = parent.rect.width * diffMax.x;
            var diffBottom = parent.rect.height * diffMin.y;
            var diffTop = parent.rect.height * diffMax.y;
            // サイズと座標の修正
            rectTransform.sizeDelta += new Vector2(diffLeft - diffRight, diffBottom - diffTop);
            var pivot = rectTransform.pivot;
            rectTransform.anchoredPosition -= new Vector2(
                (diffLeft * (1 - pivot.x)) + (diffRight * pivot.x),
                (diffBottom * (1 - pivot.y)) + (diffTop * pivot.y)
            );
        }
        /// <summary>
        /// 座標を保ったままAnchorを変更する
        /// </summary>
        /// <param name="rectTransform">自身の参照</param>
        /// <param name="minX">変更先のAnchorMinのx座標</param>
        /// <param name="minY">変更先のAnchorMinのy座標</param>
        /// <param name="maxX">変更先のAnchorMaxのx座標</param>
        /// <param name="maxY">変更先のAnchorMaxのy座標</param>
        public static void SetAnchorWithKeepingPosition(this RectTransform rectTransform, float minX, float minY, float maxX, float maxY)
        {
            rectTransform.SetAnchorWithKeepingPosition(new Vector2(minX, minY), new Vector2(maxX, maxY));
        }
    }
}