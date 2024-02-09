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
    bool _hasSword = false;
    public bool HasSword {  get { return _hasSword; } }
    bool _hasShield = false;
    public bool HasShield { get { return _hasShield; } }

    public void ShowWeapon(Weapons weapon, bool show)
    {
        switch (weapon)
        {
            case Weapons.Sword:
                if (!_hasSword) return;
                _sword.SetActive(show);
                break;
            case Weapons.Shield:
                if (!_hasShield) return;
                _shield.SetActive(show);
                if (show)
                    _weaponAnimationsController.ChangeDefense(Weapons.Shield);
                else
                    _weaponAnimationsController.ChangeDefense(Weapons.Sword);
                _shieldActive = show;
                break;
        }
    }

    public void GetWeapon(Weapons weapon)
    {
        switch (weapon)
        {
            case Weapons.Sword:
                _hasSword = true;
                _sword.SetActive(true);
                break;
            case Weapons.Shield:
                _shield.SetActive(true);
                _weaponAnimationsController.ChangeDefense(Weapons.Shield);
                _shieldActive = true;
                _hasShield = true;
                break;
        }

        PlayerController.Instance.EnableCombatControls(true);
    }
}
