using Assets.Scripts.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Models
{
    internal class ScreenControl : MonoBehaviour
    {
        [SerializeField] private float zoomSpeed = 3f;
        [SerializeField] private float moveSpeed = 3f; //Toc do di chuyen
        [SerializeField] private int moveDelta = 10; //Diem anh, duong vien chieu rong o canh ma chuyen dong hoat dong
        [SerializeField] private float dragSpeed = 2;

        private Vector3 dragOrigin;

        public bool cameraDragging = true;

        private float averageEdge = 10;

        private void Update()
        {
            if (BaseGameCTLs.Instance.State == EGameState.PLAYING)
            {
                HandleZoomCamera();
                HandleDraggingCamera();
                //HandleFollowCamera();
            }
        }

        void HandleZoomCamera()
        {
            float mouseScrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (mouseScrollWheel != 0)
            {
                Camera.main.fieldOfView = Camera.main.fieldOfView + mouseScrollWheel * zoomSpeed;
            }
        }

        void HandleDraggingCamera()
        {

            if (Input.GetMouseButtonDown(2))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(2)) return;

            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(pos.y * dragSpeed, 0, -pos.x * dragSpeed);

            Camera.main.transform.Translate(move, Space.World);
        }

        void HandleFollowCamera()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 lookPosition = Camera.main.ScreenToViewportPoint(mousePosition);

            Debug.Log(lookPosition);

            if (mousePosition.x <= moveDelta) //Left
            {
                if (lookPosition.z - averageEdge > 0)
                {
                    Camera.main.transform.position += Vector3.back * moveSpeed;
                }
            }
            if (mousePosition.x >= Screen.width - moveDelta) //Right
            {
                float right = Camera.main.transform.position.z + Screen.width / 2;
                Camera.main.transform.position += Vector3.forward * moveSpeed;
            }
            if (mousePosition.y <= moveDelta) //Down
            {
                float bottom = Camera.main.transform.position.x + Screen.height / 2;
                Camera.main.transform.position += Vector3.right * moveSpeed;
            }
            if (mousePosition.y >= Screen.height - moveDelta) //Up
            {
                float top = Camera.main.transform.position.x - Screen.height / 2;
                Camera.main.transform.position += Vector3.left * moveSpeed;
            }
        }
    }
}
