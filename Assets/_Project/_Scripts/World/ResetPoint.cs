using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressIt
{
    public class ResetPoint : MonoBehaviour
    {
        [SerializeField] new string tag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(tag))
            {
                other.GetComponent<IPooledObject>().DeactivateMe();
            }
        }
    }
}
