using UnityEngine;

public class PerspektiveResizingOverlap : MonoBehaviour
{
    [Header("Components")]
    public Transform target;            // Das Zielobjekt, das f�r die Skalierung ausgew�hlt werden soll

    [Header("Parameters")]
    public LayerMask targetMask;        // Die Layer-Maske, die verwendet wird, um mit einem Raycast nur potenzielle Ziele zu treffen
    public LayerMask ignoreTargetMask;  // Die Layer-Maske, die verwendet wird, um beim Raycast und OverlapBox Kollisionen mit Spieler und anderen Zielobjekten zu ignorieren
    public float offsetFactor;          // Der Offset-Wert, damit das Objekt nicht direkt in die Wand gedr�ckt wird

    float originalDistance;             // Die urspr�ngliche Entfernung zwischen der Kameraposition und dem Ziel
    float originalScale;                // Die urspr�ngliche Skalierung des Zielobjekts vor der Gr��enanpassung
    Vector3 targetScale;                // Die Skalierung, die dem Objekt in jedem Frame zugewiesen werden soll

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
        // �berpr�fe, ob die linke Maustaste gedr�ckt wurde
        if (Input.GetMouseButtonDown(0))
        {
            // Falls momentan kein Ziel vorhanden ist
            if (target == null)
            {
                // Schie�e einen Raycast mit der Layer-Maske, die nur potenzielle Ziele trifft
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask))
                {
                    // Setze die Zielvariable auf das Transform-Objekt, das durch den Raycast getroffen wurde
                    target = hit.transform;

                    // Deaktiviere die Physik f�r das Objekt
                    target.GetComponent<Rigidbody>().isKinematic = true;

                    // Berechne die Entfernung zwischen der Kamera und dem Objekt
                    originalDistance = Vector3.Distance(transform.position, target.position);

                    // Speichere die urspr�ngliche Skalierung des Objekts in der Variable originalScale
                    originalScale = target.localScale.x;

                    // Setze die Zielskala vor�bergehend auf den Originalwert
                    targetScale = target.localScale;
                }
            }
            // Falls bereits ein Ziel vorhanden ist
            else
            {
                // Aktiviere die Physik f�r das Zielobjekt wieder
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
        {
            return;
        }

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
            // Berechne eine vorl�ufige neue Position, indem der Trefferpunkt verwendet und ein Offset (abh�ngig von der Skalierung) abgezogen wird
            Vector3 newPosition = hit.point - transform.forward * offsetFactor * targetScale.x;
            target.position = newPosition;

            // Verwende eine OverlapBox, um zu pr�fen, ob der Cube in andere Objekte (W�nde, B�den, Decken) hineinragt
            Collider[] overlaps = Physics.OverlapBox(target.position, halfExtents, target.rotation, ignoreTargetMask);
            if (overlaps.Length > 0)
            {
                Collider targetCollider = boxCollider;
                // Gehe alle �berlappenden Collider durch
                foreach (Collider col in overlaps)
                {
                    // �berspringe das Ziel selbst
                    if (col.transform == target)
                        continue;

                    Vector3 direction;
                    float distance;
                    // Berechne, wie weit und in welche Richtung der Cube verschoben werden muss, um die Kollision zu beenden
                    if (Physics.ComputePenetration(targetCollider, target.position, target.rotation,
                                                   col, col.transform.position, col.transform.rotation,
                                                   out direction, out distance))
                    {
                        // Verschiebe den Cube entlang der berechneten Richtung um die gefundene Distanz
                        target.position += direction * distance;
                    }
                }
            }

            // Berechne die aktuelle Entfernung zwischen der Kamera und der (eventuell korrigierten) Zielposition
            float currentDistance = Vector3.Distance(transform.position, target.position);

            // Berechne das Verh�ltnis zwischen der aktuellen Entfernung und der urspr�nglichen Entfernung
            float s = currentDistance / originalDistance;

            // Aktualisiere die Skalierung basierend auf diesem Verh�ltnis
            targetScale = new Vector3(s, s, s);
            // Setze die Skalierung f�r das Zielobjekt, multipliziert mit der urspr�nglichen Skalierung
            target.localScale = targetScale * originalScale;
        }
    }
}

