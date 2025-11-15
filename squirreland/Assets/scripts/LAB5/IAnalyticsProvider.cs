// Интерфейс аналитического провайдера
// Нужен для того, чтобы можно было легко подменить SDK аналитики,
// не меняя основной код игры (например, заменить FakeAnalytics на Firebase).
public interface IAnalyticsProvider
{
    // Инициализация SDK аналитики
    void Initialize(string appKey, string userId);

    // Отправка события о запуске игры
    void TrackGameStart();

    // Событие перехода на уровень
    void TrackLevel(int levelNumber, string difficulty = "normal");

    // Универсальное событие пользователя (клик, действие, просмотр рекламы)
    void TrackUserEvent(string eventName, string value = "");

    // Принудительная отправка накопленных событий
    void Flush();

    // Флаг инициализации SDK
    bool IsInitialized { get; }
}
