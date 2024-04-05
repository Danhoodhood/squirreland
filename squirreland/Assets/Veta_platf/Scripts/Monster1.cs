using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster1 : Monser//червь

{
    
    
    private void Start()
    {
        lives = 2;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.GetDamage();
            lives--;
            Debug.Log("у червяка " + lives + " жизней");
        }
        if (lives < 1)
            Die();
    }


}
