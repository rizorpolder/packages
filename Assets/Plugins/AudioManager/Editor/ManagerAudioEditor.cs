using Plugins.AudioManager.Runtime.Core.Manager;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Plugins.AudioManager.Editor
{
#if UNITY_EDITOR

	[CustomEditor(typeof(ManagerAudio))]
	public class ManagerAudioEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			var sourcesCount = serializedObject.FindProperty("audioSources")?.FindPropertyRelative("Array.size");
			GUILayout.Label($"{sourcesCount}");
		}
	}
#endif
}