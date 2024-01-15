using System;
using System.Collections;
using UnityEngine;

public class PlayerClimber
{
    Rigidbody _rb;
    float _speed;
    PlayerModel _playerModel;

    public PlayerClimber(float climbingSpeed, Rigidbody rb, PlayerModel playerModel)
    {
        _speed = climbingSpeed;
        _rb = rb;
        _playerModel = playerModel;
    }

    public void ClimbOver()
    {
        AuxMethods.Instance.AuxStartCoroutine(ClimbOverRoutine);
    }

    IEnumerator ClimbOverRoutine()
    {
        _rb.useGravity = false;
        var t = new WaitForSeconds(Time.deltaTime);
        while (_playerModel.LowerHit)
        {
            _rb.transform.position += _rb.transform.up * _speed;
            yield return t;
        }

        float safeTop = .3f;

        while (!_playerModel.BottomHit && safeTop > 0)
        {
            _rb.transform.position += _rb.transform.forward * (_speed * .33f);
            safeTop -= Time.deltaTime;
            yield return t;
        }
        _rb.useGravity = true;
    }

    public void GrabGrip()
    {
        Debug.Log("grabbed");
        _rb.velocity = Vector3.zero;
        _rb.useGravity = false;
        _playerModel.Movement.LockMovement(Directions.ForthBack);
        _playerModel.Jump.CheckGrip();
    }

    public void ReleaseGrip()
    {
        _rb.useGravity = true;
        _playerModel.Movement.UnlockMovement();
    }
}