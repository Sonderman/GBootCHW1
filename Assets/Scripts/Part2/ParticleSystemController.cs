using UnityEngine;

namespace Part2
{
    public class ParticleSystemController : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = gameObject.GetComponent<ParticleSystem>();
        }

        public void StartPS()
        {
            //Debug.Log("PS playing");
            _particleSystem.Play();
        }

        public void StopPS()
        {
            //Debug.Log("PS Stopped");
            _particleSystem.Stop();
        }

        public bool IsPlaying()
        {
            return _particleSystem.isPlaying;
        }
    }
}