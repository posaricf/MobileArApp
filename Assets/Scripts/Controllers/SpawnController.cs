using DeltaReality.Quiz.Managers;
using System;
using UnityEngine;

namespace DeltaReality.Quiz.Controllers
{
    /// <summary>
    /// Class used for object creation and manipulation.
    /// </summary>
    public class SpawnController : MonoBehaviour
    {
        public static Action OnObjectClicked;

        [SerializeField] private Camera _camera;

        private const int GRADIENT = 500;
        private const int MAX_SCALE_FACTOR = 15;
        private const float ROTATION_SPEED_GRADIENT = 100;

        private GameObject _currentObject;
        private GameObject _loadedModel;
        private Vector3 _initialScale;

        private void OnEnable()
        {
            ArController.OnModelLoad += SetModelForInstantiation;
            ArController.OnObjectReset += SetCurrentObject;
            ArController.OnScreenTouched += CreateObject;
            TouchManager.OnSingleTouch += CheckIfObjectClicked;
            TouchManager.OnPinch += ResizeObject;
            TouchManager.OnSwipe += RotateObject;
        }

        private void OnDisable()
        {
            ArController.OnModelLoad -= SetModelForInstantiation;
            ArController.OnObjectReset -= SetCurrentObject;
            ArController.OnScreenTouched -= CreateObject;
            TouchManager.OnSingleTouch -= CheckIfObjectClicked;
            TouchManager.OnPinch -= ResizeObject;
            TouchManager.OnSwipe -= RotateObject;
        }

        private void Start()
        {
            SetCurrentObject(null);
        }

        /// <summary>
        /// Sets current object to method argument.
        /// </summary>
        /// <param name="objectCreated">Object to be set.</param>
        public void SetCurrentObject(GameObject objectCreated)
        {
            _currentObject = objectCreated;
        }

        /// <summary>
        /// Sets object prefab.
        /// </summary>
        /// <param name="objectPrefab">Prefab to be set.</param>
        private void SetModelForInstantiation(GameObject objectPrefab)
        {
            _loadedModel = objectPrefab;
        }

        /// <summary>
        /// Instantiates object on the position given as an argument.
        /// </summary>
        /// <param name="position">Position where the object will be instantiated.</param>
        private void CreateObject(Vector3 position)
        {
            if (_currentObject == null)
            {
                _currentObject = Instantiate(_loadedModel, position, Quaternion.identity);
                _initialScale = _currentObject.transform.localScale;
            }
        }

        /// <summary>
        /// Resizes object according to the scale given as an argument.
        /// </summary>
        /// <param name="scale">New object scale.</param>
        private void ResizeObject(float scale)
        {
            float factor = Mathf.Clamp(scale / GRADIENT, 1, MAX_SCALE_FACTOR);
            Vector3 currentScale = _initialScale * factor;
            _currentObject.transform.localScale = currentScale;
        }

        /// <summary>
        /// Rotates object by adding velocity vector components to its transform's rotation components.
        /// </summary>
        /// <param name="velocity">Velocity vector.</param>
        private void RotateObject(Vector3 velocity)
        {
            _currentObject.transform.rotation = Quaternion.Euler(
                _currentObject.transform.rotation.eulerAngles.x + (velocity.z / ROTATION_SPEED_GRADIENT),
                _currentObject.transform.rotation.eulerAngles.y + (velocity.x / ROTATION_SPEED_GRADIENT),
                _currentObject.transform.rotation.eulerAngles.z + (velocity.y / ROTATION_SPEED_GRADIENT));
        }

        /// <summary>
        /// Checks if an object on the screen is clicked.
        /// </summary>
        /// <param name="position">Position of the click.</param>
        private void CheckIfObjectClicked(Vector3 position)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(position);
            int layerMask = 1 << 3;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.collider.tag == "Spawnable")
                {
                    OnObjectClicked?.Invoke();
                }
            }
        }
    }
}
