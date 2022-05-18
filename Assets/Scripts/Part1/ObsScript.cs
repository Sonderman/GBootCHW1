using UnityEngine;
public class ObsScript : MonoBehaviour
{
    private int rand;
    public bool up;
    public Transform attachGobj;
    public float offset=1.5f;
    private void Update()
    {
        float x = Mathf.Cos(Time.time);
        transform.Translate(new Vector3(0,up?x:-x,0)*Time.deltaTime);
        if (attachGobj != null)
        {
            attachGobj.position = new Vector2(attachGobj.position.x,
                transform.position.y+offset);
        }
        
    }
}
