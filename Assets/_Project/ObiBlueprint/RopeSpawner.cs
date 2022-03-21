using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

namespace PressIt
{
    public class RopeSpawner : MonoBehaviour
    {
        public ObiRopeCursor obiRopeCursor;
        public ObiRope obiRope;

        [SerializeField] private Vector3 force;
        [SerializeField] private float deactivationTime = 3f;

        private float counter = 0f;
        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }
        private void Update()
        {
            //if (Input.GetKey(KeyCode.Space))
            //{
            //    orc.ChangeLength(or.restLength + 1f);
            //}

            if (gameObject.activeSelf)
            {
                if (deactivationTime <= counter)
                {
                    gameObject.SetActive(false);
                    counter = 0;
                }
                else
                {
                    counter += Time.deltaTime;
                }
            }
        }
        private void FixedUpdate()
        {
            if (gameObject.activeSelf)
            {
                rigidbody.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}