using Abilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModifierSlot : Slots.Slot
{
    [SerializeField] private ModifierOrb orb;

    public Modifier Modifier => orb.Modifier;

    private SkillAssiger assiger;

    public void Init(SkillAssiger _assigner)
    {
        assiger = _assigner;
    }

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ModifierOrb _mod = eventData.pointerDrag.GetComponent<ModifierOrb>();

            if (_mod != null && assiger.CanAssignMod(_mod.Modifier))
            {
                orb.SetMod(_mod.Modifier);
            }
        }
    }
}
