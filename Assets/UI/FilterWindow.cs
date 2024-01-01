using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class FilterWindow : MonoBehaviour
{
    public TMP_InputField namefilter;
    public Slider magfilter;

    public void Filter()
    {
        CreateStars.home.GetComponent<CreateStars>().CreateStarsWithConstraints(namefilter.text, magfilter.value, 0);
    }
}
