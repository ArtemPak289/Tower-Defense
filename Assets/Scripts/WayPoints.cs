using UnityEngine;

public class WayPoints : MonoBehaviour
{
    // Делаем массив статичным (static), чтобы любой другой скрипт (например, Enemy) 
    // мог получить к нему доступ напрямую: WayPoints.points, без поиска объекта на сцене.
    public static Transform[] points;

    // Awake вызывается самым первым, при запуске игры (даже до метода Start).
    void Awake()
    {
        // Инициализируем массив. Его размер равен количеству дочерних объектов (childCount) 
        // внутри объекта, на котором висит этот скрипт (Points).
        points = new Transform[transform.childCount];

        // Проходимся циклом по всем дочерним объектам (по всем нашим точкам).
        for (int i = 0; i < points.Length; i++)
        {
            // Записываем каждый дочерний объект в массив.
            // GetChild(i) берет объект по его порядковому номеру в иерархии Unity.
            points[i] = transform.GetChild(i);
        }
    }
}