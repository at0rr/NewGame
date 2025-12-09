using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameUtils;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f; //макс расстояние, на которое враг может подходить
    [SerializeField] private float roamingDistanceMin = 3f; //мин расстояние, на которое враг может подходить
    [SerializeField] private float roamingTimerMax = 2f; //если по истечении этого времени,
    // он не достигнет цели, то цель он поменяет

    private NavMeshAgent navMeshAgent; //navMesh - это система навигации для NPC
    private State state;
    private float roamingTime;
    private Vector3 roamPos;
    private Vector3 startingPos;

    private enum State //состояния пребывания врага
    {
        Roaming
    }

    // private void Start()
    // {
    //     startingPos = transform.position; //определяем начальное положение объекта
    // }

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        state = startingState;
    }

    private void Update() // тут мы проверяем, в каком состоянии объект находится
    {
        switch (state)
        {
            default:
            case State.Roaming: // если бродит до точки какой либо, то:
                roamingTime -= Time.deltaTime; // Time.deltaTime - время между кадрами
                if (roamingTime < 0)
                {
                    Roaming();
                    roamingTime = roamingTimerMax;
                }
                break;
        }
    }

    private void Roaming()
    {
        startingPos = transform.position;
        roamPos = GetRoamingPos(); // ищем новую точку, куда пойти врагу
        ChangeFacingDirection(startingPos, roamPos); //поворачиваем объект врага в сторону движения
        // в случае игрока мы поворачивали только спрайт
        navMeshAgent.SetDestination(roamPos); // отправляет к этой точке
    }

    private Vector3 GetRoamingPos()
    {
        return startingPos + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
        // старое положение + направление движение * длину движения
    }

    private void ChangeFacingDirection(Vector3 sourcePos /*то, где враг сейчас*/, Vector3 targetPos /*то, куда врагу надо*/)
    {
        if (targetPos.x < sourcePos.x)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0); // запись, поворачивающая объект
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
