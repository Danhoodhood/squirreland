using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_dice : MonoBehaviour
{
    public float leftEdge;
    public float rightEdge;

    // Скорость движения кнопки
    public float speed = 1.0f;

    public void Start()
    {
        // Получаем ссылки на левый и правый края канваса
        leftEdge = Screen.safeArea.xMin + transform.localScale.x;
        rightEdge = Screen.safeArea.width - Screen.safeArea.xMax - transform.localScale.x;

        // Устанавливаем начальную позицию кнопки
        transform.position = new Vector3(leftEdge + (GetComponent<RectTransform>().sizeDelta.x / 2), transform.position.y, transform.position.z);
    }

    public void Update()
    {
        // Проверяем, движется ли кнопка влево или вправо
        if (transform.position.x < leftEdge)
        {
            MoveButtonRight();
        }
        else if (transform.position.x > rightEdge)
        {
            MoveButtonLeft();
        }
        
    }

    public void MoveButtonRight()
    {
        // Перемещаем кнопку вправо
        transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
    }

    public void MoveButtonLeft()
    {
        // Перемещаем кнопку влево
        transform.position -= new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
    }
}
