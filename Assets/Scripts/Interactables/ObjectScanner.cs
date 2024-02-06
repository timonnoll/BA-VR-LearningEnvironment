using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectScanner : MonoBehaviour
{

    public LayerMask layerMask;
    public Transform scanSource;
    public float distance = 10;

    private bool rayActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(x => StartScan());
        grabInteractable.deactivated.AddListener(x => StopScan());
    }

    public void StartScan()
    {
        rayActivate = true;
    }

    public void StopScan()
    {
        rayActivate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rayActivate)
            RaycastCheck();
    }

    void RaycastCheck()
    {
        RaycastHit hit;
        bool hasHit = Physics.Raycast(scanSource.position, scanSource.forward, out hit, distance, layerMask);

        if (hasHit)
        {
            hit.transform.gameObject.SendMessage("ShowInfo", SendMessageOptions.DontRequireReceiver);
        }

    }
}
