using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class SpawnByObject : ObjectSpawner
    {
        [SerializeField] Transform _singleSpawnPoint;

        public override void SpawnObject()
        {
            base.SpawnObject();
            var x = ObjectPooler.Instance.GetObject(_objectTag, _singleSpawnPoint.position);

            x.transform.position = _singleSpawnPoint.position;
        }
    }
}