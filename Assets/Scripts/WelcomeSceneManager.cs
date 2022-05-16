using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void onStartButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

    public void onExitButtonPressed()
    {
        Application.Quit();
    }
}
