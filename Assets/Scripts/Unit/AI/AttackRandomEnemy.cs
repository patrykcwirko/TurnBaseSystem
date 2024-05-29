using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/RandomAttack")]
public class AttackRandomEnemy : UnitAI
{
    public override IEnumerator DoAction(UnitsManager _manager, UnitBehaviour _currentUnit)
    {
        List<UnitBehaviour> _posibleTargets = _currentUnit.IsEnemy ? _manager.PlayerUnits : _manager.EnemyUnits;
        UnitBehaviour _target = _posibleTargets.GetRandom();
        _target.TakeDamage(_currentUnit.Data.AttackPower);

        yield return null;
    }
}
