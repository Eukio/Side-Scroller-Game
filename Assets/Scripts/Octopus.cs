using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class Octopus : MonoBehaviour
{
    [SerializeField] GameObject bullet;

    [SerializeField] float startXPosition;
    [SerializeField] float startYPosition;
    GameObject player;
    private Vector2 movement;
    Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] float maxX;
    [SerializeField] float minX;
    [SerializeField] GameObject coin;


    [SerializeField] int lives = 2;
    long startTime;
    long elapsed;
    bool shoot;

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
        direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;
        elapsed = (DateTime.Now.Ticks - startTime) / 10000;

        if (transform.position.x <= minX)
            transform.position = new Vector2(minX + speed, transform.position.y);
        if (transform.position.x >= maxX)
            transform.position = new Vector2(maxX - speed, transform.position.y);
        if (transform.position.y >= maxY)
            transform.position = new Vector2(transform.position.x,minY- speed);
        if (transform.position.y <= minY)
            transform.position = new Vector2(transform.position.x, minY + speed);

        if (elapsed >= 5000)
        {
            elapsed = 0;
            startTime = DateTime.Now.Ticks;
            shoot = true;
            StartCoroutine(DelayedShoot());
        }
        if (lives == 0)
            StartCoroutine(DelayedDeath());
    }

    private void FixedUpdate()
    {
        if (gameObject.transform.position.x >= minX && gameObject.transform.position.x <= maxX && player.transform.position.x >= minX && gameObject.transform.position.y <= maxY&& gameObject.transform.position.y <= minY)
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

    IEnumerator DelayedShoot()
    {
        GetComponent<Animator>().SetBool("shoot", true);
        yield return new WaitForSeconds(.8f);
        GameObject b = Instantiate(bullet, transform.position + transform.up * .01f, transform.rotation);
        b.GetComponent<Bullet>().SetSpeed(45f);
        yield return new WaitForSeconds(.8f);
        Destroy(b, 2f);
        GetComponent<Animator>().SetBool("shoot", false);


    }


    public void Reset()
    {
        startTime = DateTime.Now.Ticks;
        lives = 2;
        elapsed = 0;
        transform.position= new Vector2(startXPosition, startYPosition);
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
        GameObject c = Instantiate(coin, transform.position,transform.rotation);


    }
    IEnumerator DelayedHurt()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.4f);
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;

    }
    public void SetLives(int lives)
    {
       this.lives = lives;
    }

}

