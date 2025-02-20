using UnityEngine;

public class WallCollisionController : MonoBehaviour
{
    // Referenz zum Collider des Spielers
    public Collider playerCollider;

    void Start()
    {
        // Hole den Collider der Wand (an diesem GameObject)
        Collider wallCollider = GetComponent<Collider>();

        // Falls der Spieler-Collider nicht zugewiesen wurde, versuche ihn per Tag zu finden
        if (playerCollider == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerCollider = player.GetComponent<Collider>();
        }

        // Ignoriere die Kollision zwischen der Wand und dem Spieler
        if (wallCollider != null && playerCollider != null)
        {
            Physics.IgnoreCollision(wallCollider, playerCollider, true);
        }
    }
}

