using UnityEngine;

namespace FeelGoodOpgUtils
{
	public static class VectorExtensions
	{
		public static Vector3 With(this Vector3 vector, float? x = null, float? y = null, float? z = null)
		{
			return new Vector3(x ?? vector.x, y ?? vector.y, z ?? vector.z);
		}
	}
}
