using System.Collections;
using UnityEngine;

public abstract class UnitAI : ScriptableObject
{
    public abstract IEnumerator DoAction(UnitsManager _manager, UnitBehaviour _currentUnit);
}
