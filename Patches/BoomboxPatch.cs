using HarmonyLib;
using UnityEngine;

namespace Crocosmaute.Patches;

[HarmonyPatch(typeof(BoomboxItem))]
public class BoomboxPatch {
	[HarmonyPatch("Start")]
	[HarmonyPostfix]
	private static void StartPostfix(ref AudioSource ___boomboxAudio) {
		//
	}
}