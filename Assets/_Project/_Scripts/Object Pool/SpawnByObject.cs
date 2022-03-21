using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class SpawnByObject : ObjectSpawner
    {
        [SerializeField] private Transform _singleSpawnPoint;
        [SerializeField] float[] offsets;
        [SerializeField] Vector3[] rotations;
        private Dictionary<string, Vector3> rotationsDic = new Dictionary<string, Vector3>();
        private Dictionary<string, float> offsetDic = new Dictionary<string, float>();

        protected override void Start()
        {
            base.Start();

            for (int i = 0; i < rotations.Length; i++)
            {
                SetDictionaries(_objectTags[i], rotations[i], offsets[i]);
            }
        }

        private void SetDictionaries(string tag, Vector3 rotation, float offset = 0f)
        {
            rotationsDic.Add(tag, rotation);
            offsetDic.Add(tag, offset);
        }

        public override void SpawnObject()
        {
            base.SpawnObject();

            int randomTag = Random.Range(0, _objectTags.Length);
            string selectedTag = _objectTags[randomTag];

            Vector3 position = new Vector3(0, _singleSpawnPoint.position.y + offsetDic[selectedTag], 0);
            GameObject gameObj = ObjectPooler.Instance.GetObject(selectedTag, _singleSpawnPoint.position + position, rotationsDic[selectedTag]);

            gameObj.transform.position = _singleSpawnPoint.position + position;
        }
    }
}