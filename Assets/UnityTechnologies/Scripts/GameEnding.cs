using UnityEngine; // Подключение пространства имён UnityEngine для работы с Unity API
using UnityEngine.SceneManagement; // Подключение пространства имён для управления сценами в Unity

public class GameEnding : MonoBehaviour // Класс для управления концом игры, наследуется от MonoBehaviour
{
    public float fadeDuration = 1f; // Длительность затухания изображения (секунды)
    public float displayImageDuration = 1f; // Время показа изображения после затухания (секунды)
    public GameObject player; // Ссылка на объект игрока
    public CanvasGroup exitBackgroundImageCanvasGroup; // CanvasGroup для изображения выхода (используется для плавного появления)
    public CanvasGroup caughtBackgroundImageCanvasGroup; // CanvasGroup для изображения "пойман"

    private bool m_IsPlayerCaught = false; // Флаг: был ли игрок пойман
    bool m_IsPlayerAtExit; // Флаг: находится ли игрок у выхода
    float m_Timer; // Таймер для отслеживания времени с момента окончания игры

    void OnTriggerEnter(Collider other) // Вызывается, когда объект входит в триггер-коллайдер
    {
        if (other.gameObject == player) // Если вошёл игрок
            m_IsPlayerAtExit = true;    // Устанавливаем флаг, что игрок у выхода
    }

    void Update() // Вызывается каждый кадр
    {
        if (m_IsPlayerAtExit) // Если игрок у выхода
            EndLevel(exitBackgroundImageCanvasGroup, false); // Запускаем завершение уровня (выход)
        else if (m_IsPlayerCaught) // Если игрок пойман
            EndLevel(caughtBackgroundImageCanvasGroup, true); // Запускаем завершение уровня (поражение)
    }

    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart) // Метод завершения уровня
    {
        m_Timer += Time.deltaTime; // Увеличиваем таймер на время, прошедшее с прошлого кадра
        imageCanvasGroup.alpha = m_Timer / fadeDuration; // Плавно увеличиваем прозрачность изображения выхода

        if (m_Timer > fadeDuration + displayImageDuration) // Если прошло достаточно времени для затухания и показа изображения
        {
            if (doRestart) // Если нужно перезапустить уровень (игрок пойман)
                SceneManager.LoadScene(0); // Перезагружаем сцену (уровень)
            else
                Application.Quit(); // Иначе — выходим из приложения (игрок выбрался)
        }
    }

    public void CaughtPlayer() // Метод для внешнего вызова: игрок пойман
    {
        m_IsPlayerCaught = true; // Устанавливаем флаг, что игрок пойман
    }
}