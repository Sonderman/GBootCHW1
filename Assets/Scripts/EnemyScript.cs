using System;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed = 1f;
    public float range = 1f;
    private void Update()
    {
        float x = Mathf.Cos(Time.timeSinceLevelLoad*speed)*range;
        transform.Translate(new Vector3(x,0,0) * Time.deltaTime);
    }
}
