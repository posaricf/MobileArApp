using System;
using UnityEngine.AddressableAssets;

namespace DeltaReality.Quiz
{
    /// <summary>
    /// Class that represents an AR object.
    /// </summary>
    [Serializable]
    public class ArObject
    {
        public AssetReference ObjectIcon;
        public AssetReference ObjectModel;
        public AssetReference ObjectAudio;
    }
}
