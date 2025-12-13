using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PanelAnimation : MonoBehaviour
{

    public RectTransform panel;
    public Outline outline;

    [Header("Punch")]
    public float punchScale = 0.05f;
    public float punchDuration = 0.3f;

    [Header("Glow")]
    public Color glowColor = new Color(0, 1f, 1f, 1f);
    public float glowSize = 10f;
    public float glowDuration = 0.4f;

    Vector3 originalScale;
    Color originalOutlineColor;
    Vector2 originalOutlineDistance;

    void Awake()
    {
        originalScale = panel.localScale;

        originalOutlineColor = outline.effectColor;
        originalOutlineDistance = outline.effectDistance;

        // Start with no glow
        outline.effectColor = new Color(glowColor.r, glowColor.g, glowColor.b, 0);
        outline.effectDistance = Vector2.zero;
    }

    public void PlaySubmitFeedback()
    {
        panel.DOKill();
        outline.DOKill();

        // ---- Scale Punch ----
        panel
            .DOPunchScale(Vector3.one * punchScale, punchDuration, 8, 0.8f)
            .SetEase(Ease.OutQuad);

        // ---- Glow In ----
        outline.effectDistance = new Vector2(glowSize, glowSize);

        outline
            .DOFade(1f, glowDuration * 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                // ---- Glow Out ----
                outline
                    .DOFade(0f, glowDuration)
                    .SetEase(Ease.InQuad);
            });
    }
}

