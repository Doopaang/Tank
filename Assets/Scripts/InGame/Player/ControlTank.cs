using UnityEngine;
using UnityEngine.Networking;

public class ControlTank : NetworkBehaviour
{
    [SerializeField]
    private GameObject view;

    private Rigidbody rigid;
    private Animator animator;
    [SerializeField]
    private Transform top;
    [SerializeField]
    private Transform topS;

    public float center = 1.0f;
    public float moveSpeed = 150.0f;
    public float rotateSpeed = 100.0f;
    public float topSpeed = 0.6f;
    public float topLimit = 15.0f;
    private float topDefault;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }

        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        Vector3 centerMass = rigid.centerOfMass;
        centerMass.y -= center;
        rigid.centerOfMass = centerMass;

        topDefault = topS.localEulerAngles.y;

        SetView();
    }

    void Update()
    {
        if (!GameManager.Instance.isLoaded ||
            UIManager.Instance.menu.activeSelf)
        {
            return;
        }

        Move();
        Rotate();
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontal * rotateSpeed);
        //rigid.AddRelativeForce(Vector3.forward * vertical * moveSpeed);
        //rigid.AddRelativeTorque(Vector3.up * horizontal * rotateSpeed);

        animator.SetInteger("Move", (int)vertical);
        animator.SetInteger("Turn", (int)horizontal);
    }

    private void Rotate()
    {
        float vertical = Input.GetAxis("Top X");
        float horizontal = Input.GetAxis("Top Y");

        top.Rotate(transform.up * vertical * topSpeed, Space.World);

        Vector3 vec = topS.localRotation.eulerAngles;
        vec.y = Mathf.Clamp(vec.y - horizontal * topSpeed, topDefault - topLimit, topDefault + topLimit);
        topS.localRotation = Quaternion.Euler(vec);
    }

    private void SetView()
    {
        GameObject obj = Instantiate(view);
        obj.GetComponent<ControlView>().top = top;
    }
}
