using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monser : MonoBehaviour
{
    public AudioSource audioSourceGetDamage;
    protected int lives;
    public virtual void GetDamage()
    {

        lives -= 1;
        audioSourceGetDamage.Play();
        if (lives <= 0)
        {

            Die();
        }
    }
    public virtual void Die()
    {

        Destroy(this.gameObject);
    }
}
