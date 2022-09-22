using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public AudioSource audioSource;

    public Animator Animator;
    public Animator Animator2;

    public List<AudioClip> audioClips;
    


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

}
