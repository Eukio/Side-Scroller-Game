using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer rendender;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] float speed;
    [SerializeField] GameObject player;
    bool moveBackground;
    bool left;
    bool right;
    float distance;
    float yDistance;
    float lastX;
    float lastY;
    bool checkdistance;
    bool ycheckdistance;

    void Start()
    {
        rendender = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        lastY = player.transform.position.y;
        if (Input.GetKeyDown(KeyCode.A))
        {
          lastX = player.transform.position.x;
            left = true;
        }
      if(Input.GetKeyUp(KeyCode.A))
        {
            left = false;

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastX = player.transform.position.x;
            right = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;

        }
        //  rendender.material.mainTextureOffset = new Vector2(xOffset, rendender.material.mainTextureOffset.y);
        rendender.material.mainTextureOffset = new Vector2(xOffset,yOffset);

    }
    private void FixedUpdate()
    {
        if (!checkdistance)
        {
            distance = Mathf.Abs(player.transform.position.x - lastX);
            checkdistance= true;

        }
        else if (checkdistance && (distance  - Mathf.Abs(player.transform.position.x - lastX)) !=0f )
        {
   
                if (left)
                {
                    xOffset += speed / 10 * Time.deltaTime;
                }
            if (right)
            {
                xOffset -= speed / 10 * Time.deltaTime;

            }
            checkdistance= false;
        }
        if (!ycheckdistance)
        {
           yDistance = Mathf.Abs(player.transform.position.y - lastY);

            ycheckdistance = true;
        }
        else if(ycheckdistance &&( yDistance - Mathf.Abs(player.transform.position.y - lastY)) != 0f ){
            if(player.transform.position.y - lastY >= 0)
            {
                yOffset+= speed* Time.deltaTime; ;
            }
            else
            {
                yOffset -= speed * Time.deltaTime;
            }
            ycheckdistance = false;
         //   Debug.Log(yDistance + " " + lastY + " " + player.transform.position.y);

        }
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
