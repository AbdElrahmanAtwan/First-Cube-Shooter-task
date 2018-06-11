using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemies : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy1;
    public GameObject enemy2;
    public bool drawCircle;
    public float radius;
    public int respawntime = 3;
    public List<GameObject> enemyList;

    private bool spawn = true;

    void Start()
    {
        enemyList = new List<GameObject>();
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (spawn)
            StartCoroutine("TimeToSpawn");

        spawn = false;
    }

    void ChooseEnemies()
    {
        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                SpawnEnemies(enemy1);
                break;
            case 1:
                SpawnEnemies(enemy2);
                break;
            default:
                SpawnEnemies(enemy2);
                break;
        }
    }

    void SpawnEnemies(GameObject enemyType)
    {
        var angle = Random.Range(0, 359);
        var centerX = player.transform.position.x + Random.Range(2, 0);
        var centerZ = player.transform.position.z + Random.Range(2, 0);
        Vector3 pos = new Vector3(centerX + radius * Mathf.Cos(angle), 5f, centerZ + radius * Mathf.Sin(angle));
        var go = Instantiate(enemyType, pos, Quaternion.identity);
        go.name = enemyType.name;
        enemyList.Add(go);
    }

    IEnumerator TimeToSpawn()
    {
        yield return new WaitForSeconds(respawntime);
        ChooseEnemies();
        spawn = true;
    }

    private void OnDrawGizmos()
    {
        if (drawCircle)
        {
            UnityEditor.Handles.color = Color.yellow;
            UnityEditor.Handles.DrawWireDisc(player.transform.position, Vector3.up, radius);
            UnityEditor.Handles.color = Color.cyan;
            UnityEditor.Handles.DrawWireDisc(player.transform.position, Vector3.up, radius / 2);
        }

    }

}
