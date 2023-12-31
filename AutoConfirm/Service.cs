using Dalamud.Data;
using Dalamud.Game;
using Dalamud.Game.ClientState;
using Dalamud.Game.ClientState.Conditions;
using Dalamud.Game.ClientState.Objects;
using Dalamud.Game.Command;
using Dalamud.Game.Gui;
using Dalamud.IoC;
using Dalamud.Plugin;

namespace AutoConfirm
{
    internal class Service
    {
        internal static Configuration Config { get; set; }

        [PluginService]
        internal static DalamudPluginInterface Interface { get; private set; }

        [PluginService]
        internal static ClientState ClientState { get; private set; }

        [PluginService]
        internal static CommandManager CommandManager { get; private set; }

        [PluginService] 
        public static DataManager Data { get; private set; }

        [PluginService]
        internal static ObjectTable ObjectTable { get; private set; }
        [PluginService]
        public static Framework Framework { get; private set; }

        [PluginService]
        public static Condition Condition { get; private set; }


        [PluginService]
        public static GameGui GameGui { get; private set; }
    }
}