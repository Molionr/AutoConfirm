using FFXIVClientStructs.FFXIV.Component.GUI;
using ValueType = FFXIVClientStructs.FFXIV.Component.GUI.ValueType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfirm.Clicks.BaseClicks
{
    internal abstract unsafe class BaseClick
    {
        protected AtkUnitBase* UnitBase { get; }

        internal BaseClick(AtkUnitBase* window) 
        {
            UnitBase = window;
        }

        protected unsafe void FireCallback(int index)
        {
            var atkValues = (AtkValue*)Marshal.AllocHGlobal(1 * sizeof(AtkValue));
            atkValues[0].Type = ValueType.Int;
            atkValues[0].Int = index;
            try
            {
                UnitBase->FireCallback(1, atkValues);
            }
            finally
            {
                Marshal.FreeHGlobal(new IntPtr(atkValues));
            }
        }
    }
}
