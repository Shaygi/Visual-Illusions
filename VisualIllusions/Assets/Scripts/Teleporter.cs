using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform player;      // Referenz zum Spieler-Transform
    public Transform reciever;    // Ziel-Transform, an das der Spieler teleportiert wird

    private bool isOverlapping = false;  // Gibt an, ob der Spieler sich im Teleportbereich befindet

    void Update()
    {
        // Prüfe, ob der Spieler den Teleportbereich betritt
        if (isOverlapping)
        {
            // Berechne den Vektor vom Portal (dieses Objekt) zum Spieler
            Vector3 portalToPlayer = player.position - transform.position;
            // Berechne das Skalarprodukt zwischen dem Aufwärtsvektor des Portals und dem Vektor zum Spieler
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // Wenn das Skalarprodukt positiv ist, hat der Spieler das Portal vollständig überquert
            if (dotProduct > 0f)
            {
                // Berechne den Winkelunterschied zwischen der Rotation des Portals und des Empfängers
                float rotationDifference = -Quaternion.Angle(transform.rotation, reciever.rotation);
                // Passe den Winkel an, um die korrekte Ausrichtung zu erhalten (180 Grad Unterschied)
                rotationDifference += 180;
                // Drehe den Spieler um die Y-Achse um den berechneten Winkelunterschied
                player.Rotate(Vector3.up, rotationDifference);

                // Berechne den versetzten Positionsvektor basierend auf der Rotation
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDifference, 0f) * portalToPlayer;
                // Setze die neue Position des Spielers relativ zum Empfängerportal
                player.position = reciever.position + positionOffset;

                // Setze den Überlappungsstatus zurück, da der Teleport abgeschlossen ist
                isOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Wenn der Spieler den Trigger betritt, setze isOverlapping auf true
        if (other.tag == "Player")
        {
            isOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Wenn der Spieler den Trigger verlässt, setze isOverlapping auf false
        if (other.tag == "Player")
        {
            isOverlapping = false;
        }
    }
}
