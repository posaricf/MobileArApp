using DeltaReality.Quiz.ArFunctions;
using DeltaReality.Quiz.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace DeltaReality.Quiz.Controllers
{
    public class ArController : MonoBehaviour
    {
        [Header("Asset references")]
        [SerializeField] private List<ArObject> _spawnableObjects;

        [Header("Buttons")]
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonObject;

        [Header("AR handlers")]
        [SerializeField] private ARSession _session;
        [SerializeField] private ARSessionOrigin _arSessionOrigin;
        [SerializeField] private ARRaycastManager _raycastManager;

        [Header("Audio manager")]
        [SerializeField] private AudioManager _audioManager;

        public static Action<GameObject> OnModelLoad;
        public static Action<Sprite> OnIconLoad;
        public static Action<AudioClip> OnAudioLoad;
        public static Action<GameObject> OnObjectReset;
        public static Action<Vector3> OnScreenTouched;

        private ArObject _currentArObject;
        private AssetReference _icon;
        private AssetReference _model;
        private AssetReference _audio;
        private List<ARRaycastHit> _hits;

        private void OnEnable()
        {
            _buttonBack.onClick.AddListener(OnClickBack);
            _buttonObject.onClick.AddListener(OnClickChangeObject);
            ScreenManager.OnArEntered += ToggleArView;
            ScreenManager.OnArEntered += InitializeAssets;
            SpawnController.OnObjectClicked += PlaySound;
            TouchManager.OnDone += StopPlayingSound;
            TouchManager.OnSingleTouch += ScreenTouched;
        }

        private void OnDisable()
        {
            _buttonBack.onClick.RemoveListener(OnClickBack);
            _buttonObject.onClick.RemoveListener(OnClickChangeObject);
            ScreenManager.OnArEntered -= ToggleArView;
            ScreenManager.OnArEntered -= InitializeAssets;
            SpawnController.OnObjectClicked -= PlaySound;
            TouchManager.OnDone -= StopPlayingSound;
            TouchManager.OnSingleTouch -= ScreenTouched;
        }

        private void Awake()
        {
            _hits = new List<ARRaycastHit>();
        }

        /// <summary>
        /// Changes the current AR object and loads its assets into memory.
        /// </summary>
        private void NextObject()
        {
            UnloadAssets();
            int indexOfCurrentReference = GetIndexOfCurrentArObject();
            _currentArObject = indexOfCurrentReference == _spawnableObjects.Count - 1 ? _spawnableObjects[0] : _spawnableObjects[indexOfCurrentReference + 1];
            LoadAssets(_currentArObject);
        }

        /// <summary>
        /// Plays audio clip.
        /// </summary>
        private void PlaySound()
        {
            _audioManager.PlayAudio(_currentArObject);
        }

        /// <summary>
        /// Stops audio clip.
        /// </summary>
        private void StopPlayingSound()
        {
            _audioManager.StopAudio();
        }

        /// <summary>
        /// Returns current AR object index.
        /// </summary>
        /// <returns>Index of the current AR object.</returns>
        private int GetIndexOfCurrentArObject()
        {
            int index = _spawnableObjects.IndexOf(_currentArObject);
            if (index == -1)
            {
                return 0;
            }
            return index;
        }

        /// <summary>
        /// Saves asset references to local variables.
        /// </summary>
        /// <param name="arObject">AR object which contains asset references.</param>
        private void SetAssetReference(ArObject arObject)
        {
            _icon = arObject.ObjectIcon;
            _model = arObject.ObjectModel;
            _audio = arObject.ObjectAudio;
        }

        /// <summary>
        /// Loads assets from asset references into memory.
        /// </summary>
        /// <param name="arObject">AR object providing asset references.</param>
        private void LoadAssets(ArObject arObject)
        {
            SetAssetReference(arObject);
            AddressableManager.Instance.AddressableAssetLoad(_model, OnModelLoad);
            AddressableManager.Instance.AddressableAssetLoad(_icon, OnIconLoad);
        }

        /// <summary>
        /// Unloads loaded assets from memory.
        /// </summary>
        private void UnloadAssets()
        {
            AddressableManager.Instance.AddressableAssetUnload(_model);
            AddressableManager.Instance.AddressableAssetUnload(_icon);

            if (AddressableManager.Instance.CheckIfAssetLoaded(_audio))
            {
                AddressableManager.Instance.AddressableAssetUnload(_audio);
            }
            
            OnObjectReset?.Invoke(null);
        }

        /// <summary>
        /// Toggles AR view.
        /// </summary>
        /// <param name="toggleValue">True to activate it, false otherwise.</param>
        private void ToggleArView(bool toggleValue)
        {
            _arSessionOrigin.enabled = toggleValue;
            _session.enabled = toggleValue;
            TouchManager.Instance.enabled = toggleValue;
        }

        /// <summary>
        /// Initializes starting assets.
        /// </summary>
        /// <param name="toggleValue">True to initialize, false otherwise.</param>
        private void InitializeAssets(bool toggleValue)
        {
            if (toggleValue)
            {
                int indexOfCurrentReference = GetIndexOfCurrentArObject();
                _currentArObject = _spawnableObjects[indexOfCurrentReference];
                LoadAssets(_currentArObject);
            }
        }

        /// <summary>
        /// Changes current application state and turns of AR view when back button is clicked.
        /// </summary>
        private void OnClickBack()
        {
            ScreenManager.Instance.ChangeState(AppState.MainMenu);
            ScreenManager.OnArEntered?.Invoke(false);
        }

        /// <summary>
        /// Changes objects when button is clicked.
        /// </summary>
        private void OnClickChangeObject()
        {
            NextObject();
        }

        /// <summary>
        /// Checks if screen was touched.
        /// </summary>
        /// <param name="position">Position of touch.</param>
        private void ScreenTouched(Vector3 position)
        {
            if (_raycastManager.Raycast(position, _hits))
            {
                OnScreenTouched?.Invoke(_hits[0].pose.position);
            }
        }
    }
}
