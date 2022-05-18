using System;
using DG.Tweening;
using UnityEngine;

namespace Part2
{
    public class EnemyScriptP2 : MonoBehaviour
    {
        public float duration = 1f;
        public float range = 1f;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        public Ease ease;
        private Tween _tween;

        private void Start()
        {
            _startPosition = transform.position;
            _targetPosition = new Vector3(range, 0, 0) + _startPosition;
            Move();
        }

        private void FixedUpdate()
        {
            RotateEnemy();
        }

        private void RotateEnemy()
        {
            
            if (MathF.Abs(transform.position.x - _targetPosition.x)<0.05f)
            {
                //Debug.Log("G1");
                transform.rotation = Quaternion.Euler(0, range>0? 0:-180, 0);
            }
            else if (MathF.Abs((transform.position.x - _startPosition.x))<0.05f)
            {
                //Debug.Log("G2"); 
                transform.rotation = Quaternion.Euler(0, range>0? -180:0, 0);
            }
        }

        private void Move()
        {
           _tween= transform.DOMove(_targetPosition, duration).From(_startPosition).SetLoops(-1,LoopType.Yoyo).SetEase(ease);
        }

        private void OnDestroy()
        {
            _tween.Kill();
        }
    }
}