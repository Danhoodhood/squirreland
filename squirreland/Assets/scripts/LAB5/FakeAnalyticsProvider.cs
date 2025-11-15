using System.Collections.Generic;
using FakeAnalytics;
using UnityEngine;

// Провайдер FakeAnalytics — обёртка над FakeAnalyticsSDK
// Благодаря этому SDK можно легко заменить на другой.
public class FakeAnalyticsProvider : IAnalyticsProvider
{
    // Внутренний объект SDK
    private FakeAnalyticsSDK sdk = new FakeAnalyticsSDK();

    // Свойство показывает, инициализирована ли аналитика
    public bool IsInitialized => sdk.IsInitialized();

    // Инициализация SDK аналитики
    public void Initialize(string appKey, string userId)
    {
        sdk.Initialize(appKey, userId);
        Debug.Log("[FakeAnalyticsProvider] SDK initialized");
    }

    // Событие запуска игры
    public void TrackGameStart()
    {
        sdk.TrackGameStartEvent();
    }

    // Событие перехода на уровень + параметры
    public void TrackLevel(int levelNumber, string difficulty = "normal")
    {
        var data = new Dictionary<string, string>()
        {
            { "difficulty", difficulty }
        };

        sdk.TrackLevelEvent(levelNumber, data);
    }

    // Универсальное пользовательское событие
    public void TrackUserEvent(string eventName, string value = "")
    {
        var data = new Dictionary<string, string>()
        {
            { "action", eventName },
            { "value", value }
        };

        sdk.TrackEvent("UserEvent", data);
    }

    // Принудительная отправка событий на сервер
    public void Flush()
    {
        sdk.Flush();
    }
}
