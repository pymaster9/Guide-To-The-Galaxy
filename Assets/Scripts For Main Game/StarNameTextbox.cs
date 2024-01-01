using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class StarNameTextbox : MonoBehaviour
{
    public TextMeshProUGUI starname;
    public Slider magnitude;
    public bool active;
    public GameObject button;
    public TextMeshProUGUI coords;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (LightSolver.main.closest != null)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                active = true;
            }
            starname.text = LightSolver.main.closest.gameObject.name;
            magnitude.value = LightSolver.main.closest.magnitude;
            float distancefromsun = LightSolver.main.closest.distance * 326.156f;
            float distancefromhere = ((LightSolver.main.closest.transform.position - Camera.main.transform.position).magnitude) * 326.156f;
            coords.text = "Ra:" + LightSolver.main.closest.rahours + "\nDec:" + LightSolver.main.closest.dechours + "\nMagnitude:" + LightSolver.main.closest.magnitude + "\nDistance From Sun:" + distancefromsun + " ly\nDistance From Here:" + distancefromhere + " ly";
            starname.gameObject.SetActive(active);
            coords.gameObject.SetActive(active);
            button.SetActive(active);
            GetComponent<Image>().enabled = active;
        }
    }
    public void Deactivate()
    {
        active = false;
    }
}
