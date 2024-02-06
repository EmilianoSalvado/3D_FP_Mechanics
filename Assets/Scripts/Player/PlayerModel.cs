using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour
{
    [Header("MOVEMENT")]
    [SerializeField] Rigidbody _rb;
    [SerializeField] bool _canMove = true;
    [SerializeField] float _movementSpeed, _sprintSpeed, _airSpeed, _jumpForce;
    public PlayerMovement Movement { get; private set; }
    public PlayerJump Jump { get; private set; }

    [Header("COLLISION RAYCASTING")]
    PlayerCollisionRaycaster _collisionRaycaster;
    [SerializeField] Transform _upperFrontPoint;
    [SerializeField] Transform _lowerFrontPoint;
    [SerializeField] Transform _bottomPoint;
    [SerializeField] LayerMask _upperMask;
    [SerializeField] LayerMask _lowerMask;
    [SerializeField] LayerMask _bottomMask;
    [SerializeField] float _rayDistance;
    bool _bottomHit;
    public bool BottomHit {  get { return _bottomHit; } }
    bool _upperHit;
    public bool UpperHit { get { return _upperHit; } }
    bool _lowerHit;
    public bool LowerHit { get { return _lowerHit; } }

    [Header("CLIMBING")]
    [SerializeField] float _climbingSpeed;
    public PlayerClimber Climber { get; private set; }

    [SerializeField] WeaponsManager _weaponsManager;
    public WeaponsManager WeaponsManager { get { return _weaponsManager; } }

    private void Start()
    {
        Movement = new PlayerMovement(transform, _rb, _movementSpeed);
        _collisionRaycaster = new PlayerCollisionRaycaster(_upperMask, _lowerMask, _bottomMask, _rayDistance, _upperFrontPoint, _lowerFrontPoint, _bottomPoint);
        Jump = new PlayerJump(_rb, _jumpForce, this);
        Climber = new PlayerClimber(_climbingSpeed, _rb, this);
        
    }

    private void Update()
    {
        _bottomHit = _collisionRaycaster.BottomHit();
        _lowerHit = _collisionRaycaster.LowerHit();
        _upperHit = _collisionRaycaster.UpperHit();
    }

    private void FixedUpdate()
    {
        Movement.Move();
    }

    public void SetMovementInputs(float x, float y)
    {
        if (!_canMove || (x == 0 && y == 0)) return;
        Movement.SetDirection(x, y);
    }

    public void TryJump()
    {
        Jump.Jump();
    }

    public void Sprint(bool b)
    {
        if (b) Movement.ChangeSpeed(_sprintSpeed);
        else Movement.ChangeSpeed(_movementSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_collisionRaycaster.BottomHit())
        { Movement.ChangeSpeed(_movementSpeed); _canMove = true; }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_upperFrontPoint.position, _upperFrontPoint.position + _upperFrontPoint.forward * _rayDistance);
        Gizmos.DrawLine(_lowerFrontPoint.position, _lowerFrontPoint.position + _lowerFrontPoint.forward * _rayDistance);
        Gizmos.DrawLine(_bottomPoint.position, _bottomPoint.position + _bottomPoint.forward * _rayDistance);
    }
}