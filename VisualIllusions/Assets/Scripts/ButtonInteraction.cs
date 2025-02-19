using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    [Header("Zu toggelnde Objekte")]
    public GameObject[] targetObjects;  // Objekte, die ein- bzw. ausgeschaltet werden sollen

    [Header("Knopf, dessen Farbe und Animation geändert werden sollen")]
    public GameObject knob;  // Weisen hier dein Knopf-Objekt zu (z.B. den Cylinder)

    public void Interact()
    {
        // Toggle der Zielobjekte
        foreach (GameObject target in targetObjects)
        {
            if (target != null)
                target.SetActive(!target.activeSelf);
        }

        // Farbe des Knopfs toggeln
        if (knob != null)
        {
            KnobColorToggle kct = knob.GetComponent<KnobColorToggle>();
            if (kct != null)
            {
                kct.ToggleColor();
            }

            // Animation auslösen
            KnobAnimation ka = knob.GetComponent<KnobAnimation>();
            if (ka != null)
            {
                ka.AnimatePress();
            }
        }
    }
}
