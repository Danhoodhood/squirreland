using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster1 : Monser//КЛАСС ВРАГА: жук 1

{
    //[SerializeField] private AudioSource audioSourceDieMonster;
    [SerializeField] private Animator anim; // Аниматор для управления анимациями

    private void Start()
    {
        lives = 1; // Установка начального количества жизней
    }
    private void OnCollisionEnter2D(Collision2D collision)   // Обработка столкновений с другими объектами
    {
        if (collision.gameObject.CompareTag("Player")) // Проверяем, что столкнулись с игроком
        {
            float playerY = collision.gameObject.transform.position.y;
            float monsterY = transform.position.y;

            if (playerY > monsterY) // Проверяем, что игрок находится выше по оси Y
            {
                // Игрок прыгнул на монстра сверху, наносим урон
                Debug.Log("Игрок прыгнул на монстра сверху, урон наносится монстру.");
                lives--;
                Debug.Log("У монстра " + lives + " жизней");
                
;

                // Проверка смерти монстра после получения урона
                if (lives < 1)
                {
                    
                    Die();
                }
            }
            else
            {
                // Игрок касается монстра сбоку или снизу, наносим урон игроку
                Player.Instance.GetDamage();
                anim.SetTrigger("isAttacking1");
                Debug.Log("Игрок получил урон");
                

            }
        }
    }



}
