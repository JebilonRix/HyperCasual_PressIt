using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressIt
{
    public class ResetPoints : MonoBehaviour
    {
        [SerializeField] new string[] tags;

        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                var obj = other.GetComponent<IPooledObject>();
                obj.DeactivateMe();
            }
        }
    }
}
