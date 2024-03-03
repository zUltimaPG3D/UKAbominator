using HarmonyLib;
using UnityEngine;

public class NewMovementAdditions : MonoBehaviour
{
	public bool isFlipped = false;
	
	public static void FlipPlayer(NewMovement mov) {
		if (mov.gc.onGround) {
			mov.gameObject.transform.position += Vector3.up * 2f;
		}
		mov.jumpPower *= -1;
		mov.gameObject.GetComponent<NewMovementAdditions>().isFlipped = !mov.gameObject.GetComponent<NewMovementAdditions>().isFlipped;
		mov.cc.reverseY = !mov.cc.reverseY;
		mov.cc.rotationX *= -1;
		Camera hudCam = (Camera)AccessTools.Field(typeof(CameraController), "hudCamera").GetValue(mov.cc);
		hudCam.transform.localScale = new Vector3(hudCam.transform.localScale.x, -hudCam.transform.localScale.y, hudCam.transform.localScale.z);
		mov.gameObject.transform.localScale = new Vector3(mov.gameObject.transform.localScale.x, -mov.gameObject.transform.localScale.y, mov.gameObject.transform.localScale.z);
	}

	public static void UpdateFlipPlayer(NewMovement mov) {
		mov.rb.useGravity = false;
		mov.rb.AddForce(-2*Physics.gravity, ForceMode.Acceleration);

		int m = LayerMask.GetMask("Environment", "Outdoors");

		RaycastHit hit;
		if (Physics.Raycast(mov.transform.position - new Vector3(0, 2f, 0), Vector3.down, out hit, 1f, m)) {
			if (hit.collider.transform.parent == mov.transform)
				return;
			mov.gameObject.transform.position += Vector3.up * 2f;
		}
	}
}