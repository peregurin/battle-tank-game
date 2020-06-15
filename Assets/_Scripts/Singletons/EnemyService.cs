using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Controller;
using Enemy.View;
using Enemy.Model;
using Bullet.Service;
using Bullet.Controller;
using System;

namespace Enemy.Service
{
    public class EnemyService : MonoSingletonGeneric<EnemyService>
    {
        public static event Action OnEnemyDeath;

        public EnemyView EnemyView;
        EnemyModel enemyModel;
        EnemyController enemyController;

        //List<EnemyController> enemyControllers = new List<EnemyController>();
        Dictionary<float, EnemyController> enemyControllers = new Dictionary<float, EnemyController>();

        private void Update()
        {
            SpawnEnemyAtRandomPositions();
        }

        private void SpawnEnemyAtRandomPositions()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                for(int i = 0; i < 5; i++)
                {
                    Vector3 position = GetRandomPosition();
                    float key = GetRandomKey();
                    CreateNewEnemy(position, key);
                    enemyControllers.Add(key, enemyController);
                }
            }
        }

        private float GetRandomKey()
        {
            float key = UnityEngine.Random.value;
            Debug.Log("Key = " + key);
            return key;
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 position = new Vector3(UnityEngine.Random.Range(-45.0f, 45.0f), 0f, UnityEngine.Random.Range(-45.0f, 45.0f)); ;
            return position;
        }

        private void CreateNewEnemy(Vector3 position, float key)
        {
            enemyModel = new EnemyModel();
            enemyController = new EnemyController(enemyModel, EnemyView, position, key);
        }

        public BulletController GetBullet(Vector3 position, Vector3 tankRotation)
        {
            BulletController bulletController = BulletService.Instance.PleaseGiveMeBullet(position, tankRotation);
            return bulletController;
        }

        public void DestroyControllerAndModel()
        {
            OnEnemyDeath();
            enemyModel = null;
            //enemyControllers.Remove(enemyController.EnemyControllerId);
            enemyController = null;
        }

        public IEnumerator DestroyAllEnemies()
        {
            //Debug.Log("Destroy all enemies");
            //for (int i = 0; i < enemyControllers.Count; i++)
            //{
            //    if (enemyControllers[i] != null)
            //    {
            //        yield return new WaitForSeconds(1f);
            //        enemyControllers[i].DestroyEnemyTank();
            //    }
            //    else
            //    {
            //        Debug.Log("enemy tank already destroyed");
            //    }
            //}

            foreach(var key in enemyControllers.Keys)
            {
                if(enemyControllers[key] != null)
                {
                    yield return new WaitForSeconds(1f);
                    enemyControllers[key].DestroyEnemyTank();
                }
                else
                {
                    Debug.Log("enemy tank already destroyed");
                }
            }
        }
    }
}