using UnityEngine;
using System.Collections;

public class TunnelTransition : MonoBehaviour
{
    [Header("Bereiche")]
    // Bereich, der zunächst sichtbar ist (Außenbereich)
    public GameObject outsideTunnel;
    // Längerer Innenbereich, der aktiviert wird
    public GameObject insideTunnel;

    [Header("Trigger")]
    // Starttrigger, der initial aktiv ist
    public GameObject startTrigger;
    // Endtrigger, der später aktiviert wird
    public GameObject endTrigger;

    [Header("Trigger-Verzögerung")]
    // Verzögerung (in Sekunden), bis die Trigger gewechselt werden
    public float triggerDelay = 1f;

    // Verhindert mehrfaches Auslösen
    private bool triggered = false;

    // Wird aufgerufen, wenn ein Collider den Trigger betritt
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;

            // Sofort den Tunnelbereich wechseln
            outsideTunnel.SetActive(false);
            insideTunnel.SetActive(true);

            // Starte Coroutine, um den Triggerwechsel nach der Verzögerung durchzuführen
            StartCoroutine(SwitchTriggersAfterDelay());
        }
    }

    private IEnumerator SwitchTriggersAfterDelay()
    {
        // Warte die festgelegte Zeit ab
        yield return new WaitForSeconds(triggerDelay);

        // Nach der Verzögerung: Starte-Trigger deaktivieren und End-Trigger aktivieren
        startTrigger.SetActive(false);
        endTrigger.SetActive(true);
    }

    // Wenn der Spieler den Trigger verlässt, setzen wir den Trigger zurück
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
