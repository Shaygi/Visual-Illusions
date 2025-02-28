using System.Collections;
using UnityEngine;

public class KnobAnimation : MonoBehaviour
{
    [Header("Animationseinstellungen")]
    [Tooltip("Wie weit der Knopf nach unten fährt (lokaler Abstand)")]
    public float pressDistance = 0.1f;

    [Tooltip("Dauer des Herunterfahrens (in Sekunden)")]
    public float pressDuration = 0.1f;

    private Vector3 initialLocalPosition;

    private void Start()
    {
        // Ausgangsposition im lokalen Raum speichern
        initialLocalPosition = transform.localPosition;
    }

    /// <summary>
    /// Startet die Drück-Animation: Knopf fährt runter und dann wieder hoch.
    /// </summary>
    public void AnimatePress()
    {
        StartCoroutine(PressCoroutine());
    }

    private IEnumerator PressCoroutine()
    {
        Vector3 pressedPosition = initialLocalPosition - new Vector3(0, pressDistance, 0);

        // Herunterfahren
        float elapsed = 0f;
        while (elapsed < pressDuration)
        {
            transform.localPosition = Vector3.Lerp(initialLocalPosition, pressedPosition, elapsed / pressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = pressedPosition;

        // Hochfahren
        elapsed = 0f;
        while (elapsed < pressDuration)
        {
            transform.localPosition = Vector3.Lerp(pressedPosition, initialLocalPosition, elapsed / pressDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = initialLocalPosition;
    }
}
