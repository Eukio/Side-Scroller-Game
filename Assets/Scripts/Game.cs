using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
 
    [SerializeField] Text lives;
    [SerializeField] Text win;
    [SerializeField] Text points;
    [SerializeField] Player player;
    [SerializeField] List<GameObject> coins;
    [SerializeField] List<GameObject> enemies;
    [SerializeField] GameObject plant;

    void Start()
    {
        win.text = "";
        lives.text = "Lives "+player.GetLives();
        points.text = ""+player.GetPoints();

    }

    // Update is called once per frame
    void Update()
    {
        lives.text = "Lives " + player.GetLives();
        points.text = "Coins " + player.GetPoints(); 

        if (player.GetLives() <= 0)
        {
            win.text = "You Lose, Press R to Restart";
        
        }
         if (Input.GetKeyDown(KeyCode.R)&& player.GetLives() == 0)
        {
            win.text = "";
            Reset();
            player.Reset();
        }
         if (player.GetLives() >=0 && !player.isRun())
        {
            win.text = "You Collected "+player.GetPoints()+" coins, Press R to Restart";
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();

                player.Reset();
                win.text = "";

            }

        }
        

    }
    public void Reset()
    {
  plant.GetComponent<SpriteRenderer>().color= Color.red;

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].SetActive(true);
            enemies[i].GetComponent<Animator>().SetBool("dead", false);
            if(enemies[i].GetComponent<EnemyWalk>() != null)
            enemies[i].GetComponent<EnemyWalk>().SetLives(2);
            if (enemies[i].GetComponent<Octopus>() != null)
                enemies[i].GetComponent<Octopus>().SetLives(2);


        }
        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].SetActive(true);
        }
        
        player.SetRun(true);
        for (int x = 0; x < GameObject.FindGameObjectsWithTag("Checkpoint").Length; x++)
        {
            GameObject a = GameObject.FindGameObjectsWithTag("Checkpoint")[x];
            a.gameObject.GetComponent<Checkpoint>().OffLight();
        }
        for (int x = 1; x < GameObject.FindGameObjectsWithTag("EnemyCoin").Length; x++)
        {
            GameObject c = GameObject.FindGameObjectsWithTag("EnemyCoin")[x];
            Destroy(c);
        }
        win.text = "";
        lives.text = "Lives " + player.GetLives();
        points.text = "" + player.GetPoints();
    }

}
