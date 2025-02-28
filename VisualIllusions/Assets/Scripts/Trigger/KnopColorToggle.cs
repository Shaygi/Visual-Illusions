using UnityEngine;

public class KnobColorToggle : MonoBehaviour
{
    // Speichert den aktuellen Zustand (false = rot, true = grün)
    private bool isGreen = false;
    private Renderer knobRenderer;

    // Farben, die verwendet werden (kannst du im Inspector anpassen)
    public Color greenColor = Color.green;
    public Color redColor = Color.red;

    private void Start()
    {
        // Hole den Renderer des Knopf-Objekts
        knobRenderer = GetComponent<Renderer>();
        if (knobRenderer != null)
        {
            // Starte mit Rot
            knobRenderer.material.color = redColor;
        }
    }

    // Methode zum Umschalten der Farbe
    public void ToggleColor()
    {
        isGreen = !isGreen;
        if (knobRenderer != null)
        {
            knobRenderer.material.color = isGreen ? greenColor : redColor;
        }
    }
}
