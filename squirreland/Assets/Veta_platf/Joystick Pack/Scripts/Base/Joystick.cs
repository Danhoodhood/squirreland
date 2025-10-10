using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


// КЛАСС ВИРТУАЛЬНОГО ДЖОЙСТИКА ДЛЯ МОБИЛЬНОГО УПРАВЛЕНИЯ
public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // Свойства для получения значений джойстика
    public float Horizontal { get { return (snapX) ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x; } }
    public float Vertical { get { return (snapY) ? SnapFloat(input.y, AxisOptions.Vertical) : input.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    public float HandleRange // Настройки диапазона и мертвой зоны джойстика
    {
        get { return handleRange; }
        set { handleRange = Mathf.Abs(value); }
    }

    public float DeadZone // Настройки осей и привязки
    {
        get { return deadZone; }
        set { deadZone = Mathf.Abs(value); }
    }

    public AxisOptions AxisOptions { get { return AxisOptions; } set { axisOptions = value; } }
    public bool SnapX { get { return snapX; } set { snapX = value; } }
    public bool SnapY { get { return snapY; } set { snapY = value; } }

    [SerializeField] private float handleRange = 1; // Радиус движения ручки
    [SerializeField] private float deadZone = 0;// Мертвая зона в центре
    [SerializeField] private AxisOptions axisOptions = AxisOptions.Both;// Активные оси
    [SerializeField] private bool snapX = false;// Привязка по горизонтали
    [SerializeField] private bool snapY = false;// Привязка по вертикали

    [SerializeField] protected RectTransform background = null; // Фон джойстика
    [SerializeField] private RectTransform handle = null;// Ручка джойстика
    private RectTransform baseRect = null;// Основной RectTransform


    private Canvas canvas;
    private Camera cam;

    private Vector2 input = Vector2.zero; // Входные данные джойстика

    protected virtual void Start()
    {
        HandleRange = handleRange;
        DeadZone = deadZone;
        baseRect = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            Debug.LogError("The Joystick is not placed inside a canvas");


        // Центрирование элементов джойстика

        Vector2 center = new Vector2(0.5f, 0.5f);
        background.pivot = center;
        handle.anchorMin = center;
        handle.anchorMax = center;
        handle.pivot = center;
        handle.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData eventData)// Обработка нажатия на джойстик
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)     // Обработка перемещения пальца по джойстику

    {
        cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            cam = canvas.worldCamera;

        // Конвертация позиции в локальные координаты
        Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
        Vector2 radius = background.sizeDelta / 2;
        input = (eventData.position - position) / (radius * canvas.scaleFactor);
        FormatInput();
        HandleInput(input.magnitude, input.normalized, radius, cam);
        handle.anchoredPosition = input * radius * handleRange;
    }

    // Обработка входных данных с учетом мертвой зоны
    protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
                input = normalised;
        }
        else
            input = Vector2.zero;
    }


    // Форматирование ввода согласно выбранным осям
    private void FormatInput()
    {
        if (axisOptions == AxisOptions.Horizontal) 
            input = new Vector2(input.x, 0f);// Только горизонтальное движение
        else if (axisOptions == AxisOptions.Vertical)
            input = new Vector2(0f, input.y);// Только вертикальное движение
    }


    // Привязка значений к дискретным позициям
    private float SnapFloat(float value, AxisOptions snapAxis)
    {
        if (value == 0)
            return value;

        if (axisOptions == AxisOptions.Both)
        {
            float angle = Vector2.Angle(input, Vector2.up);
            if (snapAxis == AxisOptions.Horizontal)
            {
                if (angle < 22.5f || angle > 157.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            else if (snapAxis == AxisOptions.Vertical)
            {
                if (angle > 67.5f && angle < 112.5f)
                    return 0;
                else
                    return (value > 0) ? 1 : -1;
            }
            return value;
        }
        else
        {
            if (value > 0)
                return 1;
            if (value < 0)
                return -1;
        }
        return 0;
    }

    public virtual void OnPointerUp(PointerEventData eventData)  // Сброс джойстика при отпускании
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }

    protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition) // Сброс джойстика при отпускании
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
        {
            Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
            return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
        }
        return Vector2.zero;
    }
}

public enum AxisOptions { Both, Horizontal, Vertical }// Перечисление доступных осей управления