using UnityEngine;

namespace Part2
{
    public class ParticleSystemController : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private void Start()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
        }

        public void StartPS()
        {
            _particleSystem.Play();
        }

        public void StopPS()
        {
            _particleSystem.Stop();
        }

        public bool IsPlaying()
        {
            return _particleSystem.isPlaying;
        }

        public float GetDuration()
        {
            return _particleSystem.main.duration;
        }

        public float GetLifeTime()
        {
            return _particleSystem.main.startLifetime.constant;
        }
    }
}