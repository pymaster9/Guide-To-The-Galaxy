
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class RocketController : MonoBehaviour
{
    public float speed;
    public float impulse;
    public float warp;
    public float renderspace;
    public float rotationalspeed;

    public float ra;
    public float dec;
    public float scrollspeed;
    public float zrotation;
    Vector3 lpos;
    public Vector3 rotation;

    public static RocketController main;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        GetComponent<Camera>().fieldOfView = 60;
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Camera>().fieldOfView += Input.GetAxis("Scroll")*Time.deltaTime * scrollspeed;
        if (GetComponent<Camera>().fieldOfView > 90)
        {
            GetComponent<Camera>().fieldOfView = 90;
        }



        transform.Rotate(new Vector3(-Input.GetAxis("Vertical"), -Input.GetAxis("Horizontal")));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, zrotation);

    }
}
