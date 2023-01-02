using System;
using UnityEngine;

namespace SceneAsset.Attributes.AssetPath
{
        /// <summary>
        /// We limit this attributes to fields and only allow one. Should
        /// only be applied to string types. 
        /// </summary>
        [AttributeUsage(AttributeTargets.Field)]
        public class SceneAsserAttribute : PropertyAttribute
        {
            private SceneAssetPathTypes m_PathType;
            private Type m_Type;

            /// <summary>
            /// Gets the type of asset path this attribute is watching.
            /// </summary>
            public SceneAssetPathTypes PathType => m_PathType;

            /// <summary>
            /// Gets the type of asset this attribute is expecting.
            /// </summary>
            public Type Type => m_Type;

            /// <summary>
            /// Creates the default instance of AssetPathAttribute
            /// </summary>
            public SceneAsserAttribute(Type type)
            {
                m_Type = type; 
                m_PathType = SceneAssetPathTypes.Project;
            }
    }
}
