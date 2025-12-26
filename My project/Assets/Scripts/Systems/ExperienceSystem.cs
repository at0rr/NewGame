using UnityEngine;
using System;

public class ExperienceSystem : MonoBehaviour
{
    public static ExperienceSystem Instance { get; private set; }

    [SerializeField] private int xpPerPickup = 10; // xp с одного моба
    [SerializeField] private int xpForLevelUp = 100; // xp на левел-ап

    private int currXp = 0;
    private int currLevel = 1;

    public event Action<float> OnXpChanged; // изменение XP
    public event Action<int> OnLevelUp;

    private void Awake()
    {
        Instance = this;
    }

    public void AddXp(int xp)
    {
        currXp += xp;
        if (currXp >= xpForLevelUp)
        {
            currXp -= xpForLevelUp; // сбрасываем xp
            ++currLevel;
            if (OnLevelUp != null) OnLevelUp.Invoke(currLevel);
        }

        float progress = (float)currXp / xpForLevelUp; // считаем, сколько на данный момент опыта
        if (OnXpChanged != null) OnXpChanged.Invoke(progress);
    }

}
