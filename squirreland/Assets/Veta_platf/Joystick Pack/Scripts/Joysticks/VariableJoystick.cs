using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VariableJoystick : Joystick // КЛАСС УНИВЕРСАЛЬНОГО ДЖОЙСТИКА С ВЫБОРОМ РЕЖИМОВ
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;// Порог перемещения для динамического режима
    [SerializeField] private JoystickType joystickType = JoystickType.Fixed;// Текущий тип джойстика


    private Vector2 fixedPosition = Vector2.zero;// Фиксированная позиция основания


    public void SetMode(JoystickType joystickType) // Метод смены режима работы джойстика
    {
        this.joystickType = joystickType;
        if(joystickType == JoystickType.Fixed)
        {
            // Фиксированный режим - показ фона в начальной позиции
            background.anchoredPosition = fixedPosition;
            background.gameObject.SetActive(true);
        }
        else
            // Плавающий и динамический режимы - скрытие фона
            background.gameObject.SetActive(false);
    }

    protected override void Start()
    {
        base.Start();
        fixedPosition = background.anchoredPosition;// Сохранение начальной позиции
        SetMode(joystickType);// Установка выбранного режима
    }

    public override void OnPointerDown(PointerEventData eventData)// Обработка нажатия на джойстик
    {
        if(joystickType != JoystickType.Fixed)
        {
            // Для нефиксированных режимов - установка фона в точку касания
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
        }
        base.OnPointerDown(eventData);// Вызов базовой логики нажатия
    }

    public override void OnPointerUp(PointerEventData eventData)// Обработка отпускания джойстика
    {

        if(joystickType != JoystickType.Fixed)// Для нефиксированных режимов - скрытие фона
            background.gameObject.SetActive(false);

        base.OnPointerUp(eventData);// Вызов базовой логики отпускания
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        // Обработка входных данных джойстика с учетом режима
    {
        if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
            // Для динамического режима - перемещение основания при превышении порога
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}

public enum JoystickType { Fixed, Floating, Dynamic }// Перечисление доступных типов джойстика