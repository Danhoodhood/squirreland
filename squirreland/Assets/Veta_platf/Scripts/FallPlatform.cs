using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour // КЛАСС ПАДАЮЩЕЙ ПЛАТФОРМЫ
{
    private Rigidbody2D _myRigidbody;// Физическое тело платформы
    private float _collapseTime = 0.5f; // Установите желаемое время для коллапса платформы

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>(); 
        _myRigidbody.isKinematic = true;// Отключение физики в начале
        _myRigidbody.gravityScale = 0f;// Отключение гравитации
    }

    private void OnCollisionEnter2D(Collision2D collision) // Обработка столкновения с игроком
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Collapse());// Запуск падения
        }
    }

    private IEnumerator Collapse()// Корутина задержки перед падением
    {
        yield return new WaitForSeconds(_collapseTime);// Ожидание установленного времени
        FallDown();
    }

    private void FallDown()
    {
        _myRigidbody.isKinematic = false; // Включение физического взаимодействия
        _myRigidbody.gravityScale = 1f;// Включение гравитации
    }
}
