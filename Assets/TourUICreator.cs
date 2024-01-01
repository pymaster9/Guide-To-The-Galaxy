using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourUICreator : MonoBehaviour
{
    public GameObject tourprefab;
    void Start()
    {
        TextAsset[] all = Resources.LoadAll<TextAsset>("tours");


        foreach (TextAsset current in all)
        {
            GameObject curr = Instantiate(tourprefab, transform.parent);
            curr.transform.position = transform.position;
            curr.GetComponent<ActivateTour>().tourfile = current.name;
            transform.Translate(0, -100, 0);
        }
    }
}
