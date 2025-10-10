using UnityEngine;
using System.Collections.Generic;

public class TileMapCollider : MonoBehaviour // КЛАСС ДЛЯ СОЗДАНИЯ КАРТЫ ПЛАТФОРМЕРА
{
    void Start()
    {
        // Получаем все дочерние спрайты тайлов
        SpriteRenderer[] tileSprites = GetComponentsInChildren<SpriteRenderer>();

        // Вычисляем общие размеры и центр спрайтов
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        foreach (SpriteRenderer sprite in tileSprites) // Цикл вычисления общих границ всех спрайтов
        {
            minX = Mathf.Min(minX, sprite.bounds.min.x);
            minY = Mathf.Min(minY, sprite.bounds.min.y);
            maxX = Mathf.Max(maxX, sprite.bounds.max.x);
            maxY = Mathf.Max(maxY, sprite.bounds.max.y);
        }

        
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();  // Создание BoxCollider для всей группы тайлов
        collider.size = new Vector3(maxX - minX, maxY - minY, 1);// Установка размера коллайдера по вычисленным границам
        collider.center = new Vector3((maxX + minX) * 0.5f, (maxY + minY) * 0.5f, 0);  // Центрирование коллайдера относительно всех спрайтов

        // Включение режима триггера для проходимости
        collider.isTrigger = true;
    }
}
