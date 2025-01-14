﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Org.Reddragonit.BpmEngine.Drawing.Wrappers
{
    internal class Color : IDrawingObject
    {
        public static readonly Type DrawingType = Utility.GetType(DrawingImage.ASSEMBLY_NAME, "System.Drawing.Color");
        public static readonly Type SkiaType = Utility.GetType(SkiaImage.ASSEMBLY_NAME, "SkiaSharp.SKColor");

        private static readonly MethodInfo _drawingMethod = (DrawingType == null ? null : DrawingType.GetMethod("FromArgb", new Type[] {typeof(int), typeof(int) , typeof(int) , typeof(int) }));
        private static readonly ConstructorInfo _skiaConstructor = (SkiaType==null ? null : SkiaType.GetConstructor(new Type[] { typeof(byte), typeof(byte), typeof(byte), typeof(byte) }));

        public static readonly Color White = new Color(255, 255,255,255);
        public static readonly Color Red = new Color(255, 255, 0, 0);
        public static readonly Color Green = new Color(255, 0, 255, 0);
        public static readonly Color Blue = new Color(255, 0, 0, 255);
        public static readonly Color Black = new Color(255, 0, 0, 0);

        private int _a;
        public int A { get { return _a; } }
        private int _r;
        public int R { get { return _r; } }
        private int _g;
        public int G { get { return _g; } }
        private int _b;
        public int B { get { return _b; } }

        private Color(int a,int r,int g,int b)
        {
            _a=a;
            _r=r;
            _g=g;   
            _b=b;
        }

        internal static Color FromArgb(int a, int r, int g, int b)
        {
            return new Color(a, r, g, b);
        }

        private Color(byte a, byte r, byte g, byte b)
        : this((int)a, (int)r, (int)g, (int)b) { }

        internal static Color FromDrawingObject(object obj)
        {
            if (obj.GetType().FullName==DrawingType.FullName)
            {
                return new Color(
                    (byte)DrawingType.GetProperty("A").GetValue(obj),
                    (byte)DrawingType.GetProperty("R").GetValue(obj),
                    (byte)DrawingType.GetProperty("G").GetValue(obj),
                    (byte)DrawingType.GetProperty("B").GetValue(obj)
                );
            }
            if (obj.GetType().FullName==SkiaType.FullName)
            {
                return new Color(
                    (byte)SkiaType.GetProperty("Alpha").GetValue(obj),
                    (byte)SkiaType.GetProperty("Red").GetValue(obj),
                    (byte)SkiaType.GetProperty("Green").GetValue(obj),
                    (byte)SkiaType.GetProperty("Blue").GetValue(obj)
                );
            }
            return null;
        }

        public object DrawingObject
        {
            get
            {
                return (_drawingMethod!=null ? _drawingMethod.Invoke(null,new object[] { _a, _r, _g, _b }) : null);
            }
        }

        public object SkiaObject
        {
            get
            {
                return (_skiaConstructor!=null ? _skiaConstructor.Invoke(new object[] { (byte)_r, (byte)_g, (byte)_b, (byte)_a }) : null);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Color)
            {
                Color c = (Color)obj;
                return c.A==_a && c.R==_r && c.G==_g && c.B==_b;
            }
            return false;
        }

        public int HowClose(Color c)
        {
            return Math.Abs(c.R-R)+Math.Abs(c.G-G)+Math.Abs(c.B-B)+Math.Abs(c.A-A);
        }
    }
}
