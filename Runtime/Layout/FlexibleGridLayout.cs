﻿using UnityEngine;
using UnityEngine.UI;

namespace Gameframe.GUI.Layout
{
    public class FlexibleGridLayout : LayoutGroup
    {
        public enum LayoutType
        {
            Uniform,
            FitWidth,
            FitHeight,
            FixedRows,
            FixedColumns
        }
        
        public Vector2 spacing;
        public LayoutType layoutType = LayoutType.Uniform;
        public int rows;
        public int columns;
        public Vector2 cellSize;
        public bool autoCellWidth = true;
        public bool autoCellHeight = true;
        
        public override void CalculateLayoutInputVertical()
        {
            if (layoutType != LayoutType.FixedColumns && layoutType != LayoutType.FixedRows)
            {
                autoCellWidth = true;
                autoCellHeight = true;
                
                var sqrRt = Mathf.Sqrt(transform.childCount);
                rows = Mathf.CeilToInt(sqrRt);
                columns = Mathf.CeilToInt(sqrRt);
            }
            
            if (layoutType == LayoutType.FitWidth || layoutType == LayoutType.FixedColumns)
            {
                rows = Mathf.CeilToInt(transform.childCount / (float) columns);
            }
            else if (layoutType == LayoutType.FitHeight || layoutType == LayoutType.FixedRows)
            {
                columns = Mathf.CeilToInt(transform.childCount / (float) rows);
            }

            var parentWidth = rectTransform.rect.width;
            var parentHeight = rectTransform.rect.height;

            //Declaring a variable here to avoid the property access
            var layoutPadding = padding;

            if (autoCellWidth)
            {
                var cellWidth = (parentWidth / columns) - (spacing.x / columns)*(columns-1) - (layoutPadding.left / (float)columns) - (layoutPadding.right/(float)columns);
                cellSize.x = cellWidth;
            }

            if (autoCellHeight)
            {
                var cellHeight = (parentHeight / rows) - (spacing.y / rows)*(rows-1) - (layoutPadding.top / (float)rows) - (layoutPadding.bottom/(float)rows);
                cellSize.y = cellHeight;
            }

            for (var i = 0; i < rectChildren.Count; i++)
            {
                var rowCount = i / columns;
                var columnCount = i % columns;

                var item = rectChildren[i];

                var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + layoutPadding.left;
                var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + layoutPadding.top;
                
                SetChildAlongAxis(item, 0, xPos, cellSize.x);
                SetChildAlongAxis(item, 1, yPos, cellSize.y);
            }
        }

        public override void SetLayoutHorizontal()
        {
        }

        public override void SetLayoutVertical()
        {
        }
    }
}

