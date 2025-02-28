using UnityEngine;
using System.Collections;

public class TunnelTransition : MonoBehaviour
{
    [Header("Bereiche")]
    // Bereich, der zun�chst sichtbar ist (Au�enbereich)
    public GameObject outsideTunnel;
    // L�ngerer Innenbereich, der aktiviert wird
    public GameObject insideTunnel;

    [Header("Trigger")]
    // Starttrigger, der initial aktiv ist
    public GameObject startTrigger;
    // Endtrigger, der sp�ter aktiviert wird
    public GameObject endTrigger;

    [Header("Trigger-Verz�gerung")]
    // Verz�gerung (in Sekunden), bis die Trigger gewechselt werden
    public float triggerDelay = 1f;

    // Verhindert mehrfaches Ausl�sen
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

            // Starte Coroutine, um den Triggerwechsel nach der Verz�gerung durchzuf�hren
            StartCoroutine(SwitchTriggersAfterDelay());
        }
    }

    private IEnumerator SwitchTriggersAfterDelay()
    {
        // Warte die festgelegte Zeit ab
        yield return new WaitForSeconds(triggerDelay);

        // Nach der Verz�gerung: Starte-Trigger deaktivieren und End-Trigger aktivieren
        startTrigger.SetActive(false);
        endTrigger.SetActive(true);
    }

    // Wenn der Spieler den Trigger verl�sst, setzen wir den Trigger zur�ck
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = false;
        }
    }
}
