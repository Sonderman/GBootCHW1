using DG.Tweening;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float duration = 1f;
    public float range = 1f;
    private Vector3 _position;
    private Vector3 _position2;
    public Ease ease;
    private void Start()
    {
        _position = transform.position;
        _position2 = new Vector3(range, 0, 0) + _position;
        //Move();
    }

    private void Update()
    {
        float x = Mathf.Cos(Time.timeSinceLevelLoad*duration)*range;
       transform.Translate(new Vector3(x,0,0) * Time.deltaTime);
       
    }

    private void Move()
    {
        transform.DOMove(_position2, duration).SetEase(ease).OnComplete(() =>
        {
            (_position, _position2) = (_position2, _position);
            Move();
        });
        
    }
}
