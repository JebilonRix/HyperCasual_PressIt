using UnityEngine;

namespace PressIt
{
    public class PressCollider : MonoBehaviour
    {
        [SerializeField] PressController pressController;
        [SerializeField] float checkTime = 1f;
        [SerializeField] GameObject smokeParticle;
        private BoxCollider collider;

        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
        }
        private void Start()
        {
            InvokeRepeating(nameof(ColliderChanger), checkTime, checkTime);
        }
        private void ColliderChanger()
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
                IPresseble obj = other.GetComponent<IPresseble>();

                Transform objTransform = obj.GetGameObject().transform;
                objTransform.localScale = new Vector3(objTransform.localScale.x, objTransform.localScale.y / 5, objTransform.localScale.z);
                //  objTransform.localPosition -= Vector3.up * 0.5f;

                smokeParticle.SetActive(true);
                smokeParticle.transform.position = pressController.PressPoint.position;

                float percent = obj.GetTransform().position.z - pressController.PressPoint.position.z;
                obj.Smash(Mathf.Abs(percent) * 100);
            }
            else
            {
                return;
            }
        }
    }
}
