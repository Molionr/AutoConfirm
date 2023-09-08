using Dalamud.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AutoConfirm
{
    [Serializable]
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; } = 1;

        public bool EnableContentsFinderConfirm = false;

        public bool AutoSwitchJob = false;

        public int ContentsFinderConfirmRemain = 30;
        internal void SaveConfig()
        {
            Service.Interface.SavePluginConfig(this);
        }
    }
}