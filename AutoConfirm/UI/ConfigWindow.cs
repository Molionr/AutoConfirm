using Dalamud.Interface.Windowing;
using ImGuiNET;

namespace AutoConfirm.Windows
{
    internal class ConfigWindow : Window
    {
        public ConfigWindow() : base("Auto Confirm")
        {
        }

        public void Open() => IsOpen = true;

        public override void Draw()
        {
            ImGui.BeginChild("ContentsFinderConfirm");

            if (ImGui.Checkbox("Auto Commence for Duty Ready", ref Service.Config.EnableContentsFinderConfirm))
            {
                Service.Config.SaveConfig();
            }
            if (Service.Config.EnableContentsFinderConfirm)
            {
                if (ImGui.DragInt("Remain Time", ref Service.Config.ContentsFinderConfirmRemain,1,1,45))
                {
                    Service.Config.SaveConfig();
                }
            }
            ImGui.BeginChild("ContentsFinderConfirm");

        }
    }
}