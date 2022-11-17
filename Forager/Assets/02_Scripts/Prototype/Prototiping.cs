using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prototiping : MonoBehaviour
{
    public float speed;
    public float dashSpeed = 1;
    public float curtime;
    public float cooltime = 1;
    public bool dashOn = false;

    public GameManager gameManager;
    public Text ingotCount;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.Translate(-speed * dashSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.Translate(speed * dashSpeed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Translate(0, 0, speed * dashSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Translate(0, 0, -speed * dashSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dashSpeed = 2.5f;
            dashOn = true;
        }
        if (dashOn)
        {
            curtime += Time.deltaTime;
            if (curtime >= cooltime)
            {
                dashOn = false;
                curtime = 0;
                dashSpeed = 1;
            }
        }

    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Item"))
        {
            gameManager.ingotCount++;
            ingotCount.text = $"IngotCount : {gameManager.ingotCount}";
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
