using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{


    [SerializeField] private float playerSpeed = 5f;
    // благодаря [SerializeField] можно менять переменную в самом движке, а не коде


    private Rigidbody2D rb; // эта штука для перемещения объектов (аналог БЕГАЮЩЕГО крестоносца)

    private float minSpeed = 0.0f;
    private bool isRunning = false;

    public static Player Instance { get; private set; }
    private void Awake() // запускается до Start() 1 раз, ею мы инициализируем компоненты
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() // эта функция запускается каждый кадр в игре
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 Grid = GameInput.Instance.GetMoveVector(); // сетка поля
        Grid = Grid.normalized; // теперь не будет буста скорости при движении по диагонали
        rb.MovePosition(rb.position + Grid * (playerSpeed * Time.fixedDeltaTime));
        // Time.fixedDeltaTime - можно воспринимать как константу (она равна 0.02)
        // умножаем это всё для корректировки скорости

        if (Mathf.Abs(Grid.x) > minSpeed || Mathf.Abs(Grid.y) > minSpeed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public bool IsRunning() => isRunning;

    public Vector3 GetPlayerPosition() // для сравнения мыши с персонажем, чтобы он смотрел в сторону мыши
    {
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
        return playerPos;
    }
}
