﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Org.Reddragonit.BpmEngine.Drawing.Wrappers
{
    internal class Point : IDrawingObject
    {
        public static readonly Type DrawingType = Utility.GetType(DrawingImage.ASSEMBLY_NAME, "System.Drawing.PointF");
        private static readonly ConstructorInfo _drawingConstructor = (DrawingType==null ? null : DrawingType.GetConstructor(new Type[] { typeof(float), typeof(float) }));

        private float _x;
        public float X { get { return _x; } }
        private float _y;
        public float Y { get { return _y; } }

        public Point(float x,float y)
        {
            _x=x;
            _y=y;
        }

        public object DrawingObject
        {
            get
            {
                return (_drawingConstructor!=null ? _drawingConstructor.Invoke(new object[] { _x, _y }) : null);
            }
        }
    }
}
