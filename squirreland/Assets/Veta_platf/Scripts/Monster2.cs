using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster2 : Monser
{

    private float speed = 3.5f;//скорость
    private Vector3 dir;//направление движения
    private SpriteRenderer sprite;//спрайт(Sircle внутри монстра2)

    private void Start()
    {
        dir = transform.right;//начальное направление движения
        lives = 3;

    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.up*0.1f+ transform.right*dir.x*0.7f, 0.1f);

        if (colliders.Length> 1) dir*= -1f;
        transform.position =Vector3.MoveTowards(transform.position, transform.position+dir, Time.deltaTime);
        sprite.flipX = dir.x > 0.0f;
    }
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.GetDamage();
            
        }
        
    }
}
