using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    [Header("Pickup-Einstellungen")]
    public float pickupRange = 5f;        // Maximale Reichweite zum Aufnehmen
    public float holdDistance = 2f;       // Abstand vor der Kamera, an dem das Objekt gehalten wird
    public float moveSpeed = 10f;         // Geschwindigkeit, mit der das Objekt zur Zielposition bewegt wird
    public LayerMask moveableLayer;       // Layer der aufnehmbaren Objekte (z.B. "Moveable")

    private Camera playerCamera;
    private GameObject heldObj = null;
    private Rigidbody heldRb;

    void Start()
    {
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            Debug.LogError("Keine Kamera mit dem Tag 'MainCamera' gefunden!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                // Versuche, ein Objekt aufzunehmen:
                Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
                {
                    // Prüfe, ob das getroffene Objekt im Layer Moveable liegt
                    if (((1 << hit.collider.gameObject.layer) & moveableLayer.value) != 0)
                    {
                        heldObj = hit.collider.gameObject;
                        heldRb = heldObj.GetComponent<Rigidbody>();

                        // Deaktiviere Physik-Effekte, damit das Objekt nicht zufällig herumfliegt
                        if (heldRb != null)
                        {
                            heldRb.useGravity = false;
                            heldRb.isKinematic = true;
                        }
                    }
                }
            }
            else
            {
                // Lege das Objekt wieder ab:
                if (heldRb != null)
                {
                    heldRb.useGravity = true;
                    heldRb.isKinematic = false;
                }
                heldObj = null;
                heldRb = null;
            }
        }

        // Falls ein Objekt gehalten wird, bewege es an eine feste Position vor der Kamera
        if (heldObj != null)
        {
            Vector3 targetPos = playerCamera.transform.position + playerCamera.transform.forward * holdDistance;
            // Sanfte Bewegung mithilfe von Lerp
            heldObj.transform.position = Vector3.Lerp(heldObj.transform.position, targetPos, Time.deltaTime * moveSpeed);
        }
    }
}
