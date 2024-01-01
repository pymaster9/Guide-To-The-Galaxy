using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public GameObject starpref;
    public int xsteps;
    public int ysteps;
    private void Start()
    {
        for (int x = 0; x < 360; x+= xsteps)
        {
            for (int y = 0; y < 180; y+=ysteps)
            {
                GameObject curr = Instantiate(starpref);
                curr.GetComponent<StarPositionFromSphericalCoordinates>().rahours = x;
                curr.GetComponent<StarPositionFromSphericalCoordinates>().dechours = y;
                curr.name = new Vector2(x, y).ToString();
            }
        }
    }
}
