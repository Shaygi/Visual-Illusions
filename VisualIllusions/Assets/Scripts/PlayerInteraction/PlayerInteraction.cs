using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float maxDistance = 5f;
    public float sphereRadius = 0.5f; // Radius des SphereCasts
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

        // Erfasse alle Treffer im SphereCast
        RaycastHit[] hits = Physics.SphereCastAll(ray, sphereRadius, maxDistance);
        CombinedOutline bestCombinedOutline = null;
        float bestAngle = Mathf.Infinity; // Der kleinste Winkel (zwischen Ray-Richtung und Trefferpunkt) wird bevorzugt

        foreach (var hit in hits)
        {
            CombinedOutline outline = hit.collider.GetComponentInParent<CombinedOutline>();
            if (outline != null)
            {
                // Berechne den Winkel zwischen der Blickrichtung und der Richtung zum Trefferpunkt
                Vector3 directionToHit = (hit.point - playerCamera.transform.position).normalized;
                float angle = Vector3.Angle(ray.direction, directionToHit);

                // Wähle den Treffer, der am nächsten an der Mitte liegt (kleinster Winkel)
                if (angle < bestAngle)
                {
                    bestAngle = angle;
                    bestCombinedOutline = outline;
                }
            }
        }

        if (bestCombinedOutline != null)
        {
            // Wechsel das aktuell hervorgehobene Objekt, falls sich der Treffer ändert
            if (currentCombinedOutline != bestCombinedOutline)
            {
                if (currentCombinedOutline != null)
                    currentCombinedOutline.SetOutlineActive(false);

                currentCombinedOutline = bestCombinedOutline;
                currentCombinedOutline.SetOutlineActive(true);
                hasInteracted = false;
            }

            // Zeige das Interaktions-Icon, wenn noch nicht interagiert wurde
            if (!hasInteracted && interactIcon != null && !interactIcon.activeSelf)
            {
                interactIcon.SetActive(true);
            }

            // Bei Mausklick die Interaktion ausführen
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
            // Kein interagierbares Objekt im Fokus – deaktiviere Outline und Icon
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
