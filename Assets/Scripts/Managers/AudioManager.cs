using DeltaReality.Quiz.ArFunctions;
using DeltaReality.Quiz.Controllers;
using UnityEngine;

namespace DeltaReality.Quiz.Managers
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Pool references")]
        [SerializeField] private Pool _pool;
        [SerializeField] private GameObject _poolObjectPrefab;
        [SerializeField] private GameObject _poolObjectParent;
        [SerializeField] private int _numberOfObjects;

        private GameObject _poolObject;
        private AudioSource _audioSource;

        private void Awake()
        {
            _pool.InitializePool(_poolObjectPrefab, _numberOfObjects, _poolObjectParent.transform);
        }

        private void OnEnable()
        {
            ArController.OnAudioLoad += SetAudioClip;
        }

        private void OnDisable()
        {
            ArController.OnAudioLoad -= SetAudioClip;
        }

        /// <summary>
        /// Plays audio clip.
        /// </summary>
        /// <param name="arObject">Provides object audio reference used for loading audio clip.</param>
        public void PlayAudio(ArObject arObject)
        {
            _poolObject = _pool.GetObject();
            _audioSource = _poolObject.GetComponent<AudioSource>();
            AddressableManager.Instance.AddressableAssetLoad(arObject.ObjectAudio, ArController.OnAudioLoad);
            _audioSource.Play();
        }

        /// <summary>
        /// Stops audio clip that's playing.
        /// </summary>
        public void StopAudio()
        {
            _audioSource.Stop();
            _pool.ReleaseObject(_poolObject);
            _poolObject = null;
            _audioSource = null;

        }

        /// <summary>
        /// Sets audio source's audio clip to method argument.
        /// </summary>
        /// <param name="audio">Audio clip to be set.</param>
        private void SetAudioClip(AudioClip audio)
        {
            _audioSource.clip = audio;
        }
    }
}
