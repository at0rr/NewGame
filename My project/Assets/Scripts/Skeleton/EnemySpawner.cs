using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // префаб скелета
    [SerializeField] private float cameraSafeRadius = 14f; // радиус, чтобы враги спавнились за камерой
    [SerializeField] private float spawnInterval = 2.5f;
    [SerializeField] private Vector2 mapBounds = new Vector2(50, 50); // размер карты

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemiesRoutine());
    }

    private IEnumerator SpawnEnemiesRoutine()// данная штука умеет останавливать цикл на
    // время (надо для спавна каждые 2.5 секунды)
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval); // эта штука как раз заставляет цикл ждать до следующего обновления
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPos;

        do
        {
            spawnPos = new Vector2(Random.Range(-mapBounds.x / 2, mapBounds.x / 2),
            Random.Range(-mapBounds.y / 2, mapBounds.y / 2));
        } while (IsPointInCameraView(spawnPos)); // повторяем рандом, пока враг не будет спавниться за пределами камеры

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity /*смотрит в стандартном направлении*/);
        // создаём врага
    }

    private bool IsPointInCameraView(Vector3 point) // для проверки на то, что точка находится в камере
    {
        Vector2 cameraPos = mainCamera.transform.position;

        float distance = Vector2.Distance(point, cameraPos); // расстояние от точки до камеры

        return distance < cameraSafeRadius; // если расстояние меньше безопасного радиуса, точка в зоне камеры
    }
}
