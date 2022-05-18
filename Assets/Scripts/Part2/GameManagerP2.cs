using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Part2
{
    public class GameManagerP2 : MonoBehaviour
    {
        private static GameManagerP2 _instanceObj;
        public GameObject playerPrefab;
        private GameObject _activePlayer;
        public Transform spawnLocation;
        public Transform cameraLocation;
        public int Life;
        private int _score = 0;
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI lifeText;
        public GameObject deadPanel;
        private int _currentLevel;

        private void Awake()
        {
            _currentLevel = SceneManager.GetActiveScene().buildIndex;
            if (_instanceObj == null)
            {
                _instanceObj = this;
            }
        }

        private void Start()
        {
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
            DecreaseLife();
            if (Life == 0)
            {
                KillAndOpenMenu();
            }
            else
            {
                Destroy(_activePlayer);
                SpawnPlayer();
            }
        }

        private void KillAndOpenMenu()
        {
            Destroy(_activePlayer);
            var informText = deadPanel.gameObject.transform.Find("InformText").GetComponent<TextMeshProUGUI>();

            if (Life == 0)
            {
                informText.text = "You Are Dead!";
            }
            else
                informText.text = "Congratulations!";

            deadPanel.SetActive(true);
        }

        public void RestartGame()
        {
            Life = 3;
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

        public void DecreaseLife()
        {
            Life--;
            UpdateUI();
            if (Life == 0)
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
            lifeText.text = "Life: " + Life;
            scoreText.text = "Score: " + _score;
        }
    }
}