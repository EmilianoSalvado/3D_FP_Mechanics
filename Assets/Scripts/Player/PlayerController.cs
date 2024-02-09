using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerModel _playerModel;
    float _xAxis, _yAxis;
    [SerializeField] FirstPersonCamera _firstPersonCamera;
    float _mouseX, _mouseY;
    [SerializeField] WeaponAnimationsController _weaponAnimationsController;

    public static PlayerController Instance;
    Action _update;
    bool _cam;
    bool _mov;
    bool _com;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        EnableCameraControls(true);
        EnableMovementControls(true);
    }

    void Update()
    {
        _update();
    }

    public void EnableCameraControls(bool b)
    {
        if (b && !_cam)
        { _update += Camera; _cam = true; }
        else if (!b && _cam)
        { _update -= Camera; _cam = false; }
    }

    public void EnableMovementControls(bool b)
    {
        if (b && !_mov)
        { _update += Movement; _mov = true; }
        else if (!b && _mov)
        { _update -= Movement; _mov = false; }
    }

    public void EnableCombatControls(bool b)
    {
        if (b && !_com)
        { _update += Combat; _com = true; }
        else if (!b && _com)
        { _update -= Combat; _com = false; }
    }

    void Camera()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        _firstPersonCamera.SetViewAxis(_mouseX, _mouseY);
    }

    void Movement()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _yAxis = Input.GetAxis("Vertical");

        _playerModel.SetMovementInputs(_xAxis, _yAxis);

        if (Input.GetKeyDown(KeyCode.Space))
            _playerModel.TryJump();
    }

    void Combat()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerModel.WeaponsManager.ShowWeapon(Weapons.Shield, !_playerModel.WeaponsManager.ShieldActive);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.LeftShift))
            _playerModel.Sprint(Input.GetKey(KeyCode.LeftShift));

        if (Input.GetKeyDown(KeyCode.Mouse0))
            _weaponAnimationsController.Attack();

        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyUp(KeyCode.Mouse1))
        {
            bool b = Input.GetKey(KeyCode.Mouse1);
            _weaponAnimationsController.Defense(b);
            if (b && _playerModel.WeaponsManager.ShieldActive)
                _weaponAnimationsController.ChangeAttack(Weapons.Shield);
            else
                _weaponAnimationsController.ChangeAttack(Weapons.Sword);
        }
    }
}