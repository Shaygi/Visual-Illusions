using UnityEngine;

public class SeamlessTunnelTeleport : MonoBehaviour
{
    [Header("Zielposition am Ausgang des kurzen Tunnels")]
    public Transform teleportTarget;

    // Damit der Teleport nicht mehrfach ausgel�st wird, wenn der Spieler den Trigger mehrfach betritt.
    private bool transitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !transitioning)
        {
            transitioning = true;
            TeleportPlayer(other.gameObject);
        }
    }

    private void TeleportPlayer(GameObject player)
    {
        // Teleportiere den Spieler direkt zur Zielposition
        player.transform.position = teleportTarget.position;
        player.transform.rotation = teleportTarget.rotation;

        // Setze den �bergangs-Flag zur�ck, damit er beim n�chsten Betreten erneut ausgel�st werden kann.
        transitioning = false;
    }
}
