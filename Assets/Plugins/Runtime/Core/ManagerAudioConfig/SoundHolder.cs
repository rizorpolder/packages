﻿using Plugins.AudioManager.Runtime.Core.Manager;

namespace Plugins.AudioManager.Runtime.Core.ManagerAudioConfig
{
	internal class SoundHolder
	{
		private SettingsAudioInstance audio;
		internal string tAudio;

		internal SettingsAudioInstance GetAudio()
		{
			return audio;
		}

		internal void SetAudio(SettingsAudioInstance value)
		{
			audio = value;
		}
	}
}