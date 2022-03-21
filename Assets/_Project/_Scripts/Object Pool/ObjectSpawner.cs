using PressIt;
using UnityEngine;

namespace ObjectPool
{
    [RequireComponent(typeof(TimerWorks))]
    public class ObjectSpawner : MonoBehaviour
    {
        [Header("Set Spawn Attributes")]
        [SerializeField] protected float _spawnRate;
        [SerializeField] protected uint _spawnLimit;
        [SerializeField] protected float _spawnDelaySeconds;
        [SerializeField] protected bool _isLoop;
        [Space]
        [Header("Set Object")]
        [SerializeField] protected string[] _objectTags;

        protected TimerWorks _timer;
        private bool _isCalled = false;

        #region Properties
        public uint SpawnLimit { get => _spawnLimit; set => _spawnLimit = value; }
        public float SpawnRate { get => _spawnRate; set => _spawnRate = value; }
        #endregion

        #region Unity Func.
        protected virtual void Start()
        {
            if (_timer == null)
            {
                _timer = GetComponent<TimerWorks>();
            }
        }
        protected virtual void Update()
        {
            if (!_timer.IsStart && SpawnLimit > ObjectPooler.Instance.SpawnedObjects)
            {
                if (_spawnDelaySeconds == 0)
                {
                    _timer.StartTimer(SpawnObject, SpawnRate, _isLoop);
                }
                else
                {
                    if (!_isCalled)
                    {
                        _timer.StartTimer(SpawnObject, SpawnRate, _isLoop, _spawnDelaySeconds);
                        _isCalled = true;
                    }
                }
            }

            if (_timer.IsStart && SpawnLimit <= ObjectPooler.Instance.SpawnedObjects)
            {
                _timer.StopTimer(SpawnObject);
            }
        }
        #endregion

        #region Public Func.
        public virtual void SpawnObject()
        {
            //This is for overriding.
        }
        #endregion
    }
}