using System;
using UnityEngine;

namespace SceneAsset
{
    [Serializable]
    public struct SceneAsset
    {
        /// <summary>
        /// Returns path to asset in Project space
        /// </summary>
        public string Path => path;

        [SerializeField]
        private string path;

        [SerializeField, HideInInspector]
        private string guid;

        [SerializeField, HideInInspector]
        private string type;
    }
}