using UnityEngine;
using UnityEngine.UI;
using TMPro; // Falls du TextMeshPro verwendest

public class PreviewManager : MonoBehaviour
{
    [Header("UI Referenzen")]
    public GameObject previewPanel;
    public Image previewImage;      // Das UI-Image-Element für das Vorschau-Bild
    public TextMeshProUGUI previewText; // Das UI-Text-Element für den Vorschau-Text

    [Header("Standard-Vorschau")]
    public Sprite defaultSprite;
    public string defaultText;

    public void UpdatePreview(Sprite newSprite, string newText)
    {
        if (previewImage != null)
            previewImage.sprite = newSprite;

        if (previewText != null)
            previewText.text = newText;
    }

    public void ResetPreview()
    {
        if (previewImage != null)
            previewImage.sprite = defaultSprite;

        if (previewText != null)
            previewText.text = defaultText;
    }

    // Optional: Methoden zum Ein- und Ausblenden des Panels
    public void ShowPreviewPanel()
    {
        if (previewPanel != null)
            previewPanel.SetActive(true);
    }

    public void HidePreviewPanel()
    {
        if (previewPanel != null)
            previewPanel.SetActive(false);
    }
}
