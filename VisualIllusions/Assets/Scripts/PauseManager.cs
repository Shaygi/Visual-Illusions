using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Pause Men� UI")]
    [Tooltip("Das Panel, das das Pause-Men� enth�lt.")]
    public GameObject pauseMenuUI;

    [Header("Szenen Einstellungen")]
    [Tooltip("Der Name der Men�-Szene, wie in den Build Settings eingetragen.")]
    public string menuSceneName = "Menu";

    private bool isPaused = false;

    void Update()
    {
        // Dr�cke Escape, um zwischen Pausieren und Fortsetzen zu wechseln
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // Zeige das Pause-Men�
        Time.timeScale = 0f;          // Pausiere das Spiel
        isPaused = true;
        // Cursor freischalten, damit der Spieler die Buttons bedienen kann
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false); // Verberge das Pause-Men�
        Time.timeScale = 1f;          // Setze das Spiel fort
        isPaused = false;
        // Sperre den Cursor wieder, damit die First-Person-Steuerung funktioniert
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Diese Methode wird vom Menu-Button aufgerufen
    public void LoadMenu()
    {
        Time.timeScale = 1f;  // Stelle sicher, dass das Spiel fortgesetzt wird
        SceneManager.LoadScene(menuSceneName);
    }

    // Diese Methode wird vom Quit-Button aufgerufen
    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        // Falls du im Editor spielst, beende auch den Play-Modus
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
