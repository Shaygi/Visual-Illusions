using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float maxDistance = 5f;            // Wie weit der Raycast reicht
    public GameObject tooltipUI;              // Referenz zum Tooltip (UI-Element)
    private Camera playerCamera;
    private CombinedOutline currentCombinedOutline;

    private void Start()
    {
        playerCamera = Camera.main;
        if (tooltipUI != null)
            tooltipUI.SetActive(false);
    }

    private void Update()
    {
        // Raycast aus der Mitte des Bildschirms
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            // Sucht im Hierarchie-Pfad des getroffenen Objekts nach CombinedOutline
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

                // Tooltip anzeigen
                if (tooltipUI != null)
                    tooltipUI.SetActive(true);

                // Bei Drücken von E die Interaktion auslösen
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Hole das ButtonInteraction-Skript vom gleichen Objekt wie CombinedOutline
                    ButtonInteraction buttonInteraction = currentCombinedOutline.GetComponent<ButtonInteraction>();
                    if (buttonInteraction != null)
                        buttonInteraction.Interact();
                }
            }
            else
            {
                // Kein interagierbares Objekt getroffen – Outline und Tooltip deaktivieren
                if (currentCombinedOutline != null)
                {
                    currentCombinedOutline.SetOutlineActive(false);
                    currentCombinedOutline = null;
                }
                if (tooltipUI != null)
                    tooltipUI.SetActive(false);
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
            if (tooltipUI != null)
                tooltipUI.SetActive(false);
        }
    }
}
