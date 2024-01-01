using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplayer : MonoBehaviour
{
    public static ImageDisplayer main;
    public Image child;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        main = this;
        child.enabled = false;
        GetComponent<Image>().enabled = false;
        button.SetActive(false);
    }

    public void DisplayImage(string imagetext)
    {
        child.enabled = true;
        GetComponent<Image>().enabled = true;
        button.SetActive(true);
        print(Resources.Load<Sprite>("images/" + imagetext));
        child.sprite = Resources.Load<Sprite>("images/" + imagetext);
    }

    public void CloseImage()
    {
        child.enabled = false;
        GetComponent<Image>().enabled = false;
        button.SetActive(false);
        TourGuide.main.RunStep();
    }
}
