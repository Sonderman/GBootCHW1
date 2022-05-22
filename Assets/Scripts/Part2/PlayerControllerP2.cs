using UnityEngine;

namespace Part2
{
    public class PlayerControllerP2 : MonoBehaviour
    {
        public float playerSpeed = 1.0f;
        public float jumpForce;
        private Rigidbody2D _rigidbody2D;
        private Animator _animator;
        private GameManagerP2 _gameManager;
        private float _horizontalInput;
        private bool _isSpacePressed;
        private bool _isGrounded;
        private ParticleSystemController _particleSystemController;
        public GameObject DeadPS;

        private void Start()
        {
            _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
            _animator = gameObject.GetComponent<Animator>();
            _gameManager = FindObjectOfType<GameManagerP2>();
            _particleSystemController = gameObject.GetComponentInChildren<ParticleSystemController>();
        }

        private void Update()
        {
            CaptureInputs();
        }

        private void FixedUpdate()
        {
            MoveHorizontal();
        }

        private void CaptureInputs()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _isSpacePressed = Input.GetKeyDown(KeyCode.Space);
            if (_isSpacePressed) Jump();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
            }

            if (collision.gameObject.CompareTag("Enemy"))
            {
                _isGrounded = false;
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                _particleSystemController.StopPS();
                Instantiate(DeadPS, gameObject.transform.position + new Vector3(0.25f, 0, -2), Quaternion.identity);
                _gameManager.DecreaseHealth();
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("DeadZone"))
            {
                _gameManager.KillPlayer();
            }

            if (col.gameObject.CompareTag("Finish"))
            {
                _gameManager.LoadNextLevel();
            }
        }


        private void MoveHorizontal()
        {
            if (_horizontalInput != 0f)
            {
                _rigidbody2D.AddForce(Vector2.right * (_horizontalInput * playerSpeed), ForceMode2D.Force);
                if (!_particleSystemController.IsPlaying() && _isGrounded)
                    _particleSystemController.StartPS();
            }
            else
            {
                if (_particleSystemController.IsPlaying())
                    _particleSystemController.StopPS();
            }

            if (_animator != null)
            {
                if (_horizontalInput == 0)
                {
                    _animator.enabled = false;
                }
                else
                {
                    _animator.enabled = true;
                    RotatePlayer();
                }
            }
            else if (_horizontalInput != 0f) RotatePlayer();
        }

        private void RotatePlayer()
        {
            if (_horizontalInput < 0f) transform.rotation = Quaternion.Euler(0, -180, 0);
            else transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        private void Jump()
        {
            if (_isSpacePressed && _isGrounded)
            {
                _isGrounded = false;
                _particleSystemController.StopPS();
                _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}