using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour
{

    List<GameObject> EnemyList;
    GameObject ceObj;
    GameObject closestEnemy;

    public void FindClosestEnemy(ref GameObject closestEnemy)
    {
        ceObj = GameObject.FindGameObjectWithTag("EnemyCreator");
        EnemyList = ceObj.GetComponent<CreateEnemies>().enemyList;
        if (EnemyList.Count > 0)
        {
            float distanceToClosestEnemy = Mathf.Infinity;
          
            foreach (GameObject currentEnemy in EnemyList)
            {
                float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestEnemy = currentEnemy;
                }
            }
            Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
        }
            
    }
}
