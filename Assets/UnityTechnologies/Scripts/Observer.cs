using UnityEngine; // Подключение пространства имён Unity для работы с игровыми объектами и компонентами

public class Observer : MonoBehaviour // Класс-наблюдатель, наследуется от MonoBehaviour для интеграции с Unity
{
    public Transform player;      // Ссылка на объект игрока (Transform — позиция, вращение, масштаб)
    public GameEnding gameEnding; // Ссылка на компонент, управляющий концом игры

    private bool m_IsPlayerInRange = false; // Флаг: находится ли игрок в зоне видимости наблюдателя

    void OnTriggerEnter(Collider other) // Вызывается, когда другой объект входит в триггер-коллайдер наблюдателя
    {
        if (other.transform == player)      // Если вошедший объект — игрок
            m_IsPlayerInRange = true;       // Устанавливаем флаг, что игрок в зоне видимости
    }

    void OnTriggerExit(Collider other) // Вызывается, когда объект выходит из триггер-коллайдера
    {
        if (other.transform == player)      // Если вышедший объект — игрок
            m_IsPlayerInRange = false;      // Снимаем флаг, что игрок в зоне видимости
    }

    void Update() // Вызывается каждый кадр
    {
        if (!m_IsPlayerInRange)             // Если игрок не в зоне видимости
            return;                         // Прерываем выполнение метода

        Vector3 direction = player.position - transform.position + Vector3.up;
        // Вычисляем направление от наблюдателя к игроку, немного смещая вверх (Vector3.up)

        Ray ray = new Ray(transform.position, direction);
        // Создаём луч (Ray) из позиции наблюдателя в сторону игрока

        RaycastHit hit; // Структура для хранения информации о попадании луча

        if (Physics.Raycast(ray, out hit)) // Пускаем луч и проверяем, во что он попал
        {
            if (hit.collider.transform == player) // Если луч первым встретил игрока
                gameEnding.CaughtPlayer();        // Сообщаем GameEnding, что игрок пойман
        }
    }
}
