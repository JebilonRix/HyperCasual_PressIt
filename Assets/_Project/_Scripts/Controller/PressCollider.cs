using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressIt
{
    public class PressCollider : MonoBehaviour
    {
        [SerializeField] PressController pressController;
        [SerializeField] Transform pressPoint;
        [SerializeField] float checkTime = 1f;
        private BoxCollider collider;

        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
        }
        private void Start()
        {
            InvokeRepeating(nameof(Test), checkTime, checkTime);
        }
        private void Test()
        {
            if (pressController.transform.position == pressController.LastPosition)
            {
                collider.enabled = false;
            }
            else
            {
                collider.enabled = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                var obj = other.GetComponent<IPresseble>();
                var percent = obj.GetTransform().position.z - pressPoint.position.z;

                // Debug.Log(percent);

                obj.Smash(Mathf.Abs(percent) * 100);
            }
            else
            {
                return;
            }
        }
    }
}
