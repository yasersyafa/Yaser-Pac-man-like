using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void Retry() => SceneManager.LoadScene("GameScene");
    public void Exit() => SceneManager.LoadScene("MainMenu");

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
