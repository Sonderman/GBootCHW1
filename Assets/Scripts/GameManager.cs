using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager InstanceObj;
    public GameObject playerPrefab;
    private GameObject activePlayer;
    public Transform spawnLocation;
    public Transform cameraLocation;
    public static int life=3;
    private static int _score=0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public GameObject deadPanel;
    private int currentLevel;
    private void Awake()
    {
        //Debug.Log("Awake");
        currentLevel=SceneManager.GetActiveScene().buildIndex;
        if (InstanceObj == null)
        {
            InstanceObj = this;
        }
    }

    void Start()
    {
        //Debug.Log("Start");
        SpawnPlayer();
        UpdateUI();
        deadPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void LateUpdate()
    {
        if (activePlayer != null)
        {
            var newP = Vector3.Lerp(cameraLocation.position, activePlayer.transform.position+new Vector3(0,0,-10), 2f);
            cameraLocation.position = newP;
        }
    }

    private void SpawnPlayer()
    {
       activePlayer= Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
    }

    public void KillPlayer()
    {
        DecreaseLife();
        if (life == 0)
        {
            KillAndOpenMenu();
        }
        else
        {
            Destroy(activePlayer);
            SpawnPlayer();
        }
    }

    private void KillAndOpenMenu()
    {
        Destroy(activePlayer);
        TextMeshProUGUI informText =
            deadPanel.gameObject.transform.Find("InformText").GetComponent<TextMeshProUGUI>();
        if (life == 0)
        {
            informText.text = "You Are Dead!";
        }else
        informText.text ="Congratulations!";
        deadPanel.SetActive(true);
    }

    public void RestartGame()
    {
        life = 3;
        _score = 0;
        SceneManager.LoadScene(1);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        //DontDestroyOnLoad(gameObject);
        if (currentLevel == 2)
            KillAndOpenMenu();
        else
        SceneManager.LoadScene(currentLevel + 1);
    }

    public void IncreaseScore(int value)
    {
        _score += value;
        UpdateUI();
    }

    public void DecreaseLife()
    {
        life--;
        UpdateUI();
        if (life == 0)
        {
            StartCoroutine(Waiter(1,(KillAndOpenMenu )));
        }
    }
    IEnumerator Waiter(int seconds,Action doSomething)
    {
        yield return new WaitForSeconds(seconds);
        doSomething();
    }
    public void UpdateUI()
    {
        lifeText.text = "Life: " + life;
        scoreText.text = "Score: " + _score;
    }
}
