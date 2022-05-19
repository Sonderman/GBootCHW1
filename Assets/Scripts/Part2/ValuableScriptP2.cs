using UnityEngine;

namespace Part2
{
    public class ValuableScriptP2 : MonoBehaviour
    {
        [Range(100, 1000)] public float spinSpeed = 300f;
        public int defaultValue;
        private GameManagerP2 _gameManager;
        private ParticleSystemController _particleSystemController;

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManagerP2>();
            _particleSystemController = gameObject.GetComponent<ParticleSystemController>();
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                _gameManager.IncreaseScore(defaultValue);
                gameObject.GetComponent<SpriteRenderer>().enabled=false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                _particleSystemController.StartPS();
                Destroy(gameObject,_particleSystemController.GetLifeTime()+0.2f);
            }
        }
    }
}