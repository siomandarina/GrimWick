using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public GameObject endGamePanel; // el Panel de UI con la pregunta y los botones
    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            Time.timeScale = 0f; // pausa el juego mientras se muestra la pregunta
            endGamePanel.SetActive(true);
        }
    }
}