using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject light;
    [SerializeField] GameObject light2;
    [SerializeField] GameObject light3;

    void Start()
    {
       light.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void SetLight()
    {
        light.SetActive(true);
        light2.SetActive(true);
        light3.SetActive(true);
    }
    public void OffLight()
    {
        light.SetActive(false);
        light2.SetActive(false);
        light3.SetActive(false);
    }
}
