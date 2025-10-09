using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Check_click : MonoBehaviour
{
    public AudioSource audioSourcePoint; // Звук, который проигрывается при успешном клике

    public void OnClick()
    {
        // Находим все объекты с тегом "point" (точки, по которым нужно нажимать)
        GameObject[] dots = GameObject.FindGameObjectsWithTag("point");

        bool liy = true; // Флаг, указывающий, было ли попадание по точке (true — промах)

        // Если точек нет на сцене, значит кликнули вхолостую — штрафуем игрока
        if (dots is null)
        {
            GetComponent<Image>().color = Color.black; // Меняем цвет кнопки на чёрный — визуальный сигнал промаха
            StartCoroutine(ResetColorAfterDelay()); // Возвращаем цвет позже
            GlobalScore.score--; // Уменьшаем счёт
        }

        // Проверяем пересечение кнопки с каждой активной точкой
        foreach (GameObject i in dots)
        {
            // Получаем размеры кнопки
            RectTransform rt = GetComponent<RectTransform>();
            float width1 = rt.sizeDelta.x * rt.localScale.x / 2f;
            float height1 = rt.sizeDelta.y * rt.localScale.y / 2f;

            // Получаем размеры точки
            RectTransform rt2 = i.GetComponent<RectTransform>();
            float width2 = rt2.sizeDelta.x * rt2.localScale.x / 2f;
            float height2 = rt2.sizeDelta.y * rt2.localScale.y / 2f;

            // Проверяем, пересекаются ли области кнопки и точки по координатам
            if (transform.position.x - width1 - width2 <= i.transform.position.x &&
                i.transform.position.x <= transform.position.x + width1 + width2 &&
                transform.position.y - height1 - height2 <= i.transform.position.y &&
                i.transform.position.y <= transform.position.y + height1 + height2)
            {
                // Если клик попал по точке:
                var sr = i.GetComponent<Image>(); // Берём изображение точки
                Color color = sr.color;
                color.a = 1f; // Делаем её полностью непрозрачной
                sr.color = color;

                GlobalScore.score++; // Увеличиваем счёт
                i.SetActive(false); // Скрываем точку
                liy = false; // Отмечаем, что попадание было успешно

                GetComponent<Image>().color = Color.yellow; // Подсвечиваем кнопку жёлтым цветом (успех)
                audioSourcePoint.Play(); // Проигрываем звук успешного клика
                StartCoroutine(ResetColorAfterDelay()); // Возвращаем цвет через 1 секунду
            }
        }

        // Если по точке не попали — промах
        if (liy)
        {
            GetComponent<Image>().color = Color.black; // Цвет чёрный — промах
            StartCoroutine(ResetColorAfterDelay());
            GlobalScore.score--; // Отнимаем очко
        }

        // Не позволяем счёту быть отрицательным
        if (GlobalScore.score < 0)
        {
            GlobalScore.score = 0;
        }

        // Обновляем текст счёта на экране
        TMP_Text str_score = GameObject.FindGameObjectWithTag("score").GetComponent<TMP_Text>();
        str_score.text = GlobalScore.score.ToString();
    }

    // Корутина для восстановления исходного цвета кнопки через 1 секунду
    IEnumerator ResetColorAfterDelay()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Image>().color = Color.white; // Возвращаем стандартный цвет (предположительно белый)
    }
}
