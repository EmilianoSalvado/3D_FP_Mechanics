using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField] GameObject _sword;
    [SerializeField] GameObject _shield;
    [SerializeField] WeaponAnimationsController _weaponAnimationsController;
    bool _shieldActive = false;
    public bool ShieldActive { get { return _shieldActive; } }

    public void ShowWeapon(Weapons weapon, bool show)
    {
        switch (weapon)
        {
            case Weapons.Sword:
                _sword.SetActive(show);
                break;
            case Weapons.Shield:
                _shield.SetActive(show);
                if (show)
                    _weaponAnimationsController.ChangeDefense(Weapons.Shield);
                else
                    _weaponAnimationsController.ChangeDefense(Weapons.Sword);
                _shieldActive = show;
                break;
        }
    }
}
