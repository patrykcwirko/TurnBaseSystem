using System.Collections.Generic;
using Systems.EventBus;

public struct OnTurnEnd : IEvent { }
public struct OnUnitSelect : IEvent
{
    public UnitBehaviour Unit;
}

public struct OnSkillAssigned : IEvent
{
    public List<UnitData> UnitData;
}
