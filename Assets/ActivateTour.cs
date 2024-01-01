using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class ActivateTour : MonoBehaviour
{
    public string tourfile;
    public TextMeshProUGUI nametext;
    public List<TourGuide.Step> currenttour;
    public void starttour()
    {
        TourGuide.nodes = currenttour;
        SceneManager.LoadScene(1);
    }
    private void Start()
    {
        TextAsset file = Resources.Load<TextAsset>("tours/"+tourfile);
        string text = file.text;
        string[] lines = text.Split("\n");
        string name = "NONE";
        int stars = -1;

        name = lines[0].Remove(0, 5);
        stars = int.Parse(lines[1].Remove(0, 6));

        nametext.text = name;

        for (int x = 2; x < lines.Length; x++)
        {
            string line = lines[x];

            if(line.Contains("GoTo: 0,0"))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.gotoorigintype = true;
                currenttour.Add(currentitem);
            }
            else if(line.Contains("GoTo: "))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.gototype = true;
                currentitem.star = line.Remove(0, 6);
                currenttour.Add(currentitem);
            }
            else if (line.Contains("Look: "))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.guidetotype = true;
                currentitem.star = line.Remove(0, 6);
                currenttour.Add(currentitem);
            }
            else if (line.Contains("Play: "))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.texttype = true;
                currentitem.displaytext = line.Remove(0, 6);
                currenttour.Add(currentitem);
            }
            else if (line.Contains("Img: "))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.imagetype = true;
                currentitem.displaytext = line.Remove(0, 5);
                currenttour.Add(currentitem);
            }
            else if (line.Contains("Zoom{"))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.zoomtype = true;
                currentitem.star = line.Remove(0, 11);
                currentitem.zoomvalue = int.Parse(line.Remove(0, 5).Remove(3));
                currenttour.Add(currentitem);
            }
            else if (line.Contains("$Rotate$Cam;;"))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.rotatecamtype = true;
                currenttour.Add(currentitem);
            }
            else if (line.Contains("$Stop$Cam;;"))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.stoprotatecamtype = true;
                currenttour.Add(currentitem);
            }
            else if (line.Contains("FALSE"))
            {
                break;
            }
            else if (line.Contains("RotateAround: "))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.orbitaroundtype = true;
                currentitem.star = line.Remove(0, 14);
                currenttour.Add(currentitem);
            }
            else if (line.Contains("StopRotation"))
            {
                print('H');
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.stoporbittype = true;
                currenttour.Add(currentitem);
            }
            else if (line.Contains("Snap: "))
            {
                TourGuide.Step currentitem = new TourGuide.Step();
                currentitem.snaptype = true;
                currentitem.star = line.Remove(0, 6);
                currenttour.Add(currentitem);
            }
        }
    }
}
