using UnityEngine;

public class CamControl : MonoBehaviour
{
    [HideInInspector]
    public Transform target = null;

    public float dampTrace = 40.0f;

    public float moveSpeed = 50.0f;

    private bool isDead = false;

    void Update()
    {
        if (isDead)
        {
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");

            transform.Translate((Vector3.forward * vertical + Vector3.right * horizontal) * moveSpeed * Time.deltaTime);

            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");

            transform.Rotate(Vector3.up * x * OptionManager.Instance.option.Sensitivity / 5.0f, Space.World);

            Vector3 vec = transform.localRotation.eulerAngles;
            vec.x = vec.x > 180.0f ? vec.x - 360.0f : vec.x;
            vec.x = Mathf.Clamp(vec.x - y * OptionManager.Instance.option.Sensitivity / 5.0f, -90, 90);
            transform.localRotation = Quaternion.Euler(vec);
        }
        else if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * dampTrace);
            transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * dampTrace);
        }
    }

    private void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Dead()
    {
        isDead = true;
    }
}
