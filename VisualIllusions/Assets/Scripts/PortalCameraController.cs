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
        Vector3 relativePos = entrancePortal.InverseTransformPoint(playerCamera.position);

        // Transformiere die relative Position in den Raum des Ausgang-Portals
        transform.position = exitPortal.TransformPoint(relativePos);

        // Berechne die relative Rotation
        Quaternion relativeRot = Quaternion.Inverse(entrancePortal.rotation) * playerCamera.rotation;
        transform.rotation = exitPortal.rotation * relativeRot;
    }
}
