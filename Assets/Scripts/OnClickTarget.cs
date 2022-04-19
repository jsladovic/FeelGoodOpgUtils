using UnityEngine;

namespace FeelGoodOpgUtils
{
	public abstract class OnClickTarget : MonoBehaviour
	{
		public abstract void OnClick();
		public abstract bool CanBeClicked();
	}
}
