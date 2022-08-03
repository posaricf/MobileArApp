using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

namespace DeltaReality.Quiz.ArFunctions
{
    /// <summary>
    /// Generic class used for managing addressables.
    /// </summary>
    public class AddressableManager : MonoBehaviour
    {
        public static AddressableManager Instance;

        private Dictionary<object, AsyncOperationHandle> _dictionary;
        private Dictionary<AsyncOperationHandle, List<object>> _subscriberDictionary;

        private void Awake()
        {
            Instance = this;
            _dictionary = new Dictionary<object, AsyncOperationHandle>();
            _subscriberDictionary = new Dictionary<AsyncOperationHandle, List<object>>();
        }

        /// <summary>
        /// Loads an object from asset reference argument into memory and invokes callback with it as its property.
        /// </summary>
        /// <typeparam name="T">Type which will be invoked.</typeparam>
        /// <param name="assetReference">Reference to an addressable type.</param>
        /// <param name="callback">Callback to be invoked.</param>
        public void AddressableAssetLoad<T>(AssetReference assetReference, Action<T> callback)
        {
            AsyncOperationHandle handle;
            bool isLoaded = CheckIfAssetLoaded(assetReference);
            if (isLoaded)
            {
                handle = AddNewSubscriber(assetReference);
                if (handle.IsDone)
                {
                    callback?.Invoke((T)handle.Result);
                    return;
                }

                handle.Completed += (handle) =>
                {
                    callback?.Invoke((T)handle.Result);
                };
            }
            else
            {
                handle = Addressables.LoadAssetAsync<T>(assetReference);
                _dictionary.Add(assetReference.RuntimeKey, handle);
                _subscriberDictionary.Add(handle, new List<object>());
                handle.Completed += (handle) =>
                {
                    callback?.Invoke((T)handle.Result);
                };
            }
        }

        /// <summary>
        /// Unloads and releases a reference from memory.
        /// </summary>
        /// <param name="assetReference">Asset reference to be released.</param>
        public void AddressableAssetUnload(AssetReference assetReference)
        {
            AsyncOperationHandle handle = _dictionary[assetReference.RuntimeKey];
            Addressables.Release(handle);
            _dictionary.Remove(assetReference.RuntimeKey);
        }

        /// <summary>
        /// Checks if an asset from a reference is loaded into memory.
        /// </summary>
        /// <param name="assetReference">Addressable asset reference.</param>
        /// <returns></returns>
        public bool CheckIfAssetLoaded(AssetReference assetReference)
        {
            if (_dictionary.ContainsKey(assetReference.RuntimeKey)) 
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds a new subscriber into list of subscribers for a given asset reference.
        /// </summary>
        /// <param name="assetReference">Used for obtaining a handle from a dictionary.</param>
        /// <returns>Async operation handle for a given asset reference runtime key.</returns>
        private AsyncOperationHandle AddNewSubscriber(AssetReference assetReference)
        {
            AsyncOperationHandle handle = _dictionary[assetReference.RuntimeKey];
            List<object> listOfSubscribersForHandle = _subscriberDictionary[handle];
            listOfSubscribersForHandle.Add(assetReference.RuntimeKey);

            return handle;
        }
    }
}
