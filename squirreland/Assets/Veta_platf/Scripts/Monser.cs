using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monser : MonoBehaviour // БАЗОВЫЙ КЛАСС ВСЕХ ВРАГОВ
{
    //public AudioSource audioSourceGetDamage;
    protected int lives; // Количество жизней врага
    public AudioSource audioSourceDamageMonster; // Звук получения урона
    public virtual void GetDamage() // Виртуальный метод получения урона
    {

        lives -= 1;
        //audioSourceGetDamage.Play();

        // Проверка смерти
        if (lives <= 0)
        {
            
            Die();
        }
    }
    public virtual void Die() // Виртуальный метод смерти врага
    {
        audioSourceDamageMonster.Play();// Воспроизведение звука смерти
        Destroy(this.gameObject, 0.5f);// Уничтожение объекта через 0.5 секунды

    }
}
