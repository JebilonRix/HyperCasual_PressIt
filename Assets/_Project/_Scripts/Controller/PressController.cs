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

        [Header("Animators")]
        [SerializeField] Animator animatorShake;
        [SerializeField] Animator animatorBeltUp;
        [SerializeField] Animator animatorBeltDown;

        [Header("Press")]
        [SerializeField] GameObject[] pressHeads = new GameObject[4];
        [SerializeField] Transform pressPoint;

        private int _pressIndex = 0;

        public Vector3 LastPosition { get => lastPosition; }
        public Transform PressPoint { get => pressPoint; }

        private void Start()
        {
            transform.position = startPosition;
            SetPressHead(0);
        }
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveTo(LastPosition);
                //obje hýzlanmasý ekle
            }
            else
            {
                MoveTo(startPosition);
            }

            if (transform.position == lastPosition)
            {
                animatorShake.SetBool("shaked", true);
                SetAnimatorStuff(true);
            }
            if (transform.position == startPosition)
            {
                animatorShake.SetBool("shaked", false);
                SetAnimatorStuff(false);
            }

            #region Test Input
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (_pressIndex < 3)
                {
                    var x = _pressIndex++;
                    SetPressHead(x);
                }
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_pressIndex > 0)
                {
                    var x = _pressIndex--;
                    SetPressHead(x);
                }
            } 
            #endregion
        }

        private void SetAnimatorStuff(bool t)
        {
            animatorBeltUp.SetBool("smashed", t);
            animatorBeltDown.SetBool("smashed", t);
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
        private void SetPressHead(int index)
        {
            for (int i = 0; i < pressHeads.Length; i++)
            {
                pressHeads[i].SetActive(index == i);
            }
        }
    }
}