using UnityEngine;

namespace FeelGoodOpgUtils
{
	public class Billboard : MonoBehaviour
	{
		public Transform CameraTransform;
		private Quaternion OriginalRotation;

		private void Start()
		{
			OriginalRotation = transform.rotation;
			if (CameraTransform == null)
				SetCamera(Camera.main.transform);
			else
				SetCamera(CameraTransform);
		}

		public void SetCamera(Transform camera)
		{
			CameraTransform = camera;
		}

		private void Update()
		{
			transform.rotation = CameraTransform.rotation * OriginalRotation;
		}
	}
}
