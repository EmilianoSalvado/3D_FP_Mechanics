using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationsController : MonoBehaviour
{
    [SerializeField] Animator _swordAnimatorController;
    [SerializeField] Animator _shieldAnimatorController;
    Animator _currentAttackAnimatorController;
    Animator _currentDefenseAnimatorController;

    private void Start()
    {
        _currentAttackAnimatorController = _swordAnimatorController;
        _currentDefenseAnimatorController = _swordAnimatorController;
    }

    public void Attack()
    {
        _currentAttackAnimatorController.SetTrigger("Attack");
    }

    public void Defense(bool b)
    {
        _currentDefenseAnimatorController.SetBool("Defense", b);
    }

    public void ChangeAttack(Weapons attackWeapon)
    {
        switch (attackWeapon)
        {
            case Weapons.Sword:
                _currentAttackAnimatorController = _swordAnimatorController;
                break;
            case Weapons.Shield:
                _currentAttackAnimatorController = _shieldAnimatorController;
                break;
        }
    }

    public void ChangeDefense(Weapons defenseWeapon)
    {
        switch (defenseWeapon)
        {
            case Weapons.Sword:
                _currentDefenseAnimatorController = _swordAnimatorController;
                break;
            case Weapons.Shield:
                _currentDefenseAnimatorController = _shieldAnimatorController;
                break;
        }
    }
}
