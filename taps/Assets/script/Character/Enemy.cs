using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ParentsOfParty
{
    public void Update()
    {
        //Debug.Log(name);
        ParentsUpdate();
    }


    private void Awake()
    {

        StructOfDamage ofDamage = new StructOfDamage(1, 1, false, 2);

        StructOfDamages = new()
        { ofDamage };


 
    }
}
