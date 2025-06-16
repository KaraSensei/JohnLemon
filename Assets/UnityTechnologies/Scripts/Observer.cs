using UnityEngine; // ����������� ������������ ��� Unity ��� ������ � �������� ��������� � ������������

public class Observer : MonoBehaviour // �����-�����������, ����������� �� MonoBehaviour ��� ���������� � Unity
{
    public Transform player;      // ������ �� ������ ������ (Transform � �������, ��������, �������)
    public GameEnding gameEnding; // ������ �� ���������, ����������� ������ ����

    private bool m_IsPlayerInRange = false; // ����: ��������� �� ����� � ���� ��������� �����������

    void OnTriggerEnter(Collider other) // ����������, ����� ������ ������ ������ � �������-��������� �����������
    {
        if (other.transform == player)      // ���� �������� ������ � �����
            m_IsPlayerInRange = true;       // ������������� ����, ��� ����� � ���� ���������
    }

    void OnTriggerExit(Collider other) // ����������, ����� ������ ������� �� �������-����������
    {
        if (other.transform == player)      // ���� �������� ������ � �����
            m_IsPlayerInRange = false;      // ������� ����, ��� ����� � ���� ���������
    }

    void Update() // ���������� ������ ����
    {
        if (!m_IsPlayerInRange)             // ���� ����� �� � ���� ���������
            return;                         // ��������� ���������� ������

        Vector3 direction = player.position - transform.position + Vector3.up;
        // ��������� ����������� �� ����������� � ������, ������� ������ ����� (Vector3.up)

        Ray ray = new Ray(transform.position, direction);
        // ������ ��� (Ray) �� ������� ����������� � ������� ������

        RaycastHit hit; // ��������� ��� �������� ���������� � ��������� ����

        if (Physics.Raycast(ray, out hit)) // ������� ��� � ���������, �� ��� �� �����
        {
            if (hit.collider.transform == player) // ���� ��� ������ �������� ������
                gameEnding.CaughtPlayer();        // �������� GameEnding, ��� ����� ������
        }
    }
}
