using System.Collections;
using UnityEngine;

public class StepableEdge : MonoBehaviour, RaycastResponder
{
    [SerializeField] Rigidbody _playerRB;
    [SerializeField] PlayerModel _playerModel;
    [SerializeField] FirstPersonCamera _firstPersonCamera;

    private void OnCollisionEnter(Collision collision)
    {
        _playerRB = collision.transform.GetComponent<Rigidbody>();
        _playerModel = collision.transform.GetComponent<PlayerModel>();
        _firstPersonCamera = collision.transform.GetComponentInChildren<FirstPersonCamera>();
    }

    public void AlignAndBlock()
    {
        StartCoroutine(AlignAndBlockRoutine());
    }

    IEnumerator AlignAndBlockRoutine()
    {
        var a = _playerRB.transform.forward;
        var b = transform.forward;
        var alpha = 0f;

        PlayerController.Instance.EnableCameraControls(false);

        while (alpha < 1f)
        {
            _playerRB.transform.forward = Vector3.Lerp(a, b, alpha);
            alpha += Time.deltaTime;
            yield return null;
        }
        _playerModel.Movement.LockMovement(Directions.ForthBack);
        _firstPersonCamera.Align(false);
        _firstPersonCamera.AlignWithPlayer();

        PlayerController.Instance.EnableCameraControls(true);
    }

    public void Release()
    {
        _playerModel.Movement.UnlockMovement();
        _firstPersonCamera.Align(true);
    }

    public void OnRaycastHit()
    {
        AlignAndBlock();
    }

    public void OffRaycastHit()
    {
        Release();
    }
}