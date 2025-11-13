using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monser : MonoBehaviour
{
    protected int lives;
    public AudioSource audioSourceDamageMonster;
    [SerializeField] protected MonsterData monsterData;

    // Добавляем ссылку на AI компонент
    protected EnemyAI enemyAI;

    protected virtual void Start()
    {
        if (monsterData != null)
        {
            lives = monsterData.lives;
        }

        // Получаем компонент AI
        enemyAI = GetComponent<EnemyAI>();
    }

    public virtual void GetDamage()
    {
        lives -= 1;

        // Если есть AI - переходим в состояние преследования при получении урона
        if (enemyAI != null)
        {
            enemyAI.currentState = EnemyState.Chase;
        }

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

    // Новый метод для атаки
    public virtual void PerformAttack()
    {
        // Базовая реализация атаки
        Debug.Log($"{gameObject.name} атакует!");
    }
}