using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using ValueType = FFXIVClientStructs.FFXIV.Component.GUI.ValueType;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfirm.Features.BaseFeatures
{
    internal abstract class BaseFeature :IBaseFeature
    {

        public abstract unsafe void OnSetup();

       
    }
}
