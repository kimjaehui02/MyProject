using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public AudioSource audioSource;

    public Animator Animator;
    public Animator Animator2;

    public List<AudioClip> audioClips;


    public void CalculationOfDamage(List<StructOfFight> structOfFight)
    {

        for (int i = 0; i < structOfFight.Count; i++)
        {
            //Debug.Log(structOfFight[i].structOfDamage.damage);
            structOfFight[i].attacker.Hp += structOfFight[i].structOfDamage.damage;
            structOfFight[i].defender.Hp += structOfFight[i].structOfDamage.damageOfStamina;

            //Debug.Log(structOfFight[i].attacker.gameObject.name);

            structOfFight[i].attacker.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Attack");
            structOfFight[i].defender.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
        }

        audioSource.PlayOneShot(audioClips[Random.Range(3, 5)]);

    }

    public void CalculationOfJust(List<StructOfFight> structOfFight)
    {

        for (int i = 0; i < structOfFight.Count; i++)
        {
            //Debug.Log(structOfFight[i].structOfDamage.damage);
            structOfFight[i].attacker.Hp += structOfFight[i].structOfDamage.damage;
            structOfFight[i].defender.Hp += structOfFight[i].structOfDamage.damageOfStamina;
        }

    }



    public void SwordClip(int input)
    {
        audioSource.PlayOneShot(audioClips[input]);
        Animator.SetTrigger("Attack");
        Animator2.SetTrigger("Hit");
    }

    public void MissClip()
    {
        audioSource.PlayOneShot(audioClips[5]);
        Animator.SetTrigger("Attack");
    }


    IEnumerator Shake()
    {
        yield return new WaitForSecondsRealtime(0.05f);
    }
}
