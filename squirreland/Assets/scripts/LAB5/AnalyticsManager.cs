using System.Collections.Generic;
using UnityEngine;
using FakeAnalytics; // Пространство имён SDK аналитики

// Менеджер аналитики — инициализация и управление событиями
// Реализует все пункты лабораторной: события, инициализацию, flush при потере фокуса
public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance;

    private FakeAnalyticsSDK analytics;

    [Header("SDK Настройки")]
    public string appKey = "FakeAppKey_001";
    public string userId = "Player_123";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAnalytics();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ---------------- Инициализация SDK ----------------
    private void InitializeAnalytics()
    {
        analytics = new FakeAnalyticsSDK();
        analytics.Initialize(appKey, userId);
        Debug.Log("[AnalyticsManager] SDK инициализировано с ключом " + appKey);
        TrackGameStart(); // Отправляем событие запуска игры
    }

    // ---------------- События ----------------

    // Событие старта игры
    public void TrackGameStart()
    {
        if (analytics.IsInitialized())
        {
            analytics.TrackGameStartEvent();
            Debug.Log("[AnalyticsManager] Отправлено событие: GameStartEvent");
        }
    }

    // Событие перехода на уровень
    public void TrackLevelEvent(int levelNumber, string difficulty = "normal")
    {
        if (analytics.IsInitialized())
        {
            var data = new Dictionary<string, string>()
            {
                { "level_number", levelNumber.ToString() },
                { "difficulty", difficulty }
            };
            analytics.TrackLevelEvent(levelNumber, data);
            Debug.Log("[AnalyticsManager] Отправлено событие: LevelEvent (уровень " + levelNumber + ")");
        }
    }

    // Событие действия пользователя (например, просмотр рекламы)
    public void TrackUserEvent(string eventName, string value = "")
    {
        if (analytics.IsInitialized())
        {
            var data = new Dictionary<string, string>()
            {
                { "action", eventName },
                { "value", value }
            };
            analytics.TrackEvent("UserEvent", data);
            Debug.Log("[AnalyticsManager] Отправлено событие: UserEvent (" + eventName + ")");
        }
    }

    // ---------------- Flush при потере фокуса ----------------
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus && analytics.IsInitialized())
        {
            analytics.Flush();
            Debug.Log("[AnalyticsManager] Приложение свернуто — события отправлены (Flush)");
        }
    }
}
