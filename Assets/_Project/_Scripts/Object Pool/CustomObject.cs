using UnityEngine;

namespace ObjectPool
{
    [RequireComponent(typeof(Rigidbody))]
    public class CustomObject : MonoBehaviour, IPooledObject
    {
        [SerializeField] new string tag;
        [SerializeField] float speed;
        [SerializeField] Rigidbody rb;
        private bool _isSpawned = false;

        public string Tag { get => tag; private set => tag = value; }
        public bool IsSpawned { get => _isSpawned; set => _isSpawned = value; }

        private void FixedUpdate()
        {
            if (IsSpawned)
            {
                rb.velocity = new Vector3(0, 0, -speed);

            }
        }

        #region Public Func.
        public virtual void OnObjectSpawned()
        {
            IsSpawned = true;
            transform.rotation = Quaternion.Euler(-90, 0, 0);
        }
        public virtual void DeactivateMe()
        {
            IsSpawned = false;
            ObjectPooler.Instance.RelaseObject(Tag, gameObject);
        }
        public string GetTag()
        {
            return Tag;
        }
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        #endregion
    }
}