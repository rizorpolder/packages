using System;
using UnityEngine;

namespace Plugins.AudioManager.Runtime.Core.ControllerAudio
{
	[Serializable]
	internal class EventsHolder
	{
		public EventTypes type;
		public string tAudio;

		public string stringValue;
		public int intValue;
		public Animator animatorValue;
	}
}