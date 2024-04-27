using HarmonyLib;
using UnityEngine;

namespace Crocosmaute.Patches;

[HarmonyPatch(typeof(BoomboxItem))]
public class BoomboxPatch {
	[HarmonyPatch("Start")]
	[HarmonyPostfix]
	private static void StartPostfix(ref AudioClip[] ___musicAudios) {
		AudioClip[] audioClips = Crocosmaute.newSFX;

		if (audioClips == null || audioClips.Length == 0) {
			return;
		}

		___musicAudios = [
			audioClips[0],
		];
	}
}