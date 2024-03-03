using BepInEx;
using BepInEx.Logging;
using Configgy;
using HarmonyLib;

namespace UKAbominator;

[BepInPlugin(PluginData.GUID, PluginData.Name, PluginData.Version)]
public class Plugin : BaseUnityPlugin
{
	private ConfigBuilder config;
	[Configgable(displayName: "Abominator Keybind")]
	public static ConfigKeybind abominator = new(UnityEngine.KeyCode.T);
	private void Awake()
	{
		config = new ConfigBuilder(PluginData.GUID, PluginData.Name);
		config.BuildAll();

		Harmony harmony = new Harmony(PluginData.GUID);
		harmony.PatchAll(typeof(Patches));
	}
}
