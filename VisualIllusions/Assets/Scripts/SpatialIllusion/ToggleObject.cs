using UnityEngine;

public class ToggleObjectWithCount : MonoBehaviour
{
    [Header("Zu toggelndes Objekt")]
    // Hier im Inspector das GameObject zuweisen, das ein- bzw. ausgeschaltet werden soll.
    public GameObject targetObject;

    [Header("Anzahl Durchl�ufe bis Toggle")]
    // Anzahl der Trigger-Durchl�ufe, die n�tig sind, um das Objekt umzuschalten.
    // Beispiel: 3 = Der Spieler muss den Trigger dreimal betreten, bevor das Objekt getoggled wird.
    public int requiredTriggerCount = 3;

    // Interner Z�hler f�r die Trigger-Durchl�ufe.
    private int triggerCounter = 0;

    private void OnTriggerEnter(Collider other)
    {
        // �berpr�fe, ob der kollidierende Collider den Tag "Player" besitzt.
        if (other.CompareTag("Player"))
        {
            triggerCounter++;

            // Wenn der Z�hler den festgelegten Schwellenwert erreicht oder �berschreitet...
            if (triggerCounter >= requiredTriggerCount)
            {
                // ...toggle (umschalten) das Zielobjekt.
                targetObject.SetActive(!targetObject.activeSelf);

                // Z�hler zur�cksetzen, damit der Vorgang erneut ausgef�hrt werden kann.
                triggerCounter = 0;
            }
        }
    }
}
