using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoSingleton<GameManager>
{
    public GameObject playerPrefab;

    public UnityAction<int> OnCollectCoin;

    private TextMeshProUGUI _scoreText;
    private Image _lifeImage;
    private GameObject _deadPanel;
    private GameObject _activePlayer;
    private Transform _spawnLocation;
    private Transform _cameraLocation;
    private AudioManager _audioManager;

    private void Awake()
    {
        if (Instance != this) Destroy(gameObject);
        _deadPanel = GameObject.Find("DeadPanel");
        _lifeImage = GameObject.Find("LifeImage").GetComponent<Image>();
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _spawnLocation = GameObject.Find("SpawnPoint").transform;
        _cameraLocation = GameObject.Find("Main Camera").transform;
        GameData.Instance.currentLevel = SceneManager.GetActiveScene().buildIndex;
        _audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        GameData.Instance.currentHealth = GameData.Instance.maxHealth;
        SpawnPlayer();
        UpdateUI();
        _deadPanel.SetActive(false);
        EventManager.Instance.OnHealthChanged += DecreaseHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void LateUpdate()
    {
        if (_activePlayer != null)
        {
            var newP = Vector3.Lerp(_cameraLocation.position,
                _activePlayer.transform.position + new Vector3(0, 0, -10), 2f);
            _cameraLocation.position = newP;
        }
    }

    private void SpawnPlayer()
    {
        _activePlayer = Instantiate(playerPrefab, _spawnLocation.position, Quaternion.identity);
    }

    public void KillPlayer()
    {
        if (GameData.Instance.currentHealth <= 0)
        {
            KillAndOpenMenu();
        }
        else
        {
            Destroy(_activePlayer);
            GameData.Instance.currentHealth = GameData.Instance.maxHealth;
            UpdateUI();
            SpawnPlayer();
        }
    }

    private void KillAndOpenMenu()
    {
        Destroy(_activePlayer);
        var informText = _deadPanel.gameObject.transform.Find("InformText").GetComponent<TextMeshProUGUI>();

        if (GameData.Instance.currentHealth <= 0)
        {
            informText.text = "You Are Dead!";
        }
        else
            informText.text = "Congratulations!";

        _deadPanel.SetActive(true);
    }

    public void RestartGame()
    {
        GameData.Instance.currentHealth = GameData.Instance.maxHealth;
        GameData.Instance.score = 0;
        _deadPanel.SetActive(false);
        SceneManager.LoadScene(3);
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        if (GameData.Instance.currentLevel == 4)
            KillAndOpenMenu();
        else
            SceneManager.LoadScene(GameData.Instance.currentLevel + 1);
    }

    public void IncreaseScore(int value)
    {
        GameData.Instance.score += value;
        UpdateUI();
    }

    public void DecreaseHealth()
    {
        GameData.Instance.currentHealth -= 20f;
        UpdateUI();
        if (GameData.Instance.currentHealth <= 0)
        {
            _audioManager.StopAudio();
            _audioManager.PlayClip(AudioManager.AudioClips.Dead, gameObject.transform.position);
            StartCoroutine(Waiter(1, (KillAndOpenMenu)));
        }
    }

    IEnumerator Waiter(int seconds, Action doSomething)
    {
        yield return new WaitForSeconds(seconds);
        doSomething();
    }

    private void UpdateUI()
    {
        _lifeImage.fillAmount = GameData.Instance.currentHealth / GameData.Instance.maxHealth;
        _scoreText.text = "Score: " + GameData.Instance.score;
    }
}