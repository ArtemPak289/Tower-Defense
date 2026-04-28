using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Скорость передвижения врага. Модификатор public позволяет настраивать ее прямо в инспекторе.
    public float speed = 10f;

    // Переменная для хранения текущей цели (точки), к которой сейчас идет враг.
    private Transform target;

    // Индекс текущей точки в массиве WayPoints.points. Начинаем с 0 (первая точка).
    private int wavePointIndex = 0;

    void Start()
    {
        // Защита от ошибок: если точек на карте нет, прерываем выполнение, 
        // чтобы игра не вылетела с ошибкой (NullReferenceException).
        if (WayPoints.points == null || WayPoints.points.Length == 0) return;

        // При появлении врага его первой целью становится самая первая точка маршрута (под индексом 0).
        target = WayPoints.points[0];
    }

    void Update()
    {
        // Если цели нет (например, массив пуст), скрипт просто ничего не делает в этом кадре.
        if (target == null) return;

        // Вычисляем вектор направления: отнимаем от позиции цели позицию самого врага.
        Vector3 dir = target.position - transform.position;

        // Двигаем врага. 
        // dir.normalized - делает длину вектора направления равной 1 (чтобы скорость не зависела от расстояния до точки).
        // Умножаем на speed для скорости и на Time.deltaTime, чтобы движение не зависело 
        // от частоты кадров (FPS), а было плавным.
        // Space.World означает, что мы двигаемся в глобальных координатах сцены.
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        // Проверяем дистанцию между врагом и текущей точкой-целью.
        // Если расстояние меньше или равно 0.4 юнита (враг почти наступил на точку)...
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            // ...вызываем метод для переключения на следующую точку маршрута.
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        // Проверяем, не дошли ли мы до последней точки.
        // Length - 1 — это индекс самого последнего элемента в массиве.
        // Если текущий индекс больше или равен последнему, значит путь окончен.
        if (wavePointIndex >= WayPoints.points.Length - 1)
        {
            // Уничтожаем игровой объект врага (он дошел до конца карты).
            Destroy(gameObject);

            // return прерывает выполнение метода, чтобы код ниже не попытался найти несуществующую точку.
            return;
        }

        // Увеличиваем индекс на 1 (переходим к следующей по счету точке).
        wavePointIndex++;

        // Обновляем цель на новую точку из массива.
        target = WayPoints.points[wavePointIndex];
    }
}