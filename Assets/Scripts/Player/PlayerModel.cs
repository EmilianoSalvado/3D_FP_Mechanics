using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour
{
    [Header("MOVEMENT")]
    PlayerMovement _movement;
    [SerializeField] Rigidbody _rb;
    [SerializeField] bool _canMove = true;
    [SerializeField] float _movementSpeed, _sprintSpeed, _airSpeed, _jumpForce;

    [Header("COLLISION RAYCASTING")]
    PlayerCollisionRaycaster _collisionRaycaster;
    [SerializeField] Transform _upperFrontPoint;
    [SerializeField] Transform _lowerFrontPoint;
    [SerializeField] Transform _bottomPoint;
    [SerializeField] LayerMask _detectable;
    [SerializeField] float _rayDistance;

    [Header("CLIMBING")]
    PlayerClimber _playerClimber;
    [SerializeField] float _climbingSpeed;

    private void Start()
    {
        _movement = new PlayerMovement(transform, _rb, _movementSpeed, _jumpForce);
        _collisionRaycaster = new PlayerCollisionRaycaster(_detectable, _rayDistance, _upperFrontPoint, _lowerFrontPoint, _bottomPoint);
        _playerClimber = new PlayerClimber(_climbingSpeed);
    }

    private void Update()
    {
        if (_collisionRaycaster.BottomHit()) return;

        if (_collisionRaycaster.LowerHit() && !_collisionRaycaster.UpperHit())
        { StartCoroutine(_playerClimber.ClimbOver(_rb, _collisionRaycaster.LowerHit, _collisionRaycaster.BottomHit)); }
    }

    private void FixedUpdate()
    {
        _movement.Move();
    }

    public void SetMovementInputs(float x, float y)
    {
        if (!_canMove || (x == 0 && y == 0)) return;
        _movement.SetDirection(x, y);
    }

    public void Jump()
    {
        if (!_collisionRaycaster.BottomHit()) return;
        _movement.Jump();
        _movement.ChangeSpeed(_airSpeed);
    }

    public void Sprint(bool b)
    {
        if (b) _movement.ChangeSpeed(_sprintSpeed);
        else _movement.ChangeSpeed(_movementSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_collisionRaycaster.BottomHit())
        { _movement.ChangeSpeed(_movementSpeed); _canMove = true; }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_upperFrontPoint.position, _upperFrontPoint.position + _upperFrontPoint.forward * _rayDistance);
        Gizmos.DrawLine(_lowerFrontPoint.position, _lowerFrontPoint.position + _lowerFrontPoint.forward * _rayDistance);
        Gizmos.DrawLine(_bottomPoint.position, _bottomPoint.position + _bottomPoint.forward * _rayDistance);
    }
}