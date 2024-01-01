using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollider : MonoBehaviour
{
    public LightSolver parent;

    void Awake()
    {
        parent = Camera.main.GetComponent<LightSolver>();
    }

    private void OnTriggerEnter()
    {
        parent.inside.Add(GetComponent<Collider>());
        //GetComponent<Renderer>().enabled = true;
        //if (GetComponent<Renderer>().isVisible == true && !parent.inside.Contains(GetComponent<Collider>()))
        //{
        //    parent.inside.Add(GetComponent<Collider>());
        //}
        //else if (GetComponent<Renderer>().isVisible == false)
        //{
        //    parent.inside.Remove(GetComponent<Collider>());
        //}
    }

    private void OnTriggerExit()
    {
        parent.inside.Remove(GetComponent<Collider>());
    }
    private void OnDestroy()
    {
        parent.inside.Remove(GetComponent<Collider>());
    }

    //void OnBecameVisible()
    //{
    //    print("HELLO");
    //    parent.inside.Add(GetComponent<Collider>());
    //    GetComponent<Renderer>().material.color = Color.green;
    //}


    //void OnBecameInvisible()
    //{
    //    print("GOODBYE!");
    //    parent.inside.Remove(GetComponent<Collider>());
    //    GetComponent<Renderer>().material.color = Color.red;
    //}
}
