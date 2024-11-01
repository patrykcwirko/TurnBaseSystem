using Abilities;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifier/DamageModifier")]
public class DamageModifier : Modifier
{
    [SerializeField] private int damage;

    public override Abilitie[] DoModifier(Abilitie[] _abilities)
    {
        for (int i = 0; i < _abilities.Length; i++)
        {
            for (int j = 0; j < Tags.Length; j++)
            {
                if (_abilities[i].Tags.Contains(Tags[j]))
                {
                    ((DamageAbilities)_abilities[i]).amount += damage;
                }
            }
        }

        return _abilities;
    }

    public override Abilitie[] UndoModifier(Abilitie[] _abilities)
    {
        for (int i = 0; i < _abilities.Length; i++)
        {
            for (int j = 0; j < Tags.Length; j++)
            {
                if (_abilities[i].Tags.Contains(Tags[j]))
                {
                    ((DamageAbilities)_abilities[i]).amount -= damage;
                }
            }
        }

        return _abilities;
    }
}
