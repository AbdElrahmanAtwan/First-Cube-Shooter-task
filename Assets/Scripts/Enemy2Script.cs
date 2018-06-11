using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Script : MonoBehaviour
{

    [Header("Health")]
    public int hp = 5;

    [Header("Fire Bullets")]
    public GameObject bulletPrefab;
    public Transform fireFrom;
    public float shootingSpeed;
    public int shootingTime = 2;

    private GameObject player;
    private Transform head;
    private Rigidbody rb;
    private bool fire = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        head = transform.GetChild(0);
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (transform.position.y < 0.6)
        {
            head.LookAt(player.transform.GetChild(0));
            if (fire)
                StartCoroutine("Fire");
            fire = false;
        }
    }

    void FireMissiles()
    {
        var go = Instantiate(bulletPrefab, fireFrom.position, fireFrom.rotation);
        go.GetComponent<Rigidbody>().velocity = (fireFrom.up * shootingSpeed + fireFrom.forward * Vector3.Distance(player.transform.position, head.position));
        go.GetComponent<BulletExplosion>().Name = "Enemy2";

    }
    IEnumerator Fire()
    {
        FireMissiles();
        yield return new WaitForSeconds(shootingTime);
        fire = true;
    }

}
