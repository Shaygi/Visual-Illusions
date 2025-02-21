using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DoorMovementDirection
{
    Left,
    Right,
    Forward,
    Backward,
    Up,
    Down,
    Custom
}

[System.Serializable]
public class DoorPair
{
    public GameObject leftDoor;
    public GameObject rightDoor;
    public Vector3 leftDoorClosedPos;
    public Vector3 rightDoorClosedPos;
    public bool doorsOpen = false;
}

public class DoorManager : MonoBehaviour
{
    public List<DoorPair> doorPairs; // Liste aller Türpaare
    public float doorMoveDistance = 2.0f;
    public float moveDuration = 1.0f;

    [Header("Tür Bewegungsrichtung")]
    // Hier per Dropdown festlegen, in welche Richtung sich die Türen bewegen sollen
    public DoorMovementDirection leftDoorMovement = DoorMovementDirection.Left;
    public DoorMovementDirection rightDoorMovement = DoorMovementDirection.Right;
    // Falls 'Custom' gewählt wird, hier den Vektor angeben:
    public Vector3 leftDoorCustomDirection = Vector3.left;
    public Vector3 rightDoorCustomDirection = Vector3.right;

    [Header("Spieler Erkennung")]
    public Transform playerTransform;
    public float activationDistance = 5.0f;

    // Hilfsmethode: Wandelt die Enum-Auswahl in einen normierten Vektor um.
    private Vector3 GetDirection(DoorMovementDirection movement, Vector3 customDirection)
    {
        switch (movement)
        {
            case DoorMovementDirection.Left:
                return Vector3.left;
            case DoorMovementDirection.Right:
                return Vector3.right;
            case DoorMovementDirection.Forward:
                return Vector3.forward;
            case DoorMovementDirection.Backward:
                return Vector3.back;
            case DoorMovementDirection.Up:
                return Vector3.up;
            case DoorMovementDirection.Down:
                return Vector3.down;
            case DoorMovementDirection.Custom:
                return customDirection.normalized;
            default:
                return Vector3.zero;
        }
    }

    void Start()
    {
        // Ausgangspositionen für jedes Türpaar speichern (lokale Positionen)
        foreach (DoorPair pair in doorPairs)
        {
            if (pair.leftDoor != null)
                pair.leftDoorClosedPos = pair.leftDoor.transform.localPosition;
            if (pair.rightDoor != null)
                pair.rightDoorClosedPos = pair.rightDoor.transform.localPosition;
        }
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        // Für jedes Türpaar den Abstand vom Spieler zum Paar (Mittelpunkt) berechnen
        foreach (DoorPair pair in doorPairs)
        {
            Vector3 pairCenter = Vector3.zero;
            if (pair.leftDoor != null && pair.rightDoor != null)
            {
                pairCenter = (pair.leftDoor.transform.position + pair.rightDoor.transform.position) / 2f;
            }
            else if (pair.leftDoor != null)
            {
                pairCenter = pair.leftDoor.transform.position;
            }
            else if (pair.rightDoor != null)
            {
                pairCenter = pair.rightDoor.transform.position;
            }

            float distance = Vector3.Distance(playerTransform.position, pairCenter);

            if (distance <= activationDistance && !pair.doorsOpen)
            {
                OpenPair(pair);
            }
            else if (distance > activationDistance && pair.doorsOpen)
            {
                ClosePair(pair);
            }
        }
    }

    void OpenPair(DoorPair pair)
    {
        Vector3 leftDir = GetDirection(leftDoorMovement, leftDoorCustomDirection);
        Vector3 rightDir = GetDirection(rightDoorMovement, rightDoorCustomDirection);

        if (pair.leftDoor != null)
            StartCoroutine(MoveDoor(pair.leftDoor,
                pair.leftDoor.transform.localPosition,
                pair.leftDoorClosedPos + leftDir * doorMoveDistance,
                moveDuration));
        if (pair.rightDoor != null)
            StartCoroutine(MoveDoor(pair.rightDoor,
                pair.rightDoor.transform.localPosition,
                pair.rightDoorClosedPos + rightDir * doorMoveDistance,
                moveDuration));

        pair.doorsOpen = true;
    }

    void ClosePair(DoorPair pair)
    {
        if (pair.leftDoor != null)
            StartCoroutine(MoveDoor(pair.leftDoor,
                pair.leftDoor.transform.localPosition,
                pair.leftDoorClosedPos,
                moveDuration));
        if (pair.rightDoor != null)
            StartCoroutine(MoveDoor(pair.rightDoor,
                pair.rightDoor.transform.localPosition,
                pair.rightDoorClosedPos,
                moveDuration));

        pair.doorsOpen = false;
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
