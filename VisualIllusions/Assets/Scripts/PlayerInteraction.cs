using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float maxDistance = 5f;
    private Camera playerCamera;
    private CombinedOutline currentCombinedOutline;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        // Raycast aus der Mitte des Bildschirms
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            // Suche im Hierarchie-Pfad des getroffenen Objekts nach CombinedOutline
            CombinedOutline combinedOutline = hit.collider.GetComponentInParent<CombinedOutline>();

            if (combinedOutline != null)
            {
                // Falls ein neues interagierbares Objekt erkannt wird:
                if (currentCombinedOutline != combinedOutline)
                {
                    if (currentCombinedOutline != null)
                        currentCombinedOutline.SetOutlineActive(false);

                    currentCombinedOutline = combinedOutline;
                    currentCombinedOutline.SetOutlineActive(true);
                }

                // Bei Drücken von E alle "Interact"-Methoden aufrufen
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // SendMessage ruft "Interact" in allen Komponenten dieses GameObjects auf,
                    // sofern sie eine solche Methode besitzen.
                    currentCombinedOutline.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                // Kein interagierbares Objekt getroffen – Outline deaktivieren
                if (currentCombinedOutline != null)
                {
                    currentCombinedOutline.SetOutlineActive(false);
                    currentCombinedOutline = null;
                }
            }
        }
        else
        {
            // Kein Treffer – deaktivieren
            if (currentCombinedOutline != null)
            {
                currentCombinedOutline.SetOutlineActive(false);
                currentCombinedOutline = null;
            }
        }
    }
}
