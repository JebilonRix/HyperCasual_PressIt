using PressIt;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] bool _isDebug;
        [SerializeField] bool _isDontDestroy;
        [SerializeField] List<Pool> _pools;

        private static ObjectPooler _instance;
        private Dictionary<string, Queue<IPooledObject>> _poolDictionary;
        private uint _objectTypeCount;
        private uint _spawnedObjectsCount;

        #region Properties
        public static ObjectPooler Instance { get => _instance; private set => _instance = value; }

        /// <summary>
        /// Increases or decreases with IPooled interface.
        /// </summary>
        public uint SpawnedObjects { get => _spawnedObjectsCount; private set => _spawnedObjectsCount = value; }
        public uint ObjectTypeCount { get => _objectTypeCount; private set => _objectTypeCount = value; }
        #endregion

        #region Unity Func.
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;

                if (_isDontDestroy)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
            else
            {
                Destroy(gameObject);
            }

            _poolDictionary = new Dictionary<string, Queue<IPooledObject>>();

            foreach (Pool item in _pools)
            {
                Queue<IPooledObject> objectPool = new Queue<IPooledObject>();
                CreateObjects(item, objectPool);
            }

            ObjectTypeCount = (uint)_poolDictionary.Count;
        }
        #endregion

        #region Public Func.

        /// <summary>
        /// This method is getting an object to pool.
        /// </summary>
        public GameObject GetObject(string tag, Vector3 spawnPoint, Vector3 rotation = default)
        {
            if (!_poolDictionary.ContainsKey(tag))
            {
                Debug.Log(tag + " is not contained.");
                return null;
            }

            IPooledObject pooledObject = GetComponent<IPooledObject>();

            if (_poolDictionary[tag].Count == 0)
            {
                pooledObject = CreateObject(tag);
            }
            else
            {
                pooledObject = _poolDictionary[tag].Dequeue();
            }

            SetGameObjectAttributes(pooledObject.GetGameObject(), true);

            pooledObject.GetGameObject().transform.rotation = rotation == default ? Quaternion.identity : Quaternion.Euler(rotation);

            if (pooledObject != null)
            {
                pooledObject.OnObjectSpawned();
            }
            else
            {
                BugHandler.Log(nameof(GetObject) + " The object do not have IPooledObject interface.", _isDebug);
            }

            SpawnedObjects++;

            return pooledObject.GetGameObject();
        }
        /// <summary>
        /// This method is relasesing an object to pool.
        /// </summary>
        public void RelaseObject(string tag, GameObject obj)
        {
            _poolDictionary[tag].Enqueue(obj.GetComponent<IPooledObject>());            
            obj.SetActive(false);
            SpawnedObjects--;
        }

        /// <summary>
        /// This method is used for relasing all spawned objects to pools. You should use this in end of level, game over etc.
        /// </summary>
        public void ReleaseAllObject()
        {
            Debug.Log("ReleaseAllObject");

            IPooledObject[] iPooled = FindObjectsOfType<MonoBehaviour>().OfType<IPooledObject>().ToArray();

            for (int i = 0; i < iPooled.Length; i++)
            {
                if (iPooled[i].IsSpawned)
                {
                    iPooled[i].DeactivateMe();
                }
                else
                {
                    continue;
                }
            }

            TimerWorks[] timers = FindObjectsOfType<TimerWorks>();

            for (int i = 0; i < timers.Length; i++)
            {
                timers[i].ResetTimer();
            }
        }
        #endregion

        #region Private Func.
        private void CreateObjects(Pool pool, Queue<IPooledObject> objectPool)
        {
            for (int i = 0; i < pool.Amount; i++)
            {
                GameObject obj = Instantiate(pool.ObjectPrefab);
                SetGameObjectAttributes(obj);
                objectPool.Enqueue(obj.GetComponent<IPooledObject>());
            }

            _poolDictionary.Add(pool.Tag, objectPool);
        }
        private IPooledObject CreateObject(string tag)
        {
            IPooledObject obj = null;

            for (int i = 0; i < _pools.Count; i++)
            {
                if (tag == _pools[i].Tag)
                {
                    obj = Instantiate(_pools[i].ObjectPrefab).GetComponent<IPooledObject>();
                    SetGameObjectAttributes(obj.GetGameObject());
                }
            }

            return obj;
        }
        private void SetGameObjectAttributes(GameObject obj, bool isActive = false)
        {
            obj.SetActive(isActive);
            obj.transform.parent = transform;
        }
        #endregion
    }

    [System.Serializable]
    public struct Pool
    {
        [SerializeField] string tag;
        [SerializeField] private GameObject objectPrefab;
        [SerializeField] int amount;

        public GameObject ObjectPrefab { get => objectPrefab; }
        public string Tag { get => tag; }
        public int Amount { get => amount; }
    }
    internal interface IPooledObject
    {
        string Tag { get; }
        bool IsSpawned { get; set; }
        void OnObjectSpawned();
        void DeactivateMe();
        string GetTag();
        GameObject GetGameObject();
    }
}