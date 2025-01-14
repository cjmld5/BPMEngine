﻿using Org.Reddragonit.BpmEngine.Drawing.Icons.IconParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Org.Reddragonit.BpmEngine.Drawing.Icons.Tasks
{
    [IconTypeAttribute(Elements.Diagrams.BPMIcons.ReceiveTask)]
    internal class ReceiveTask : AIcon
    {
        private static readonly IIconPart[] _PARTS = new IIconPart[] {
            new Envelope(false,true)
        };

        protected override IIconPart[] _parts
        {
            get { return _PARTS; }
        }
    }
}
