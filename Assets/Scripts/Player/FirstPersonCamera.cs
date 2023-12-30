using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _sensivity, _upperTop, _lowerTop;
    float _ver, _hor;
    Action _update;
    bool _isAligning;

    private void Awake()
    {
        _update = Rotate;
        Align(true);
    }
    public void SetViewAxis(float x, float y)
    {
        if (x == 0 && y == 0) return;

        _ver = Mathf.Clamp(_ver + -y * (_sensivity * Time.deltaTime), _upperTop, _lowerTop);

        _hor += x * (_sensivity * Time.deltaTime);
        if (_hor == 360 || _hor == -360) _hor = 0;

        _update();
    }

    public void Align(bool b)
    {
        if (b && !_isAligning)
        {
            _update += AlignWithView;
            _isAligning = b;
            return;
        }
        if (_isAligning)
        {
            _update -= AlignWithView;
            _isAligning = b;
        }
    }

    public void Rotate()
    {
        transform.eulerAngles = Vector3.up * _hor + Vector3.right * _ver;
    }

    public void AlignWithView()
    {
        _playerTransform.forward = transform.forward - new Vector3(0, transform.forward.y, 0);
    }

    public void AlignWithPlayer()
    {
        transform.forward = _playerTransform.forward;
    }
}