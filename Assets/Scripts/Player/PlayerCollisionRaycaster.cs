using UnityEngine;

public class PlayerCollisionRaycaster
{
    LayerMask _detectable;
    float _rayDistance;

    Transform _upperFrontPoint;
    Transform _lowerFrontPoint;
    Transform _bottomPoint;

    RaycastHit _hit;
    bool _responderHit;
    RaycastResponder _rayResponder;

    public PlayerCollisionRaycaster(LayerMask detectable, float rayDistance, Transform upperFrontPoint, Transform lowerFrontPoint, Transform bottomPoint)
    {
        _detectable = detectable;
        _rayDistance = rayDistance;
        _upperFrontPoint = upperFrontPoint;
        _lowerFrontPoint = lowerFrontPoint;
        _bottomPoint = bottomPoint;
    }

    public bool UpperHit() { return RayDoesHit(_upperFrontPoint); }
    public bool LowerHit() { return RayDoesHit(_lowerFrontPoint); }
    public bool BottomHit() { return RayDoesHit(_bottomPoint); }

    bool RayDoesHit(Transform point)
    {
        var hasHit = Physics.Raycast(point.position, point.forward, out _hit, _rayDistance, _detectable);

        if (!hasHit) { return hasHit; }

        if (_hit.collider.GetComponent<RaycastResponder>() != null && !_responderHit)
        { _rayResponder = _hit.collider.GetComponent<RaycastResponder>(); _rayResponder.OnRaycastHit(); _responderHit = true; }
        else if (_hit.collider.GetComponent<RaycastResponder>() != _rayResponder && _responderHit)
        { _rayResponder.OffRaycastHit(); _responderHit = false; }

        return hasHit;
    }
}
