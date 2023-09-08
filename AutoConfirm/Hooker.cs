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
                    valuesStr += value.Type.ToString() + ":";
                    switch (value.Type)
                    {
                        case FFXIVClientStructs.FFXIV.Component.GUI.ValueType.Bool :
                            valuesStr += value.Byte.ToString() + ",";
                            break;
                        case FFXIVClientStructs.FFXIV.Component.GUI.ValueType.Int:
                            valuesStr += value.Int.ToString() + ",";
                            break;
                        case FFXIVClientStructs.FFXIV.Component.GUI.ValueType.Float:
                            valuesStr += value.Float.ToString() + ",";
                            break;
                        case FFXIVClientStructs.FFXIV.Component.GUI.ValueType.UInt:
                            valuesStr += value.UInt.ToString() + ",";
                            break;
                        case FFXIVClientStructs.FFXIV.Component.GUI.ValueType.String:
                            {
                                byte* bytePtr = value.String; // 假设 bytePtr 是一个 byte* 指针

                                int length = 0;
                                while (bytePtr[length] != 0)
                                {
                                    length++;
                                }

                                byte[] byteArray = new byte[length];
                                for (int a = 0; a < length; a++)
                                {
                                    byteArray[a] = bytePtr[a];
                                }

                                string stringValue = Encoding.UTF8.GetString(byteArray);

                                valuesStr += stringValue + ",";
                                break;
                            }

                        default:
                            valuesStr += value.Type.ToString() + "??,";
                            break;
                    }
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