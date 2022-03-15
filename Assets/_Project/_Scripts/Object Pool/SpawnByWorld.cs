using PressIt;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool
{
    public class SpawnByWorld : ObjectSpawner
    {
        [SerializeField] Transform[] _spawnPoints;
        [SerializeField] SpawnType _spawnType;
        [SerializeField] Dimension _dimension;

        private float xMin;
        private float xMax;
        private float yMin;
        private float yMax;
        private float zMin;
        private float zMax;

        private enum Dimension
        {
            TwoDimension, ThreeDimension
        }
        private enum SpawnType
        {
            RandomSpawnInPoints, RandomSpawnInArea
        }

        protected override void Start()
        {
            if (_spawnType == SpawnType.RandomSpawnInArea)
            {
                SetSpawnArea(_spawnPoints);
            }
        }
        public override void SpawnObject()
        {
            base.SpawnObject();

            switch (_spawnType)
            {
                case SpawnType.RandomSpawnInArea:

                    ObjectPooler.Instance.GetObject(_objectTag, SpawnRandomlyInArea());
                    break;

                case SpawnType.RandomSpawnInPoints:

                    ObjectPooler.Instance.GetObject(_objectTag, SpawnRandomlyInSpawnPoints(_spawnPoints));
                    break;
            }
        }

        #region Private Func.
        private Vector3 SpawnRandomlyInSpawnPoints(Transform[] spawnPoints)
        {
            Vector3 returnVector;

            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            switch (_dimension)
            {
                case Dimension.TwoDimension:

                    returnVector = new Vector2(randomPoint.position.x, randomPoint.position.y);
                    break;
                case Dimension.ThreeDimension:

                    returnVector = new Vector3(randomPoint.position.x, randomPoint.position.y, randomPoint.position.z);
                    break;

                default:
                    returnVector = Vector3.zero;
                    break;
            }

            return returnVector;
        }
        private Vector3 SpawnRandomlyInArea()
        {
            Vector3 returnVector;

            switch (_dimension)
            {
                case Dimension.TwoDimension:

                    returnVector = new Vector2(Random.Range(xMin, xMax), Random.Range(yMin, yMax));
                    break;

                case Dimension.ThreeDimension:

                    returnVector = new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), Random.Range(zMin, zMax));
                    break;

                default:
                    returnVector = Vector3.zero;
                    break;
            }

            return returnVector;
        }
        private void SetSpawnArea(Transform[] spawnPoints)
        {
            List<float> listX = new List<float>();
            List<float> listY = new List<float>();
            List<float> listZ = new List<float>();

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                listX.Add(spawnPoints[i].position.x);
                listY.Add(spawnPoints[i].position.y);
                listZ.Add(spawnPoints[i].position.z);
            }

            xMin = listX.GetMinValue();
            xMax = listX.GetMaxValue();

            yMin = listY.GetMinValue();
            yMax = listY.GetMaxValue();

            zMin = listZ.GetMinValue();
            zMax = listZ.GetMaxValue();
        }
        #endregion
    }
}