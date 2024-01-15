using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump
{
    Rigidbody _rb;
    float _jumpForce;
    Func<bool> _checker;
    PlayerModel _playerModel;

    public PlayerJump(Rigidbody rb, float jumpForce, PlayerModel playerModel)
    {
        _rb = rb;
        _jumpForce = jumpForce;
        _playerModel = playerModel;
        _checker = () => _playerModel.BottomHit;
    }

    public void Jump()
    {
        if (_playerModel.LowerHit && !_playerModel.UpperHit)
        {
            _playerModel.Climber.ClimbOver();
            return;
        }

        if (_checker())
        {
            _playerModel.Movement.UnlockMovement();
            _rb.AddForce((Vector3.up) * _jumpForce, ForceMode.Impulse);
            CheckFloor();
        }
    }

    public void CheckFloor()
    {
        _checker = () => _playerModel.BottomHit;
        _rb.useGravity = true;
    }

    public void CheckGrip()
    {
        _checker = () => true;
    }
}
