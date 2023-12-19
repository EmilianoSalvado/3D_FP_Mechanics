using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _sensivity, _upperTop, _lowerTop;
    float _ver, _hor;

    public void Rotate(float x, float y)
    {
        if (x == 0 && y == 0) return;

        _ver = Mathf.Clamp(_ver +- y * (_sensivity * Time.deltaTime), _upperTop, _lowerTop);

        _hor += x * (_sensivity * Time.deltaTime);
        if (_hor == 360 || _hor == -360) _hor = 0;

        transform.eulerAngles = Vector3.up * _hor + Vector3.right * _ver;

        _playerTransform.forward = transform.forward - new Vector3(0, transform.forward.y, 0);
    }
}