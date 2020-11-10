using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FeelGoodOpgUtils
{
	public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
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
			OriginalRotation = transform.rotation;
			Image = GetComponent<Image>();
			DefaultSprite = Image.sprite;
			Clicked = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (Disabled == true)
				return;

			SetState(State.Hovered);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (Disabled == true)
				return;

			SetState(State.Default);
			Clicked = false;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (Disabled == true)
				return;

			SetState(State.Clicked);
			Clicked = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (Disabled == true)
				return;

			if (Clicked == false)
				return;

			SetState(State.Hovered);
			OnClickEvent.Invoke();
		}

		private void SetState(State state)
		{
			switch (state)
			{
				case State.Default:
					if (DefaultSprite != null)
						Image.sprite = DefaultSprite;
					transform.rotation = OriginalRotation;
					return;
				case State.Hovered:
					if (HoveredSprite != null)
						Image.sprite = HoveredSprite;
					transform.rotation = OriginalRotation * Quaternion.Euler(0.0f, 0.0f, HoveredRotation);
					return;
				case State.Clicked:
					if (ClickedSprite != null)
						Image.sprite = ClickedSprite;
					transform.rotation = OriginalRotation * Quaternion.Euler(0.0f, 0.0f, ClickedRotation);
					return;
				default:
					throw new UnityException($"Unknown button state {state}");
			}
		}

		private enum State
		{
			Default = 0,
			Hovered = 1,
			Clicked = 2,
		}
	}
}
