using UnityEngine;

// Главный менеджер аналитики.
// Хранится между сценами (DontDestroyOnLoad)
// и работает через интерфейс IAnalyticsProvider.
public class AnalyticsManager : MonoBehaviour
{
    // Глобальный доступ (Singleton)
    public static AnalyticsManager Instance;

    // Текущий провайдер аналитики (может быть любой SDK)
    private IAnalyticsProvider provider;

    [Header("SDK Настройки")]
    public string appKey = "FakeAppKey_001"; // Ключ SDK
    public string userId = "Player_123";     // ID игрока

    private void Awake()
    {
        // Стандартная реализация Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Инициализируем провайдер
            provider = new FakeAnalyticsProvider();
            provider.Initialize(appKey, userId);

            // Отправляем событие старта игры
            TrackGameStart();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ------------------- События -------------------

    // Событие начала игры
    public void TrackGameStart()
    {
        if (provider.IsInitialized)
        {
            provider.TrackGameStart();
            Debug.Log("[Analytics] GameStartEvent sent");
        }
    }

    // Событие перехода на уровень
    public void TrackLevelEvent(int levelNumber, string difficulty = "normal")
    {
        if (provider.IsInitialized)
        {
            provider.TrackLevel(levelNumber, difficulty);
            Debug.Log("[Analytics] LevelEvent sent (Level " + levelNumber + ")");
        }
    }

    // Пользовательские события (клики, кнопки и т.д.)
    public void TrackUserEvent(string eventName, string value = "")
    {
        if (provider.IsInitialized)
        {
            provider.TrackUserEvent(eventName, value);
            Debug.Log("[Analytics] UserEvent: " + eventName);
        }
    }

    // ------------------- Flush при потере фокуса -------------------
    // Когда приложение сворачивается — отправляем накопленные события
    private void OnApplicationFocus(bool focus)
    {
        // Если окно потеряло фокус — делаем flush
        if (!focus && provider.IsInitialized)
        {
            provider.Flush();
            Debug.Log("[Analytics] Flush executed (focus lost)");
        }
    }
}
