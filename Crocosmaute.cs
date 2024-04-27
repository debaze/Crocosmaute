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
public class Crocosmaute : BaseUnityPlugin
{
    public static Crocosmaute Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    internal static Harmony? Harmony { get; set; }
	internal static AudioClip[] newSFX;


    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;
        Logger.LogInfo(((BaseUnityPlugin)Instance).Info.Location);
        AssetBundle val = AssetBundle.LoadFromFile(((BaseUnityPlugin)Instance).Info.Location.TrimEnd("Crocosmaute.dll".ToCharArray()) + "freebird");
        if (val == null)
        {
            Logger.LogError((object)"Failed to load audio assets!");
            return;
        }

        newSFX = val.LoadAssetWithSubAssets<AudioClip>("assets/freebird.mp3");

        Logger.LogInfo(newSFX);

        Patch();

        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void Patch()
    {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll();

        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }
}
