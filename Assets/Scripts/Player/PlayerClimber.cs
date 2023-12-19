using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimber
{
    float _speed;

    public PlayerClimber(float climbingSpeed)
    {
        _speed = climbingSpeed;
    }

    public IEnumerator ClimbOver(Rigidbody rb, Func<bool> lowerFrontHit, Func<bool> bottomHit)
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = false;

        while (lowerFrontHit())
        {
            rb.AddForce(Vector3.up * _speed, ForceMode.Force);
            yield return null;
        }

        rb.velocity = Vector3.zero;
        rb.useGravity = true;
        float safeTop = .2f;

        while (!bottomHit() && safeTop > 0)
        {
            rb.AddForce(rb.transform.forward * (_speed * .5f), ForceMode.Force);
            safeTop -= Time.deltaTime;
            yield return null;
        }
    }
}