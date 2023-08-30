using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private bool jump, left, right;
    [SerializeField] float speed; Rigidbody2D rb;
    [SerializeField] GameObject RayObject;
    [SerializeField] GameObject swordSwipe;
    RaycastHit2D hitWall;
    bool run;
    int lives = 5;
    int coins;
    Vector3 checkpoint;
    long startTime;
    long elapsed;


    GameObject p;
    void Start()
    {
        startTime = DateTime.Now.Ticks;
        rb = GetComponent<Rigidbody2D>();
        Reset();
        checkpoint = new Vector3(-2.08f, 3.42f, 0);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (run)
        {
            elapsed = (DateTime.Now.Ticks - startTime) / 10000;

            if (Input.GetKeyDown(KeyCode.Space))
                jump = true;
            if (Input.GetKeyDown(KeyCode.A))
            {
                left = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                right = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
                jump = false;
            if (Input.GetKeyUp(KeyCode.A))
            {
                left = false;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                right = false;

            }
            if (Input.GetMouseButtonDown(0)&& elapsed >= 1000)
            {
                
                    elapsed = 0;
                    startTime = DateTime.Now.Ticks;
      
                StartCoroutine(DelayedSwipe());

            }
        }
        if(lives == 0)
        {
            left = false;
            jump = false;
            right = false;
            run = false;
            GetComponent<Animator>().SetBool("dead", true);
           


        }

    }
    private void FixedUpdate()
    {

        hitWall = Physics2D.Raycast(RayObject.transform.position, -Vector2.up);
        Debug.DrawRay(RayObject.transform.position, -Vector2.up * hitWall.distance, Color.red);

        if (jump && hitWall.distance <= 0.01  )
        {
            rb.AddForce(Vector3.up * 160);
            GetComponent<Animator>().SetBool("jump", true);
            StartCoroutine(DelayedJump());

        }
        float xChange = 0f;
        float yChange = 0f;
        if (left)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            GetComponent<BoxCollider2D>().offset = new Vector2(0.0832950473f, -0.0194945931f);

            RayObject.transform.position = new Vector3(transform.position.x+0.07399988f, RayObject.transform.position.y);

            xChange -= speed;
            GetComponent<Animator>().SetBool("isRunning 0", true);
        }
        if (right)
        {
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.0832950473f, -0.0194945931f);

            GetComponent<SpriteRenderer>().flipX = false;
            RayObject.transform.position = new Vector3(transform.position.x -0.07399988f, RayObject.transform.position.y);

            xChange += speed;
            GetComponent<Animator>().SetBool("isRunning 0", true);

        }
        if (!left && !right)
        {
            GetComponent<Animator>().SetBool("isRunning 0", false);

        }
        GetComponent<Transform>().position += new Vector3(xChange * Time.deltaTime, yChange * Time.deltaTime, 0);

    }
    public int GetLives()
    {
        return lives;
    }
    public int GetPoints()
    {
        return coins;
    }
    public bool isRun()
    {
        return run;
    }
    public void SetRun(bool run)
    {
        this.run= run;
    }
    void RestartTime()
    {
        //  speedStartTime = DateTime.Now.Ticks;
        // speedElasped = 0;
    }
    public void Reset()
    {
        GetComponent<Animator>().SetBool("dead", false);

        swordSwipe.SetActive(false);
        lives = 3;
        left = false;
        jump = false;
        right = false;
        run = true;
        coins = 0;
        checkpoint = new Vector3(-2.08f, 3.42f, 0);
        gameObject.transform.position = new Vector3(-2.08f, 3.42f, 0);
        startTime = DateTime.Now.Ticks;
        elapsed = 0;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && lives >0)
        {
            gameObject.transform.position = checkpoint;
            lives--;
        }
        if (collision.gameObject.tag == "Checkpoint")
        {
            checkpoint = collision.gameObject.transform.position;
        }
        if (collision.gameObject.tag == "Coin" || collision.gameObject.tag == "EnemyCoin")
        {
            collision.gameObject.SetActive(false);
            coins++;
        }
        if (collision.gameObject.name == "Door")
        {
            run = false;
            collision.gameObject.GetComponent<SpriteRenderer>().color= Color.white;
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            checkpoint = collision.gameObject.transform.position;
            for (int x = 0; x < GameObject.FindGameObjectsWithTag("Checkpoint").Length; x++)
            {
                GameObject a = GameObject.FindGameObjectsWithTag("Checkpoint")[x];
                a.gameObject.GetComponent<Checkpoint>().OffLight();
            }

            collision.GetComponent<Checkpoint>().SetLight();


        }

    }
    IEnumerator DelayedSwipe()
    {
        swordSwipe.SetActive(true);
        GetComponent<Animator>().SetBool("attack", true);
        yield return new WaitForSeconds(.8f);
        swordSwipe.SetActive(false);
        GetComponent<Animator>().SetBool("attack", false);


    }
    IEnumerator DelayedJump()
    {
       
        yield return new WaitForSeconds(.6f);
        GetComponent<Animator>().SetBool("jump", false);


    }
    public void LoseLife()
    {
        lives--;
        gameObject.transform.position = checkpoint;

    }
 
}
