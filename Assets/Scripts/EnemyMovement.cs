using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent navMeshAgent;

    [Header("������������ ������")]
    public float delayBeforeChase = 3f;   // �������� ����� �������� �������������
    public float moveSpeed = 3.5f;        // �������� �������������

    private bool isChasing = false;       // �� ����� ��� ����� �������
    private float timer = 0f;             // �������� ����

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = 0f; // �������� ����� �����
    }

    void Update()
    {
        if (player == null) return;

        // ���� �� �� ����� ������ � ������
        if (!isChasing)
        {
            timer += Time.deltaTime;

            if (timer >= delayBeforeChase)
            {
                isChasing = true;
                navMeshAgent.speed = moveSpeed; // ������������ ��������
            }
        }

        // ���� ��� ����� � ��������� �� �������
        if (isChasing)
        {
            navMeshAgent.SetDestination(player.position);
        }
    }
}
