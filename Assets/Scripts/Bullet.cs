using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    bool hit;
    Rigidbody2D r;
    [SerializeField] float speed;
    private Vector2 movement;
    Vector3 direction;
    GameObject player;

    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;

    }

    // Update is called once per frame
    void Update()
    {
      if(hit)
        {
            player.GetComponent<Player>().LoseLife();
            Debug.Log("hit");
            hit = false;

        }
    }
    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = direction * Time.deltaTime * speed ;

      
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Yesy");
            player.GetComponent<Player>().LoseLife();
            Destroy(gameObject);

        }
        if (!collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    
    public bool Hit()
    {
        return hit;
    }
    public void setHit(bool hit)
    {
        this.hit = hit;
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

}
