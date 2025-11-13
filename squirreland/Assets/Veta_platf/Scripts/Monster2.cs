using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Monster2 теперь реализует простой конечный автомат + поле зрения
public class Monster2 : Monser
{
    private enum State { Patrol, Chase, Return }

    [Header("Patrol")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float patrolSpeed = 1.5f;

    [Header("Chase")]
    [SerializeField] private float chaseSpeed = 3.0f;
    [SerializeField] private float viewRadius = 4.0f;      // радиус "зрения"
    [Range(0, 360)][SerializeField] private float viewAngle = 90f; // угол зрения
    [SerializeField] private LayerMask playerLayer;        // слой игрока
    [SerializeField] private LayerMask obstacleLayer;      // слой препятствий для raycast

    [Header("Other")]
    [SerializeField] private Animator anim;
    [SerializeField] private float lostTime = 2.0f; // время до перехода в Return после потери игрока

    private Vector3 dir;
    private SpriteRenderer sprite;
    private State state = State.Patrol;
    private Transform playerTransform;
    private float lastSeenTime = -Mathf.Infinity;
    private Vector3 lastSeenPosition;
    private Vector3 patrolTarget;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected override void Start()
    {
       
        
        base.Start();
        dir = transform.right;
        patrolTarget = new Vector3(maxX, transform.position.y, transform.position.z);
        // Найдём игрока по тегу (подходит для простого задания)
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Found player object: " + (p ? p.name : "NULL"));
        if (p != null) playerTransform = p.transform;

      

    }

    private void Update()
    {
        switch (state)
        {
            case State.Patrol:
                PatrolUpdate();
                // проверяем видит ли враг игрока
                if (CanSeePlayer())
                {
                    TransitionTo(State.Chase);
                }
                break;

            case State.Chase:
                ChaseUpdate();
                if (CanSeePlayer())
                {
                    lastSeenTime = Time.time;
                    lastSeenPosition = playerTransform.position;
                }
                else
                {
                    // если игрок не виден и прошло lostTime -> Return
                    if (Time.time - lastSeenTime > lostTime)
                        TransitionTo(State.Return);
                }
                break;

            case State.Return:
                ReturnUpdate();
                if (CanSeePlayer())
                {
                    TransitionTo(State.Chase);
                }
                else
                {
                    // если дошли до патрульной зоны -> Patrol
                    if (Mathf.Abs(transform.position.x - patrolTarget.x) < 0.1f)
                        TransitionTo(State.Patrol);
                }
                break;
        }
    }

    #region State updates

    private void PatrolUpdate()
    {
        // простое патрулирование между minX и maxX (как было)
        if (transform.position.x < minX) dir = transform.right;
        else if (transform.position.x > maxX) dir = -transform.right;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, patrolSpeed * Time.deltaTime);
        sprite.flipX = dir.x > 0.0f;
        anim?.SetBool("isRunning", Mathf.Abs(patrolSpeed) > 0.01f);
        // патрульная цель — ближайшая граница (для Return вычисляем обратно)
        patrolTarget = dir.x > 0 ? new Vector3(maxX, transform.position.y, transform.position.z) : new Vector3(minX, transform.position.y, transform.position.z);
    }

    private void ChaseUpdate()
    {
        if (playerTransform == null) return;
        // перемещаемся к игроку
        Vector3 target = new Vector3(playerTransform.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);

        // поворот спрайта
        sprite.flipX = (playerTransform.position.x - transform.position.x) > 0.0f;

        // анимация атаки/бега
        anim?.SetBool("isRunning", true);
    }

    private void ReturnUpdate()
    {
        // идём к последнему патрульному направлению (patrolTarget)
        transform.position = Vector3.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);
        sprite.flipX = (patrolTarget.x - transform.position.x) > 0.0f;
        anim?.SetBool("isRunning", true);
    }

    #endregion

    private void TransitionTo(State newState)
    {
        if (state == newState) return;
        Debug.Log($"[Monster2] {state} -> {newState}");
        state = newState;

        if (state == State.Chase)
        {
            lastSeenTime = Time.time;
            if (playerTransform != null) lastSeenPosition = playerTransform.position;
        }
    }

    // проверка видимости игрока (радиус + угол + raycast на препятствие)
    private bool CanSeePlayer()
    {
        if (playerTransform == null) return false;

        Vector2 toPlayer = playerTransform.position - transform.position;
        float dist = toPlayer.magnitude;
        if (dist > viewRadius) return false;

        float angleToPlayer = Vector2.Angle(transform.right * dir.x, toPlayer);
        if (angleToPlayer > viewAngle * 0.5f) return false;

        // raycast на препятствия
        RaycastHit2D hit = Physics2D.Raycast(transform.position, toPlayer.normalized, viewRadius, ~(~0 << 31)); // default mask
        // Лучше использовать obstacleLayer: если hit и это не игрок — блокировка
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, toPlayer.normalized, viewRadius, obstacleLayer | playerLayer);
        if (hit2.collider != null)
        {
            if (hit2.collider.CompareTag("Player")) return true;
            else return false;
        }

        // на всякий случай fallback
        return false;
    }

    // визуализируем сектор зрения
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 forward = transform.right * (dir.x >= 0 ? 1f : -1f);
        float half = viewAngle * 0.5f;
        Vector3 a = Quaternion.Euler(0, 0, half) * forward;
        Vector3 b = Quaternion.Euler(0, 0, -half) * forward;
        Gizmos.DrawLine(transform.position, transform.position + a.normalized * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + b.normalized * viewRadius);
    }

    // обработка столкновений оставляем как раньше
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float playerY = collision.gameObject.transform.position.y;
            float monsterY = transform.position.y;

            if (playerY > monsterY)
            {
                GetDamage();
                Debug.Log("Игрок прыгнул на монстра сверху, урон наносится монстру.");

                if (lives < 1)
                    Die();
            }
            else
            {
                Player.Instance.GetDamage();
                anim?.SetTrigger("isAttacking1");
                Debug.Log("Монстр касается игрока сбоку или снизу, урон наносится.");
            }
        }
    }
}
