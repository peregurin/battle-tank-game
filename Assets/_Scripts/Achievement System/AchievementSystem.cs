using UnityEngine;
using Enemy.Service;
using System;

namespace Achievement
{
    public class AchievementSystem : MonoBehaviour
    {
        private static int count = 0;

        private void Start()
        {
            EnemyService.OnEnemyDeath += EnemyService_OnEnemyDeath;
        }

        private void EnemyService_OnEnemyDeath()
        {
            count++;
            if(count >= 10)
            {
                Debug.Log("Achievement Unlocked -> 10 enenmies killed");
            }
            else
            {
                Debug.Log("killed " + count + " enemies");
            }
        }
    }
}