﻿// Copyright (c) Wiesław Šoltés. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//
// Parts of this source file are adapted from the https://github.com/vvvv/SVG
using System.Collections.Generic;
using SkiaSharp;
using Svg;

namespace Svg.Skia
{
    internal struct Fragment : IElement
    {
        public SvgFragment svgFragment;
        public List<IElement> children;
        public float x;
        public float y;
        public float width;
        public float height;
        public SKRect bounds;
        public SKMatrix matrix;

        public Fragment(SvgFragment fragment)
        {
            svgFragment = fragment;
            children = new List<IElement>();

            foreach (var svgElement in svgFragment.Children)
            {
                var element = ElementFactory.Create(svgElement);
                if (element != null)
                {
                    children.Add(element);
                }
            }

            x = svgFragment.X.ToDeviceValue(null, UnitRenderingType.Horizontal, svgFragment);
            y = svgFragment.Y.ToDeviceValue(null, UnitRenderingType.Vertical, svgFragment);
            width = svgFragment.Width.ToDeviceValue(null, UnitRenderingType.Horizontal, svgFragment);
            height = svgFragment.Height.ToDeviceValue(null, UnitRenderingType.Vertical, svgFragment);
            bounds = SKRect.Create(x, y, width, height);

            matrix = SkiaUtil.GetSKMatrix(svgFragment.Transforms);
            var viewBoxMatrix = SkiaUtil.GetSvgViewBoxTransform(svgFragment.ViewBox, svgFragment.AspectRatio, x, y, width, height);
            SKMatrix.Concat(ref matrix, ref matrix, ref viewBoxMatrix);
        }

        public void Draw(SKCanvas skCanvas, SKSize skSize, CompositeDisposable disposable)
        {
            skCanvas.Save();

            var skPaintOpacity = SkiaUtil.SetOpacity(skCanvas, svgFragment, disposable);
            SkiaUtil.SetTransform(skCanvas, matrix);

            for (int i = 0; i < children.Count; i++)
            {
                children[i].Draw(skCanvas, skSize, disposable);
            }

            if (skPaintOpacity != null)
            {
                skCanvas.Restore();
            }

            skCanvas.Restore();
        }
    }
}
