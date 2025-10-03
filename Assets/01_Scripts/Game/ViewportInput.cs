using UnityEngine;
using UnityEngine.InputSystem;

public class ViewportInput : MonoBehaviour
{
    public CameraController[] cameraControllers;

    private float horBorder;
    private float verBorder;

    void Start()
    {
        horBorder = Screen.width / 2;
        verBorder = Screen.height / 2;
    }

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        bool isRight = mousePos.x > horBorder;
        bool isTop = mousePos.y > verBorder;

        int activeIndex = -1;
        if (!isRight && isTop) activeIndex = 0;
        if (isRight && isTop) activeIndex = 1;
        if (!isRight && !isTop) activeIndex = 2;
        if (isRight && !isTop) activeIndex = 3;

        for (int i = 0; i < cameraControllers.Length; i++)
        {
            if (cameraControllers[i] != null)
            {
                cameraControllers[i].enabled = (i == activeIndex);
            }
        }
    }
}
