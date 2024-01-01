using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickHover : MonoBehaviour
{
    public static TextMeshProUGUI main;
    // Start is called before the first frame update
    void Start()
    {
        main = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
