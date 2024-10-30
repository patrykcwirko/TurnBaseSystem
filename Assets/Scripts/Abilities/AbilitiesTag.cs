using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Tag")]
    public class AbilitiesTag : ScriptableObject
    {
        public virtual bool ContainTag(Skill _skill)
        {
            return _skill.ContainTag(this);
        }
    }
}