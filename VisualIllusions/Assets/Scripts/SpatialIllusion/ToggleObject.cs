using UnityEngine;

public class ToggleObjectWithCount : MonoBehaviour
{
    [Header("Zu toggelndes Objekt")]
    // Hier im Inspector das GameObject zuweisen, das ein- bzw. ausgeschaltet werden soll.
    public GameObject targetObject;

    [Header("Anzahl Durchläufe bis Toggle")]
    // Anzahl der Trigger-Durchläufe, die nötig sind, um das Objekt umzuschalten.
    // Beispiel: 3 = Der Spieler muss den Trigger dreimal betreten, bevor das Objekt getoggled wird.
    public int requiredTriggerCount = 3;

    // Interner Zähler für die Trigger-Durchläufe.
    private int triggerCounter = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Überprüfe, ob der kollidierende Collider den Tag "Player" besitzt.
        if (other.CompareTag("Player"))
        {
            triggerCounter++;

            // Wenn der Zähler den festgelegten Schwellenwert erreicht oder überschreitet...
            if (triggerCounter >= requiredTriggerCount)
            {
                // ...toggle (umschalten) das Zielobjekt.
                targetObject.SetActive(!targetObject.activeSelf);

                // Zähler zurücksetzen, damit der Vorgang erneut ausgeführt werden kann.
                triggerCounter = 0;
            }
        }
    }
}
