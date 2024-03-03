using System.Reflection;
using Configgy;
using HarmonyLib;
using UnityEngine;

public class Patches 
{
	[HarmonyPatch(typeof(NewMovement), "Update")]
	[HarmonyPostfix]
	public static void p_NewMovement_Update(ref NewMovement __instance)
	{
		if (UKAbominator.Plugin.abominator.WasPeformed()) {
			NewMovementAdditions.FlipPlayer(__instance);
		}
		if (__instance.gameObject.GetComponent<NewMovementAdditions>().isFlipped) {
			NewMovementAdditions.UpdateFlipPlayer(__instance);
		}
	}
	[HarmonyPatch(typeof(Rigidbody), "velocity", MethodType.Setter)]
	[HarmonyPrefix]
	public static void p_set_Rigidbody_velocity(ref Rigidbody __instance, ref Vector3 __0)
	{
		// very fucking risky and i hate myself.
		if (__instance.gameObject.GetComponent<NewMovementAdditions>() && __instance.gameObject.GetComponent<NewMovementAdditions>().isFlipped && __0 == new Vector3(0, -100, 0)) {
			__0.y = 100f;
		}
	}
	[HarmonyPatch(typeof(NewMovement), "Start")]
	[HarmonyPostfix]
	public static void p_NewMovement_Start(ref NewMovement __instance)
	{
		__instance.gameObject.AddComponent<NewMovementAdditions>();
	}
}