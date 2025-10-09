using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class Gen_B : MonoBehaviour
{
    public GameObject[] objectToSpawn;     // Массив объектов, которые будут появляться (точки, плитки и т.п.)

    [SerializeField] private float[] wait; // Время ожидания перед каждым спавном
    [SerializeField] private float[] speed; // Базовая скорость для каждого появления

    [SerializeField] public float varriable_speed = 17f; // Общий множитель скорости (можно менять для ускорения игры)

    // Start вызывается при запуске сцены
    void Start()
    {
        // Запускаем корутину, которая поочередно создаёт объекты
        StartCoroutine(Create_dots());
    }

    // Корутина, создающая объекты с задержками
    IEnumerator Create_dots()
    {
        // Перебираем массив задержек
        for (int i = 0; i < wait.Length; i++)
        {
            // Ждём перед следующим спавном
            yield return new WaitForSeconds(wait[i]);

            // Перебираем все объекты в списке, ищем неактивный
            foreach (GameObject obj in objectToSpawn)
            {
                if (!obj.activeSelf) // Если объект выключен — активируем
                {
                    // Перемещаем объект в позицию генератора (позиция этого объекта)
                    obj.transform.position = transform.position;

                    // Назначаем скорость движущемуся объекту (умножаем на множитель)
                    obj.GetComponent<Move_obj>().speed = speed[i] * varriable_speed;

                    // Активируем объект
                    obj.SetActive(true);

                    // Восстанавливаем прозрачность (если объект использовался ранее и был полупрозрачным)
                    var sr = obj.GetComponent<Image>();
                    Color color = sr.color;
                    color.a = 1f;
                    sr.color = color;

                    // Прерываем цикл, чтобы заспавнить только один объект за итерацию
                    break;
                }
            }
        }
    }
}
