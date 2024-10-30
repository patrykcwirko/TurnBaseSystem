using Systems.EventBus;

public struct OnTurnEnd : IEvent { }
public struct OnUnitSelect : IEvent
{
    public UnitBehaviour Unit;
}
