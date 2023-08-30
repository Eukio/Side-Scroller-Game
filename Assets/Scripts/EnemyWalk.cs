using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyWalk : MonoBehaviour
{

    [SerializeField] float startXPosition;
    [SerializeField] float startYPosition;
    GameObject player;
    private Vector2 movement;
    Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] int lives = 2;
    [SerializeField] GameObject coin;

    long startTime;
    long elapsed;

    // Start is called before the first frame update
    void Start()
    {
        startTime = DateTime.Now.Ticks;
        player = GameObject.Find("Player");
        gameObject.transform.position = new Vector2(startXPosition, startYPosition);

    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x - transform.position.x >= 0)
        {
            direction = Vector2.right;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            direction = Vector2.left;
                GetComponent<SpriteRenderer>().flipX=false;
        }
     //   if (transform.position.x <= minX)
         //   transform.position = new Vector2(minX + speed, transform.position.y);
     //   if (transform.position.x >= maxX)
           // transform.position = new Vector2(maxX - speed, transform.position.y);


        elapsed = (DateTime.Now.Ticks - startTime) / 10000;
        if (elapsed >= 5000)
        {
            elapsed = 0;
            startTime = DateTime.Now.Ticks;
        }
        if (lives == 0)
            StartCoroutine(DelayedDeath());
    }

    private void FixedUpdate()
    {
        if (gameObject.transform.position.x >= minX && gameObject.transform.position.x <= maxX && player.transform.position.x >= minX)
            MoveTowards(direction);

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && !collision.gameObject.tag.Equals("Swipe"))
        {
            gameObject.transform.position = new Vector2(startXPosition, startYPosition);
        }
    }
    void MoveTowards(Vector2 direction)
    {
        gameObject.transform.position = ((Vector2)transform.position + (direction * speed * Time.deltaTime));

    }


    public void Reset()
    {
        startTime = DateTime.Now.Ticks;
        lives = 2;
        elapsed = 0;
        transform.position = new Vector2(startXPosition, startYPosition);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("SwordSwipe"))
        {
            lives--;
            StartCoroutine(DelayedHurt());


        }
    }
    IEnumerator DelayedDeath()
    {
        GetComponent<Animator>().SetBool("dead", true);
        yield return new WaitForSeconds(.6f);
        gameObject.SetActive(false);
        GameObject c = Instantiate(coin, transform.position, transform.rotation);
    }
    IEnumerator DelayedHurt()
    {
        gameObject.GetComponent<SpriteRenderer>().color= Color.red;
        yield return new WaitForSeconds(.4f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

    }
   public GameObject GetCoin()
    {
        return coin;
    }
    public void SetLives(int lives)
    {
        this.lives = lives;
    }
}

