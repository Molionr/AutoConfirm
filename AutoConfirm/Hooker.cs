using Dalamud.Hooking;
using Dalamud.Logging;
using Dalamud.Utility.Signatures;
using ECommons.Automation;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace AutoConfirm
{
    public unsafe class Hooker
    {
        private delegate void ViewFireCallback(IntPtr node, int valueCount, AtkValue* values, void* a4);

        [Signature("E8 ?? ?? ?? ?? 8B 44 24 20 C1 E8 05", DetourName = nameof(MyFireCallback))]
        private Hook<ViewFireCallback> ViewFireCallbackHook { get; init; }

        private unsafe void MyFireCallback(IntPtr node, int valueCount, AtkValue* values, void* a4)
        {
            try
            {
                string valuesStr = Callback.DecodeValues(valueCount, values).Replace("\n", ", ");

                //PluginLog.Information("count:" + valueCount.ToString());
#if DEBUG
                PluginLog.Information(valuesStr);
#endif
            }
            finally
            {
                ViewFireCallbackHook.Original(node, valueCount, values, a4);
            }
        }


        internal unsafe Hooker()
        {
            SignatureHelper.Initialise(this);

            ViewFireCallbackHook.Enable();

        }

        public unsafe void Dispose()
        {
            ViewFireCallbackHook.Dispose();
        }
    }
}