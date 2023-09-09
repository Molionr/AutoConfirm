using FFXIVClientStructs.FFXIV.Component.GUI;
using ValueType = FFXIVClientStructs.FFXIV.Component.GUI.ValueType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ECommons.Automation;

namespace AutoConfirm.Clicks.BaseClicks
{
    internal abstract unsafe class BaseClick
    {
        protected AtkUnitBase* UnitBase { get; }

        internal BaseClick(AtkUnitBase* window) 
        {
            UnitBase = window;
        }

        protected void Click(params object[] values)
        {
            Callback.Fire(UnitBase, true, values);
        }
    }
}
