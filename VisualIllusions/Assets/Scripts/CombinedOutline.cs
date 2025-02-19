using UnityEngine;

public class CombinedOutline : MonoBehaviour
{
    // Alle Outline-Komponenten in den Child-Objekten
    private Outline[] outlines;

    private void Awake()
    {
        // Suche alle Outline-Komponenten in diesem Objekt und seinen Kindern
        outlines = GetComponentsInChildren<Outline>();
        SetOutlineActive(false);
    }

    /// <summary>
    /// Schaltet den Outline-Effekt für alle Kinder ein oder aus.
    /// </summary>
    public void SetOutlineActive(bool active)
    {
        foreach (Outline o in outlines)
        {
            o.enabled = active;
        }
    }
}
