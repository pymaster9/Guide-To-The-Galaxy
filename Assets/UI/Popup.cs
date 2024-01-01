using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Popup : MonoBehaviour
{
    public static StarPositionFromSphericalCoordinates cactive;

    public string identifier;
    public TextMeshProUGUI identifierobj;

    public float rahours;
    public float ramins;
    public float rasecs;
    public float dechours;
    public float decmins;
    public float decsecs;
    public TextMeshProUGUI positionobj;

    public string spectraltype;
    public TextMeshProUGUI spectraltypeobj;

    public float distance;
    public TextMeshProUGUI distanceobj;

    public float sizeint;
    public float exponent;
    public TextMeshProUGUI sizeobj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StarPositionFromSphericalCoordinates active = cactive;
        if (active != null)
        {
            //Relay data
            identifier = active.gameObject.name;
            rahours = active.rahours;
            dechours = active.dechours;
            spectraltype = active.magnitude.ToString();
            distance = active.distance;
            sizeint = active.radius;
            exponent = active.exponent;

            float absolutemagnitude = active.absolutemagnitude;
            float dist = (Camera.main.transform.position - active.transform.position).magnitude * 100;
            float relativemagnitude = absolutemagnitude - 5 + 5 * (Mathf.Log10(dist));

            //set text
            identifierobj.text = identifier;
            positionobj.text = "(Relative to Earth) RA:" + (Mathf.Round(rahours*100)/100).ToString()  + " DEC:"+ (Mathf.Round(dechours * 100) / 100).ToString();
            spectraltypeobj.text = "Earth Magnitude:" + Mathf.Round(float.Parse(spectraltype)*10)/10 + " Relative Magnitude:" + Mathf.Round(relativemagnitude*10)/10;
            distanceobj.text = "Distance To Earth:" + Mathf.Round(distance*10000)/100 + " Pc" + " Distance From Here:" + Mathf.Round(dist * 100) / 100 + "Pc";
        }
    }
}
