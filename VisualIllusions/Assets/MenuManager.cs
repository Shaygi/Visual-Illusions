using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Falls du UI-Buttons verwendest

public class MenuManager : MonoBehaviour
{
    [Header("Menü-Gruppen")]
    public GameObject menu1;          // Erster Satz von Buttons
    public GameObject menu2;          // Zweiter Satz von Buttons (anfangs deaktiviert)

    [Header("Kamera-Einstellungen")]
    public Camera mainCamera;         // Deine Hauptkamera
    public Transform cameraTarget;    // Zielposition/-rotation für den Vorwärtsschwenk
    public float cameraMoveDuration = 1f; // Dauer des Kameraschwenks

    // Originalposition und -rotation der Kamera
    private Vector3 originalPos;
    private Quaternion originalRot;

    private void Start()
    {
        // Ursprüngliche Kameraeinstellungen speichern
        originalPos = mainCamera.transform.position;
        originalRot = mainCamera.transform.rotation;

        // Menu2 initial deaktivieren
        menu2.SetActive(false);
    }

    // Diese Methode wird vom OnClick-Event des ersten Buttons in Menu1 aufgerufen
    public void OnFirstButtonClicked()
    {
        // Blende Menu1 aus
        menu1.SetActive(false);
        // Schwenke die Kamera zum Ziel (cameraTarget)
        StartCoroutine(MoveCamera(cameraTarget.position, cameraTarget.rotation, () =>
        {
            // Sobald der Schwenk abgeschlossen ist, wird Menu2 eingeblendet
            menu2.SetActive(true);
        }));
    }

    // Diese Methode wird vom OnClick-Event des Zurück-Buttons in Menu2 aufgerufen
    public void OnBackButtonClicked()
    {
        // Blende Menu2 aus
        menu2.SetActive(false);
        // Schwenke die Kamera zurück zur ursprünglichen Position
        StartCoroutine(MoveCamera(originalPos, originalRot, () =>
        {
            // Nach dem Rückschwenk wird Menu1 wieder eingeblendet
            menu1.SetActive(true);
        }));
    }

    // Coroutine für einen sanften Kameraschwenk von der aktuellen Position zu einer Zielposition/-rotation
    IEnumerator MoveCamera(Vector3 targetPos, Quaternion targetRot, System.Action onComplete)
    {
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < cameraMoveDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / cameraMoveDuration);
            mainCamera.transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsedTime / cameraMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Stelle sicher, dass die Kamera exakt am Ziel ist
        mainCamera.transform.position = targetPos;
        mainCamera.transform.rotation = targetRot;

        // Führe den Callback aus, sobald der Schwenk abgeschlossen ist
        onComplete?.Invoke();
    }
}
