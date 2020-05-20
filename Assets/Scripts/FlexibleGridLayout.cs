using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
	public enum FitType
	{
		Uniform = 1,
		Width = 2,
		Height = 3,
		FixedRows = 4,
		FixedColumns = 5
	}

	public FitType LayoutFitType;
	public int Rows;
	public int Columns;
	public Vector2 CellSize;

	public Vector2 Spacing;

	public bool FitX;
	public bool FitY;

	public bool StretchX;
	public bool StretchY;

	public override void CalculateLayoutInputHorizontal()
	{
		base.CalculateLayoutInputHorizontal();
		if (rectChildren.Count == 0)
			return;

		ValidateInput();

		if (LayoutFitType == FitType.Uniform || LayoutFitType == FitType.Height || LayoutFitType == FitType.Width)
		{
			FitX = true;
			FitY = true;

			float sqrt = Mathf.Sqrt(transform.childCount);
			Rows = Mathf.CeilToInt(sqrt);
			Columns = Mathf.CeilToInt(sqrt);
		}

		if (LayoutFitType == FitType.Width || LayoutFitType == FitType.FixedColumns)
		{
			Rows = Mathf.CeilToInt(transform.childCount / (float)Columns);
		}
		if (LayoutFitType == FitType.Height || LayoutFitType == FitType.FixedRows)
		{
			Columns = Mathf.CeilToInt(transform.childCount / (float)Rows);
		}

		float parentWidth = rectTransform.rect.width;
		float parentHeight = rectTransform.rect.height;

		float cellWidth = (parentWidth / Columns) - ((Columns - 1) * Spacing.x / Columns) - (padding.left / Columns) - (padding.right / Columns);
		float cellHeight = (parentHeight / Rows) - ((Rows - 1) * Spacing.y / Rows) - (padding.top / Rows) - (padding.bottom / Rows);

		CellSize.x = FitX ? cellWidth : CellSize.x;
		CellSize.y = FitY ? cellHeight : CellSize.y;

		int columnCount = 0;
		int rowCount = 0;

		for (int i = 0; i < rectChildren.Count; i++)
		{
			rowCount = i / Columns;
			columnCount = i % Columns;

			RectTransform item = rectChildren[i];

			float xPos = (CellSize.x * columnCount) + (Spacing.x * columnCount) + padding.left;
			float yPos = (CellSize.y * rowCount) + (Spacing.y * rowCount) + padding.top;

			SetChildAlongAxis(item, 0, xPos, CellSize.x);
			SetChildAlongAxis(item, 1, yPos, CellSize.y);
		}

		if (StretchX == true || StretchY == true)
		{
			rectTransform.sizeDelta = new Vector2(
				StretchX ? (columnCount + 1) * (CellSize.x + Spacing.x) + padding.left + padding.right : rectTransform.sizeDelta.x,
				StretchY ? (rowCount + 1) * (CellSize.y + Spacing.y) + padding.top + padding.bottom : rectTransform.sizeDelta.y);
		}
	}

	private void ValidateInput()
	{
		if (Spacing.x < 0.0f)
			Spacing.x = 0.0f;
		if (Spacing.y < 0.0f)
			Spacing.y = 0.0f;
		if (padding.left < 0)
			padding.left = 0;
		if (padding.right < 0)
			padding.right = 0;
		if (padding.top < 0)
			padding.top = 0;
		if (padding.bottom < 0)
			padding.bottom = 0;
		if (LayoutFitType == FitType.FixedColumns && Columns == 0)
			Columns = 1;
		if (LayoutFitType == FitType.FixedRows && Rows == 0)
			Rows = 1;
		if (FitX == true)
			StretchX = false;
		if (FitY == true)
			StretchY = false;
	}

	public override void CalculateLayoutInputVertical()
	{
	}

	public override void SetLayoutHorizontal()
	{
	}

	public override void SetLayoutVertical()
	{
	}
}
