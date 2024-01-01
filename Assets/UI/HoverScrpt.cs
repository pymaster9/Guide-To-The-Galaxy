using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HoverScrpt : MonoBehaviour
{
    public GameObject corrsair;
    public List<StarPositionFromSphericalCoordinates> inside;
    // Start is called before the first frame update
    void Start()
    {
        inside = new List<StarPositionFromSphericalCoordinates>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("h"))
        {
            corrsair.SetActive(!corrsair.activeSelf);
        }

        float closestdistance = Mathf.Pow(10,10);
        StarPositionFromSphericalCoordinates closest = null;
        foreach (StarPositionFromSphericalCoordinates curr in inside)
        {
            float distance = (curr.transform.position - transform.position).magnitude;
            if (distance < closestdistance)
            {
                closestdistance = distance;
                closest = curr;
            }
            
        }
        if (closest != null)
        {
            QuickHover.main.text = closest.gameObject.name;
            Popup.cactive = closest;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        StarPositionFromSphericalCoordinates curr;
        if (other.TryGetComponent<StarPositionFromSphericalCoordinates>(out curr))
        {
            inside.Add(curr);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        StarPositionFromSphericalCoordinates curr;
        if (other.TryGetComponent<StarPositionFromSphericalCoordinates>(out curr))
        {
            inside.Remove(curr);
        }
    }
    //RaycastHit hit;
    //Debug.DrawRay(transform.position, transform.forward* 100000000.0f);
    //    if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 100000000.0f))
    //    {
    //        StarPositionFromSphericalCoordinates curr;
    //        if (hit.collider.TryGetComponent<StarPositionFromSphericalCoordinates>(out curr))
    //        {
    //            QuickHover.main.text = curr.gameObject.name;
    //            if (true)
    //            {
    //                print("HERE");
    //Popup.cactive = curr;
    //            }
    //        }
    //    }
}
