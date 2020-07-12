using UnityEngine;

namespace FeelGoodOpgUtils
{
	public class Billboard : MonoBehaviour
	{
		public Transform CameraTransform;
		private Transform CanvasTransform;
		private Quaternion OriginalRotation;

		private void Start()
		{
			if (CameraTransform == null)
				CameraTransform = Camera.main.transform;

			CanvasTransform = GetComponent<Canvas>()?.transform;
			if (CanvasTransform == null)
				throw new UnityException("Can't find canvas on object");
			OriginalRotation = CanvasTransform.rotation;
		}

		private void Update()
		{
			CanvasTransform.rotation = CameraTransform.rotation * OriginalRotation;
		}
	}
}
