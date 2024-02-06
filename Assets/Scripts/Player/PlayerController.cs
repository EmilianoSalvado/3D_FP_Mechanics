using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerModel _playerModel;
    float _xAxis, _yAxis;
    [SerializeField] FirstPersonCamera _firstPersonCamera;
    float _mouseX, _mouseY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _firstPersonCamera.SetViewAxis(_mouseX, _mouseY);

        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");

        _playerModel.SetMovementInputs(_xAxis, _yAxis);

        if (Input.GetKeyDown(KeyCode.Space))
            _playerModel.TryJump();

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            _playerModel.Sprint(Input.GetKey(KeyCode.LeftShift));
    }
}