using UnityEngine;
using UnityEngine.UI;

public class UIGridLayoutGroup : LayoutGroup
{
    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns,
    }

    public FitType fitType;
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    public bool fixX;
    public bool fixY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        fixX = fitType == FitType.FixedColumns;
        fixY = fitType == FitType.FixedRows;

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            float _sgrRt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(_sgrRt);
            columns = Mathf.CeilToInt(_sgrRt);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float _parentWidth = rectTransform.rect.width;
        float _parentHeight = rectTransform.rect.height;

        float _cellWidth = (_parentWidth / (float)columns) - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float _cellHeight = (_parentHeight / (float)rows) - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fixX ? _cellWidth : cellSize.x;
        cellSize.y = fixY ? _cellHeight : cellSize.y;

        int _columnCount = 0;
        int _rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            _rowCount = i / columns;
            _columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * _columnCount) + (spacing.x * _columnCount) + padding.left;
            var yPos = (cellSize.y * _rowCount) + (spacing.y * _rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
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
