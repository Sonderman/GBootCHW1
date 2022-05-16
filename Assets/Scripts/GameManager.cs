using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
    private static int _gold=0;
    public TextMeshProUGUI goldText;
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

    void LateUpdate()
    {
        if (activePlayer != null)
        {
            var newP = Vector3.Lerp(cameraLocation.position, activePlayer.transform.position+new Vector3(0,0,-10), 2f);
            cameraLocation.position = newP;
        }
    }
    
    public void SpawnPlayer()
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
        _gold = 0;
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

    public void IncreaseGold()
    {
        _gold += 10;
        UpdateUI();
    }

    public void DecreaseLife()
    {
        life--;
        UpdateUI();
        if (life == 0)
        {
            StartCoroutine(waiter(1,(KillAndOpenMenu )));
        }
    }
    IEnumerator waiter(int seconds,Action doSomething)
    {
        yield return new WaitForSeconds(seconds);
        doSomething();
    }
    public void UpdateUI()
    {
        lifeText.text = "Life: " + life;
        goldText.text = "Gold: " + _gold;
    }
}
