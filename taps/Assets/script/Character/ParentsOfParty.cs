using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsOfParty : MonoBehaviour
{
    public CharacterUi characterUi;

    [field: SerializeField]
    public int MaxHp { get; set; }

    [field: SerializeField]
    public int Hp { get; set; }

    [field: SerializeField]
    public int MaxStamina { get; set; }

    [field: SerializeField]
    public int Stamina { get; set; }

    public List<StructOfDamage> StructOfDamages { get; set; }
}
