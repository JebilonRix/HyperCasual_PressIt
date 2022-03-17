using System.Collections;
using UnityEngine;

namespace PressIt
{
    [RequireComponent(typeof(Camera))]
    public class CameraShake : MonoBehaviour
    {
        private Camera camera;
        private bool isShaked = false;
        private Vector3 originalPos;

        public bool IsShaked { get => isShaked; set => isShaked = value; }

        private void Awake()
        {
            camera = GetComponent<Camera>();
            originalPos = transform.position;
        }
        public IEnumerator Shake(float duration)
        {
            if (!isShaked)
            {
                float elapsed = 0f;

                while (elapsed < duration)
                {
                    transform.position = new Vector3(Random.insideUnitSphere.x, Random.insideUnitSphere.y, originalPos.z);

                    elapsed += Time.deltaTime;

                    yield return null;
                }

                transform.position = originalPos;
            }
        }
    }
}