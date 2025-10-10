using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour // КЛАСС ПРЕПЯТСТВИЯ С УРОНОМ
{
    
    private void OnCollisionEnter2D(Collision2D collision) // Обработка столкновения с игроком
    {

        if(collision.gameObject  == Player.Instance.gameObject)
        {

            Player.Instance.GetDamage(); // Нанесение урона игроку при касании
        }
    }
}
