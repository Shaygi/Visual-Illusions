using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Vorschau-Daten f�r diesen Button")]
    public Sprite previewSprite; // Das Bild, das angezeigt werden soll
    [TextArea]
    public string previewText;   // Der Text, der angezeigt werden soll

    [Header("Referenz zum Preview Manager")]
    public PreviewManager previewManager; // Das Skript, das die Vorschau steuert

    // Wird aufgerufen, wenn der Mauszeiger in das Button-Feld einf�hrt
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (previewManager != null)
        {
            previewManager.UpdatePreview(previewSprite, previewText);
        }
    }

    // Wird aufgerufen, wenn der Mauszeiger das Button-Feld verl�sst
    public void OnPointerExit(PointerEventData eventData)
    {
        if (previewManager != null)
        {
            previewManager.ResetPreview();
        }
    }
}
