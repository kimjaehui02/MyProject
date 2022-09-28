using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ParentsOfParty
{
 



    [field: SerializeField]
    public int MaxOfSkillPoint { get; set; }

    [field: SerializeField]
    public int PlayerSkillPoint { get; set; }





    public void Update()
    {
        //Debug.Log(name);
        characterUi.UpdateOfCharacterUi(Hp);
    }

    public void Active()
    {

    }

    

}
