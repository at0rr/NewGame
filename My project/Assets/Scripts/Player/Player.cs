using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[SelectionBase] // для того, чтобы при перетаскивании в движке перса, перетаскивался не только спрайт

public class Player : MonoBehaviour
{


    [SerializeField] private float playerSpeed = 5f;
    // благодаря [SerializeField] можно менять переменную в самом движке, а не коде
    private Vector2 supGrid;
    Vector2 Grid;


    private Rigidbody2D rb; // эта штука для перемещения объектов (аналог БЕГАЮЩЕГО крестоносца)

    private float minSpeed = 0.0f;
    private bool isRunning = false;

    public static Player Instance { get; private set; }
    private void Awake() // запускается до Start() 1 раз, ею мы инициализируем компоненты
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GameInput.Instance.OnPlayerAttack += Player_OnPlayerAttack;
    }

    private void Player_OnPlayerAttack(object sender, System.EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack(); // вызываем активное оружее и выполняем атаку
    }

    private void Update() {
        Grid = GameInput.Instance.GetMoveVector(); // сетка движения персонажа
        //Grid = Grid.normalized; // теперь не будет буста скорости при движении по диагонали
        // добавил в движке движение стрелками, это автоматически нормализовало ходьбу, поэтому эта строка теперь не нужна
    }

    private void FixedUpdate() // эта функция запускается каждый кадр в игре
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        rb.MovePosition(rb.position + Grid * (playerSpeed * Time.fixedDeltaTime));
        // Time.fixedDeltaTime - можно воспринимать как константу (она равна 0.02)
        // умножаем это всё для корректировки скорости
        supGrid = Grid;

        if (Mathf.Abs(Grid.x) > minSpeed || Mathf.Abs(Grid.y) > minSpeed)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    public Vector2 GetGrid() => supGrid;

    public bool IsRunning() => isRunning;

    // public Vector3 GetPlayerPosition() // для сравнения мыши с персонажем, чтобы он смотрел в сторону мыши
    // {
    //     // я это используя для оружия
    //     Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);
    //     return playerPos;
    // }
}
