using Obi;
using ObjectPool;
using UnityEngine;

namespace PressIt
{
    [RequireComponent(typeof(Rigidbody), typeof(MeshCollider))]
    public class ObjectAttributes : MonoBehaviour, IPresseble, IPooledObject
    {
        [SerializeField] string _tag;
        [SerializeField] float _speed = 2f;
        [SerializeField] float stopSeconds = 0.5f;
        [SerializeField] uint _moneyValue = 10;
        // [SerializeField] ObiRope rope;

        private PlayerBank _bank;
        private Rigidbody _rb;
        private MeshCollider _meshCollider;
        private bool _isSpawned;
        private bool _isSmashed;
        private Vector3 _scale;
        private Vector3 _pos;
        private float counter = 0f;

        public bool IsSpawned { get => _isSpawned; set => _isSpawned = value; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;

            _scale = transform.localScale;
            _pos = transform.position;

            _meshCollider = GetComponent<MeshCollider>();
            _meshCollider.convex = true;
            _meshCollider.isTrigger = true;

            transform.localScale = _scale;
            transform.position = _pos;
        }
        private void FixedUpdate()
        {
            if (IsSpawned && !_isSmashed)
            {
                _rb.velocity = new Vector3(0, 0, -_speed);
            }
            else if (IsSpawned && _isSmashed)
            {
                StopAfterSmash();
            }
        }
        public void OnObjectSpawned()
        {
            if (_bank == null)
            {
                _bank = FindObjectOfType<PlayerBank>();
            }

            IsSpawned = true;
        }
        public void DeactivateMe()
        {
            IsSpawned = false;
            ObjectPooler.Instance.RelaseObject(_tag, gameObject);
        }
        public void Smash(float multiplier)
        {
            uint kat_lana_rak;

            if (multiplier <= 25)
            {
                kat_lana_rak = 5;
            }
            else if (multiplier > 25 && multiplier <= 50)
            {
                kat_lana_rak = 3;
            }
            else if (multiplier > 50 && multiplier <= 75)
            {
                kat_lana_rak = 2;
            }
            else
            {
                kat_lana_rak = 1;
            }

            _bank.GainMoney(_moneyValue * kat_lana_rak);

            _isSmashed = true;
            //rope.gameObject.SetActive(true);
        }
        public GameObject GetGameObject()
        {
            return gameObject;
        }
        public string GetTag()
        {
            return _tag;
        }
        public Transform GetTransform()
        {
            return transform;
        }
        private void StopAfterSmash()
        {
            if (counter >= stopSeconds)
            {
                _isSmashed = false;
            }
            else
            {
                _rb.velocity = new Vector3(0, 0, 0);
                counter += Time.deltaTime;
            }
        }
    }
}