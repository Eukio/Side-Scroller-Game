using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    [SerializeField] Player player;
    private Vector2 rel;
    private Vector2 newPos;
    [SerializeField] float speed;


    // Start is called before the first frame update
    void Start()
    {
        newPos = Vector2.SmoothDamp(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y), ref rel, speed);

    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= -0.05f)
        {
            newPos = Vector2.SmoothDamp(new Vector2(-0.05f, transform.position.y), new Vector2(-0.05f, player.transform.position.y), ref rel, speed);

            transform.position = new Vector3(-0.05f, newPos.y, transform.position.z);
        }
        if (transform.position.x >= 7.89f)
        {
            newPos = Vector2.SmoothDamp(new Vector2(7.89f, transform.position.y), new Vector2(7.89f, player.transform.position.y), ref rel, speed);

            transform.position = new Vector3(7.89f, newPos.y, transform.position.z);
        }
        if (transform.position.y >= 2.69f)
        {
            newPos = Vector2.SmoothDamp(new Vector2(transform.position.x, 2.69f), new Vector2(transform.position.x, 2.69f), ref rel, speed);

            transform.position = new Vector3(newPos.x, 2.69f, transform.position.z);
        }
        if (transform.position.y <= -1.69f)
        {
            newPos = Vector2.SmoothDamp(new Vector2(transform.position.x, -1.69f), new Vector2(transform.position.x, -1.69f), ref rel, speed);

            transform.position = new Vector3(newPos.x, -1.69f, transform.position.z);
        }
        if (transform.position.x >= -0.05f && transform.position.x <= 7.89f && transform.position.y <= 2.69f && transform.position.y >= -1.69f)
        {
            newPos = Vector2.SmoothDamp(new Vector2(transform.position.x, transform.position.y), new Vector2(player.transform.position.x, player.transform.position.y), ref rel, speed);
            // transform.position = player.transform.position + new Vector3(0, 1, -5);
            transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
        }
    }
}
