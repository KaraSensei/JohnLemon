using UnityEngine; // ����������� ������������ ��� UnityEngine ��� ������ � Unity API
using UnityEngine.SceneManagement; // ����������� ������������ ��� ��� ���������� ������� � Unity

public class GameEnding : MonoBehaviour // ����� ��� ���������� ������ ����, ����������� �� MonoBehaviour
{
    public float fadeDuration = 1f; // ������������ ��������� ����������� (�������)
    public float displayImageDuration = 1f; // ����� ������ ����������� ����� ��������� (�������)
    public GameObject player; // ������ �� ������ ������
    public CanvasGroup exitBackgroundImageCanvasGroup; // CanvasGroup ��� ����������� ������ (������������ ��� �������� ���������)
    public CanvasGroup caughtBackgroundImageCanvasGroup; // CanvasGroup ��� ����������� "������"

    private bool m_IsPlayerCaught = false; // ����: ��� �� ����� ������
    bool m_IsPlayerAtExit; // ����: ��������� �� ����� � ������
    float m_Timer; // ������ ��� ������������ ������� � ������� ��������� ����

    public AudioSource exitAudio; // �������������� ��� ��������������� ������ ������ � ���������� ������
    public AudioSource caughtAudio; // �������������� ��� ��������������� ������ ������ � ���������� ������
    bool m_HasAudioPlayed; // ���� ��� ������������, ���� �� �������������� �����

    void OnTriggerEnter(Collider other) // ����������, ����� ������ ������ � �������-���������
    {
        if (other.gameObject == player) // ���� ����� �����
            m_IsPlayerAtExit = true;    // ������������� ����, ��� ����� � ������
    }

    void Update() // ���������� ������ ����
    {
        if (m_IsPlayerAtExit) // ���� ����� � ������
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio); // ��������� ���������� ������ (�����)
        else if (m_IsPlayerCaught) // ���� ����� ������
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio); // ��������� ���������� ������ (���������)
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audio) // ����� ���������� ������
    {
        if (!m_HasAudioPlayed)
        {
            audio.Play(); // ������������� ����� (����� ��� ������)
            m_HasAudioPlayed = true; // ������������� ����, ��� ����� ��� ��������������
        }

        m_Timer += Time.deltaTime; // ����������� ������ �� �����, ��������� � �������� �����
        imageCanvasGroup.alpha = m_Timer / fadeDuration; // ������ ����������� ������������ ����������� ������

        if (m_Timer > fadeDuration + displayImageDuration) // ���� ������ ���������� ������� ��� ��������� � ������ �����������
        {
            if (doRestart) // ���� ����� ������������� ������� (����� ������)
                SceneManager.LoadScene(0); // ������������� ����� (�������)
            else
                Application.Quit(); // ����� � ������� �� ���������� (����� ��������)
        }
    }

    public void CaughtPlayer() // ����� ��� �������� ������: ����� ������
    {
        m_IsPlayerCaught = true; // ������������� ����, ��� ����� ������
    }
}