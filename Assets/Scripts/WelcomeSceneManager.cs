using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeSceneManager : MonoBehaviour
{
    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene(1);
    }
    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
