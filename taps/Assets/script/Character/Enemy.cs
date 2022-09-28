using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ParentsOfParty
{
    public void Update()
    {
        //Debug.Log(name);
        characterUi.UpdateOfCharacterUi(Hp);
    }


    private void Awake()
    {

        StructOfDamage ofDamage = new StructOfDamage(1, 1, false, 2);
        //Debug.Log(ofDamage.damage);
        //Debug.Log(ofDamage.damageOfStamina);
        //Debug.Log(ofDamage.splash);
        //Debug.Log(ofDamage.numberOfNote);
        StructOfDamages = new()
        { ofDamage };


 
    }
}
