using System;
using UnityEngine;

namespace SFAsset
{
	[Serializable]
	public struct SFAsset
	{
        /// <summary>
        ///     Returns path to asset in Project space
        /// </summary>
        public string Path => path;

		[SerializeField]
		private string path;

		[SerializeField] [HideInInspector]
		private string guid;

		[SerializeField] [HideInInspector]
		private string type;
	}
}