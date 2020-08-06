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

		public static bool GetMousePositionOnPlane(this Camera camera, Vector3 position, out Vector3 positionOnPlane, Vector3? cursorPosition = null)
		{
			if (cursorPosition.HasValue == false)
				cursorPosition = Input.mousePosition;

			positionOnPlane = Vector3.zero;
			Plane plane = new Plane(Vector3.up, position);
			Ray ray = Camera.main.ScreenPointToRay(cursorPosition.Value);
			if (plane.Raycast(ray, out float entryPoint) == true)
			{
				positionOnPlane = ray.GetPoint(entryPoint);
				return true;
			}
			return false;
		}
	}
}
