﻿using System;
using Xml;

namespace Svg
{
    [Element("style")]
    public class SvgStyle : SvgElement,
        ISvgCommonAttributes
    {
        [Attribute("type", SvgNamespace)]
        public string? Type
        {
            get => GetAttribute("type");
            set => SetAttribute("type", value);
        }

        [Attribute("media", SvgNamespace)]
        public string? Media
        {
            get => GetAttribute("media");
            set => SetAttribute("media", value);
        }

        [Attribute("title", SvgNamespace)]
        public string? Title
        {
            get => GetAttribute("title");
            set => SetAttribute("title", value);
        }

        public override void Print(Action<string> write, string indent)
        {
            base.Print(write, indent);

            if (Type != null)
            {
                write($"{indent}{nameof(Type)}: \"{Type}\"");
            }
            if (Media != null)
            {
                write($"{indent}{nameof(Media)}: \"{Media}\"");
            }
            if (Title != null)
            {
                write($"{indent}{nameof(Title)}: \"{Title}\"");
            }
        }
    }
}
