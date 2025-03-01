using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    [Header("Pickup-Einstellungen")]
    public float pickupRange = 5f;       // Maximale Reichweite zum Aufnehmen
    public LayerMask moveableLayer;      // Layer der aufnehmbaren Objekte (z.B. "Moveable")

    [Header("Joint-Einstellungen (ConfigurableJoint)")]
    public float positionSpring = 1000f; // Federkraft, um das Objekt zum Haltepunkt zu ziehen
    public float positionDamper = 100f;  // Dämpfung, um Schwingungen zu minimieren
    public float maximumForce = 1000f;   // Maximale Kraft, die der Joint anwenden darf

    [Header("Haltpunkt")]
    public Transform holdParent;         // Leeres GameObject als Haltepunkt (Kind der Kamera)

    private Camera playerCamera;
    private GameObject heldObj = null;
    private ConfigurableJoint heldJoint;
    private Rigidbody heldRb;

    void Start()
    {
        playerCamera = Camera.main;
        if (playerCamera == null)
        {
            Debug.LogError("Keine Kamera mit dem Tag 'MainCamera' gefunden!");
        }
        if (holdParent == null)
        {
            Debug.LogError("Kein Haltpunkt (holdParent) zugewiesen!");
        }
    }

    void Update()
    {
        // Mit linker Maustaste aufnehmen oder ablegen
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                // Objekt aufnehmen
                Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
                {
                    // Prüfe, ob das getroffene Objekt im Layer Moveable liegt
                    if (((1 << hit.collider.gameObject.layer) & moveableLayer.value) != 0)
                    {
                        heldObj = hit.collider.gameObject;
                        heldRb = heldObj.GetComponent<Rigidbody>();

                        if (heldRb != null)
                        {
                            // Füge einen ConfigurableJoint hinzu, um das Objekt am Haltepunkt stabil zu verbinden
                            heldJoint = heldObj.AddComponent<ConfigurableJoint>();
                            heldJoint.autoConfigureConnectedAnchor = false;

                            // Verbinde den Joint mit dem Rigidbody des holdParent
                            Rigidbody holdRb = holdParent.GetComponent<Rigidbody>();
                            if (holdRb == null)
                            {
                                Debug.LogError("Der holdParent benötigt einen Rigidbody (isKinematic = true)!");
                                return;
                            }
                            heldJoint.connectedBody = holdRb;

                            // Setze Ankerpunkte
                            heldJoint.anchor = Vector3.zero;
                            heldJoint.connectedAnchor = Vector3.zero;
                            heldJoint.targetPosition = Vector3.zero;

                            // Sperre die Rotationsbewegung
                            heldJoint.angularXMotion = ConfigurableJointMotion.Locked;
                            heldJoint.angularYMotion = ConfigurableJointMotion.Locked;
                            heldJoint.angularZMotion = ConfigurableJointMotion.Locked;

                            // Erlaube freie lineare Bewegung, die durch den Joint angetrieben wird
                            heldJoint.xMotion = ConfigurableJointMotion.Free;
                            heldJoint.yMotion = ConfigurableJointMotion.Free;
                            heldJoint.zMotion = ConfigurableJointMotion.Free;

                            // Konfiguriere den Positionsdrive
                            JointDrive drive = new JointDrive();
                            drive.positionSpring = positionSpring;
                            drive.positionDamper = positionDamper;
                            drive.maximumForce = maximumForce;
                            heldJoint.xDrive = drive;
                            heldJoint.yDrive = drive;
                            heldJoint.zDrive = drive;

                            // Optional: Rotation des aufgenommenen Objekts einfrieren
                            heldRb.freezeRotation = true;
                        }
                    }
                }
            }
            else
            {
                // Objekt ablegen
                if (heldJoint != null)
                {
                    Destroy(heldJoint);
                }
                if (heldRb != null)
                {
                    heldRb.freezeRotation = false;
                }
                heldObj = null;
                heldRb = null;
            }
        }
    }
}
