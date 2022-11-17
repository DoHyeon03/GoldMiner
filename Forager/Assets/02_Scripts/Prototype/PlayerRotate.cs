using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float speed;

    private RaycastHit hit;
    private int layerMask;

    public Ore ore;

    public float curTime;
    public float coolTime = 0.3f;
    public bool attackOn = false;

    private void Start()
    {
        layerMask = 1 << 6;
    }
    void Update()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;

        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));
        }

        if (attackOn)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, layerMask))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ore = GameObject.Find(hit.collider.gameObject.name).GetComponent<Ore>();
                    ore.oreHp--;
                    attackOn = false;
                }
                Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.forward * 5f, Color.red);
            }
        }
        else
        {
            curTime += Time.deltaTime;
            if (curTime >= coolTime)
            {
                attackOn = true;
                curTime = 0;
            }
        }
    }
}
