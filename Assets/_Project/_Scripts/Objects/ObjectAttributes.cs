using ObjectPool;
using UnityEngine;

namespace PressIt
{
    [RequireComponent(typeof(Rigidbody))]
    public class ObjectAttributes : MonoBehaviour, IPresseble, IPooledObject
    {
        [SerializeField] string _tag;
        [SerializeField] float _speed;
        [SerializeField] uint _moneyValue;

        private PlayerBank _bank;
        private Rigidbody _rb;
        private bool _isSpawned;
        public string Tag { get => _tag; private set => _tag = value; }
        public bool IsSpawned { get => _isSpawned; set => _isSpawned = value; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
        }
        private void FixedUpdate()
        {
            if (IsSpawned)
            {
                _rb.velocity = new Vector3(0, 0, -_speed);
            }
        }
        public void OnObjectSpawned()
        {
            if (_bank == null)
            {
                _bank = FindObjectOfType<PlayerBank>();
                Debug.Log("buldum");
            }

            IsSpawned = true;
        }
        public void DeactivateMe()
        {
            IsSpawned = false;
            ObjectPooler.Instance.RelaseObject(Tag, gameObject);
        }
        public void Smash(float multiplier)
        {
            uint kat_la_na_rak;

            if (multiplier <= 25)
            {
                kat_la_na_rak = 5;
            }
            else if (multiplier > 25 && multiplier <= 50)
            {
                kat_la_na_rak = 3;
            }
            else if (multiplier > 50 && multiplier <= 75)
            {
                kat_la_na_rak = 2;
            }
            else
            {
                kat_la_na_rak = 1;
            }

            _bank.GainMoney(_moneyValue * kat_la_na_rak);

            DeactivateMe();
        }
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public string GetTag()
        {
            return Tag;
        }
        public Transform GetTransform()
        {
            return transform;
        }
    }
}