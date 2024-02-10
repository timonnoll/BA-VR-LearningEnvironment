using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TN
{
    /// <summary>
    /// Create and handle body socket interactors to store items.
    /// </summary>
    public class BodySockets : MonoBehaviour
    {
        [Header("Player Settings")]
        public GameObject playerCamera;
        [Range(0.01f, 1f)]
        public float heightRatio;

        [Header("Socket Holder")]
        public GameObject leftBodySocket;
        public GameObject rightBodySocket;


        private Vector3 currentCameraLocalPosition;
        private Quaternion currentCameraRotation;

        // Update body sockets depending on the player camera.
        private void Update()
        {
            currentCameraLocalPosition = playerCamera.transform.localPosition;
            currentCameraRotation = playerCamera.transform.rotation;
            UpdateBodySocketHeight(leftBodySocket);
            UpdateBodySocketHeight(rightBodySocket);
            UpdateBodySocket();
        }

        // Update body sockets height.
        private void UpdateBodySocketHeight(GameObject bodySocket)
        {
            bodySocket.transform.localPosition = new Vector3(bodySocket.transform.localPosition.x, currentCameraLocalPosition.y * heightRatio, bodySocket.transform.localPosition.z);
        }

        // Update body sockets rotation.
        private void UpdateBodySocket()
        {
            transform.localPosition = new Vector3(currentCameraLocalPosition.x, 0, currentCameraLocalPosition.z);
            transform.rotation = new Quaternion(transform.rotation.x, currentCameraRotation.y, transform.rotation.z, currentCameraRotation.w);
        }
    }

}
