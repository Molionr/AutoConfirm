using Dalamud.Game.Command;
using Dalamud.Plugin;
using AutoConfirm.Windows;

namespace AutoConfirm
{
    public class AutoConfirmPlugin : IDalamudPlugin
    {
        public string Name => "Auto Confirm";

        internal Hooker Hooker { get; }
        internal Controller Controller { get; }
        internal WindowManager WindowManager { get; }

        public AutoConfirmPlugin(DalamudPluginInterface pluginInterface)
        {
            pluginInterface.Create<Service>();
            Service.Config = Service.Interface.GetPluginConfig() as Configuration ?? new Configuration();

            WindowManager = new WindowManager();

            Hooker = new Hooker();
            Controller = new Controller();

            Service.CommandManager.AddHandler("/AutoConfirm", new CommandInfo(OnCommand)
            {
                HelpMessage = "Open a config window about Auto Confirm.",
            });
        }

        public void Dispose()
        {
            Service.CommandManager.RemoveHandler("/AutoConfirm");

            Hooker.Dispose();
            Controller.Dispose();
            WindowManager.Dispose();
        }

        private void OnCommand(string command, string arguments)
        {
            WindowManager.ConfigWindow.Open();
        }
    }
}