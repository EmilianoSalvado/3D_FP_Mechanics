using UnityEngine;

public class PlayerCollisionRaycaster
{
    LayerMask _upperMask;
    LayerMask _lowerMask;
    LayerMask _bottomMask;
    float _rayDistance;

    Transform _upperFrontPoint;
    Transform _lowerFrontPoint;
    Transform _bottomPoint;

    RaycastHit _hit;
    bool _responderHit;
    RaycastResponder _rayResponder;

    public PlayerCollisionRaycaster(LayerMask upperMask, LayerMask lowerMask, LayerMask bottomMask, float rayDistance, Transform upperFrontPoint, Transform lowerFrontPoint, Transform bottomPoint)
    {
        _upperMask = upperMask;
        _lowerMask = lowerMask;
        _bottomMask = bottomMask;
        _rayDistance = rayDistance;
        _upperFrontPoint = upperFrontPoint;
        _lowerFrontPoint = lowerFrontPoint;
        _bottomPoint = bottomPoint;
    }

    public bool UpperHit() { return RayDoesHit(_upperFrontPoint, _upperMask); }
    public bool LowerHit() { return RayDoesHit(_lowerFrontPoint, _lowerMask); }
    public bool BottomHit() { return RayDoesHit(_bottomPoint, _bottomMask); }

    bool RayDoesHit(Transform point, LayerMask mask)
    {
        var hasHit = Physics.Raycast(point.position, point.forward, out _hit, _rayDistance, mask);

        if (!hasHit) { return hasHit; }

        if (_hit.collider.GetComponent<RaycastResponder>() != null && !_responderHit)
        { _rayResponder = _hit.collider.GetComponent<RaycastResponder>(); _rayResponder.OnRaycastHit(); _responderHit = true; }
        else if (_hit.collider.GetComponent<RaycastResponder>() != _rayResponder && _responderHit)
        { _rayResponder.OffRaycastHit(); _responderHit = false; }

        return hasHit;
    }
}
