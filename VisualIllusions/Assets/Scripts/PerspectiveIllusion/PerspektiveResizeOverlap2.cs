using UnityEngine;

public class PerspektiveResizingOverlap2 : MonoBehaviour
{
    [Header("Components")]
    public Transform target;            // Das Zielobjekt, das für die Skalierung ausgewählt werden soll

    [Header("Parameters")]
    public LayerMask targetMask;        // Layer, um potenzielle Ziele zu treffen
    public LayerMask ignoreTargetMask;  // Layer, die beim Raycast und Overlap ignoriert werden
    public float offsetFactor;          // Offset, damit das Objekt nicht direkt in die Wand gedrückt wird

    [Header("Collision Settings")]
    public int collisionResolutionIterations = 3; // Anzahl der Iterationen zur Kollisionsauflösung
    public float collisionLerpSpeed = 0.2f;         // Lerp-Geschwindigkeit für sanftere Anpassungen

    [Header("Scale Limits")]
    public float minScale = 0.1f;  // Mindestskala, damit das Objekt nicht zu klein wird
    public float maxScale = 10.0f; // Maximalskala, damit das Objekt nicht zu groß wird

    [Header("Distance Settings")]
    public float minDistanceToCamera = 0.5f;  // Mindestabstand, um zu verhindern, dass das Objekt zu nah an die Kamera kommt

    float originalDistance;             // Ursprüngliche Entfernung zwischen Kamera und Ziel
    float originalScale;                // Ursprüngliche Skalierung des Ziels vor der Anpassung
    Vector3 targetScale;                // Skalierung, die dem Objekt in jedem Frame zugewiesen wird

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        HandleInput();
        ResizeTarget();
    }

    void HandleInput()
    {
        // Überprüfe, ob die linke Maustaste gedrückt wurde
        if (Input.GetMouseButtonDown(0))
        {
            if (target == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask))
                {
                    // Setze die Zielvariable auf das Transform-Objekt, das durch den Raycast getroffen wurde
                    target = hit.transform;

                    // Deaktiviere die Physik für das Objekt
                    target.GetComponent<Rigidbody>().isKinematic = true;

                    // Berechne die Entfernung zwischen der Kamera und dem Objekt
                    originalDistance = Vector3.Distance(transform.position, target.position);

                    // Speichere die ursprüngliche Skalierung des Objekts in der Variable originalScale
                    originalScale = target.localScale.x;

                    // Setze die Zielskala vorübergehend auf den Originalwert
                    targetScale = target.localScale;
                }
            }
            // Falls bereits ein Ziel vorhanden ist
            else
            {
                // Aktiviere die Physik für das Zielobjekt wieder
                target.GetComponent<Rigidbody>().isKinematic = false;

                // Setze die Zielvariable auf null
                target = null;
            }
        }
    }

    void ResizeTarget()
    {
        // Falls das Ziel null ist, beende diese Methode
        if (target == null)
            return;

        // Hole den BoxCollider des Ziels (da es sich um einen Cube handelt)
        BoxCollider boxCollider = target.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            Debug.LogWarning("Kein BoxCollider am Zielobjekt gefunden!");
            return;
        }

        // Berechne die halben Abmessungen (HalfExtents) des Cubes in Weltkoordinaten
        Vector3 halfExtents = Vector3.Scale(boxCollider.size * 0.5f, target.lossyScale);

        RaycastHit hit;
        // Verwende einen Raycast, um den ersten Treffer entlang der Blickrichtung zu ermitteln
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask))
        {
            Vector3 desiredPosition = hit.point - transform.forward * offsetFactor * targetScale.x;

            // Berechne den Abstand von der Kamera zur gewünschten Position
            float distanceToCamera = Vector3.Distance(transform.position, desiredPosition);

            // Stelle sicher, dass das Objekt nicht zu nah an die Kamera kommt
            if (distanceToCamera < minDistanceToCamera)
            {
                desiredPosition = transform.position + transform.forward * minDistanceToCamera;
            }

            // Interpoliere die Position für eine sanftere Anpassung
            target.position = Vector3.Lerp(target.position, desiredPosition, collisionLerpSpeed);

            // Iterative Kollisionsauflösung
            for (int i = 0; i < collisionResolutionIterations; i++)
            {
                Collider[] overlaps = Physics.OverlapBox(target.position, halfExtents, target.rotation, ignoreTargetMask);
                foreach (Collider col in overlaps)
                {
                    if (col.transform == target)
                        continue;

                    Vector3 direction;
                    float distance;
                    if (Physics.ComputePenetration(boxCollider, target.position, target.rotation,
                                                   col, col.transform.position, col.transform.rotation,
                                                   out direction, out distance))
                    {
                        target.position += direction * distance;
                    }
                }
            }

            // Berechne die aktuelle Entfernung zwischen der Kamera und der (eventuell korrigierten) Zielposition
            float currentDistance = Vector3.Distance(transform.position, target.position);
            // Berechne das Verhältnis zwischen der aktuellen Entfernung und der urspr�nglichen Entfernung
            float s = currentDistance / originalDistance;

            // Begrenze die Skalierung innerhalb des zulässigen Bereichs
            s = Mathf.Clamp(s * originalScale, minScale, maxScale);

            // Setze die Skalierung für das Zielobjekt, multipliziert mit der ursprünglichen Skalierung
            targetScale = new Vector3(s, s, s);
            target.localScale = targetScale;
        }
    }
}
