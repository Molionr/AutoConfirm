using AutoConfirm.Clicks;
using AutoConfirm.Features.BaseFeatures;
using Dalamud.Logging;
using ECommons.Automation;
using ECommons.DalamudServices;
using ECommons.GameFunctions;
using ECommons.GameHelpers;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using FFXIVClientStructs.FFXIV.Component.GUI;
using Lumina.Excel.GeneratedSheets;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfirm.Features
{
    internal unsafe class ContentsFinderConfirm : BaseFeature
    {
        private static readonly Stopwatch stopwatch = new();

        public AtkUnitBase* BannerWindow { get; private set; }

        public ContentsFinderConfirm()
        {
            BannerWindow = (AtkUnitBase*)Service.GameGui.GetAddonByName("ContentsFinderConfirm", 1);
        }

        public override void OnSetup()
        {
            if (!Service.Config.EnableContentsFinderConfirm) return;

            AddonContentsFinderConfirm();
        }

        private unsafe void AddonContentsFinderConfirm()
        {
            if (BannerWindow == null) return;
            if (!BannerWindow->IsVisible) return;

            var text = BannerWindow->GetTextNodeById(60)->NodeText.ToString().Split(':');
            if (text.Length < 2) return;
            if (int.Parse(text[1]) >= Service.Config.ContentsFinderConfirmRemain) return;

            var node = BannerWindow->GetImageNodeById(40);
            if (node == null) return;
            var iconId = node->PartsList->Parts[node->PartId].UldAsset->AtkTexture.Resource->IconID;
            if (iconId is < 62100 or >= 62200) return;

            var classJobId = (uint)(iconId - 62100);
            if (Service.Config.AutoSwitchJob)
            {
                SwitchClassJob(classJobId);
            }

            if (classJobId != Player.Object.ClassJob.Id) return;
            try
            {
                new ClickContentsFinderConfirm(BannerWindow).Commence();
            }
            catch (Exception e)
            {

                PluginLog.Error(e, "Error");
            }
        }

        /// <summary>
        /// Switch Player Job
        /// </summary>
        /// <param name="jobId"></param>
        private unsafe void SwitchClassJob(uint jobId)
        {
            if (jobId == Player.Object.ClassJob.Id)
            {
                stopwatch.Stop();
                return;
            }
            var classJob = Service.Data.Excel.GetSheet<ClassJob>()?.GetRow(jobId);
            if (classJob == null) return;

            if (stopwatch.IsRunning && stopwatch.ElapsedMilliseconds < 1000) return;

            for (var i = 0; i < 101; i++)
            {
                var gs = RaptureGearsetModule.Instance()->Gearset[i];
                if (gs->Flags.HasFlag(RaptureGearsetModule.GearsetFlag.Exists) && gs->ClassJob == jobId)
                {
                    Chat.Instance.SendMessage($"/gearset change {gs->ID + 1}");
                    stopwatch.Restart();
                    return;
                }
            }
        }
    }
}
