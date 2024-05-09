using UnityEngine;
using System.Collections.Generic;

public class TileMapCollider : MonoBehaviour
{
    void Start()
    {
        // Получаем все дочерние спрайты
        SpriteRenderer[] tileSprites = GetComponentsInChildren<SpriteRenderer>();

        // Вычисляем общие размеры и центр спрайтов
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        foreach (SpriteRenderer sprite in tileSprites)
        {
            minX = Mathf.Min(minX, sprite.bounds.min.x);
            minY = Mathf.Min(minY, sprite.bounds.min.y);
            maxX = Mathf.Max(maxX, sprite.bounds.max.x);
            maxY = Mathf.Max(maxY, sprite.bounds.max.y);
        }

        // Создаем BoxCollider и настраиваем его размер и смещение
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(maxX - minX, maxY - minY, 1);
        collider.center = new Vector3((maxX + minX) * 0.5f, (maxY + minY) * 0.5f, 0);

        // Включаем режим триггера
        collider.isTrigger = true;
    }
}
