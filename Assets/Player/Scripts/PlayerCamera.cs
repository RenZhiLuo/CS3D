using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Character view

    [SerializeField] private Vector2 viewSpeed;
    [SerializeField] private Transform followDiractionPoint;
    private Vector2 viewAngle = Vector2.zero;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        viewAngle.x += Input.GetAxis("Mouse X") * viewSpeed.x;
        viewAngle.y += -Input.GetAxis("Mouse Y") * viewSpeed.y;
    }
    private void FixedUpdate()
    {
        CameraAim();
    }
    private void CameraAim()
    {
        if (viewAngle.x > 360) viewAngle.x -= 360;
        else if (viewAngle.x < -360) viewAngle.x += 360;
        viewAngle.y = Mathf.Clamp(viewAngle.y, -80, 80);
        Vector3 rotation = new Vector3(viewAngle.y, viewAngle.x * viewSpeed.x, 0);
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
        followDiractionPoint.rotation = Quaternion.Euler(rotation.x, followDiractionPoint.eulerAngles.y, 0);
    }

}
