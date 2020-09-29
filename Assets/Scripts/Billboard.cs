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
				CameraTransform = Camera.main.transform;
			
			OriginalRotation = transform.rotation;
		}

		private void Update()
		{
			transform.rotation = CameraTransform.rotation * OriginalRotation;
		}
	}
}
