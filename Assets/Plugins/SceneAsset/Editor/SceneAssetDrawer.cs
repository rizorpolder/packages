﻿using System;
using System.Collections.Generic;
using SceneAsset.Attributes.AssetPath;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace SFAsset.Editor
{
    [CustomPropertyDrawer(typeof(SceneAsserAttribute))]
    public class SceneAssetDrawer : PropertyDrawer
    {
        // A helper warning label when the user puts the attribute above a non string type.
        private const string m_InvalidTypeLabel = "Attribute invalid for type ";
        private string m_ActivePickerPropertyPath;
        private int m_PickerControlID = -1;

        // A shared array of references to the objects we have loaded
        private IDictionary<string, Object> m_References;


        /// <summary>
        /// Invoked when unity creates our drawer. 
        /// </summary>
        public SceneAssetDrawer()
        {
            m_References = new Dictionary<string, Object>();
        }

        /// <summary>
        /// Invoked when we want to try our property. 
        /// </summary>
        /// <param name="position">The position we have allocated on screen</param>
        /// <param name="property">The field our attribute is over</param>
        /// <param name="label">The nice display label it has</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property = GetProperty(property);

            if (property.type != typeof(SceneAsset.SceneAsset).Name)
            {
                // Create a rect for our label
                Rect labelPosition = position;
                // Set it's width 
                labelPosition.width = EditorGUIUtility.labelWidth;
                // Draw it
                GUI.Label(labelPosition, label);
                // Create a rect for our content
                Rect contentPosition = position;
                // Move it over by the x
                contentPosition.x += labelPosition.width;
                // Shrink it in width since we moved it over
                contentPosition.width -= labelPosition.width;
                // Draw our content warning;
                EditorGUI.HelpBox(contentPosition, m_InvalidTypeLabel + this.fieldInfo.FieldType.Name,
                    MessageType.Error);
            }
            else
            {
                HandleObjectReference(position, property, label);
            }
        }

        protected virtual SerializedProperty GetProperty(SerializedProperty rootProperty)
        {
            return rootProperty;
        }


        protected virtual Type ObjectType()
        {
            // Get our attribute
            SceneAsserAttribute attr = attribute as SceneAsserAttribute;
            // Return back the type.
            return attr?.Type;
        }

        private void HandleObjectReference(Rect position, SerializedProperty property, GUIContent label)
        {
            Type objectType = ObjectType();

            if (objectType == typeof(Scene))
            {
                objectType = typeof(SceneAsset.SceneAsset);
            }

            // First get our value
            Object propertyValue = null;

            var guidProperty = property.FindPropertyRelative("guid");

            var guid = guidProperty.stringValue;
            // Save our path
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            // Have a label to say it's missing
            //bool isMissing = false;
            // Check if we have a key
            if (m_References.ContainsKey(guid))
            {
                // Get the value. 
                propertyValue = m_References[guid];
            }

            // Now if its null we try to load it
            if (propertyValue == null && !string.IsNullOrEmpty(assetPath))
            {
                // Try to load our asset
                propertyValue = AssetDatabase.LoadAssetAtPath(assetPath, objectType);

                if (propertyValue != null)
                {
                    m_References[guid] = propertyValue;
                }
            }

            // Draw our object field.
            propertyValue = EditorGUI.ObjectField(position, label, propertyValue, objectType, false);

            OnSelectionMade(propertyValue, property);
        }

        protected virtual void OnSelectionMade(Object newSelection, SerializedProperty property)
        {
            string assetPath = string.Empty;

            if (newSelection != null)
            {
                // Get our path
                assetPath = AssetDatabase.GetAssetPath(newSelection);
            }

            var guid = AssetDatabase.AssetPathToGUID(assetPath);

            // Save our value.
            m_References[guid] = newSelection;

            var pathProperty = property.FindPropertyRelative("path");
            var guidProperty = property.FindPropertyRelative("guid");
            var typeProperty = property.FindPropertyRelative("type");

            guidProperty.stringValue = guid;
            pathProperty.stringValue = assetPath;

            if (newSelection != null)
                typeProperty.stringValue = newSelection.GetType().FullName;
            else
                typeProperty.stringValue = string.Empty;
        }
    }
}