using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrippableEdge : MonoBehaviour, RaycastResponder
{
    [SerializeField] PlayerModel _playerModel;
    [SerializeField] FirstPersonCamera _firstPersonCamera;

    private void OnCollisionEnter(Collision collision)
    {
        _playerModel = collision.transform.GetComponent<PlayerModel>();
        _firstPersonCamera = collision.transform.GetComponentInChildren<FirstPersonCamera>();
    }

    public void OffRaycastHit()
    {
        _playerModel.Climber.ReleaseGrip();
        _firstPersonCamera.Align(true);
    }

    public void OnRaycastHit()
    {
        if (_playerModel.transform.position.y > transform.position.y) return;
        _playerModel.Climber.GrabGrip();
        _playerModel.transform.forward = -transform.forward;
        _firstPersonCamera.Align(false);
    }
}