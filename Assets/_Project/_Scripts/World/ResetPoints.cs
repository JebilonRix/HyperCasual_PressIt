using ObjectPool;
using UnityEngine;

namespace PressIt
{
    public class ResetPoints : MonoBehaviour
    {
        [SerializeField] string[] tags;

        private void OnTriggerEnter(Collider other)
        {
            if (other != null)
            {
                Debug.Log(other.gameObject.name);

                IPooledObject obj = other.GetComponent<IPooledObject>();

                for (int i = 0; i < tags.Length; i++)
                {
                    if (obj.GetTag() == tags[i])
                    {
                        obj.DeactivateMe();
                    }
                }
            }
        }
    }
}
