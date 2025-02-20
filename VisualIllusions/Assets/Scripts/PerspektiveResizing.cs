using UnityEngine;

public class PerspektiveResizing : MonoBehaviour
{
    [Header("Components")]
    public Transform target;            // Das Zielobjekt, das f�r die Skalierung ausgew�hlt werden soll

    [Header("Parameters")]
    public LayerMask targetMask;        // Die Layer-Maske, die verwendet wird, um mit einem Raycast nur potenzielle Ziele zu treffen
    public LayerMask ignoreTargetMask;  // Die Layer-Maske, die verwendet wird, um beim Raycasting den Spieler und Zielobjekte zu ignorieren
    public float offsetFactor;          // Der Offset-Wert f�r die Positionierung des Objekts, damit es nicht in W�nde hineinragt

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
        // Falls das Ziel null ist
        if (target == null)
        {
            // Beende diese Methode
            return;
        }

        // Schie�e einen Raycast von der Kameraposition in Vorw�rtsrichtung und ignoriere die Layer, 
        // die zur Zielerfassung verwendet werden, damit das aktuell angeh�ngte Ziel nicht getroffen wird
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask))
        {
            // Setze die neue Position des Ziels, indem der Trefferpunkt verwendet und abh�ngig von der Skalierung 
            // und dem Offset-Faktor etwas zur�ckverschoben wird
            target.position = hit.point - transform.forward * offsetFactor * targetScale.x;

            // Berechne die aktuelle Entfernung zwischen der Kamera und dem Zielobjekt
            float currentDistance = Vector3.Distance(transform.position, target.position);

            // Berechne das Verh�ltnis zwischen der aktuellen Entfernung und der urspr�nglichen Entfernung
            float s = currentDistance / originalDistance;

            // Setze die Skalierungs-Variable (Vector3) auf das Verh�ltnis der Entfernungen
            targetScale.x = targetScale.y = targetScale.z = s;

            // Setze die Skalierung f�r das Zielobjekt, multipliziert mit der urspr�nglichen Skalierung
            target.localScale = targetScale * originalScale;
        }
    }
}
