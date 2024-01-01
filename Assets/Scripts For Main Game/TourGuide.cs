using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourGuide : MonoBehaviour
{
    [System.Serializable]
    public struct Step
    {
        public bool gototype;
        public bool guidetotype;
        public bool texttype;
        public bool imagetype;
        public bool zoomtype;
        public bool rotatecamtype;
        public bool stoprotatecamtype;
        public bool orbitaroundtype;
        public bool stoporbittype;
        public bool gotoorigintype;
        public bool snaptype;
        public string star;
        public string displaytext;
        public int zoomvalue;
    }

    public static List<Step> nodes;
    Step run;
    public Transform createStars;
    int index = -1;
    public static TourGuide main;

    [Header("CameraLerp")]
    public bool lerpcam;
    public Vector3 lerpfrom;
    public Vector3 lerpto;
    public float lerptime;
    public float maxlerp;

    [Header("CameraRotate")]
    public bool rotating;
    public int rotateseconds;
    public float rotationtime;

    [Header("Orbit")]
    public bool orbiting;
    public GameObject orbitingGameObject;
    public int secuntilfullorbit;
    public float currentorbit;

    private void Start()
    {
        main = this;
        print(nodes.Count);
        Invoke(nameof(RunStep), 0.1f);
    }

    public void RunStep()
    {
        index += 1;
        if (index >= nodes.Count)
        {
            return;
        }
        else
        {
            run = nodes[index];
            if (run.gototype)
            {
                foreach (Transform curr in createStars.GetComponentsInChildren<Transform>())
                {
                    if (curr.name.ToLower().Contains(run.star.ToLower()))
                    {
                        lerpcam = true;
                        lerpfrom = Camera.main.transform.position;
                        lerpto = curr.transform.position;
                        lerptime = 0;
                        maxlerp = 1;
                        NavigatorTriangle.main.onnavinstruction = false;
                    }
                }
            }
            else if (run.guidetotype)
            {
                foreach (Transform curr in createStars.GetComponentsInChildren<Transform>())
                {
                    if (curr.name.ToLower().Contains(run.star.ToLower()))
                    {
                        NavigatorTriangle.main.pointtowards = curr.transform.position;
                        NavigatorTriangle.main.onnavinstruction = true;
                        print("Here");
                    }
                }
            }
            else if (run.texttype)
            {
                TextToSpeech.main.DisplayText(run.displaytext);
                NavigatorTriangle.main.onnavinstruction = false;
            }
            else if(run.imagetype)
            {
                ImageDisplayer.main.DisplayImage(run.displaytext);
                NavigatorTriangle.main.onnavinstruction = false;
            }
            else if(run.zoomtype)
            {
                foreach (Transform curr in createStars.GetComponentsInChildren<Transform>())
                {
                    if (curr.name.ToLower().Contains(run.star.ToLower()))
                    {
                        lerpcam = true;
                        lerpfrom = Camera.main.transform.position;
                        lerpto = curr.transform.position;
                        lerptime = 0;
                        maxlerp = run.zoomvalue/100f;
                        print(maxlerp);
                        NavigatorTriangle.main.onnavinstruction = false;
                    }
                }
            }
            else if(run.rotatecamtype)
            {
                rotating = true;
                rotationtime = 0;
                RunStep();
            }
            else if(run.stoprotatecamtype)
            {
                rotating = false;
                rotationtime = 0;
                RocketController.main.zrotation = 0;
                RunStep();
            }
            else if(run.orbitaroundtype)
            {
                foreach (Transform curr in createStars.GetComponentsInChildren<Transform>())
                {
                    if (curr.name.ToLower().Contains(run.star.ToLower()))
                    {
                        orbiting = true;
                        orbitingGameObject = curr.gameObject;
                        currentorbit = 0;
                        NavigatorTriangle.main.onnavinstruction = false;
                    }
                }
                RunStep();
            }
            else if(run.stoporbittype)
            {
                print("Here");
                orbiting = false;
                Camera.main.transform.parent = null;
                RunStep();
            }
            else if(run.gotoorigintype)
            {
                lerpcam = true;
                lerpfrom = Camera.main.transform.position;
                lerpto = Vector3.zero;
                lerptime = 0;
                maxlerp = 1;
                NavigatorTriangle.main.onnavinstruction = false;
            }
            else if (run.snaptype)
            {
                foreach (Transform curr in createStars.GetComponentsInChildren<Transform>())
                {
                    if (curr.name.ToLower().Contains(run.star.ToLower()))
                    {
                        Camera.main.transform.LookAt(curr.transform);
                        RunStep();
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (lerpcam)
        {
            print(maxlerp);
            lerptime += Time.deltaTime;
            Camera.main.transform.position = Vector3.Slerp(lerpfrom, lerpto, lerptime);
        }
        if(lerptime >= maxlerp && lerpcam)
        {
            lerpcam = false;
            lerpfrom = Vector3.zero;
            lerpto = Vector3.zero;
            lerptime = 0;
            RunStep();
        }

        if(rotating)
        {
            rotationtime += Time.deltaTime;
            float currentrotation = (360 * rotationtime) / rotateseconds;
            RocketController.main.zrotation = currentrotation;
        }

        if(orbiting)
        {
            Camera.main.transform.parent = orbitingGameObject.transform;
            currentorbit += Time.deltaTime;
            float currrot = (360 * currentorbit) / secuntilfullorbit;
            orbitingGameObject.transform.rotation = Quaternion.Euler(currrot,0,0);
        }
    }
}
