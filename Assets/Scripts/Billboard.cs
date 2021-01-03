using UnityEngine;

namespace FeelGoodOpgUtils
{
	public class Billboard : MonoBehaviour
	{
		public Transform CameraTransform;
		private Quaternion OriginalRotation;

		private void Start()
		{
			if (CameraTransform == null)
				SetCamera(Camera.main.transform);
			else
				SetCamera(CameraTransform);
		}

		public void SetCamera(Transform camera)
		{
			CameraTransform = camera;
			OriginalRotation = transform.rotation;
		}

		private void Update()
		{
			transform.LookAt(CameraTransform);
		}
	}
}
