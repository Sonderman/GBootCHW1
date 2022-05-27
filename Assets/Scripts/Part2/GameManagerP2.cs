using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Part2
{
    public class GameManagerP2 : MonoBehaviour
    {
        public GameObject playerPrefab;
        private GameObject _activePlayer;
        public Transform spawnLocation;
        public Transform cameraLocation;
        private float currentHealth;
        public float maxHealth;
        private int _score = 0;
        public TextMeshProUGUI scoreText;
        public Image LifeImage;
        public GameObject deadPanel;
        private int _currentLevel;
        private AudioManager _audioManager;

        private void Awake()
        {
            _currentLevel = SceneManager.GetActiveScene().buildIndex;
            _audioManager = FindObjectOfType<AudioManager>();
        }

        private void Start()
        {
            currentHealth = maxHealth;
            SpawnPlayer();
            UpdateUI();
            deadPanel.SetActive(false);
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
                var newP = Vector3.Lerp(cameraLocation.position,
                    _activePlayer.transform.position + new Vector3(0, 0, -10), 2f);
                cameraLocation.position = newP;
            }
        }

        private void SpawnPlayer()
        {
            _activePlayer = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        }

        public void KillPlayer()
        {
            if (currentHealth <= 0)
            {
                KillAndOpenMenu();
            }
            else
            {
                Destroy(_activePlayer);
                currentHealth = maxHealth;
                UpdateUI();
                SpawnPlayer();
            }
        }

        private void KillAndOpenMenu()
        {
            _audioManager.PlayClip(AudioManager.AudioClips.Dead,gameObject.transform.position);
            Destroy(_activePlayer);
            var informText = deadPanel.gameObject.transform.Find("InformText").GetComponent<TextMeshProUGUI>();

            if (currentHealth <= 0)
            {
                informText.text = "You Are Dead!";
            }
            else
                informText.text = "Congratulations!";

            deadPanel.SetActive(true);
        }

        public void RestartGame()
        {
            currentHealth = maxHealth;
            _score = 0;
            SceneManager.LoadScene(3);
        }

        public void ReturnMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void LoadNextLevel()
        {
            if (_currentLevel == 3)
                KillAndOpenMenu();
            else
                SceneManager.LoadScene(_currentLevel + 1);
        }

        public void IncreaseScore(int value)
        {
            _score += value;
            UpdateUI();
        }

        public void DecreaseHealth()
        {
            currentHealth-=20f;
            UpdateUI();
            if (currentHealth <= 0)
            {
                StartCoroutine(Waiter(1, (KillAndOpenMenu)));
            }
        }

        IEnumerator Waiter(int seconds, Action doSomething)
        {
            yield return new WaitForSeconds(seconds);
            doSomething();
        }

        public void UpdateUI()
        {
            LifeImage.fillAmount = currentHealth / maxHealth;
            scoreText.text = "Score: " + _score;
        }
    }
}