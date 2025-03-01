using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTeleporter : MonoBehaviour
{
    // Das Ziel, an das der Spieler teleportiert werden soll
    public Transform target;

    private void OnTriggerEnter(Collider other)
    {
        // Prüfen, ob das Objekt, das den Trigger betritt, den Tag "Player" hat
        if (other.CompareTag("Player"))
        {
            // Position des Spielers auf die Zielposition setzen
            other.transform.position = target.position;
        }
    }
}
