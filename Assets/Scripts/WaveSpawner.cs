using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Обязательная библиотека для работы с элементами интерфейса, такими как Text.

public class WaveSpawner : MonoBehaviour
{
    // Ссылка на префаб врага, которого мы будем клонировать.
    public Transform enemyPrefab;

    // Ссылка на точку (объект START), где враги будут появляться.
    public Transform spawnPoint;

    // Время в секундах между нападениями волн.
    public float timeBetweenWaves = 5f;

    // Текущее значение таймера. Установили 2f, чтобы дать игроку 2 секунды подготовки перед самой первой волной.
    private float countdown = 2f;

    // Номер текущей волны. Начинаем с 0, чтобы перед спавном он стал 1, и появился ровно 1 враг.
    private int waveNumber = 0;

    // Ссылка на текстовый компонент на Canvas, где будет отображаться обратный отсчет.
    public Text countdownText;

    void Update()
    {
        // Если таймер опустился до нуля или ниже...
        if (countdown <= 0f)
        {
            // ...запускаем корутину (специальный метод, который может делать паузы во времени) для спавна волны врагов.
            StartCoroutine(SpawnWave());

            // Сбрасываем таймер обратно на 5 секунд (или другое значение timeBetweenWaves) для следующей волны.
            countdown = timeBetweenWaves;
        }

        // Каждый кадр отнимаем от таймера время, прошедшее с предыдущего кадра (Time.deltaTime).
        countdown -= Time.deltaTime;

        // Защита для визуала: ограничиваем значение таймера, чтобы он не уходил в минус (0 - это минимум).
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        // Округляем число таймера (Mathf.Round), превращаем его в текст (ToString) и выводим на экран.
        countdownText.text = Mathf.Round(countdown).ToString();
    }

    // IEnumerator - тип возвращаемого значения для корутин. Позволяет использовать команду yield (подождать).
    IEnumerator SpawnWave()
    {
        // При старте новой волны увеличиваем ее номер на 1.
        waveNumber++;

        // Запускаем цикл. Он сработает столько раз, какой сейчас номер волны 
        // (1-я волна = 1 проход = 1 враг, 5-я волна = 5 проходов = 5 врагов).
        for (int i = 0; i < waveNumber; i++)
        {
            // Вызываем метод создания одного врага.
            SpawnEnemy();

            // Делаем паузу в выполнении корутины на 1.5 секунды.
            // Благодаря этому враги не появляются друг в друге разом, а идут красивой цепочкой.
            yield return new WaitForSeconds(1.5f);
        }
    }

    void SpawnEnemy()
    {
        // Instantiate - команда движка для создания объекта на сцене.
        // Передаем ей 3 параметра: что создаем (enemyPrefab), где (spawnPoint.position) и с каким поворотом (spawnPoint.rotation).
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}