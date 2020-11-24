using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FeelGoodOpgUtils
{
	public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		public ButtonState State { get; private set; }
		public bool Disabled;
		private Image Image;
		private Sprite DefaultSprite;
		public Sprite HoveredSprite;
		public Sprite ClickedSprite;

		private Quaternion OriginalRotation;
		public float HoveredRotation;
		public float ClickedRotation;

		public UnityEvent OnClickEvent;
		private bool Clicked;

		private void Awake()
		{
			OriginalRotation = transform.localRotation;
			Image = GetComponent<Image>();
			DefaultSprite = Image.sprite;
			Clicked = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Disabled == true)
				return;

			SetState(ButtonState.Hovered);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (Disabled == true)
				return;

			SetState(ButtonState.Default);
			Clicked = false;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (Disabled == true)
				return;

			SetState(ButtonState.Clicked);
			Clicked = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (Disabled == true)
				return;

			if (Clicked == false)
				return;

			SetState(ButtonState.Hovered);
			OnClickEvent.Invoke();
		}

		private void SetState(ButtonState state)
		{
			State = state;
			switch (state)
			{
				case ButtonState.Default:
					if (DefaultSprite != null)
						Image.sprite = DefaultSprite;
					transform.localRotation = OriginalRotation;
					return;
				case ButtonState.Hovered:
					if (HoveredSprite != null)
						Image.sprite = HoveredSprite;
					transform.localRotation = OriginalRotation * Quaternion.Euler(0.0f, 0.0f, HoveredRotation);
					return;
				case ButtonState.Clicked:
					if (ClickedSprite != null)
						Image.sprite = ClickedSprite;
					transform.localRotation = OriginalRotation * Quaternion.Euler(0.0f, 0.0f, ClickedRotation);
					return;
				default:
					throw new UnityException($"Unknown button state {state}");
			}
		}

		public void SetDefaultSprite(Sprite sprite)
		{
			DefaultSprite = sprite;
			Image.sprite = sprite;
		}
	}

	public enum ButtonState
	{
		Default = 0,
		Hovered = 1,
		Clicked = 2,
	}
}
