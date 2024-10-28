using Abilities;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillSlot : Slots.Slot
{
    [SerializeField] private SkillOrb orb;

    public SkillObject Skill => orb.SkillObject;

    public override void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.GetComponent<SkillOrb>() != null)
        {
            orb.SetSkill(eventData.pointerDrag.GetComponent<SkillOrb>().SkillObject);
        }
    }
}
