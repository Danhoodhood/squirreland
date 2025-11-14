// Monster2 — класс врага, наследует Monser
// Реализует простой конечный автомат с тремя состояниями:
// Patrol — патрулирование, Chase — преследование игрока, Return — возвращение на патрульный маршрут
using UnityEngine;

public class Monster2 : Monser
{
    private enum State { Patrol, Chase, Return } // состояния ИИ

    [Header("Patrol")]
    [SerializeField] private float minX; // левая граница патруля
    [SerializeField] private float maxX; // правая граница патруля
    [SerializeField] private float patrolSpeed = 1.5f; // скорость патруля

    [Header("Chase")]
    [SerializeField] private float chaseSpeed = 3.0f; // скорость преследования
    [SerializeField] private float viewRadius = 4.0f; // радиус обзора игрока
    [Range(0, 360)][SerializeField] private float viewAngle = 90f; // угол зрения
    [SerializeField] private LayerMask playerLayer; // слой игрока
    [SerializeField] private LayerMask obstacleLayer; // слой препятствий для raycast

    [Header("Other")]
    [SerializeField] private Animator anim; // аниматор для управления анимациями
    [SerializeField] private float lostTime = 2.0f; // время, через которое монстр перестает преследовать после потери игрока

    private SpriteRenderer sprite;
    private State state = State.Patrol; // текущее состояние ИИ
    private Transform playerTransform; // ссылка на игрока
    private float lastSeenTime = -Mathf.Infinity; // последний раз, когда игрок был виден
    private Vector3 lastSeenPosition; // позиция игрока при последнем видении
    private Vector3 patrolTarget; // текущая цель для патруля
    private Vector3 originalScale; // исходный масштаб спрайта для корректного разворота

    // Инициализация компонентов
    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        originalScale = transform.localScale; // сохраняем исходный масштаб
    }

    protected override void Start()
    {
        base.Start();

        // задаем начальную патрульную цель
        patrolTarget = new Vector3(maxX, transform.position.y, transform.position.z);

        // ищем игрока по тегу
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) playerTransform = p.transform;
    }

    // Основной апдейт состояния ИИ
    private void Update()
    {
        if (playerTransform == null) return;

        switch (state)
        {
            case State.Patrol:
                PatrolUpdate(); // движение между границами
                if (CanSeePlayer()) TransitionTo(State.Chase); // переход в преследование
                break;

            case State.Chase:
                ChaseUpdate(); // преследуем игрока
                if (CanSeePlayer())
                {
                    lastSeenTime = Time.time;
                    lastSeenPosition = playerTransform.position;
                }
                else if (Time.time - lastSeenTime > lostTime)
                {
                    TransitionTo(State.Return); // игрок потерян — возвращаемся к патрулю
                }
                break;

            case State.Return:
                ReturnUpdate(); // возвращаемся к патрульной линии
                if (CanSeePlayer()) TransitionTo(State.Chase); // снова увидели игрока
                else if (Vector3.Distance(transform.position, patrolTarget) < 0.1f)
                    TransitionTo(State.Patrol); // достигли цели — патрулируем дальше
                break;
        }
    }

    #region State updates

    // Патрулирование: движение к границам minX/maxX
    private void PatrolUpdate()
    {
        if (transform.position.x < minX) patrolTarget = new Vector3(maxX, transform.position.y, transform.position.z);
        else if (transform.position.x > maxX) patrolTarget = new Vector3(minX, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);

        // разворот спрайта по направлению движения
        Vector3 moveDir = patrolTarget - transform.position;
        if (moveDir.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        anim?.SetBool("isRunning", true);
    }

    // Преследование игрока: движение к игроку
    private void ChaseUpdate()
    {
        if (playerTransform == null) return;

        // перемещение к игроку
        Vector3 target = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);

        // разворот спрайта к игроку
        if (playerTransform.position.x > transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        anim?.SetBool("isRunning", true);
    }

    // Возврат к патрульной линии после потери игрока
    private void ReturnUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);

        // разворот к цели
        Vector3 moveDir = patrolTarget - transform.position;
        if (moveDir.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        anim?.SetBool("isRunning", true);
    }

    #endregion

    // Переход между состояниями
    private void TransitionTo(State newState)
    {
        if (state == newState) return;
        state = newState;

        if (state == State.Chase)
        {
            lastSeenTime = Time.time;
            if (playerTransform != null) lastSeenPosition = playerTransform.position;
        }
    }

    // Проверка видимости игрока (радиус + Raycast)
    private bool CanSeePlayer()
    {
        if (playerTransform == null) return false;

        Vector2 toPlayer = playerTransform.position - transform.position;
        float dist = toPlayer.magnitude;
        if (dist > viewRadius) return false;

        Vector2 origin = (Vector2)transform.position + Vector2.up * 0.15f;
        int mask = playerLayer.value | obstacleLayer.value;
        Collider2D[] selfColliders = GetComponentsInChildren<Collider2D>();

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, toPlayer.normalized, viewRadius, mask);
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (var h in hits)
        {
            if (h.collider == null) continue;

            bool isSelf = false;
            foreach (var sc in selfColliders)
                if (h.collider == sc) { isSelf = true; break; }
            if (isSelf) continue;

            return h.collider.CompareTag("Player"); // если первый внешний hit — игрок, видим его
        }

        return false;
    }

    // Обработка столкновения с игроком
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float playerY = collision.gameObject.transform.position.y;
            float monsterY = transform.position.y;

            if (playerY > monsterY)
            {
                GetDamage(); // игрок прыгнул сверху
                if (lives < 1) Die();
            }
            else
            {
                Player.Instance.GetDamage(); // игрок столкнулся сбоку/снизу
                anim?.SetTrigger("isAttacking1");
            }
        }
    }
}
