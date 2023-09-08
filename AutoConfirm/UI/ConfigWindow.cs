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

            ImGui.Checkbox("", ref Service.Config.EnableContentsFinderConfirm);
            ImGui.SameLine();

            if (ImGui.TreeNode("Auto Commence for Duty Ready"))
            {
                ImGui.Indent();
                ImGui.Checkbox("Auto Switch Job", ref Service.Config.AutoSwitchJob);

                ImGui.DragInt("Remain Time", ref Service.Config.ContentsFinderConfirmRemain, 1, 1, 45);
                ImGui.Unindent();

                ImGui.TreePop();
            }


            ImGui.BeginChild("ContentsFinderConfirm");

            Service.Config.SaveConfig();
        }
    }
}