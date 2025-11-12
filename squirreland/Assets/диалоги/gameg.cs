using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameg : MonoBehaviour
{
    public GameObject startbutton;
    public string namelvl;

    // При входе в триггер появляется кнопка "Старт"
    void OnTriggerEnter2D(Collider2D col)
    {
        startbutton.SetActive(true); // Игрок вошёл в зону триггера — показываем кнопку "Старт"
    }

    // При выходе из триггера — скрываем кнопку
    void OnTriggerExit2D(Collider2D col)
    {
        startbutton.SetActive(false);  // Игрок вышел из зоны — скрываем кнопку
    }

    // При нажатии кнопки — сначала показываем рекламу, потом загружаем уровень
    public void startgame()
    {
        // Проверяем, есть ли AdsManager
        if (AdsManager.Instance != null)
        {
            // Показываем рекламу и переходим после закрытия
            AdsManager.Instance.ShowInterstitial(() =>
            {
                Debug.Log("[GameG] Реклама завершена, загружаем сцену: " + namelvl);
                SceneManager.LoadScene(namelvl);
            });
        }
        else
        {
            // Если рекламы нет — просто загружаем сцену
            SceneManager.LoadScene(namelvl);
        }
    }
}
