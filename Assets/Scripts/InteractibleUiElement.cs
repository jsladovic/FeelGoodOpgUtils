using UnityEngine;

namespace FeelGoodOpgUtils
{
	public abstract class InteractibleUiElement : MonoBehaviour
	{
		public abstract void SetState(ButtonState state);
	}

	public enum ButtonState
	{
		Default = 0,
		Hovered = 1,
		Clicked = 2,
	}
}
