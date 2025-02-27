using UnityEngine;
using System.Collections;

public class FreezeShake : MonoBehaviour
{
    [Header("Einstellungen für das Zittern")]
    [Tooltip("Maximale Auslenkung der Kamera")]
    public float shakeAmplitude = 0.02f;

    [Tooltip("Geschwindigkeit des Zitterns, anpassbar an die Atemfrequenz")]
    public float shakeFrequency = 1f;

    [Tooltip("Dauer des Zitterns in Sekunden")]
    public float shakeDuration = 2f;

    [Tooltip("Dauer der Pause in Sekunden, in der kein Zittern stattfindet")]
    public float pauseDuration = 1f;

    private Vector3 originalPos;
    private bool shakeActive = false;
    private Coroutine shakeCoroutine;

    private void Start()
    {
        originalPos = transform.localPosition;
    }

    /// <summary>
    /// Aktiviert oder deaktiviert den Shake-Zyklus.
    /// </summary>
    public void SetShakeActive(bool active)
    {
        if (active && !shakeActive)
        {
            shakeActive = true;
            shakeCoroutine = StartCoroutine(ShakeCycle());
        }
        else if (!active && shakeActive)
        {
            shakeActive = false;
            if (shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
                shakeCoroutine = null;
            }
            // Die Kamera in die Originalposition zurücksetzen
            transform.localPosition = originalPos;
        }
    }

    /// <summary>
    /// Führt einen Zyklus aus Zittern und Pause aus.
    /// </summary>
    private IEnumerator ShakeCycle()
    {
        while (shakeActive)
        {
            // Zittern-Phase:
            float elapsed = 0f;
            while (elapsed < shakeDuration && shakeActive)
            {
                float offsetX = (Mathf.PerlinNoise(Time.time * shakeFrequency, 0f) - 0.5f) * 2f;
                float offsetY = (Mathf.PerlinNoise(0f, Time.time * shakeFrequency) - 0.5f) * 2f;
                Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0f) * shakeAmplitude;
                transform.localPosition = originalPos + shakeOffset;

                elapsed += Time.deltaTime;
                yield return null;
            }

            // Pause-Phase:
            transform.localPosition = originalPos;
            float pauseElapsed = 0f;
            while (pauseElapsed < pauseDuration && shakeActive)
            {
                pauseElapsed += Time.deltaTime;
                yield return null;
            }
        }
        // Sicherheitshalber die Kamera wieder in die Originalposition setzen
        transform.localPosition = originalPos;
    }
}
