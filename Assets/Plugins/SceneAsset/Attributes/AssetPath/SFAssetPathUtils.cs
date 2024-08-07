﻿using System;
using System.IO;

namespace SFAsset.Attributes.AssetPath
{
	public static class SFAssetPathUtils
	{
		private const string RESOURCES_FOLDER_NAME = "/Resources/";

        /// <summary>
        /// </summary>
        /// <param name="assetPath"></param>
        /// <returns>Type of asset Resources Or Bundle</returns>
        public static SFAssetPathTypes GetAssetPathType(string assetPath)
		{
			if (string.IsNullOrEmpty(assetPath)) return SFAssetPathTypes.None;

			if (assetPath.Contains("Resources/")) return SFAssetPathTypes.Resources;

			return SFAssetPathTypes.Project;
		}

		public static string GetAssetName(string projectPath)
		{
			// Make sure it's not empty
			if (string.IsNullOrEmpty(projectPath)) return string.Empty;

			// Get the index of the resources folder
			var folderIndex = projectPath.IndexOf(RESOURCES_FOLDER_NAME, StringComparison.Ordinal);

			// If it's -1 we this asset is not in a resource folder
			if (folderIndex == -1) return string.Empty;

			// We don't include the 'Resources' part in our final path
			folderIndex += RESOURCES_FOLDER_NAME.Length;

			// Calculate the full length of our substring 
			var length = projectPath.Length - folderIndex;

			// Get the substring
			var resourcesPath = projectPath.Substring(folderIndex, length);

			// Return it.
			return Path.GetFileNameWithoutExtension(resourcesPath);
		}

        /// <summary>
        ///     Takes the string from the Asset Path Attribute and converts it into
        ///     a usable resources path.
        /// </summary>
        /// <param name="projectPath">The project path that AssetPathAttribute serializes</param>
        /// <returns>The resources path if it exists otherwise returns the same path</returns>
        public static string ConvertToResourcesPath(string projectPath, bool keepExtension)
		{
			// Make sure it's not empty
			if (string.IsNullOrEmpty(projectPath)) return string.Empty;

			// Get the index of the resources folder
			var folderIndex = projectPath.IndexOf(RESOURCES_FOLDER_NAME, StringComparison.Ordinal);

			// If it's -1 we this asset is not in a resource folder
			if (folderIndex == -1) return string.Empty;

			// We don't include the 'Resources' part in our final path
			folderIndex += RESOURCES_FOLDER_NAME.Length;

			// Calculate the full length of our substring 
			var length = projectPath.Length - folderIndex;

			// Get the substring
			var resourcesPath = projectPath.Substring(folderIndex, length);

			// Return it.
			return keepExtension ? resourcesPath : Path.ChangeExtension(resourcesPath, string.Empty).TrimEnd('.');
		}

//        /// <summary>
//        /// Takes the string from the Asset Path Attribute and converts it into
//        /// a usable resources path.
//        /// </summary>
//        /// <param name="projectPath">The project path that AssetPathAttribute serializes</param>
//        /// <returns>The resources path if it exists otherwise returns the same path</returns>
//        public static string ConvertToStreamingAssetsPath(string projectPath)
//        {
//            // Make sure it's not empty
//            if (string.IsNullOrEmpty(projectPath))
//            {
//                return string.Empty;
//            }
//
//            // Get the index of the resources folder
//            int folderIndex = projectPath.IndexOf(STREAMING_FOLDER_NAME, StringComparison.Ordinal);
//
//            // If it's -1 we this asset is not in a resource folder
//            if (folderIndex == -1)
//            {
//                return string.Empty;
//            }
//
//            // We don't include the 'Resources' part in our final path
//            folderIndex += STREAMING_FOLDER_NAME.Length;
//
//            // Calculate the full length of our substring 
//            int length = projectPath.Length - folderIndex;
//
//            // Get the substring
//            string resourcesPath = projectPath.Substring(folderIndex, length);
//
//            // Return it.
//            return Application.streamingAssetsPath + "/" + resourcesPath;
//        }
	}
}