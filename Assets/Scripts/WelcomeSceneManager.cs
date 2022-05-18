using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeSceneManager : MonoBehaviour
{
    public void onPart1ButtonPressed()
    {
        SceneManager.LoadScene(1);
    }
    public void onPart2ButtonPressed()
    {
        SceneManager.LoadScene(3);
    }

    public void onExitButtonPressed()
    {
        Application.Quit();
    }
}
