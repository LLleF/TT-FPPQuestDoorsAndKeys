using UnityEngine;

public class CameraMouseLookY : MonoBehaviour
{
    private float sensitivityVert = 7.0f;
    private float minimumVert = -65.0f;
    private float maximumVert = 45.0f;
    private float _rotationX = 0;

    void Update()
    {
        MouseLook();
    }

    private void MouseLook()
    {
        if (!Cursor.visible)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }      
    }
}
