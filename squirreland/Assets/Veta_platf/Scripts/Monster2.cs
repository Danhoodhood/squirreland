using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : Monser //КЛАСС ВРАГА "ЖУК 2" С ПАТРУЛИРОВАНИЕМ
{

    //private float speed = 3.5f;//скорость
    private Vector3 dir;//// Направление движения монстра
    private SpriteRenderer sprite;//// Спрайт для визуального отображения


    [SerializeField]private float minX;  // Минимальная граница патрулирования
    [SerializeField]private float maxX;// Максимальная граница патрулирования

    [SerializeField] private Animator anim; // Аниматор для анимаций монстра
    private void Start()
    {
        dir = transform.right;//Начальное направление движения - вправо
        lives = 2; // Жизни жука 2

    }

    private void Update()
    {
        Move();// Вызов метода движения каждый кадр
    }
   

    private void Move()// Движение монстра между границами
    {
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up*0.1f+ transform.right*dir.x*0.7f, 0.1f);

        //if (colliders.Length> 1) dir*= -1f;

        // разворот при достижении границ
        if (transform.position.x < minX) dir = transform.right;
        else if (transform.position.x > maxX) dir = -transform.right;

        // Плавное перемещение монстра 
        transform.position =Vector3.MoveTowards(transform.position, transform.position+dir, Time.deltaTime);
        sprite.flipX = dir.x > 0.0f;  // Поворот спрайта в сторону движения
    }
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>(); // Получение компонента спрайта
    }
    public override void GetDamage()  // Переопределенный метод получения урона
    {
        
        lives  -= 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)// Обработка столкновений с игроком
    {
        if (collision.gameObject.CompareTag("Player")) // Проверяем, что столкнулись с игроком
        {
            float playerY = collision.gameObject.transform.position.y;
            float monsterY = transform.position.y;

            if (playerY > monsterY) // Проверяем, что игрок находится выше по оси Y
            {
                // Игрок прыгнул на монстра сверху, наносим урон монстру
                GetDamage();
                Debug.Log("Игрок прыгнул на монстра сверху, урон наносится монстру.");

                if (lives < 1)
                    Die();
            }
            else
            {
                // Игрок касается монстра сбоку или снизу, урон наносится игроку
                Player.Instance.GetDamage();
                anim.SetTrigger("isAttacking1");
                //anim.SetBool("isAttacking", true);
                Debug.Log("Монстр касается игрока сбоку или снизу,  урон  наносится.");
               // anim.SetBool("isAttacking", false);
            }
        }
    }

}
