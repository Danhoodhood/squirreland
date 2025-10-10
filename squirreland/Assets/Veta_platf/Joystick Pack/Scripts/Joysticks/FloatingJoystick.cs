using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick // КЛАСС ПЛАВАЮЩЕГО ДЖОЙСТИКА С ФИКСИРОВАННЫМ ОСНОВАНИЕМ
{
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false); // Скрытие фона при старте
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
}