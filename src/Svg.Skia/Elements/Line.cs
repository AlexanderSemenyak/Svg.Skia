﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//
// Parts of this source file are adapted from the https://github.com/vvvv/SVG
using System;
using SkiaSharp;
using Svg;

namespace Svg.Skia
{
    internal struct Line : IElement
    {
        public SvgLine svgLine;
        public float x0;
        public float y0;
        public float x1;
        public float y1;
        public SKRect bounds;
        public SKMatrix matrix;

        public Line(SvgLine line)
        {
            svgLine = line;
            x0 = svgLine.StartX.ToDeviceValue(null, UnitRenderingType.Horizontal, svgLine);
            y0 = svgLine.StartY.ToDeviceValue(null, UnitRenderingType.Vertical, svgLine);
            x1 = svgLine.EndX.ToDeviceValue(null, UnitRenderingType.Horizontal, svgLine);
            y1 = svgLine.EndY.ToDeviceValue(null, UnitRenderingType.Vertical, svgLine);
            float x = Math.Min(x0, x1);
            float y = Math.Min(y0, y1);
            float width = Math.Abs(x0 - x1);
            float height = Math.Abs(y0 - y1);
            bounds = SKRect.Create(x, y, width, height);
            matrix = SkiaUtil.GetSKMatrix(svgLine.Transforms);
        }

        public void Draw(SKCanvas skCanvas, SKSize skSize, CompositeDisposable disposable)
        {
            skCanvas.Save();

            var skPaintOpacity = SkiaUtil.SetOpacity(skCanvas, svgLine, disposable);
            var skPaintFilter = SkiaUtil.SetFilter(skCanvas, svgLine, disposable);
            SkiaUtil.SetTransform(skCanvas, matrix);

            if (svgLine.Stroke != null)
            {
                var skPaint = SkiaUtil.GetStrokeSKPaint(svgLine, skSize, bounds, disposable);
                skCanvas.DrawLine(x0, y0, x1, y1, skPaint);
            }

            if (skPaintFilter != null)
            {
                skCanvas.Restore();
            }

            if (skPaintOpacity != null)
            {
                skCanvas.Restore();
            }

            skCanvas.Restore();
        }
    }
}
