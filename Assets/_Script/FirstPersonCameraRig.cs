using UnityEngine;

public class FirstPersonCameraRig : MonoBehaviour
{
    [Header("회전 대상")]
    [SerializeField] private Transform playerRoot;       // 좌우 회전
    [SerializeField] private Transform cameraPitchPivot; // 상하 회전

    [Header("감도")]
    [SerializeField] private float lookSensitivity = 3f;

    [Header("상하 각도 제한")]
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private float pitch;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (playerRoot == null || cameraPitchPivot == null)
            return;

        float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

        // 좌우(Yaw): 플레이어 몸체 회전
        playerRoot.Rotate(Vector3.up * mouseX);

        // 상하(Pitch): 카메라 피벗 회전
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        cameraPitchPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}