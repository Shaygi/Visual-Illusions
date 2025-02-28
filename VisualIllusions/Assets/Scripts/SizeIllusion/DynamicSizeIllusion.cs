using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicSizeIllusion : MonoBehaviour
{
    // Auswahl des Verhaltens:
    public enum SizeBehavior { ShrinkWhenNear, EnlargeWhenNear, Constant }
    [Tooltip("W�hle, wie sich das Objekt verhalten soll, wenn der Spieler sich n�hert.")]
    public SizeBehavior behavior = SizeBehavior.ShrinkWhenNear;

    [Header("Einstellungen")]
    [Tooltip("Referenz zur Spieler-Kamera")]
    public Transform playerCamera;

    [Tooltip("Skalierung, die das Objekt bei einer Referenzentfernung haben soll.")]
    public float baseScale = 1.0f;

    [Tooltip("Die Entfernung, bei der das Objekt genau den Basiswert (baseScale) haben soll.")]
    public float referenceDistance = 5.0f;

    [Tooltip("Minimal zul�ssiger Skalierungsfaktor.")]
    public float minScale = 0.2f;

    [Tooltip("Maximal zul�ssiger Skalierungsfaktor.")]
    public float maxScale = 3.0f;

    void Update()
    {
        // Berechne die Distanz zwischen dem Objekt und der Spieler-Kamera
        float distance = Vector3.Distance(transform.position, playerCamera.position);

        // Brechne einen Skalierungsfaktor basierend auf dem Verh�ltnis zwischen der Referenzdistanz und der aktuellen Entfernung.
        // Je nach gew�hltem Verhalten kehrt sich die Rechnung um.
        float scaleFactor = baseScale;

        switch (behavior)
        {
            case SizeBehavior.ShrinkWhenNear:
                // Wenn der Spieler n�her kommt (kleinerer Abstand) wird der Faktor kleiner.
                // Beispiel: Bei referenceDistance wird der Faktor baseScale; wenn distance < referenceDistance, wird scaleFactor < baseScale.
                scaleFactor = (distance / referenceDistance) * baseScale;
                break;

            case SizeBehavior.EnlargeWhenNear:
                // Hier soll das Objekt beim N�herkommen gr��er werden.
                // Beispiel: Bei distance == referenceDistance ist der Faktor baseScale; wenn distance kleiner ist, wird scaleFactor > baseScale.
                // Daf�r invertiere das Verh�ltnis:
                scaleFactor = (referenceDistance / distance) * baseScale;
                break;

            case SizeBehavior.Constant:
                // Unabh�ngig von der Entfernung bleibt der Skalierungsfaktor gleich.
                scaleFactor = baseScale;
                break;
        }

        // Begrenze den Skalierungsfaktor auf den gew�nschten Wertebereich:
        scaleFactor = Mathf.Clamp(scaleFactor, minScale, maxScale);

        // Setze die lokale Skalierung des Objekts (gleichm��ig in allen Dimensionen)
        transform.localScale = Vector3.one * scaleFactor;
    }
}
