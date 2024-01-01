using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PositionPannel : MonoBehaviour
{
    public Transform cam;
    public TextMeshProUGUI position;
    public TextMeshProUGUI rotation;
    public TMP_InputField ra;
    public TMP_InputField dec;

    public void PointTowardsLocation()
    {
        //Convert RA and DEC to degrees
        float rad = float.Parse(ra.text);
        float decd = float.Parse(dec.text);


        cam.GetComponent<RocketController>().ra = rad;
        cam.GetComponent<RocketController>().dec = decd;


    }
    void Update()
    {
        position.text = "Position:(" + (cam.position.x * 100).ToString() + "," + (cam.position.y * 100).ToString() + "," + (cam.position.z * 100).ToString() + ")ly";
        rotation.text = "Rotation:(" + (Mathf.RoundToInt(cam.GetComponent<RocketController>().ra * 100) / 100).ToString() + "," + (Mathf.RoundToInt(cam.GetComponent<RocketController>().dec * 100) / 100).ToString() + ")";


    }

    public void ReturnToEarth()
    {
        cam.position = Vector3.zero;
    }

    public void GoToStar()
    {
        cam.position = Popup.cactive.transform.position;
    }
}
