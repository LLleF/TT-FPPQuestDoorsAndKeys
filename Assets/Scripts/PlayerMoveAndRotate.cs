using UnityEngine;

public class PlayerMoveAndRotate : MonoBehaviour
{
    private float sensitivityHor = 7.0f;
    private float speed = 3.0f;
    private float gravity = -9.8f;
    private CharacterController _charController;


    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMouseLookX();
        PlayerMove();
    }

    private void PlayerMove()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, gravity, deltaZ);
        movement = transform.TransformDirection(movement * speed * Time.deltaTime);
        _charController.Move(movement);
    }

    private void PlayerMouseLookX()
    {
        if (!Cursor.visible)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
    }


}