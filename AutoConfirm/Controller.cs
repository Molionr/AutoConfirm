using AutoConfirm.Features;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using System.Runtime.InteropServices;
using ValueType = FFXIVClientStructs.FFXIV.Component.GUI.ValueType;

namespace AutoConfirm
{
    internal unsafe class Controller
    {
        internal unsafe Controller()
        {
            Service.Framework.Update += FrameworkUpdate;
        }

        public void Dispose()
        {
            Service.Framework.Update -= FrameworkUpdate;
        }

        private unsafe void FrameworkUpdate(Dalamud.Game.Framework framework)
        {
            if (Service.ClientState.LocalPlayer == null) return;

            new ContentsFinderConfirm().OnSetup();

        }
    }
}
