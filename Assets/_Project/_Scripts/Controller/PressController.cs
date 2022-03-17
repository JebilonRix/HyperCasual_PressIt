using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressIt
{
    public class PressController : MonoBehaviour
    {
        [SerializeField] Vector3 startPosition = new Vector3(0f, 3f, 0f);
        [SerializeField] Vector3 lastPosition = new Vector3(0f, 0f, 0f);
        [Range(1f, 50f)] [SerializeField] float speed = 1f;
        //[SerializeField] CameraShake cameraShake;
        [SerializeField] Animator animator;

        public Vector3 LastPosition { get => lastPosition; }

        private void Start()
        {
            transform.position = startPosition;
        }
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveTo(LastPosition);
            }
            else
            {
                MoveTo(startPosition);
            }

            if (transform.position == lastPosition)
            {
                animator.SetBool("shaked", true);
            }
            if (transform.position == startPosition)
            {
                animator.SetBool("shaked", false);
            }
        }
        private void MoveTo(Vector3 destination)
        {
            if (transform.position != destination)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            }
            else
            {
                transform.position = destination;
            }
        }
    }
}