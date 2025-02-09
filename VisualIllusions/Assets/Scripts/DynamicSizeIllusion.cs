using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSizeIllusion : MonoBehaviour
{
    // Auswahl des Verhaltens:
    public enum SizeBehavior { ShrinkWhenNear, EnlargeWhenNear, Constant }
    [Tooltip("Wähle, wie sich das Objekt verhalten soll, wenn der Spieler sich nähert.")]
    public SizeBehavior behavior = SizeBehavior.ShrinkWhenNear;

    [Header("Einstellungen")]
    [Tooltip("Referenz zur Spieler-Kamera")]
    public Transform playerCamera;

    [Tooltip("Skalierung, die das Objekt bei einer Referenzentfernung haben soll.")]
    public float baseScale = 1.0f;

    [Tooltip("Die Entfernung, bei der das Objekt genau den Basiswert (baseScale) haben soll.")]
    public float referenceDistance = 5.0f;

    [Tooltip("Minimal zulässiger Skalierungsfaktor.")]
    public float minScale = 0.2f;

    [Tooltip("Maximal zulässiger Skalierungsfaktor.")]
    public float maxScale = 3.0f;

    void Update()
    {
        // Berechne die Distanz zwischen dem Objekt und der Spieler-Kamera
        float distance = Vector3.Distance(transform.position, playerCamera.position);

        // Brechne einen Skalierungsfaktor basierend auf dem Verhältnis zwischen der Referenzdistanz und der aktuellen Entfernung.
        // Je nach gewähltem Verhalten kehrt sich die Rechnung um.
        float scaleFactor = baseScale;

        switch (behavior)
        {
            case SizeBehavior.ShrinkWhenNear:
                // Wenn der Spieler näher kommt (kleinerer Abstand) wird der Faktor kleiner.
                // Beispiel: Bei referenceDistance wird der Faktor baseScale; wenn distance < referenceDistance, wird scaleFactor < baseScale.
                scaleFactor = (distance / referenceDistance) * baseScale;
                break;

            case SizeBehavior.EnlargeWhenNear:
                // Hier soll das Objekt beim Näherkommen größer werden.
                // Beispiel: Bei distance == referenceDistance ist der Faktor baseScale; wenn distance kleiner ist, wird scaleFactor > baseScale.
                // Dafür invertiere das Verhältnis:
                scaleFactor = (referenceDistance / distance) * baseScale;
                break;

            case SizeBehavior.Constant:
                // Unabhängig von der Entfernung bleibt der Skalierungsfaktor gleich.
                scaleFactor = baseScale;
                break;
        }

        // Begrenze den Skalierungsfaktor auf den gewünschten Wertebereich:
        scaleFactor = Mathf.Clamp(scaleFactor, minScale, maxScale);

        // Setze die lokale Skalierung des Objekts (gleichmäßig in allen Dimensionen)
        transform.localScale = Vector3.one * scaleFactor;
    }
}
