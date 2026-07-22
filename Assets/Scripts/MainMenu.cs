using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevelScene = "Level1";

    public void OnPlayClicked()
    {
        Time.timeScale = 1f; // por si quedó pausado de una partida anterior
        SceneManager.LoadScene(firstLevelScene);
    }

    public void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}