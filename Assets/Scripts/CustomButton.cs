using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace FeelGoodOpgUtils
{
	public class CustomButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
	{
		private Image Image;
		private Sprite DefaultSprite;
		public Sprite HoveredSprite;
		public Sprite ClickedSprite;

		public UnityEvent OnClickEvent;
		private bool Clicked;

		private void Awake()
		{
			Image = GetComponent<Image>();
			DefaultSprite = Image.sprite;
			Clicked = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			Image.sprite = HoveredSprite;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			Image.sprite = DefaultSprite;
			Clicked = false;
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			Image.sprite = ClickedSprite;
			Clicked = true;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			if (Clicked == false)
				return;
			Debug.Log($"Clicked");
			Image.sprite = HoveredSprite;
			OnClickEvent.Invoke();
		}
	}
}
