using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : Joystick// КЛАСС ДИНАМИЧЕСКОГО ДЖОЙСТИКА С ПЕРЕМЕЩЕНИЕМ ОСНОВАНИЯ
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1; // Порог перемещения основания

    protected override void Start()
    {
        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);// Скрытие фона при старте
    }

    // Активация джойстика в месте касания
    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);// Показ фона при касании
        base.OnPointerDown(eventData);
    }

    // Скрытие джойстика при отпускании
    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);// Скрытие фона
        base.OnPointerUp(eventData);
    }

    // Перемещение основания джойстика при выходе за порог
    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;// Сдвиг основания
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}