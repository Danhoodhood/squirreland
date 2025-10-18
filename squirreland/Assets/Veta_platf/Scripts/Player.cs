using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Monser //ИГРОК
{
    [SerializeField] float speed = 3f; //скорость движения
    
    [SerializeField] private float jumpForce = 15f;// сила прыжка
    [SerializeField] private bool isGrounded = false;
    /*[Range(-5f, 5f)]public float checkGroundOffsetY = -1.8f;
    [Range(0f, 5f)] public float  checkGroundRadius = 0.3f;*/

    [SerializeField]private bool isAttacking = false; // атакуем ли
    

    public Transform attackPos;//позиция атаки 
    public float attackRange;//дальность атаки
    public LayerMask enemy;//Слой врагов

    private float moveInput;//// Ввод движения
    private bool factingRight = true; //направление движения в начале
    
    private Rigidbody2D rb;// Физическое тело
    private SpriteRenderer sprite;// Визуал персонажа
    public static Player Instance { get;  set; }//теперь можно обращаться к методам этого класса из других классов не создавая экземпляра этого класса в другом
    public Joystick joystick;
    public AudioSource audioSourceJump;// Звук прыжка
    public AudioSource audioSourceDamagePlayer;// Звук получения урона


    [SerializeField]private Animator anim;// Аниматор персонажа



    private void Awake()//ИНИЦИАЛИЗАЦИЯ
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
        

        
        lives = 5;
    }

    private void Run(float move) //ДВИЖЕНИЕ ИГРОКА
    {
        Vector3 dir = transform.right * move;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
    }

    public void Jump1() // ОСНОВНОЙ ПРЫЖОК
    {


        // Запуск анимации прыжка
        anim.SetTrigger("jumpUp");

        // Проверка прыжка на врага
        if (!isGrounded)
        { // Поиск всех коллайдеров в радиусе 0.3 единицы
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    // Проверяем, что игрок прыгает на врага
                    if (transform.position.y > collider.transform.position.y)
                    {
                       
                       collider.GetComponent<Monser>().GetDamage(); // Нанесение урона врагу
                                                                    //audioSourceDamageMonster.Play();
                    }
                }
            }
        }
        else  {
            // Обычный прыжок
            rb.velocity = Vector2.up*jumpForce;
            audioSourceJump.Play();

        }
    }
    public void Jump() //ПРОВЕРКА ПРЫЖКА
    {
        if (isGrounded) { Jump1(); }
    }
    void Flip()//ПОВОРОТ ПЕРСОНАЖА
    {
        factingRight = !factingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *=-1;
        transform.localScale = Scaler;  
    }
    private void CheckGround() //ПРОВЕРКА ЗЕМЛИ(ПЕРСОНАЖ НА ЗЕМЛЕ)
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
      

    }
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    private void FixedUpdate() //УПРАВЛЕНИЕ
    {
        CheckGround();
        // Объединяем ввод с клавиатуры и с джойстика
        moveInput = Input.GetAxis("Horizontal") + joystick.Horizontal;

        // ПОВОРОТ ПЕРСОНАЖА ПО НАПРАВЛЕНИЮ ДВИЖЕНИЯ
        if (factingRight == false && joystick.Horizontal >0)
        {
            Flip();
        }
        else if(factingRight == true && joystick.Horizontal < 0)
        {
            Flip();
        }
    }
    void Update()//ОБНОВЛЕНИЕ КАЖДЫЙ КАДР 
    {

        if (Input.GetKeyDown(KeyCode.Space)) // Обработка прыжка 
        {
            Jump();
        }

        // Анимации
        if (isGrounded)
        {
            anim.SetBool("isJump", false);
          
           
        }
        else
        {
            
            anim.SetBool("isJump", true);
            
        }

        if (Mathf.Abs(moveInput) > 0.01f && !isAttacking)
        {
            Run(moveInput); // передаём направление
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }



    }
    public override void GetDamage() //// Получение урона персонажем
    {
        HeartSystem.health--;
        
        lives  -= 1;

        Debug.Log(lives);
        audioSourceDamagePlayer.Play();
    }
    private IEnumerator AttackCoolDown() //ПЕРЕЗАРЯДКА АТАКИ
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
    public void Attack()//АТАКА ИГРОКА
    {
        if (isGrounded )
        { // Атака возможна только стоя на земле
          //  State = States.attack;
            isAttacking = true;
            //isRecharged = false;

            StartCoroutine(AttackCoolDown()); // Запуск перезарядки атаки

            Debug.Log("УДАР");

            // Поиск врагов в зоне атаки: круг вокруг attackPos
            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);
            

            for (int i = 0; i < colliders.Length; i++) // Перебор всех найденных врагов
            {

                colliders[i].GetComponent<Monser>().GetDamage();// Нанесение урона каждому врагу в зоне
            }
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for(int i = 0; i < colliders.Length; i++) {
            colliders[i].GetComponent<Monser>().GetDamage();
        }   
    }
    private void OnDrawGizmosSelected() //// Визуализация зоны атаки 
    {   
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)// Присоединение к движущейся платформе
    {
        if (collision.gameObject.name.Equals("moving_platform"))
        {
            this.transform.parent = collision.transform;
        }

    }
    
    private void OnCollisionExit2D(Collision2D collision)//// Отсоединение от движущейся платформы
    {
        if (collision.gameObject.name.Equals("moving_platform"))
        {
            this.transform.parent = null;
        }

    }

    
}
