using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour// КЛАСС ДВИЖУЩЕЙСЯ ПЛАТФОРМЫ
{
    public Transform platform; // Сама платформа для движения
    public Transform startPoint;  // Начальная точка пути
    public Transform endPoint;// Конечная точка пути
    public float speed = 1f;// Скорость движения платформы

    int direction = 1; // Направление движения: 1 = к startPoint, -1 = к endPoint

    private void Update()
    {
        Vector2 target = currentMovementTarget();// Получение текущей цели движения

        // Плавное перемещение платформы 
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime); // Плавное движение платформы


        float distance = (target - (Vector2)platform.position).magnitude; // Расчет расстояния до цели

        // Смена направления при достижении точки
        if (distance <= 1f)
        {
            direction *= -1;// Разворот направления движения // Смена направления движения — физическое взаимодействие через движение платформы
        }
    }

    Vector2 currentMovementTarget()   // Определение текущей цели движения
    {
        if (direction == 1)
        {
            return startPoint.position; // Движение к начальной точке
        }
        else
        {
            return endPoint.position; // Движение к конечной точке
        }
    }


    // Визуализация пути движения платформы
    private void OnDrawGizmos()  // Визуализация путей в редакторе
    {
        if (platform != null && startPoint != null && endPoint != null)
        {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }
}
