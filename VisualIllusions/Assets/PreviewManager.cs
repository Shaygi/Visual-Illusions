using UnityEngine;
using UnityEngine.UI;

public class PreviewManager : MonoBehaviour
{
    [Header("Panel und UI-Referenzen")]
    [Tooltip("Das Panel, das die Vorschau enthält.")]
    public GameObject previewPanel;

    [Tooltip("Das Image-Element innerhalb des Panels.")]
    public Image previewImage;

    [Tooltip("Das Text-Element innerhalb des Panels.")]
    public Text previewText;

    [Header("Standard-Vorschau")]
    [Tooltip("Standard-Sprite, das angezeigt wird, wenn keine Vorschau aktiv ist.")]
    public Sprite defaultSprite;

    [Tooltip("Standard-Text, der angezeigt wird, wenn keine Vorschau aktiv ist.")]
    public string defaultText;

    /// <summary>
    /// Aktualisiert die Vorschau mit dem angegebenen Sprite und Text.
    /// </summary>
    /// <param name="newSprite">Das neue Sprite, das angezeigt werden soll.</param>
    /// <param name="newText">Der neue Text, der angezeigt werden soll.</param>
    public void UpdatePreview(Sprite newSprite, string newText)
    {
        if (previewImage != null)
        {
            previewImage.sprite = newSprite;
        }
        if (previewText != null)
        {
            previewText.text = newText;
        }
    }

    /// <summary>
    /// Setzt die Vorschau auf die Standardwerte zurück.
    /// </summary>
    public void ResetPreview()
    {
        if (previewImage != null)
        {
            previewImage.sprite = defaultSprite;
        }
        if (previewText != null)
        {
            previewText.text = defaultText;
        }
    }

    /// <summary>
    /// Optional: Blendet das Vorschau-Panel ein.
    /// </summary>
    public void ShowPreviewPanel()
    {
        if (previewPanel != null)
        {
            previewPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Optional: Blendet das Vorschau-Panel aus.
    /// </summary>
    public void HidePreviewPanel()
    {
        if (previewPanel != null)
        {
            previewPanel.SetActive(false);
        }
    }
}
