using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    public string level1SceneName = "Level1"; // 🔥 pon aquí el nombre exacto de tu escena de Nivel1

    public void OnYesClicked()
    {
        Time.timeScale = 1f; // reactiva el tiempo antes de cambiar de escena
        SceneManager.LoadScene(level1SceneName);
    }

    public void OnNoClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // para que funcione al probar en el Editor
#else
        Application.Quit();
#endif
    }
}