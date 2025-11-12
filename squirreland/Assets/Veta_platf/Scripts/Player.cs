using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Monser // Игрок наследуется от Monser (врагов)
{
    [SerializeField] float speed = 3f; // Скорость движения
    [SerializeField] private float jumpForce = 15f; // Сила прыжка
    [SerializeField] private bool isGrounded = false; // Проверка, стоит ли игрок на земле

    [SerializeField] private bool isAttacking = false; // Флаг атаки

    public Transform attackPos; // Позиция зоны атаки
    public float attackRange; // Радиус зоны атаки
    public LayerMask enemy; // Слой врагов для атаки

    private float moveInput; // Ввод по горизонтали
    private bool factingRight = true; // Направление взгляда игрока

    private Rigidbody2D rb; // Rigidbody2D — физическое тело игрока
    private SpriteRenderer sprite; // Отображение спрайта игрока
    public static Player Instance { get; set; } // Синглтон для доступа из других классов

    public Joystick joystick; // Джойстик для управления
    public AudioSource audioSourceJump; // Звук прыжка
    public AudioSource audioSourceDamagePlayer; // Звук получения урона

    [SerializeField] private Animator anim; // Аниматор игрока

    private void Awake() // Инициализация
    {
        rb = GetComponent<Rigidbody2D>(); // Получение Rigidbody2D
        sprite = GetComponentInChildren<SpriteRenderer>(); // Получение спрайта
        Instance = this;

        lives = 5; // Начальные жизни игрока
    }

    private void Run(float move) // Движение игрока
    {
        Vector3 dir = transform.right * move;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        // Отражение спрайта при смене направления движения
        sprite.flipX = dir.x < 0.0f;
    }

    public void Jump1() // Основной прыжок
    {
        anim.SetTrigger("jumpUp"); // Анимация прыжка

        if (!isGrounded) // Если игрок в воздухе
        {
            // Проверка прыжка на врага
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy") && transform.position.y > collider.transform.position.y)
                {
                    collider.GetComponent<Monser>().GetDamage(); // Нанесение урона врагу
                }
            }
        }
        else
        {
            // Обычный прыжок при нахождении на земле
            rb.velocity = Vector2.up * jumpForce; // Используем Rigidbody2D для физического прыжка
            audioSourceJump.Play();
        }
    }

    public void Jump() // Метод проверки возможности прыжка
    {
        if (isGrounded) Jump1();
    }

    void Flip() // Разворот персонажа
    {
        factingRight = !factingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void CheckGround() // Проверка, стоит ли игрок на земле
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f); // Проверка коллайдерами
        isGrounded = collider.Length > 1; // Если есть коллайдеры кроме самого игрока
    }

    private void FixedUpdate() // Физический апдейт
    {
        CheckGround();

        // Объединяем ввод с клавиатуры и джойстика
        moveInput = Input.GetAxis("Horizontal") + joystick.Horizontal;

        // Разворот игрока по направлению движения
        if (!factingRight && joystick.Horizontal > 0) Flip();
        else if (factingRight && joystick.Horizontal < 0) Flip();
    }

    void Update() // Апдейт каждый кадр
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump(); // Прыжок

        // Анимации
        anim.SetBool("isJump", !isGrounded);
        anim.SetBool("isRunning", Mathf.Abs(moveInput) > 0.01f && !isAttacking);

        if (Mathf.Abs(moveInput) > 0.01f && !isAttacking)
            Run(moveInput); // Движение игрока
    }

    public override void GetDamage() // Получение урона
    {
        HeartSystem.health--; // Система здоровья
        lives -= 1;
        audioSourceDamagePlayer.Play();
        Debug.Log(lives);
    }

    private IEnumerator AttackCoolDown() // Перезарядка атаки
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    public void Attack() // Атака игрока
    {
        if (isGrounded)
        {
            isAttacking = true;
            StartCoroutine(AttackCoolDown());

            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].GetComponent<Monser>().GetDamage();
            }
        }
    }

    private void OnDrawGizmosSelected() // Визуализация зоны атаки
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision) // Обработка столкновений
    {
        // Присоединение к движущейся платформе
        if (collision.gameObject.name.Equals("moving_platform"))
        {
            this.transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision) // Выход со столкновения
    {
        if (collision.gameObject.name.Equals("moving_platform"))
        {
            this.transform.parent = null;
        }
    }
}
