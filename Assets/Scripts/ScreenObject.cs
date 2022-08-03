using System;
using UnityEngine;

namespace DeltaReality.Quiz
{
    /// <summary>
    /// Class that represents an object shown on the screen.
    /// </summary>
    [Serializable]
    public class ScreenObject
    {
        public AppState State;
        public GameObject ScreenGameObject;
    }
}
