using UnityEngine;

namespace FeelGoodOpgUtils
{
	public static class CameraExtensions
	{
		public static Vector3 MousePosition(this Camera camera)
		{
			Vector3 position = camera.ScreenToWorldPoint(Input.mousePosition);
			position.z = 0.0f;
			return position;
		}

		public static bool IsMouseOverCamera(this Camera camera)
		{
			return camera.pixelRect.Contains(Input.mousePosition) == true;
		}
	}
}
