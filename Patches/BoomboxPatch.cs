using HarmonyLib;
using UnityEngine;

namespace Crocosmaute.Patches;

[HarmonyPatch(typeof(BoomboxItem))]
public class BoomboxPatch {
	[HarmonyPatch("Start")]
	[HarmonyPostfix]
	private static void StartPostfix(ref AudioClip[] ___musicAudios) {
		AudioClip[] audioClips = Crocosmaute.audioClips;

		if (audioClips == null || audioClips.Length == 0) {
			return;
		}

		___musicAudios = audioClips;
	}

	[HarmonyPatch("StartMusic")]
	[HarmonyPostfix]
	private static void StartMusicPostFix(ref AudioSource ___boomboxAudio) {
		___boomboxAudio.pitch = Random.Range(.9f, 1.1f);
	}
}