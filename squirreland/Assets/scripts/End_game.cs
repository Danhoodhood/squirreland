using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_game : MonoBehaviour
{
    public AudioSource audioSource;       // Фоновый звук или музыка, которую можно поставить на паузу
    public TMP_Text vin_lose;             // Текст для вывода сообщения о победе или поражении
    public TMP_Text score_text;           // Текст для отображения набранных очков

    public int wait;                      // Время (в секундах) до окончания уровня
    public float minValue;                // Минимальное количество очков для успешного прохождения уровня
    public float maxValue;                // (Не используется, возможно для будущей логики)
    public Color minColor;                // Цвет для минимального результата
    public Color maxColor;                // Цвет для максимального результата

    public GameObject button_continue;    // Кнопка "Продолжить"
    public GameObject menu;               // Панель меню, показываемая после окончания уровня
    public GameObject defeat;             // Панель поражения
    public int sceneIndex;                // Индекс следующей сцены в Build Settings

    // Start вызывается при запуске сцены
    void Start()
    {
        Save_progress.Save();             // Сохраняем прогресс игрока
        Time.timeScale = 1;               // Убедимся, что время идёт нормально
        StartCoroutine(SomeCoroutine());  // Запускаем корутину для окончания уровня
    }

    // Корутина, которая ждёт заданное время и завершает уровень
    private IEnumerator SomeCoroutine()
    {
        yield return new WaitForSeconds(wait); // Ждём указанное количество секунд

        audioSource.Pause();               // Приостанавливаем звук/музыку
        menu.SetActive(true);              // Показываем меню
        score_text.text = string.Format("набрано {0} очков", GlobalScore.score); // Выводим очки

        Time.timeScale = 0f;               // Останавливаем время в игре

        // Проверяем, хватило ли очков для успешного прохождения
        if (GlobalScore.score <= minValue)
        {
            defeat.SetActive(true);        // Показываем панель поражения
        }
        else
        {
            SceneManager.LoadScene("Platf_Level1"); // Переходим к следующему уровню (здесь хардкод)
        }
    }

    // Метод для перезапуска текущего уровня
    public void Reload_level()
    {
        GlobalScore.score = 0;                           // Сбрасываем очки
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Загружаем текущую сцену заново
    }

    // Метод для перехода к следующему уровню
    public void Next_level()
    {
        GlobalScore.score = 0;           // Сбрасываем очки
        SceneManager.LoadScene(sceneIndex); // Загружаем сцену по индексу
    }
}
