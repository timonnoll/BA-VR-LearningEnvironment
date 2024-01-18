using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableRayInteractor : MonoBehaviour
{
    public GameObject rightRayInteractor;
    public GameObject leftRayInteractor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rightRayInteractor.SetActive(true);
            leftRayInteractor.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rightRayInteractor.SetActive(false);
            leftRayInteractor.SetActive(false);
        }
    }
}
