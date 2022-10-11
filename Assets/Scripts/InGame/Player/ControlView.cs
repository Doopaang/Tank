using UnityEngine;

public class ControlView : MonoBehaviour
{
    public Transform cameraPos;
    [HideInInspector]
    public Transform top;

    private float defaultRot;
    public float limit = 35.0f;

    void Start()
    {
        defaultRot = transform.localEulerAngles.x;

        Camera.main.SendMessage("SetTarget", cameraPos);
    }

    void Update()
    {
        if (!GameManager.Instance.isLoaded)
        {
            return;
        }

        if (GameManager.Instance.isDead)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = top.position;

        if (UIManager.Instance.menu.activeSelf)
        {
            return;
        }

        RotateView();
    }

    private void RotateView()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up * x * OptionManager.Instance.option.Sensitivity / 5.0f, Space.World);

        Vector3 vec = cameraPos.localRotation.eulerAngles;
        vec.x = vec.x > 180.0f ? vec.x - 360.0f : vec.x;
        vec.x = Mathf.Clamp(vec.x - y * OptionManager.Instance.option.Sensitivity / 5.0f, defaultRot - limit, defaultRot + limit);
        cameraPos.localRotation = Quaternion.Euler(vec);
    }
}
