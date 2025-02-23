using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PainEffect : MonoBehaviour
{
    // Referenz auf das rote Overlay-Image
    public Image redOverlay;
    // Dauer, wie lange das Overlay sichtbar sein soll
    public float effectDuration = 0.5f;
    // Intensität des Kamera-Shakes
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.2f;

    private bool isShaking = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Starte den Schmerz-Effekt UI Overlay  optional Kamera-Shake
            StartCoroutine(ActivatePainEffect());
            StartCoroutine(ShakeCamera());
        }
    }

    private IEnumerator ActivatePainEffect()
    {
        redOverlay.gameObject.SetActive(true);
        // Setze den Alpha des Overlays sofort auf voll sichtbar (1)
        Color color = redOverlay.color;
        color.a = 0.5f;
        redOverlay.color = color;

        yield return new WaitForSeconds(effectDuration);
        // Die Fade-Out Coroutine starten, z.B. mit 0.5 Sekunden Übergang
        StartCoroutine(FadeOutOverlay(0.5f));
    }


    private IEnumerator FadeOutOverlay(float fadeDuration)
    {
        // Sicherstellen, dass das Overlay aktiv ist und die Farbe einen Alpha von 1 hat.
        redOverlay.gameObject.SetActive(true);
        Color color = redOverlay.color;
        color.a = 1f;
        redOverlay.color = color;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            // Berechnung des neuen Alpha-Werts, interpoliert von 1 zu 0
            float newAlpha = Mathf.Lerp(0.5f, 0f, elapsed / fadeDuration);
            color.a = newAlpha;
            redOverlay.color = color;
            yield return null;
        }
        //Den Alpha abschließend auf 0 setzen und das Overlay deaktiviere
        color.a = 0f;
        redOverlay.color = color;
        redOverlay.gameObject.SetActive(false);
    }

    private IEnumerator ShakeCamera()
    {
        if (isShaking) yield break;
        isShaking = true;
        Vector3 originalPos = Camera.main.transform.localPosition;
        float elapsed = 0f;
        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeIntensity, shakeIntensity);
            float offsetY = Random.Range(-shakeIntensity, shakeIntensity);
            Camera.main.transform.localPosition = originalPos + new Vector3(offsetX, offsetY, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.transform.localPosition = originalPos;
        isShaking = false;
    }
}
