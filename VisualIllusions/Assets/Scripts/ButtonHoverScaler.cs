using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Skalierungseinstellungen")]
    [Tooltip("Faktor, um den der Button beim Hovern vergrößert wird (z.B. 1.1 = 10% größer).")]
    public float hoverScaleFactor = 1.1f;

    [Tooltip("Dauer der Skalierung (in Sekunden).")]
    public float scaleTransitionDuration = 0.2f;

    private Vector3 originalScale;

    private void Start()
    {
        // Ursprüngliche Skalierung speichern
        originalScale = transform.localScale;
    }

    // Wird aufgerufen, wenn der Mauszeiger in das UI-Element einfährt
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Falls bereits eine Skalierungs-Coroutine läuft, stoppen
        StopAllCoroutines();
        // Starte die Coroutine zum sanften Skalieren auf den vergrößerten Wert
        StartCoroutine(ScaleTo(originalScale * hoverScaleFactor));
    }

    // Wird aufgerufen, wenn der Mauszeiger das UI-Element verlässt
    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        // Starte die Coroutine, um wieder auf die ursprüngliche Größe zu skalieren
        StartCoroutine(ScaleTo(originalScale));
    }

    // Coroutine für den sanften Skalierungseffekt
    private IEnumerator ScaleTo(Vector3 targetScale)
    {
        Vector3 startScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < scaleTransitionDuration)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / scaleTransitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
