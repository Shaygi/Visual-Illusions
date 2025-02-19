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
        // Berechne die Position des Spielers relativ zum Eingang-Portal
        Vector3 relativePos = playerCamera.position - exitPortal.position;
        transform.position = entrancePortal.position + relativePos;

        float angularDifferencePortalRotation = Quaternion.Angle(entrancePortal.rotation, exitPortal.rotation);

        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferencePortalRotation, Vector3.up);

        Vector3 cameraDirection = portalRotationDifference * playerCamera.forward;
        transform.rotation = Quaternion.LookRotation(cameraDirection, Vector3.up);
    }
}
