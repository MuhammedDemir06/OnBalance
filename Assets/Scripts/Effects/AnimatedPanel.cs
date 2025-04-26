using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimatedPanel : MonoBehaviour
{
    [Header("Target Panel")]
    public RectTransform targetPanel;

    [Header("Animations Settings")]
    public float animationDuration = 0.4f;
    public Ease openEase = Ease.OutBack;
    public Ease closeEase = Ease.InBack;
    public bool IsText = false;
    [SerializeField] private TextMeshProUGUI warningText;

    [Header("Hide On Start")]
    public bool hideOnStart = true;

    private Vector3 originalScale;

    [Header("Sound")]
    [SerializeField] private AudioSource openingSound;

    private void Start()
    {
        if (targetPanel == null) return;

        originalScale = targetPanel.localScale;

        if (hideOnStart)
        {
            targetPanel.localScale = Vector3.zero;
            targetPanel.gameObject.SetActive(false);
        }
    }
    public void TextSetting(string text)
    {
        if (IsText)
            warningText.text = text;

        Show();
        Invoke(nameof(Hide), 2f);
    }
    public void Show()
    {
        if (targetPanel == null) return;

        if (openingSound.enabled)
            openingSound.Play();
        targetPanel.gameObject.SetActive(true);
        targetPanel.localScale = Vector3.zero;
        targetPanel.DOScale(originalScale, animationDuration).SetEase(openEase);
    }
    public void Hide()
    {
        if (targetPanel == null) return;

        targetPanel.DOScale(Vector3.zero, animationDuration).SetEase(closeEase)
            .OnComplete(() => targetPanel.gameObject.SetActive(false));
    }
}
