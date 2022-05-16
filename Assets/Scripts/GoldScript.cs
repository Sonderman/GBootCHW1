using UnityEngine;

public class GoldScript : MonoBehaviour
{
    public float spinSpeed = 300f;
    private void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
    }
}
