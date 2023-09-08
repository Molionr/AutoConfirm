using Dalamud.Hooking;
using Dalamud.Logging;
using Dalamud.Utility.Signatures;
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
                string valuesStr = string.Empty;
                for (int i = 0; i < valueCount; i++)
                {
                    var value = values[i];
                    valuesStr += value.Type.ToString();
                    valuesStr += value.Type switch
                    {
                        FFXIVClientStructs.FFXIV.Component.GUI.ValueType.Bool => value.Byte.ToString() + ",",
                        FFXIVClientStructs.FFXIV.Component.GUI.ValueType.Int => value.Int.ToString() + ",",
                        FFXIVClientStructs.FFXIV.Component.GUI.ValueType.Float => value.Float.ToString() + ",",
                        FFXIVClientStructs.FFXIV.Component.GUI.ValueType.UInt => value.UInt.ToString() + ",",
                        _ => value.Type.ToString() + "??,",
                    };
                }

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