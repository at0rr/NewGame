using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float platerSpeed = 5f;
    // благодаря [SerializeField] можно менять переменную в самом движке, а не коде


    private Rigidbody2D rb; // эта штука для перемещения объектов (аналог БЕГАЮЩЕГО крестоносца)

    private void Awake() // запускается до Start() 1 раз, ею мы инициализируем компоненты
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start() // запускается до Update() 1 раз, ею мы запускаем компоненты
    {

    }

    private void FixedUpdate() // эта функция запускается каждый кадр в игре
    {
        Vector2 Grid = GameInput.Instance.GetMoveVector(); // сетка поля

        Grid = Grid.normalized; // теперь не будет буста скорости при движении по диагонали

        rb.MovePosition(rb.position + Grid * (platerSpeed * Time.fixedDeltaTime));
        // Time.fixedDeltaTime - можно воспринимать как константу (она равна 0.02)
        // умножаем это всё для корректировки скорости
    }
}
