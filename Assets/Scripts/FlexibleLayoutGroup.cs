using UnityEngine;
using UnityEngine.UI;

public class FlexibleLayoutGroup : LayoutGroup
{

    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    [Header("Grid")]
    public FitType fitType = FitType.Uniform;

    public int rows;
    public int columns;

    public Vector2 spacing;

    float aspectRatio = 599f/840f;

    [Header("Padding")]
    public bool fitX = true;
    public bool fitY = true;

    private Vector2 cellSize;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        int childCount = rectChildren.Count;

        if (fitType == FitType.Uniform ||
            fitType == FitType.Width ||
            fitType == FitType.Height)
        {
            float sqrt = Mathf.Sqrt(childCount);

            rows = Mathf.CeilToInt(sqrt);
            columns = Mathf.CeilToInt(sqrt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(childCount / (float)columns);
        }

        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float availableWidth =
            parentWidth
            - padding.left
            - padding.right
            - spacing.x * (columns - 1);

        float availableHeight =
            parentHeight
            - padding.top
            - padding.bottom
            - spacing.y * (rows - 1);

        float cellWidth = availableWidth / columns;
        float cellHeight = availableHeight / rows;

        float widthBasedHeight = cellWidth / aspectRatio;
        float heightBasedWidth = cellHeight * aspectRatio;

        if (widthBasedHeight <= cellHeight)
        {
            cellSize.x = cellWidth;
            cellSize.y = widthBasedHeight;
        }
        else
        {
            cellSize.x = heightBasedWidth;
            cellSize.y = cellHeight;
        }

        float totalHeight = rows * cellSize.y + (rows - 1) * spacing.y;

        float startY = (parentHeight - totalHeight) * 0.5f;

        for (int i = 0; i < childCount; i++)
        {
            int row = i / columns;
            int column = i % columns;

            int itemsInThisRow = Mathf.Min(columns, childCount - row * columns);

            float rowWidth = itemsInThisRow * cellSize.x + (itemsInThisRow - 1) * spacing.x;

            float startX = (parentWidth - rowWidth) * 0.5f;

            float xPos = startX + column * (cellSize.x + spacing.x);

            float yPos = startY + row * (cellSize.y + spacing.y);

            SetChildAlongAxis(rectChildren[i], 0, xPos, cellSize.x);
            SetChildAlongAxis(rectChildren[i], 1, yPos, cellSize.y);
        }
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
