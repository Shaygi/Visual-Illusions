using UnityEngine;

public class PortalCameraController : MonoBehaviour
{
    [Header("Portal-Referenzen")]
    [Tooltip("Das Portal, vor dem der Spieler steht (Eingangsportal).")]
    public Transform entrancePortal;

    [Tooltip("Das gegenüberliegende Portal (Ausgangsportal).")]
    public Transform exitPortal;

    [Tooltip("Die Spieler-Kamera (z. B. Main Camera).")]
    public Transform playerCamera;

    void LateUpdate()
    {
        // Berechne die Position des Spielers relativ zum Ausgangsportal
        Vector3 relativePos = playerCamera.position - exitPortal.position;
        // Setze die Position der Portal-Kamera relativ zum Eingangstortal
        transform.position = entrancePortal.position + relativePos;

        // Berechne den Winkelunterschied zwischen den Rotationen des Eingangstors und Ausgangstors
        float angularDifferencePortalRotation = Quaternion.Angle(entrancePortal.rotation, exitPortal.rotation);

        // Erzeuge eine Drehung um die Y-Achse basierend auf dem berechneten Winkelunterschied
        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferencePortalRotation, Vector3.up);

        // Berechne die neue Blickrichtung der Kamera, indem die Drehung auf die Vorwärtsrichtung der Spieler-Kamera angewendet wird
        Vector3 cameraDirection = portalRotationDifference * playerCamera.forward;
        // Setze die Rotation der Portal-Kamera so, dass sie in die berechnete Richtung zeigt
        transform.rotation = Quaternion.LookRotation(cameraDirection, Vector3.up);
    }
}
