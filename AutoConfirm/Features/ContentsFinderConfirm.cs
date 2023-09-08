using AutoConfirm.Clicks;
using AutoConfirm.Features.BaseFeatures;
using Dalamud.Logging;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfirm.Features
{
    internal unsafe class ContentsFinderConfirm : BaseFeature
    {
        public override void OnSetup()
        {
            if (Service.ClientState.LocalPlayer == null) return;

            if (!Service.Config.EnableContentsFinderConfirm) return;

            var bannerWindow = (AtkUnitBase*)Service.GameGui.GetAddonByName("ContentsFinderConfirm", 1);
            if (bannerWindow == null) return;
            if (!bannerWindow->IsVisible) return;
            //if (bannerWindow->GetHashCode() == _lastBanner) return;
            //_lastBanner = bannerWindow->GetHashCode();
            try
            {
                var text = bannerWindow->GetTextNodeById(60)->NodeText.ToString().Split(':');

                if (text.Length < 2) return;

                PluginLog.Information(text[1]);
                
                if (int.Parse(text[1]) < Service.Config.ContentsFinderConfirmRemain) {
                    new ClickContentsFinderConfirm(bannerWindow).Commence();
                }

            }
            catch (Exception e)
            {

                PluginLog.Error(e, "Error");
            }
        }
    }
}
