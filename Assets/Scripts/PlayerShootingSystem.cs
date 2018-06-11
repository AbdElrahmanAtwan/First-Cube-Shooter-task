using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerShootingSystem : MonoBehaviour
{
    [Header("Health")]
    public int hp = 200;

    [Header("Head Rotation")]
    public float rotationSpeed = 10;

    [Header("Shoot")]
    public bool switchShootingMethod;

    [Space(5)]
    public Transform bulletSpawnPosition;
    public GameObject bulletPrefab;
    public float bulletShootingSpeed;
    public float bulletshootingTime;

    [Space(5)]
    public Transform missileSpawnPosition;
    public GameObject missilePrefab;
    public float missileShootingSpeed;
    public float missileshootingTime;

    [Header("UI")]
    public Text playerHp;
    public Text enemyKills;

    private Rigidbody rb;
    private Transform head;
    private FindClosest find;
    private GameObject ceObj;
    private bool Bullet = true;
    private bool Missile = true;
    private int enemyKilled = 0;
    private bool findclose = true;
    private GameObject closestEnemy;
    private List<GameObject> EnemyList;

    public int EnemyKilled
    {
        get
        {
            return enemyKilled;
        }

        set
        {
            enemyKilled = value;
        }
    }

    void Start()
    {
        head = transform.GetChild(0);
        ceObj = GameObject.FindGameObjectWithTag("EnemyCreator");
        EnemyList = ceObj.GetComponent<CreateEnemies>().enemyList;
        playerHp = GameObject.FindGameObjectWithTag("playerHp").GetComponent<Text>();
        playerHp.text = "Player Hp";
        enemyKills = GameObject.FindGameObjectWithTag("enemyKills").GetComponent<Text>();
        enemyKills.text = "Enemy Killed";
       

    }

    void Update()
    {
        playerHp.text = "Player Hp  : " + hp;
        enemyKills.text = "Enemy Killed : " + enemyKilled;
       
        if (EnemyList.Count > 0)
        {
            if (findclose)
                FindClosestEnemy();
            findclose = false;
            RotateObject();
            //head.LookAt(closestEnemy.transform.GetChild(0));
            if (closestEnemy.name == "Enemy1")
            {
                if (closestEnemy.GetComponent<Enemy1Script>().hp > 0)
                {
                    if (switchShootingMethod)
                        BulletAttack();
                    else
                        MissleAttack();
                }
                else
                {
                    findclose = true;
                    EnemyList.Remove(closestEnemy);
                    Destroy(closestEnemy);
                    EnemyKilled += 1;
                }
            }
            if (closestEnemy.name == "Enemy2")
            {
                if (closestEnemy.GetComponent<Enemy2Script>().hp > 0)
                {
                    if (switchShootingMethod)
                        BulletAttack();
                    else
                        MissleAttack();
                }
                else
                {
                    findclose = true;
                    EnemyList.Remove(closestEnemy);
                    Destroy(closestEnemy);
                    EnemyKilled += 1;
                }
            }
        }
    }

    void FireBullets()
    {
        var go = Instantiate(bulletPrefab, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
        go.GetComponent<Rigidbody>().velocity = (bulletSpawnPosition.forward * bulletShootingSpeed + bulletSpawnPosition.forward * Vector3.Distance(closestEnemy.transform.position, head.position));
        go.GetComponent<BulletExplosion>().Name = "Player";
    }

    void FireMissiles()
    {
        var go = Instantiate(missilePrefab, missileSpawnPosition.position, missileSpawnPosition.rotation);
        go.GetComponent<Rigidbody>().velocity = (missileSpawnPosition.up * missileShootingSpeed + missileSpawnPosition.forward * Vector3.Distance(closestEnemy.transform.position, head.position));
        go.GetComponent<BulletExplosion>().Name = "Player";

    }

    IEnumerator Fire()
    {
        if (switchShootingMethod)
        {
            FireBullets();
            yield return new WaitForSeconds(bulletshootingTime);
            Bullet = true;
        }
        else
        {
            FireMissiles();
            yield return new WaitForSeconds(missileshootingTime);
            Missile = true;
        }
    }

    void BulletAttack()
    {
        if (Bullet)
        {
            StartCoroutine("Fire");
        }
        Bullet = false;
    }
    void MissleAttack()
    {
        if (Missile)
        {
            StartCoroutine("Fire");
        }
        Missile = false;
    }

    public void FindClosestEnemy()
    {
        EnemyList = ceObj.GetComponent<CreateEnemies>().enemyList;
        //GameObject[] EnemyList = GameObject.FindGameObjectsWithTag("Enemy2");
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
    void RotateObject()
    {
        Vector3 targetDir = closestEnemy.transform.GetChild(0).position - head.position;
        float step = rotationSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(head.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        head.rotation = Quaternion.LookRotation(newDir);
    }
}
