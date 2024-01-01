using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CreateStars : MonoBehaviour
{
    public GameObject starpref;
    public static GameObject home;
    public GameObject bufferpref;
    public List<Color> materials;
    public GameObject batcherparent;
    public List<string> names;
    public GameObject popup;
    public List<GameObject> stars;
    public List<GameObject> batchers;
    public float distancescale;
    public List<float> startemps;
    public BoxCollider filter;
    public Dictionary<string, List<string>> starlookup;
    public static CreateStars main;



    public void CreateStarsWithConstraints(string scanfor, float magnitude, int year)
    {
        filter.enabled = false;
        StarPositionFromSphericalCoordinates.popup = popup;
        home = gameObject;
        foreach (GameObject star in stars)
        {
            Destroy(star);
        }
        stars = new List<GameObject>();
        foreach (GameObject batcher in batchers)
        {
            Destroy(batcher);
        }
        batchers = new List<GameObject>();

        TextAsset textasset = Resources.Load<TextAsset>("SimbadStarData");
        if (textasset == null) //Check if File Exists
        {
            Debug.LogError("Could not find file. No stars created.");
        }
        else//Main Loop
        {
            //Get the files
            string text = textasset.text;
            string[] lines = text.Split("\n");
            //Mainloop
            for (int linenum = 0; linenum < lines.Length-1; linenum++)
            {
                //Fetch lines
                string curr = lines[linenum];
                //Split the regular file into components
                string[] segments = curr.Split('|');


                //Check the names
                bool inbigdipper = segments[6].ToLower().Contains(scanfor.ToLower());

                if (float.Parse(segments[0]) > magnitude)
                {
                    inbigdipper = false;
                }
                if (inbigdipper)
                {
                    //Extract and format the coordinates

                    //Create the GameObject
                    GameObject currentgo = Instantiate(starpref, transform.position, transform.rotation);
                    //Set the GO's parent
                    currentgo.name = segments[6];
                    currentgo.transform.parent = transform;
                    //SetProperMotions;
                    float pmra = float.Parse(segments[4]) * Mathf.Pow(10, -3);
                    float pmdec= float.Parse(segments[5]) * Mathf.Pow(10, -3);

                    currentgo.GetComponent<StarPositionFromSphericalCoordinates>().pmra = pmra;
                    currentgo.GetComponent<StarPositionFromSphericalCoordinates>().pmdec= pmdec;

                    //SetCoordiantes
                    currentgo.GetComponent<StarPositionFromSphericalCoordinates>().rahours = float.Parse(segments[1]) + (pmra * year);
                    currentgo.GetComponent<StarPositionFromSphericalCoordinates>().dechours = float.Parse(segments[2]) + (pmdec * year);
                    //SetDistance
                    currentgo.GetComponent<StarPositionFromSphericalCoordinates>().distance = float.Parse(segments[3])*10;
                    //SetMagnitude
                    currentgo.GetComponent<StarPositionFromSphericalCoordinates>().magnitude = float.Parse(segments[0]);
                    //SetAbsoluteMagnitude
                    currentgo.GetComponent<StarPositionFromSphericalCoordinates>().absolutemagnitude = float.Parse(segments[7]);
                    
                    stars.Add(currentgo);
                }
            }
            CreateConstellationLines();
            LightSolver.forcerender = true;
            filter.enabled = true;
        }
    }

    public void CreateConstellationLines()
    {
        starlookup = new Dictionary<string, List<string>>();
        TextAsset textasset = Resources.Load<TextAsset>("ConstellationLines");
        int linenum = 0;
        if (textasset == null) //Check if File Exists
        {
            Debug.LogError("Could not find file. No constellation lines created.");
        }
        else
        {
            string[] lines = textasset.text.Split("\n");
            int linenumber = 0;
            foreach (string line in lines)
            {
                linenumber++;
                string[] components = line.Split('|');
                int r;
                if (int.TryParse(components[1], out r) || int.TryParse(components[2], out r))
                {
                    Debug.LogWarning("One of your stars was a number. Line number: " + linenumber.ToString() + " Line:" + line);
                }

                if (!starlookup.ContainsKey(components[1]))
                {
                    List<string> curr = new List<string>();
                    curr.Add(components[2]);
                    starlookup.Add(components[1], curr);
                }
                else
                {
                    List<string> curr = (List<string>)starlookup[components[1]];
                    curr.Add(components[2]);
                    starlookup[components[1]] = curr;
                }
                if (!starlookup.ContainsKey(components[2]))
                {
                    List<string> curr = new List<string>();
                    curr.Add(components[1]);
                    starlookup.Add(components[2], curr);
                }
                else
                {
                    List<string> curr = (List<string>)starlookup[components[2]];
                    curr.Add(components[1]);
                    starlookup[components[2]] = curr;
                }

                foreach (var sname in components)
                {
                    GameObject comp1 = stars.Find(x => x.gameObject.name.Equals(sname, System.StringComparison.OrdinalIgnoreCase));
                    if (comp1 != null)
                        comp1.GetComponent<StarPositionFromSphericalCoordinates>().partofconst = true;
                }

            }
        }
    }

    private void Start()
    {
        main = this;
        
        CreateStarsWithConstraints("", 7f, 0);
    }
}