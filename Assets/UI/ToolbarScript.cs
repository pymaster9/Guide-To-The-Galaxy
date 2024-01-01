using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarScript : MonoBehaviour
{
    public GameObject toggle;
    public void Clicked()
    {
        toggle.SetActive(!toggle.activeSelf); 
    }
}
