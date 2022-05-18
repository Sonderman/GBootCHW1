using UnityEngine;

public class ValuableScript : MonoBehaviour
{
    [Range(100, 1000)]
    public float spinSpeed = 300f;
    public int defaultValue;
    private GameManager _gameManager;
    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
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
            Destroy(gameObject);
        }
    }
}
