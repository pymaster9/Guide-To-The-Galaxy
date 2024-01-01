using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenuScript : MonoBehaviour
{
    public List<GameObject> ui;
    public List<GameObject> escapemenu;
    public bool paused;
    public bool UIHidden;
    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        paused = false;
        foreach (GameObject curr in ui)
        {
            curr.SetActive(true);
        }
        foreach (GameObject curr in escapemenu)
        {
            curr.SetActive(false);
        }
    }

    public void Pause()
    {
        foreach (GameObject curr in ui)
        {
            curr.SetActive(false);
        }
        foreach (GameObject curr in escapemenu)
        {
            curr.SetActive(true);
        }
    }

    void UpdateUI(bool on)
    {
        if (!paused)
        {
            foreach (GameObject curr in ui)
            {
                curr.SetActive(on);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
        {
            Pause();
        }
        else
        {
            Resume();
        }

        
        UpdateUI(false);
    }
}
