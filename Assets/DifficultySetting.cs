using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySettings : MonoBehaviour
{
    public Slider difset;
    public SceneChangeButton sc;
    // Update is called once per frame
    void Update()
    {
        sc.statam = difset.value;
    }
}
