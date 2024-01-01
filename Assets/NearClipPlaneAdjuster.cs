using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearClipPlaneAdjuster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Camera>().nearClipPlane = 0.001f;
    }
}
