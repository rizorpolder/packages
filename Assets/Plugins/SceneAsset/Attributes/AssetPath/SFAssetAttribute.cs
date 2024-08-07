﻿using System;
using UnityEngine;

namespace SFAsset.Attributes.AssetPath
{
	/// <summary>
	///     We limit this attributes to fields and only allow one. Should
	///     only be applied to string types.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class SFAssetAttribute : PropertyAttribute
	{
		/// <summary>
		///     Creates the default instance of AssetPathAttribute
		/// </summary>
		public SFAssetAttribute(Type type)
		{
			Type = type;
			PathType = SFAssetPathTypes.Project;
		}

		/// <summary>
		///     Gets the type of asset path this attribute is watching.
		/// </summary>
		public SFAssetPathTypes PathType { get; }

		/// <summary>
		///     Gets the type of asset this attribute is expecting.
		/// </summary>
		public Type Type { get; }
	}
}