using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Speed")]
    public float rotateSpeed = 100.0f;
    public float panSpeed = 30.0f;
    public float zoomSpeed = 15.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += rotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            pitch -= rotateSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        if (Input.GetMouseButton(2))
        {
            float moveX = -Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            float moveY = -Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

            Vector3 move = transform.right * moveX + transform.up * moveY;
            
            transform.Translate(move, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            Vector3 move = scroll * zoomSpeed * transform.forward;
            transform.position += move;
        }
    }
}
