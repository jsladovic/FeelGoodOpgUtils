using UnityEngine;

namespace FeelGoodOpgUtils
{
	public static class Bezier
	{
		private const int CurveNumberOfPoints = 50;

		public static void SetBezier(this LineRenderer lineRenderer, params Vector3[] positions)
		{
			if (positions.Length < 2 || positions.Length > 4)
				throw new UnityException($"Must submit between 2 and 4 points, sent {positions.Length} instead");

			lineRenderer.positionCount = CurveNumberOfPoints;

			for (int i = 0; i < CurveNumberOfPoints; i++)
			{
				float t = i / (float)CurveNumberOfPoints;
				if (positions.Length == 2)
					lineRenderer.SetPosition(i, LinearPosition(t, positions[0], positions[1]));
				else if (positions.Length == 3)
					lineRenderer.SetPosition(i, QuadraticPosition(t, positions[0], positions[1], positions[2]));
				else if (positions.Length == 4)
					lineRenderer.SetPosition(i, CubicPosition(t, positions[0], positions[1], positions[2], positions[3]));
			}
		}

		public static Vector3 LinearPosition(float t, Vector3 p0, Vector3 p1)
		{
			return p0 + t * (p1 - p0);
		}

		public static Vector3 QuadraticPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2)
		{
			float u = 1 - t;
			return u * u * p0 + 2 * u * t * p1 + t * t * p2;
		}

		public static Vector3 CubicPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			float u = 1 - t;
			float uu = u * u;
			float tt = t * t;
			return u * uu * p0 + 3 * uu * t * p1 + 3 * u * tt * p2 + tt * t * p3;
		}
	}
}
