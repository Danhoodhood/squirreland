using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monser : MonoBehaviour
{
    //public AudioSource audioSourceGetDamage;
    protected int lives;
    public AudioSource audioSourceDamageMonster;
    public virtual void GetDamage()
    {

        lives -= 1;
        //audioSourceGetDamage.Play();
        if (lives <= 0)
        {
            
            Die();
        }
    }
    public virtual void Die()
    {
        audioSourceDamageMonster.Play();
        Destroy(this.gameObject, 0.5f);
        
    }
}
