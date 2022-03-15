using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PressIt
{
    public class PressCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Press"))
            {
                Debug.Log("Uff, ouch");
            }
        }
    }
}
