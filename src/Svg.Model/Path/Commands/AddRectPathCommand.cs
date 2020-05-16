﻿namespace Svg.Model
{
    public class AddRectPathCommand : PathCommand
    {
        public Rect Rect;

        public AddRectPathCommand(Rect rect)
        {
            Rect = rect;
        }
    }
}
