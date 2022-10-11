using UnityEngine;

public class SpinView : MonoBehaviour
{
    private int direction = 1;

    public float speed = 0.2f;
    public float limit = 35.0f;
    
    void Update()
    {
        transform.Rotate(Vector3.up, direction * speed);

        float temp = transform.localEulerAngles.y;
        temp = temp > 180.0f ? temp - 360.0f : temp;
        if (temp < -limit)
        {
            direction = 1;
        }
        else if(temp > limit)
        {
            direction = -1;
        }
    }
}
