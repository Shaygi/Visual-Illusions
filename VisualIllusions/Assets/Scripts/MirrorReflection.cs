using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorReflection : MonoBehaviour
{
    // Referenzen auf die Kameras:
    // PlayerCamera: Die Kamera des Spielers
    // MirrorCamera: Die Kamera, die den Spiegel-Effekt darstellt
    public Transform PlayerCamera, MirrorCamera;

    // Start-Methode: Wird einmalig beim Start des Spiels aufgerufen
    void Start()
    {
        // Initialisierungen können hier vorgenommen werden, falls benötigt
    }

    // Update-Methode: Wird in jedem Frame aufgerufen
    void Update()
    {
        // Erstelle einen Vektor (PosY) mit der x- und z-Position des Spiegels,
        // aber mit der y-Position der Spieler-Kamera.
        // So wird sichergestellt, dass nur die Höhe der Spieler-Kamera berücksichtigt wird.
        Vector3 PosY = new Vector3(transform.position.x, PlayerCamera.transform.position.y, transform.position.z);

        // Berechne den Vektor von PosY zur Position der Spieler-Kamera.
        // Dieser Vektor gibt die seitliche Verschiebung der Kamera relativ zum Spiegel an.
        Vector3 side1 = PlayerCamera.transform.position - PosY;

        // side2 entspricht der Vorwärtsrichtung des Spiegels (welche als Spiegel-Normal dient)
        Vector3 side2 = transform.forward;

        // Berechne den signierten Winkel zwischen side1 und side2 um die y-Achse.
        // Dieser Winkel gibt an, wie sehr die Spieler-Kamera relativ zur Spiegel-Normal abweicht.
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);

        // Setze die lokalen Euler-Winkel der Spiegel-Kamera so, dass nur die y-Achse rotiert wird.
        // Dadurch wird der Spiegel-Effekt entsprechend dem berechneten Winkel angepasst.
        MirrorCamera.localEulerAngles = new Vector3(0, angle, 0);
    }
}
