using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : MonoBehaviour
{
    [SerializeField] protected PlayerModel _playerModel;
    public abstract void Pick();
}
