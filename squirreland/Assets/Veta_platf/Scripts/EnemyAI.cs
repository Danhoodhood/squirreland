using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Patrol,
    Chase,
    Attack,
    Return
}

public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    public EnemyState currentState = EnemyState.Patrol;
    public float chaseRange = 5f;
    public float attackRange = 1.5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3.5f;

    [Header("Patrol Points")]
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    private Transform player;
    private NavMeshAgent agent;
    private Vector3 startPosition;
    private Monser monsterController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        monsterController = GetComponent<Monser>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        startPosition = transform.position;

        // Настройка NavMeshAgent
        agent.speed = patrolSpeed;
        agent.stoppingDistance = attackRange - 0.2f;

        // Начинаем с патрулирования
        SetNextPatrolPoint();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case EnemyState.Patrol:
                PatrolBehavior(distanceToPlayer);
                break;
            case EnemyState.Chase:
                ChaseBehavior(distanceToPlayer);
                break;
            case EnemyState.Attack:
                AttackBehavior(distanceToPlayer);
                break;
            case EnemyState.Return:
                ReturnBehavior(distanceToPlayer);
                break;
        }
    }

    void PatrolBehavior(float distanceToPlayer)
    {
        // Если игрок в зоне обнаружения - начинаем преследование
        if (distanceToPlayer <= chaseRange)
        {
            currentState = EnemyState.Chase;
            agent.speed = chaseSpeed;
            return;
        }

        // Патрулирование между точками
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            SetNextPatrolPoint();
        }
    }

    void ChaseBehavior(float distanceToPlayer)
    {
        // Прекращаем преследование если игрок слишком далеко
        if (distanceToPlayer > chaseRange * 1.5f)
        {
            currentState = EnemyState.Return;
            agent.speed = patrolSpeed;
            return;
        }

        // Если в зоне атаки - атакуем
        if (distanceToPlayer <= attackRange)
        {
            currentState = EnemyState.Attack;
            agent.isStopped = true;
            return;
        }

        // Продолжаем преследование
        agent.SetDestination(player.position);
    }

    void AttackBehavior(float distanceToPlayer)
    {
        // Смотрим на игрока
        Vector3 direction = (player.position - transform.position).normalized;
        // Здесь можно добавить поворот спрайта

        // Если игрок отошел - продолжаем преследование
        if (distanceToPlayer > attackRange)
        {
            currentState = EnemyState.Chase;
            agent.isStopped = false;
        }
        else
        {
            // Здесь будет логика атаки
            // Можно вызвать метод атаки из monsterController
            Debug.Log("Атакую игрока!");
        }
    }

    void ReturnBehavior(float distanceToPlayer)
    {
        // Возвращаемся к патрулированию если снова увидели игрока
        if (distanceToPlayer <= chaseRange)
        {
            currentState = EnemyState.Chase;
            agent.speed = chaseSpeed;
            return;
        }

        // Возвращаемся на стартовую позицию
        agent.SetDestination(startPosition);

        // Если вернулись - начинаем патрулирование
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentState = EnemyState.Patrol;
            SetNextPatrolPoint();
        }
    }

    void SetNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
    }

    // Визуализация в редакторе
    void OnDrawGizmosSelected()
    {
        // Зона преследования
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        // Зона атаки
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Текущее состояние
        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.white;
#if UNITY_EDITOR
        UnityEditor.Handles.Label(transform.position + Vector3.up * 2, $"State: {currentState}", style);
#endif
    }
}