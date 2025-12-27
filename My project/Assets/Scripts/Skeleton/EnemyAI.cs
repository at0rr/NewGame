using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameUtils;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2.5f;

    private NavMeshAgent navMeshAgent; //navMesh - это система навигации для NPC

    private Transform player; // для будущей позиции игрока

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false; // поворот спрайта
        navMeshAgent.updateUpAxis = false; // отключаем поворот для Z
    }

    private void Start()
    {
        navMeshAgent.speed = enemySpeed; // скорость скелетов
        if (Player.Instance != null)
        {
            player = Player.Instance.transform; // подтягиваем позицию игрока
        }
    }

    private void Update() // тут мы проверяем, в каком состоянии объект находится
    {
        navMeshAgent.SetDestination(player.position);
        ChangeFacingDirection();
    }

    private void ChangeFacingDirection()
    {
        if (navMeshAgent.velocity.x < 0) // если враг идёт влево
        {
            transform.rotation = Quaternion.Euler(0, -180, 0); // запись, поворачивающая объект
        }
        else if (navMeshAgent.velocity.x > 0) // если враг идёт вправо
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
