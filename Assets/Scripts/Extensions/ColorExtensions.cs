using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    public static class ColorExtensions
    {
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha)
        {
            var color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        public static void SetAlpha(this Graphic graphic, float alpha)
        {
            var color = graphic.color;
            color.a = alpha;
            graphic.color = color;
        }

        public static void SetAlphasInChildren(this GameObject obj, float alpha)
        {
            var spriteRenderers = obj.GetComponentsInChildren<SpriteRenderer>();
            var graphics = obj.GetComponentsInChildren<Graphic>();

            if (spriteRenderers != null)
            {
                foreach (var spriteRenderer in spriteRenderers)
                {
                    spriteRenderer.SetAlpha(alpha);
                }
            }

            if (graphics != null)
            {
                foreach (var graphic in graphics)
                {
                    graphic.SetAlpha(alpha);
                }
            }
        }
    }
}