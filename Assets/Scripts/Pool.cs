using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeltaReality.Quiz
{
    /// <summary>
    /// Generic class for manipulation of a pool of objects.
    /// </summary>
    public class Pool : MonoBehaviour
    {
        public static Action<List<GameObject>> OnCurrentlyUsed;

        private GameObject _prefab;
        private Transform _parentTransform;
        private List<GameObject> _freeObjects;
        private List<GameObject> _usedObjects;

        private void Awake()
        {
            _freeObjects = new List<GameObject>();
            _usedObjects = new List<GameObject>();
        }

        /// <summary>
        /// Initializes pool of objects with size given as an argument.
        /// </summary>
        /// <param name="objectPrefab">Type of objects in the pool.</param>
        /// <param name="numberOfObjects">Size of the pool.</param>
        /// <param name="parentTransform">Parent transform of all objects in the pool.</param>
        public void InitializePool(GameObject objectPrefab, int numberOfObjects, Transform parentTransform)
        {
            if (_freeObjects.Count == 0 && _usedObjects.Count == 0)
            {
                _prefab = objectPrefab;
                _parentTransform = parentTransform;

                _freeObjects = new List<GameObject>();
                _usedObjects = new List<GameObject>();

                for (int i = 0; i < numberOfObjects; i++)
                {
                    CreateObject();
                }
            }
        }

        /// <summary>
        /// Returns a single pool object.
        /// </summary>
        /// <returns>Pool object.</returns>
        public GameObject GetObject()
        {
            if (_freeObjects.Count == 0)
            {
                CreateObject();
            }

            GameObject gameObject = _freeObjects[0];
            _freeObjects.RemoveAt(0);
            _usedObjects.Add(gameObject);
            return gameObject;
        }

        /// <summary>
        /// Releases an object from active duty.
        /// </summary>
        /// <param name="gameObject">Object to be released.</param>
        public void ReleaseObject(GameObject gameObject)
        {
            _usedObjects.Remove(gameObject);
        }

        /// <summary>
        /// Instantiates a single pool object.
        /// </summary>
        private void CreateObject()
        {
            GameObject initializedObject = Instantiate(_prefab, _parentTransform);
            _freeObjects.Add(initializedObject);
        }
    }
}
