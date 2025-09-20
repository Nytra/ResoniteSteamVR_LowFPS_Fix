using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Renderite.Unity;
using Valve.VR;

namespace SteamVR_LowFPS_Fix;

[BepInPlugin("Nytra.SteamVR_LowFPS_Fix", "SteamVR Low FPS Fix", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource? Log;

	void Awake()
	{
		Log = base.Logger;

		var harmony = new Harmony("Nytra.SteamVR_LowFPS_Fix");
		harmony.PatchAll();
	}
}

[HarmonyPatch(typeof(RenderingManager), "UpdateVR_Active")]
class Patch
{
	static bool Prefix(bool vrActive)
	{
		SteamVR.instance?.compositor.SuspendRendering(!vrActive);
		return true;
	}
}