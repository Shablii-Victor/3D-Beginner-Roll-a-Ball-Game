using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent navMeshAgent;

    [Header("Ќалаштуванн€ ворога")]
    public float delayBeforeChase = 3f;   // затримка перед початком пересл≥дуванн€
    public float moveSpeed = 3.5f;        // швидк≥сть пересл≥дуванн€

    private bool isChasing = false;       // чи ворог вже почав гнатис€
    private float timer = 0f;             // л≥чильник часу

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 0f; // —початку ворог стоњть
    }

    void Update()
    {
        if (player == null) return;

        // якщо ще не почав погоню Ч чекаЇмо
        if (!isChasing)
        {
            timer += Time.deltaTime;

            if (timer >= delayBeforeChase)
            {
                isChasing = true;
                navMeshAgent.speed = moveSpeed; // встановлюЇмо швидк≥сть
            }
        }

        // якщо вже почав Ч ган€Їтьс€ за гравцем
        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
