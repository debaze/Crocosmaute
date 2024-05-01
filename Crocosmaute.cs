using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using UnityEngine;

namespace Crocosmaute;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("BMX.LobbyCompatibility", BepInDependency.DependencyFlags.HardDependency)]
[LobbyCompatibility(CompatibilityLevel.ClientOnly, VersionStrictness.None)]
public class Crocosmaute : BaseUnityPlugin {
	public static Crocosmaute Instance {
		get; private set;
	} = null!;

	internal new static ManualLogSource Logger {
		get;
		private set;
	} = null!;

	internal static Harmony? Harmony {
		get;
		set;
	}

	internal static AudioClip[]? audioClips;

	private void Awake() {
		Logger = base.Logger;
		Instance = this;

		string assetBundlePath = Instance.Info.Location.TrimEnd(".dll".ToCharArray());
		AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);

		if (assetBundle == null) {
			Logger.LogError("Failed to load AssetBundle.");

			return;
		}

		audioClips = assetBundle.LoadAllAssets<AudioClip>();

		Patch();

		Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
	}

	internal static void Patch() {
		Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

		Logger.LogDebug("Patching...");

		Harmony.PatchAll();

		Logger.LogDebug("Finished patching!");
	}

	internal static void Unpatch() {
		Logger.LogDebug("Unpatching...");

		Harmony?.UnpatchSelf();

		Logger.LogDebug("Finished unpatching!");
	}
}