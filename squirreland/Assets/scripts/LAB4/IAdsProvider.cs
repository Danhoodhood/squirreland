using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Интерфейс рекламного провайдера
// В лабораторной задаче требуется использовать абстракцию провайдера рекламы
// Это позволяет:
// 1) Легко менять SDK рекламы (разные платформы/SDK),
// 2) Не ломать игровую логику,
// 3) Обрабатывать события рекламы централизованно
public interface IAdsProvider
{
    void Initialize();         // Метод инициализации рекламного SDK
    void ShowBanner();         // Показ баннера
    void ShowInterstitial();   // Показ межстраничной рекламы (interstitial)
    void ShowRewarded();       // Показ вознаграждаемой рекламы (rewarded)

    // События, которые требуется отслеживать по заданию лабораторной
    event System.Action OnAdLoaded;     // Объявление успешно загружено
    event System.Action OnAdFailedToLoad; // Ошибка загрузки
    event System.Action OnAdShown;      // Реклама начата
    event System.Action OnAdClicked;    // Игрок кликнул на рекламу
    event System.Action OnAdDismissed;  // Реклама закрыта
    event System.Action OnAdRewarded;   // Вознаграждение за просмотр получено
}
