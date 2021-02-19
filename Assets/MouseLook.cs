using UnityEngine;

/// <summary>
///     Mouse look component for a first person character;
///     requires character to have a separate head (camera) child object.
/// </summary>
public class MouseLook : MonoBehaviour {
    [Tooltip("Mouse X sensitivity")] [SerializeField]
    private float turnSpeed = 360;

    [Tooltip("Mouse Y sensitivity (set to negative to invert")] [SerializeField]
    private float aimSpeed = 180;

    [Tooltip("lowest valid pitch")] [Range(-90, 0)] [SerializeField]
    private float minAim = -85;

    [Tooltip("highest valid pitch")] [Range(0, 90)] [SerializeField]
    private float maxAim = 85;

    [Tooltip("child object holding the camera; its local rotation should" +
             "start at zero, local position all zero except y value")]
    [SerializeField]
    private Transform head;

    private float _yaw;
    private float _pitch;

    private void Start(){
        _yaw = transform.eulerAngles.y;
        _pitch = head.eulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void LateUpdate(){
        //get desired turn/aim movement
        float horizontal = Input.GetAxis("Mouse X")*Time.deltaTime*turnSpeed;
        float vertical = -Input.GetAxis("Mouse Y")*Time.deltaTime*aimSpeed;

        //apply to Yaw and Pitch, clamping the latter
        _yaw += horizontal;
        _pitch += vertical;
        _pitch = Mathf.Clamp(_pitch, minAim, maxAim);
        head.eulerAngles = new Vector3(_pitch, 0, 0);
        transform.eulerAngles = new Vector3(0, _yaw, 0);
    }
}