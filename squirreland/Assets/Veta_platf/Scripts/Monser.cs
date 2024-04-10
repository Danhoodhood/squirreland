using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monser : MonoBehaviour
{
    protected int lives;
    public virtual void GetDamage()
    {
        lives -= 1;
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
