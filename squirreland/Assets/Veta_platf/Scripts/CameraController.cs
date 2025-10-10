using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour // КОНТРОЛЛЕР КАМЕРЫ
{
    [SerializeField] private Transform player; // Ссылка на игрока
    private Vector3 pos;// Временная позиция камеры


    private void Awake()
    {
        if(!player)  // Автопоиск игрока
        {

            player = FindObjectOfType<Player>().transform; 
        }
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        pos = player.position;// Позиция игрока
        pos.z = -10f;// Фиксируем Z для 2D камеры
        pos.y += 1f;// Смещение камеры вверх

        // Плавное перемещение камеры к игроку
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime);
    }
}
