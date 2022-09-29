using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ParentsOfParty
{
 



    [field: SerializeField]
    public int MaxOfSkillPoint { get; set; }

    [field: SerializeField]
    public int PlayerSkillPoint { get; set; }




    private void Awake()
    {
        StructOfDamage ofDamage = new StructOfDamage(1, 1, false, 2);

        StructOfDamages = new()
        { ofDamage };
    }
    public void Update()
    {
        //Debug.Log(name);
        ParentsUpdate();
    }

    public void Active()
    {

    }

    

}
