using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickable : Pickable
{
    [SerializeField] Weapons _weapon;

    public override void Pick()
    {
        _playerModel.WeaponsManager.GetWeapon(_weapon);
        Destroy(gameObject);
    }
}
