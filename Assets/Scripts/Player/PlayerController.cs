using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerModel _playerModel;
    float _xAxis, _yAxis;
    [SerializeField] FirstPersonCamera _firstPersonCamera;
    float _mouseX, _mouseY;
    [SerializeField] WeaponAnimationsController _weaponAnimationsController;

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

        if (!_playerModel.WeaponsManager.HasShield && !_playerModel.WeaponsManager.HasSword) return;

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