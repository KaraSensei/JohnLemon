using UnityEngine;
using UnityEngine.AI;

public class WaypointControl : MonoBehaviour
{
    public NavMeshAgent navMeshAgent; // ������ �� NavMeshAgent, ������� ����� ��������� ��������� �������
    public Transform[] waypoints; // ������ ����� ���� ��� ���������
    int m_CurrentWaypointIndex; // ������ ������� ����� ����, � ������� �������� ������

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        { 
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length; // ������� � ��������� ����� ����, ����������
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position); // ��������� ����� ���� ��� NavMeshAgent
        }
    }
}
