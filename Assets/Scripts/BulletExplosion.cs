using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{

    [Header("Effects")]
    public GameObject explosion;
    public int damage = 1;

    private string name;

    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(gameObject);
        Destroy(expl, 3);
        if (other.tag == "Player")
            other.GetComponent<PlayerShootingSystem>().hp -= damage;

        if (other.tag == "Enemy1")
        {
            if (Name == "Player")
                other.GetComponent<Enemy1Script>().hp -= damage;
        }
        if (other.tag == "Enemy2")
        {
            if (Name == "Player")
                other.GetComponent<Enemy2Script>().hp -= damage;
        }
    }
}
