using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanable : MonoBehaviour
{
    public GameObject scanData;
    public float disableDisplayTime = 5;
    private float timer = 0;

    void Start()
    {
        scanData.SetActive(false);
    }
    public void ShowInfo()
    {
        scanData.SetActive(true);
        timer += Time.deltaTime;
        if (timer > disableDisplayTime)
        {
            scanData.SetActive(false);
        }
    }
}
