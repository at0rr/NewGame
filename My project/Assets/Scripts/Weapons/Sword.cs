using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 5;

    public event EventHandler OnSwordSwing; // событие для того, чтобы система поняла, что пора взмахнуть мечом

    private PolygonCollider2D _polygonCollider2D;

    private void Awake()
    {
        _polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        AttackColliderTurnOff();
    }

    public void Attack()
    {
        AttackColliderTurnOffOn();

        if (OnSwordSwing != null) OnSwordSwing.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null || collision.transform == null) return; // проверка на уничттоженный объект
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) enemyEntity.TakeDamage(_damageAmount);
        // проверка на столкноверние с каким то другим коллайдером
    }

    public void AttackColliderTurnOff() // выключаем коллайдер, когда взмаха мечом нет
    {
        _polygonCollider2D.enabled = false;
    }

    private void AttackColliderTurnOn() // включаем коллайдер, когда взмаха мечом есть
    {
        _polygonCollider2D.enabled = true;
    }

    private void AttackColliderTurnOffOn() // это для того, чтобы человек при нажатии 10^10 кликов
    //  в секунду всё равно наносил урон
    {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
    }
}
