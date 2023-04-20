using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveCycle : MonoBehaviour
{
    public Light sun;
    public GameObject Camera1;
    public GameObject Camera2;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void hide()
    {
    }

    public void show()
    {
    }

    public void TurnSunOn()
    {
        sun.GetComponent<Light>().enabled = true;
        CameraOne();
        hide();
    }

    public void TurnSunOff()
    {
        sun.GetComponent<Light>().enabled = false;
        CameraTwo();
    }

    public void CameraOne()
    {
        Camera1.SetActive(true);
        Camera2.SetActive(false);
    }

    public void CameraTwo()
    {
        Camera1.SetActive(false);
        Camera2.SetActive(true);
    }
}
