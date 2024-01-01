using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSolver : MonoBehaviour
{
    Texture2D render;
    Vector3 prevpos;
    Quaternion prevrot;
    public int frame = 0;
    public Vector2 imagesize;
    public List<Collider> inside;
    public Image applyto;
    public Transform sensor;
    [SerializeField]public static bool forcerender;
    public int lmf = 10;
    int forcerenderframe;
    public List<Color[]> cache;
    public Color[] background;
    public float prevfov = 0;
    public float closestdist;
    public StarPositionFromSphericalCoordinates closest;
    public Dictionary<string, Vector2> cachedpositions;
    public int lines;
    public System.Exception exception;
    public static LightSolver main;
    private void Start()
    {
        main = this;
        cache = new
        List<Color[]>();
        forcerender = true;
        prevpos = Vector3.zero;
        prevrot = Quaternion.Euler(0, 0, 0);
        imagesize = new Vector2(GetComponent<Camera>().aspect * 1000, 1000);
        //Cache the background
        List<Color> currentimg = new List<Color>();


        for (int x = 0; x < Mathf.RoundToInt(imagesize.x); x++)
        {
            for (int y = 0; y < Mathf.RoundToInt(imagesize.y); y++)
            {
                currentimg.Add(Color.black);
            }
        }
        background = currentimg.ToArray();
        //Create a cache for different bitmaps

        for (int starsize = 0; starsize < 100; starsize++)
        {
            List<Color> currentlist = new List<Color>();
            for (int x = -starsize; x < starsize; x++)
            {
                float maximum = Mathf.Sqrt((starsize * starsize) - (x * x));
                float minimum = -maximum;
                for (int y = -starsize; y < starsize; y++)
                {


                    if (y < maximum && y > minimum)
                    {
                        currentlist.Add(new Color(1, 1, 1, 1));
                    }
                    else
                    {
                        currentlist.Add(new Color(0, 0, 0, 1));
                    }
                }
            }
            cache.Add(currentlist.ToArray());
        }
    }

    void SetupTexture()
    {
        prevpos = transform.position;
        prevrot = transform.rotation;
        render = new Texture2D(Mathf.RoundToInt(imagesize.x), Mathf.RoundToInt(imagesize.y));

        ////Set the background to black
        render.SetPixels(background);
    }

    void EvaluateCollider(Collider curr)
    {
        StarPositionFromSphericalCoordinates starclass;
        if (curr.gameObject.TryGetComponent<StarPositionFromSphericalCoordinates>(out starclass))
        {
            Vector3 relativepos = GetComponent<Camera>().WorldToViewportPoint(curr.transform.position);


            if (Mathf.Abs(relativepos.x) < 1 && Mathf.Abs(relativepos.y) < 1)
            {

                Vector2 pixelcoords = new Vector2(relativepos.x * imagesize.x, relativepos.y * imagesize.y);

                //Get star's magnitude
                float absolutemagnitude = starclass.absolutemagnitude;
                float dist = (transform.position - starclass.transform.position).magnitude*100;
                float magnitude = absolutemagnitude + 5 * (Mathf.Log10(dist));


                float intensity = Mathf.Sqrt(Mathf.Pow(10, magnitude / -2.5f));
                int starsize = Mathf.RoundToInt(intensity * 100);

                bool outsidescreen = false;
                if (pixelcoords.x + starsize > imagesize.x || pixelcoords.x - starsize < 0 || pixelcoords.y + starsize > imagesize.x || pixelcoords.y - starsize < 0) { }
                else
                {
                    try
                    {
                        try
                        {
                            starsize += 0;
                            render.SetPixels(Mathf.RoundToInt(pixelcoords.x) - starsize, Mathf.RoundToInt(pixelcoords.y) - starsize, starsize * 2, starsize * 2, cache[starsize]);
                        }
                        catch (System.Exception ex)
                        {
                            List<Color> currentlist = new List<Color>();
                            for (int x = -starsize; x < starsize; x++)
                            {
                                float maximum = Mathf.Sqrt((starsize * starsize) - (x * x));
                                float minimum = -maximum;
                                for (int y = -starsize; y < starsize; y++)
                                {


                                    if (y < maximum && y > minimum)
                                    {
                                        currentlist.Add(new Color(1, 1, 1, 1));
                                    }
                                    else
                                    {
                                        currentlist.Add(new Color(0, 0, 0, 1));
                                    }
                                }
                            }
                            render.SetPixels(Mathf.RoundToInt(pixelcoords.x) - starsize, Mathf.RoundToInt(pixelcoords.y) - starsize, starsize * 2, starsize * 2, currentlist.ToArray());
                            exception = ex;
                        }
                    }
                    catch (System.Exception ex) {
                        exception = ex;
                        outsidescreen = true;
                    }

                    float distance = (new Vector2(relativepos.x, relativepos.y) - new Vector2(0.5f, 0.5f)).magnitude;
                    if (distance < closestdist)
                    {
                        closestdist = distance;
                        closest = curr.GetComponent<StarPositionFromSphericalCoordinates>();
                    }

                    //Draw starlines
                    if(starclass.partofconst)
                    {
                        cachedpositions.Add(curr.name, pixelcoords);
                    }
                }
            }
        }
    }

    void ConstellationLines()
    {
        foreach(var starname in cachedpositions.Keys)
        {
            if (CreateStars.main.starlookup.ContainsKey(starname))
            {
                List<string> others = CreateStars.main.starlookup[starname];
                foreach (string other in others)
                {
                    if (cachedpositions.ContainsKey(other))
                    {
                        CreateLine(cachedpositions[starname], cachedpositions[other]);
                    }
                }
            }
        }
    }

    void CreateTexture()
    {
        cachedpositions = new Dictionary<string, Vector2>();
        closestdist = 100;
        if (forcerender)
        {
            forcerenderframe = 5 + frame;
            forcerender = false;
        }
        bool primary = render == null || prevpos != transform.position || prevrot != transform.rotation || frame == forcerenderframe || frame == 10 || prevfov != GetComponent<Camera>().fieldOfView;
        if (primary)
        {
            lines = 0;
            prevfov = GetComponent<Camera>().fieldOfView;
            forcerender = false;
            SetupTexture();
            foreach (Collider curr in inside)
            {
                EvaluateCollider(curr);
            }

            if (Camera.main.transform.position == Vector3.zero)
            {
                ConstellationLines();
            }


            //Apply the image
            render.Apply();
            Sprite applysprite = Sprite.Create(render, new Rect(0, 0, Mathf.RoundToInt(imagesize.x), Mathf.RoundToInt(imagesize.y)), new Vector2(0.5f, 0.5f));
            applyto.sprite = applysprite;

            //if (closest != null)
            //{
            //    QuickHover.main.text = closest.gameObject.name;
            //    Popup.cactive = closest;
            //}
        }
    }

    public void CreateLine(Vector2 position1, Vector2 position2)
    {
        lines++;
        float frac = 1 / Mathf.Sqrt(Mathf.Pow(position2.x - position1.x, 2) + Mathf.Pow(position2.y - position1.y, 2));
        float ctr = 0;

        Vector2 t = position1;
        while ((int)t.x != (int)position2.x || (int)t.y != (int)position2.y)
        {
            t = Vector2.Lerp(position1, position2, ctr);
            ctr += frac;
            render.SetPixel((int)t.x, (int)t.y, Color.blue);
        }

    }

    void Update()
    {
        Invoke("CreateTexture", 0f);
        frame += 1;

    }   
}