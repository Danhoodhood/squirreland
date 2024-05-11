using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : Monser
{

    //private float speed = 3.5f;//скорость
    private Vector3 dir;//направление движения
    private SpriteRenderer sprite;//спрайт(Sircle внутри монстра2)


    [SerializeField]private float minX;
    [SerializeField]private float maxX;

    [SerializeField] private Animator anim;
    private void Start()
    {
        dir = transform.right;//начальное направление движения
        lives = 2;

    }

    private void Update()
    {
        Move();
    }
   

    private void Move()
    {
        // Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up*0.1f+ transform.right*dir.x*0.7f, 0.1f);
        
        //if (colliders.Length> 1) dir*= -1f;
        if (transform.position.x < minX) dir = transform.right;
        else if (transform.position.x > maxX) dir = -transform.right;

        transform.position =Vector3.MoveTowards(transform.position, transform.position+dir, Time.deltaTime);
        sprite.flipX = dir.x > 0.0f;
    }
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    public override void GetDamage()
    {
        
        lives  -= 1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
                // Игрок касается монстра сбоку или снизу, урон не наносится игроку
                Player.Instance.GetDamage();
                anim.SetBool("isAttacking", true);
                Debug.Log("Монстр касается игрока сбоку или снизу, но урон не наносится.");
               // anim.SetBool("isAttacking", false);
            }
        }
    }

}
