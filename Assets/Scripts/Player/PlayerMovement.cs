using System;
using UnityEngine;
public class PlayerMovement
{
    Transform _transform;
    Rigidbody _rb;
    Vector3 _dir;
    float _speed;
    Action<float, float> _setDir;

    public PlayerMovement(Transform transform, Rigidbody rb, float speed)
    {
        _transform = transform;
        _rb = rb;
        _speed = speed;
        _setDir = (x, y) =>
        {
            _dir = _transform.right * x + _transform.forward * y;
            if (_dir.sqrMagnitude > 1f) _dir.Normalize();
            _rb.MovePosition(_transform.position + _dir * (_speed * Time.deltaTime));
        };
    }

    public void SetDirection(float x, float y)
    {
        _setDir(x,y);
    }

    public void Move()
    {
        _rb.MovePosition(_transform.position + _dir * (_speed * Time.deltaTime));
    }

    public void LockMovement(Directions dirToBlock)
    {
        switch (dirToBlock)
        {
            case Directions.RightLeft:
                _setDir = (x, y) =>
                {
                    _dir = _transform.right * 0f + _transform.forward * y;
                    _rb.MovePosition(_transform.position + _dir * (_speed * Time.deltaTime));
                };
                break;
            case Directions.ForthBack:
                _setDir = (x, y) =>
                {
                    _dir = _transform.right * x + _transform.forward * 0f;
                    _rb.MovePosition(_transform.position + _dir * (_speed * Time.deltaTime));
                };
                break;
        }
    }

    public void UnlockMovement()
    {
        _setDir = (x, y) =>
        {
            _dir = _transform.right * x + _transform.forward * y;
            if (_dir.sqrMagnitude > 1f) _dir.Normalize();
            _rb.MovePosition(_transform.position + _dir * (_speed * Time.deltaTime));
        };
    }

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }
}