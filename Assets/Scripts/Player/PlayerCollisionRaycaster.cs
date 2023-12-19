using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionRaycaster
{
    LayerMask _detectable;
    float _rayDistance;

    Transform _upperFrontPoint;
    Transform _lowerFrontPoint;
    Transform _bottomPoint;

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
        return Physics.Raycast(point.position, point.forward, _rayDistance, _detectable);
    }

    public RaycastHit GetHit(Transform point)
    {
        RaycastHit hit;
        Physics.Raycast(point.position, point.forward, out hit, _rayDistance, _detectable);
        return hit;
    }
}
