using UnityEngine;
public class PlayerMovement
{
    Transform _transform;
    Rigidbody _rb;
    Vector3 _dir;
    float _speed, _jumpForce;

    public PlayerMovement(Transform transform, Rigidbody rb, float speed, float jumpForce)
    {
        _transform = transform;
        _rb = rb;
        _speed = speed;
        _jumpForce = jumpForce;
    }

    public void SetDirection(float x, float y)
    {
        _dir = _transform.right * x + _transform.forward * y;
        if (_dir.sqrMagnitude > 1f) _dir.Normalize();
        _rb.MovePosition(_transform.position + _dir * (_speed * Time.deltaTime));
    }

    public void Move()
    {
        _rb.MovePosition(_transform.position + _dir * (_speed * Time.deltaTime));
    }

    public void Jump()
    {
        _rb.AddForce((Vector3.up + _dir * .5f) * _jumpForce, ForceMode.Impulse);
    }

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }
}