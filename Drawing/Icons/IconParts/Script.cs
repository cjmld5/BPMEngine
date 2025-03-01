﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace Org.Reddragonit.BpmEngine.Drawing.Icons.IconParts
{
    internal class Script : ABase64Icon
    {
        protected override string _imageData
        {
            get
            {
                return "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";
            }
        }
    }
}
