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

    [field: SerializeField]
    public bool Dead { get; set; }

    public List<StructOfDamage> StructOfDamages { get; set; }

    public void ParentsUpdate()
    {
        //Debug.Log(name);
        characterUi.UpdateOfCharacterUi(Hp, Stamina);
    }
}

public struct DataOfParty
{

    public int MaxHp { get; set; }

    public int Hp { get; set; }

    public int MaxStamina { get; set; }

    public int Stamina { get; set; }

    public bool Dead { get; set; }
}
