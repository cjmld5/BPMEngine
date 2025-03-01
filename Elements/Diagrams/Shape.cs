﻿using Org.Reddragonit.BpmEngine.Attributes;
using Org.Reddragonit.BpmEngine.Drawing.Wrappers;
using Org.Reddragonit.BpmEngine.Elements.Processes.Events;
using Org.Reddragonit.BpmEngine.Elements.Processes.Gateways;
using Org.Reddragonit.BpmEngine.Elements.Processes.Tasks;
using Org.Reddragonit.BpmEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Org.Reddragonit.BpmEngine.Elements.Diagrams
{
    [XMLTag("bpmndi","BPMNShape")]
    [RequiredAttribute("id")]
    [ValidParent(typeof(Plane))]
    internal class Shape : ADiagramElement
    {
        public Rectangle Rectangle
        {
            get
            {
                foreach (IElement elem in Children)
                {
                    if (elem is Bounds)
                        return ((Bounds)elem).Rectangle;
                }
                return new Rectangle(0,0,0,0);
            }
        }

        public Label Label
        {
            get
            {
                foreach (IElement elem in Children)
                {
                    if (elem is Label)
                        return (Label)elem;
                }
                return null;
            }
        }

        public Shape(XmlElement elem, XmlPrefixMap map, AElement parent)
            : base(elem, map, parent) { }

        public BPMIcons? GetIcon(Definition definition)
        {
            BPMIcons? ret = null;
            IElement elem = _GetLinkedElement(definition);
            if (elem != null)
            {
                if (elem is AEvent)
                {
                    AEvent evnt = (AEvent)elem;
                    if (elem is StartEvent)
                    {
                        ret = BPMIcons.StartEvent;
                        if (evnt.SubType.HasValue)
                        {
                            switch (evnt.SubType.Value)
                            {
                                case EventSubTypes.Message:
                                    ret = BPMIcons.MessageStartEvent;
                                    break;
                                case EventSubTypes.Conditional:
                                    ret = BPMIcons.ConditionalStartEvent;
                                    break;
                                case EventSubTypes.Signal:
                                    ret = BPMIcons.SignalStartEvent;
                                    break;
                                case EventSubTypes.Timer:
                                    ret = BPMIcons.TimerStartEvent;
                                    break;
                            }
                        }
                    }
                    else if (elem is IntermediateThrowEvent)
                    {
                        if (evnt.SubType.HasValue)
                        {
                            switch (evnt.SubType.Value)
                            {
                                case EventSubTypes.Message:
                                    ret = BPMIcons.MessageIntermediateThrowEvent;
                                    break;
                                case EventSubTypes.Compensation:
                                    ret = BPMIcons.CompensationIntermediateThrowEvent;
                                    break;
                                case EventSubTypes.Escalation:
                                    ret = BPMIcons.EscalationIntermediateThrowEvent;
                                    break;
                                case EventSubTypes.Link:
                                    ret = BPMIcons.LinkIntermediateThrowEvent;
                                    break;
                                case EventSubTypes.Signal:
                                    ret = BPMIcons.SignalIntermediateThrowEvent;
                                    break;
                                case EventSubTypes.Timer:
                                    ret = BPMIcons.TimerStartEvent;
                                    break;
                            }
                        }
                    }
                    else if (elem is IntermediateCatchEvent)
                    {
                        if (evnt.SubType.HasValue)
                        {
                            switch (evnt.SubType.Value)
                            {
                                case EventSubTypes.Conditional:
                                    ret = BPMIcons.ConditionalIntermediateCatchEvent;
                                    break;
                                case EventSubTypes.Link:
                                    ret = BPMIcons.LinkIntermediateCatchEvent;
                                    break;
                                case EventSubTypes.Message:
                                    ret = BPMIcons.MessageIntermediateCatchEvent;
                                    break;
                                case EventSubTypes.Signal:
                                    ret = BPMIcons.SignalIntermediateCatchEvent;
                                    break;
                                case EventSubTypes.Timer:
                                    ret = BPMIcons.TimerIntermediateCatchEvent;
                                    break;
                            }
                        }
                    }
                    else if (elem is EndEvent)
                    {
                        ret = BPMIcons.EndEvent;
                        if (evnt.SubType.HasValue)
                        {
                            switch (evnt.SubType.Value)
                            {
                                case EventSubTypes.Compensation:
                                    ret = BPMIcons.CompensationEndEvent;
                                    break;
                                case EventSubTypes.Escalation:
                                    ret = BPMIcons.EscalationEndEvent;
                                    break;
                                case EventSubTypes.Message:
                                    ret = BPMIcons.MessageEndEvent;
                                    break;
                                case EventSubTypes.Signal:
                                    ret = BPMIcons.SignalEndEvent;
                                    break;
                            }
                        }
                    }
                }
                else if (elem is AGateway)
                    ret = (BPMIcons)Enum.Parse(typeof(BPMIcons), elem.GetType().Name);
                else if (elem is ATask)
                    ret = (BPMIcons)Enum.Parse(typeof(BPMIcons), elem.GetType().Name);
            }
            return ret;
        }

        public override bool IsValid(out string[] err)
        {
            bool found = false;
            foreach (IElement elem in Children)
            {
                found = found | (elem is Bounds);
            }
            if (!found)
            {
                err = new string[] { "No bounds specified." };
                return false;
            }
            return base.IsValid(out err);
        }
    }
}
