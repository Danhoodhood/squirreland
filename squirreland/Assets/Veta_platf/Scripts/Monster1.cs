using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster1 : Monser//червь

{
    //[SerializeField] private AudioSource audioSourceDieMonster;


    private void Start()
    {
        lives = 1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Проверяем, что столкнулись с игроком
        {
            float playerY = collision.gameObject.transform.position.y;
            float monsterY = transform.position.y;

            if (playerY > monsterY) // Проверяем, что игрок находится выше по оси Y
            {
                // Игрок прыгнул на монстра сверху, не наносим урон
                Debug.Log("Игрок прыгнул на монстра сверху, урон наносится монстру.");
                lives--;
                Debug.Log("У монстра " + lives + " жизней");
                //
;
                if (lives < 1)
                {
                    //audioSourceDieMonster.Play();
                    Die();
                }
            }
            else
            {
                // Игрок касается монстра сбоку или снизу, наносим урон
                Player.Instance.GetDamage();
                Debug.Log("Игрок получил урон");
               
            }
        }
    }



}
