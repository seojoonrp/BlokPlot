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
        // 1. 회전 (Rotate) - 마우스 오른쪽 버튼
        if (Input.GetMouseButton(1))
        {
            // 마우스 이동량에 따라 yaw, pitch 값 변경
            yaw += rotateSpeed * Input.GetAxis("Mouse X") * Time.deltaTime;
            pitch -= rotateSpeed * Input.GetAxis("Mouse Y") * Time.deltaTime;

            // pitch(상하 각도)가 90도를 넘지 않도록 제한 (카메라 뒤집힘 방지)
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            // 계산된 값으로 카메라 회전 적용
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        // 2. 평행 이동 (Pan) - 마우스 가운데 버튼
        if (Input.GetMouseButton(2))
        {
            // 마우스 이동량만큼 카메라를 상하좌우로 이동
            float moveX = -Input.GetAxis("Mouse X") * panSpeed * Time.deltaTime;
            float moveY = -Input.GetAxis("Mouse Y") * panSpeed * Time.deltaTime;

            // 카메라의 로컬 좌표계를 기준으로 이동 벡터 계산
            Vector3 move = transform.right * moveX + transform.up * moveY;
            
            // 월드 좌표계를 기준으로 카메라 위치 변경
            transform.Translate(move, Space.World);
        }

        // 3. 확대/축소 (Zoom) - 마우스 휠 스크롤
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            // 카메라가 바라보는 방향(앞/뒤)으로 이동
            Vector3 move = scroll * zoomSpeed * transform.forward;
            transform.position += move;
        }
    }
}
