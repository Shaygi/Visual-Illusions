using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    [Header("Tür Objekte")]
    public GameObject leftDoor;   // Linke Tür
    public GameObject rightDoor;  // Rechte Tür

    [Header("Bewegungseinstellungen")]
    public float doorMoveDistance = 2.0f;  // Distanz, um die die Türen bewegt werden
    public float moveDuration = 1.0f;      // Dauer der Bewegung in Sekunden

    [Header("Optionale Knopf Einstellungen")]
    public GameObject knob;  // Knopf-Objekt für Farbwechsel und Animation

    [Header("Öffnungsrichtungen (lokal)")]
    // Diese Richtungen wirken relativ zum Elternobjekt (und sollten nicht von der Türrotation beeinflusst werden)
    public Vector3 leftDoorOpenDirection = Vector3.left;
    public Vector3 rightDoorOpenDirection = Vector3.right;

    private bool doorsOpen = false;
    private Vector3 leftDoorClosedPos;
    private Vector3 rightDoorClosedPos;

    void Start()
    {
        // Ausgangspositionen der Türen als lokale Positionen speichern
        if (leftDoor != null)
            leftDoorClosedPos = leftDoor.transform.localPosition;
        if (rightDoor != null)
            rightDoorClosedPos = rightDoor.transform.localPosition;
    }

    public void Interact()
    {
        if (!doorsOpen)
        {
            // Türen öffnen: 
            // Für die linke Tür: Wir subtrahieren den Verschiebungsvektor, sodass sie in die entgegengesetzte Richtung von leftDoorOpenDirection geht.
            StartCoroutine(MoveDoor(leftDoor, leftDoor.transform.localPosition, leftDoorClosedPos - leftDoorOpenDirection * doorMoveDistance, moveDuration));
            // Rechte Tür bleibt unverändert: Sie bewegt sich in Richtung rightDoorOpenDirection.
            StartCoroutine(MoveDoor(rightDoor, rightDoor.transform.localPosition, rightDoorClosedPos + rightDoorOpenDirection * doorMoveDistance, moveDuration));
            doorsOpen = true;
        }
        else
        {
            // Türen schließen: Rückkehr zur gespeicherten Ausgangsposition
            StartCoroutine(MoveDoor(leftDoor, leftDoor.transform.localPosition, leftDoorClosedPos, moveDuration));
            StartCoroutine(MoveDoor(rightDoor, rightDoor.transform.localPosition, rightDoorClosedPos, moveDuration));
            doorsOpen = false;
        }

        // Optional: Knopffarbe toggeln und Animation auslösen
        if (knob != null)
        {
            KnobColorToggle kct = knob.GetComponent<KnobColorToggle>();
            if (kct != null)
                kct.ToggleColor();

            KnobAnimation ka = knob.GetComponent<KnobAnimation>();
            if (ka != null)
                ka.AnimatePress();
        }
    }

    IEnumerator MoveDoor(GameObject door, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (door == null)
            yield break;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            door.transform.localPosition = Vector3.Lerp(startPos, endPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        door.transform.localPosition = endPos;
    }
}
