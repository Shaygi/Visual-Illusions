using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float maxDistance = 5f;
    public float sphereRadius = 0.5f; // Radius des SphereCasts, anpassen je nach Bedarf
    public GameObject interactIcon;

    private Camera playerCamera;
    private CombinedOutline currentCombinedOutline;
    private bool hasInteracted = false;

    private void Start()
    {
        playerCamera = Camera.main;
        if (interactIcon != null)
            interactIcon.SetActive(false);
    }

    private void Update()
    {
        // Erstelle einen Ray aus der Mitte des Bildschirms
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        // Verwende einen SphereCast, um einen Bereich abzufragen
        if (Physics.SphereCast(ray, sphereRadius, out RaycastHit hit, maxDistance))
        {
            // Suche nach einem CombinedOutline im Hierarchie-Pfad des getroffenen Objekts
            CombinedOutline combinedOutline = hit.collider.GetComponentInParent<CombinedOutline>();

            if (combinedOutline != null)
            {
                if (currentCombinedOutline != combinedOutline)
                {
                    if (currentCombinedOutline != null)
                        currentCombinedOutline.SetOutlineActive(false);

                    currentCombinedOutline = combinedOutline;
                    currentCombinedOutline.SetOutlineActive(true);
                    hasInteracted = false;
                }

                // Zeige das Icon, falls noch nicht interagiert wurde
                if (!hasInteracted && interactIcon != null && !interactIcon.activeSelf)
                {
                    interactIcon.SetActive(true);
                }

                // Bei linkem Mausklick Interaktion ausführen und Icon ausblenden
                if (Input.GetMouseButtonDown(0))
                {
                    currentCombinedOutline.SendMessage("Interact", SendMessageOptions.DontRequireReceiver);
                    hasInteracted = true;
                    if (interactIcon != null)
                        interactIcon.SetActive(false);
                }
            }
            else
            {
                // Kein interagierbares Objekt getroffen – deaktiviere Outline und Icon
                if (currentCombinedOutline != null)
                {
                    currentCombinedOutline.SetOutlineActive(false);
                    currentCombinedOutline = null;
                }
                if (interactIcon != null)
                    interactIcon.SetActive(false);
            }
        }
        else
        {
            // Kein Treffer – deaktiviere Outline und Icon
            if (currentCombinedOutline != null)
            {
                currentCombinedOutline.SetOutlineActive(false);
                currentCombinedOutline = null;
            }
            if (interactIcon != null)
                interactIcon.SetActive(false);
        }
    }
}
