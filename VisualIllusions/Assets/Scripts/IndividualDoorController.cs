using UnityEngine;
using System.Collections;

public class IndividualDoorController : MonoBehaviour
{
    [Header("Tür Objekte (Kinder des leeren Objekts)")]
    public GameObject leftDoor;   // Linke Tür
    public GameObject rightDoor;  // Rechte Tür

    [Header("Bewegungseinstellungen")]
    public float doorMoveDistance = 2.0f;  // Distanz, um die sich die Türen bewegen
    public float moveDuration = 1.0f;      // Dauer der Bewegung in Sekunden

    [Header("Öffnungsrichtungen (lokal)")]
    // Lege hier fest, in welche Richtung sich die Türen bewegen sollen.
    // Beispiel: Für links/rechts: leftDoorOpenDirection = Vector3.left, rightDoorOpenDirection = Vector3.right.
    public Vector3 leftDoorOpenDirection = Vector3.left;
    public Vector3 rightDoorOpenDirection = Vector3.right;

    [Header("Spieler Erkennung")]
    public Transform playerTransform;      // Transform des Spielers (z. B. per Tag "Player" zuweisen)
    public float activationDistance = 5.0f;  // Abstand, ab dem die Türen reagieren

    // Gespeicherte Ausgangspositionen der Türen (als lokale Positionen)
    private Vector3 leftDoorClosedPos;
    private Vector3 rightDoorClosedPos;
    private bool doorsOpen = false;

    void Start()
    {
        // Ausgangspositionen speichern
        if (leftDoor != null)
            leftDoorClosedPos = leftDoor.transform.localPosition;
        if (rightDoor != null)
            rightDoorClosedPos = rightDoor.transform.localPosition;
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        // Da dieses Skript an dem leeren Türpaar-Objekt hängt, verwenden von dessen Position als Mittelpunkt
        float distance = Vector3.Distance(playerTransform.position, transform.position);

        if (distance <= activationDistance && !doorsOpen)
        {
            OpenDoors();
        }
        else if (distance > activationDistance && doorsOpen)
        {
            CloseDoors();
        }
    }

    void OpenDoors()
    {
        // Linke Tür bewegt sich vom geschlossenen Zustand in Richtung leftDoorOpenDirection
        if (leftDoor != null)
            StartCoroutine(MoveDoor(leftDoor,
                leftDoor.transform.localPosition,
                leftDoorClosedPos + leftDoorOpenDirection * doorMoveDistance,
                moveDuration));

        // Rechte Tür bewegt sich vom geschlossenen Zustand in Richtung rightDoorOpenDirection
        if (rightDoor != null)
            StartCoroutine(MoveDoor(rightDoor,
                rightDoor.transform.localPosition,
                rightDoorClosedPos + rightDoorOpenDirection * doorMoveDistance,
                moveDuration));

        doorsOpen = true;
    }

    void CloseDoors()
    {
        if (leftDoor != null)
            StartCoroutine(MoveDoor(leftDoor, leftDoor.transform.localPosition, leftDoorClosedPos, moveDuration));
        if (rightDoor != null)
            StartCoroutine(MoveDoor(rightDoor, rightDoor.transform.localPosition, rightDoorClosedPos, moveDuration));

        doorsOpen = false;
    }

    IEnumerator MoveDoor(GameObject door, Vector3 startPos, Vector3 endPos, float duration)
    {
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
