using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySockets : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject leftBodySocket;
    public GameObject rightBodySocket;

    [Range(0.01f, 1f)]
    public float heightRatio;

    private Vector3 currentCameraLocalPosition;
    private Quaternion currentCameraRotation;

    void Update()
    {
        currentCameraLocalPosition = playerCamera.transform.localPosition;
        currentCameraRotation = playerCamera.transform.rotation;
        UpdateBodySocketHeight(leftBodySocket);
        UpdateBodySocketHeight(rightBodySocket);
        UpdateBodySocket();
    }
    private void UpdateBodySocketHeight(GameObject bodySocket)
    {
        bodySocket.transform.localPosition = new Vector3(bodySocket.transform.localPosition.x, (currentCameraLocalPosition.y * heightRatio), bodySocket.transform.localPosition.z);
    }

    private void UpdateBodySocket()
    {
        transform.localPosition = new Vector3(currentCameraLocalPosition.x, 0, currentCameraLocalPosition.z);
        transform.rotation = new Quaternion(transform.rotation.x, currentCameraRotation.y, transform.rotation.z, currentCameraRotation.w);
    }
}
