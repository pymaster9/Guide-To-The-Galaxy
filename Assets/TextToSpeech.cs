using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TextToSpeech : MonoBehaviour
{
    public static TextToSpeech main;
    public bool underinstruction;
    void Start()
    {
        main = this;
    }
    public void DisplayText(string text)
    {
        GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("audio/"+text);
        GetComponent<AudioSource>().Play();
        underinstruction = true;
    }
    public void Update()
    {
        if(underinstruction)
        {
            if(!GetComponent<AudioSource>().isPlaying)
            {
                TourGuide.main.RunStep();
                underinstruction = false;
            }
        }
    }
}
