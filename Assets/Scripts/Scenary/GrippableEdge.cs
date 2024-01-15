using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrippableEdge : MonoBehaviour, RaycastResponder
{
    [SerializeField] PlayerModel _playerModel;

    private void OnCollisionEnter(Collision collision)
    {
        _playerModel = collision.transform.GetComponent<PlayerModel>();
    }

    public void OffRaycastHit()
    {
        _playerModel.Climber.ReleaseGrip();
    }

    public void OnRaycastHit()
    {
        if (_playerModel.transform.position.y > transform.position.y) return;
        _playerModel.Climber.GrabGrip();
    }
}