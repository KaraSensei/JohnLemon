using UnityEngine;
using UnityEngine.AI;

public class WaypointControl : MonoBehaviour
{
    public NavMeshAgent navMeshAgent; // Ссылка на NavMeshAgent, который будет управлять движением объекта
    public Transform[] waypoints; // Массив точек пути для навигации
    int m_CurrentWaypointIndex; // Индекс текущей точки пути, к которой движется объект

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
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length; // Переход к следующей точке пути, циклически
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position); // Установка новой цели для NavMeshAgent
        }
    }
}
