using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FeelGoodOpgUtils
{
	public class CustomButton : InteractibleUiElement, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		public bool Disabled;

		[SerializeField] private Image Image;
		private Sprite DefaultSprite;
		[SerializeField] private Sprite HoveredSprite;
		[SerializeField] private Sprite ClickedSprite;

		private Quaternion OriginalRotation;
		[SerializeField] private float HoveredRotation;
		[SerializeField] private float ClickedRotation;

		[SerializeField] private bool UseColorTransition;
		private Color DefaultColor;
		[ShowIf(nameof(UseColorTransition))] [SerializeField] private Color HoveredColor;
		[ShowIf(nameof(UseColorTransition))] [SerializeField] private Color ClickedColor;

		[SerializeField] private bool UseScalingTransition;
		private Vector3 OriginalScale;
		[ShowIf(nameof(UseScalingTransition))] [SerializeField] private bool TweenWhenScaling;
		[ShowIf(nameof(UseScalingTransition))] [SerializeField] private Vector3 HoveredScale;
		[ShowIf(nameof(UseScalingTransition))] [SerializeField] private Vector3 ClickedScale;

		private Action<CustomButton> OnHoverCallback;
		private Action<CustomButton> OnUnhoverCallback;

		public UnityEvent OnClickEvent;
		[SerializeField] private OnClickTarget OnClickTarget;
		protected bool Clicked;

		private const float TweenScaleTime = 0.25f;

		private void Awake()
		{
			OriginalRotation = transform.localRotation;
			if (Image == null)
				Image = GetComponent<Image>();
			DefaultSprite = Image.sprite;
			DefaultColor = Image.color;
			OriginalScale = Image.transform.localScale;
			Clicked = false;
		}

		public void Initialize(Action<CustomButton> onHoverCallback, Action<CustomButton> onUnhoverCallback)
		{
			OnHoverCallback = onHoverCallback;
			OnUnhoverCallback = onUnhoverCallback;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (CanBeClicked == false)
				return;

			SetState(ButtonState.Hovered);
			if (OnHoverCallback != null)
				OnHoverCallback.Invoke(this);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (CanBeClicked == false)
				return;

			SetState(ButtonState.Default);
			Clicked = false;
			if (OnUnhoverCallback != null)
				OnUnhoverCallback.Invoke(this);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (CanBeClicked == false)
				return;

			SetState(ButtonState.Clicked);
			Clicked = true;
		}

		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (CanBeClicked == false)
				return;

			if (Clicked == false)
				return;

			SetState(ButtonState.Hovered);
			InvokeOnClickEvent();
		}

		public void InvokeOnClickEvent()
		{
			if (AnyOnClickEvents == true)
				OnClickEvent.Invoke();
			if (AnyOnClickTarget == true)
				OnClickTarget.OnClick();
		}

		public override void SetState(ButtonState state)
		{
			State = state;
			switch (state)
			{
				case ButtonState.Default:
					if (DefaultSprite != null)
						Image.sprite = DefaultSprite;
					if (UseColorTransition == true)
						Image.color = DefaultColor;
					if (UseScalingTransition == true)
						ScaleImageToSize(OriginalScale);
					transform.localRotation = OriginalRotation;
					return;
				case ButtonState.Hovered:
					if (HoveredSprite != null)
						Image.sprite = HoveredSprite;
					if (UseColorTransition == true)
						Image.color = HoveredColor;
					if (UseScalingTransition == true)
						ScaleImageToSize(HoveredScale);
					transform.localRotation = OriginalRotation * Quaternion.Euler(0.0f, 0.0f, HoveredRotation);
					return;
				case ButtonState.Clicked:
					if (ClickedSprite != null)
						Image.sprite = ClickedSprite;
					if (UseColorTransition == true)
						Image.color = ClickedColor;
					if (UseScalingTransition == true)
						ScaleImageToSize(ClickedScale);
					transform.localRotation = OriginalRotation * Quaternion.Euler(0.0f, 0.0f, ClickedRotation);
					return;
				default:
					throw new UnityException($"Unknown button state {state}");
			}
		}

		private void ScaleImageToSize(Vector3 scale)
		{
			if (TweenWhenScaling == true)
				LeanTween.scale(Image.gameObject, scale, TweenScaleTime);
			else
				Image.transform.localScale = scale;
		}

		private bool AnyOnClickEvents => OnClickEvent != null && OnClickEvent.GetPersistentEventCount() > 0;

		private bool AnyOnClickTarget => OnClickTarget != null && OnClickTarget.CanBeClicked() == true;

		public bool CanBeClicked => Disabled == false && (AnyOnClickEvents == true || AnyOnClickTarget == true);

		public void SetDefaultSprite(Sprite sprite)
		{
			DefaultSprite = sprite;
			Image.sprite = sprite;
		}
	}
}
