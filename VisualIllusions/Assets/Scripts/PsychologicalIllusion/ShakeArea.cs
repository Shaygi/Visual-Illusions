using UnityEngine;

public class ShakeArea : MonoBehaviour
{
    // Wenn der Trigger Bereich betreten wird...
    private void OnTriggerEnter(Collider other)
    {
        // ...prüfen ob  Spieler (mit Tag "Player") den Bereich betritt
        if (other.CompareTag("Player"))
        {
            // Hauptkamera setzen
            Camera cam = Camera.main;
            if (cam != null)
            {
                // ... angehängtes Skript zum zittern aktivieren
                FreezeShake shakeScript = cam.GetComponent<FreezeShake>();
                if (shakeScript != null)
                {
                    shakeScript.SetShakeActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera cam = Camera.main;
            if (cam != null)
            {
                FreezeShake shakeScript = cam.GetComponent<FreezeShake>();
                if (shakeScript != null)
                {
                    shakeScript.SetShakeActive(false);
                }
            }
        }
    }
}
