using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerModel _playerModel;
    float _xAxis, _yAxis;
    [SerializeField] FirstPersonCamera _firstPersonCamera;
    float _mouseX, _mouseY;

    void Update()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _firstPersonCamera.Rotate(_mouseX, _mouseY);

        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");

        _playerModel.SetMovementInputs(_xAxis, _yAxis);

        if (Input.GetKeyDown(KeyCode.Space))
            _playerModel.Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            _playerModel.Sprint(Input.GetKey(KeyCode.LeftShift));
    }
}
