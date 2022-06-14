using UnityEngine;

public class ObsScript : MonoBehaviour
{
    private int _rand;
    public bool up;
    public Transform attachGameObj;
    public float offset=1.5f;
    private void Update()
    {
        float x = Mathf.Cos(Time.time);
        transform.Translate(new Vector3(0,up?x:-x,0)*Time.deltaTime);
        if (attachGameObj != null)
        {
            attachGameObj.position = new Vector2(attachGameObj.position.x,
                transform.position.y+offset);
        }
        
    }
}