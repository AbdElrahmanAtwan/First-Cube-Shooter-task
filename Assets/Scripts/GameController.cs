using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject allEnemies;
    public GameObject player;
    public Text gameOver;
    private bool switchAttack = false;
    private int hp;

    void Start()
    {
        var go = Instantiate(allEnemies);
        go = Instantiate(player, new Vector3(0f, 5f, 0f), Quaternion.identity);
        go.name = player.name;
        gameOver = GameObject.FindGameObjectWithTag("gameOver").GetComponent<Text>();
        gameOver.text = "";
    }
    private void Update()
    {

        hp = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShootingSystem>().hp;
        if (hp <= 0)
        {
            gameOver.text = "Game Over";
            Time.timeScale = 0;
        }
    }
    public void ChangeAttack()
    {
        switchAttack = !switchAttack;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShootingSystem>().switchShootingMethod = switchAttack;
    }
}
