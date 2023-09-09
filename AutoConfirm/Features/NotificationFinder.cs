using AutoConfirm.Clicks;
using AutoConfirm.Features.BaseFeatures;
using ECommons.Logging;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfirm.Features
{
    internal unsafe class NotificationFinder : BaseFeature
    {
        private static readonly Stopwatch stopwatch = new();

        public AtkUnitBase* NotificationWindow { get; private set; }

        public AtkUnitBase* NotificationFinderWindow { get; private set; }

        public NotificationFinder()
        {
            NotificationWindow = (AtkUnitBase*)Service.GameGui.GetAddonByName("_Notification", 1);
            NotificationFinderWindow = (AtkUnitBase*)Service.GameGui.GetAddonByName("_NotificationFinder", 1);
        }

        public override void OnSetup()
        {
            // 自动进副本
            var str = "The Bowl of Embers";
            ClickNotificationWindow(str);
        }


        /// <summary>
        /// 点击Notification窗口
        /// </summary>
        /// <param name="str"></param>
        private unsafe void ClickNotificationWindow(string str)
        {
            if (!Service.Config.EnableContentsFinderConfirm) return;

            if (NotificationFinderWindow == null) return;
            if (!NotificationFinderWindow->IsVisible) return;


            var s = NotificationFinderWindow->GetNodeById(2)->GetAsAtkComponentNode()->Component->GetTextNodeById(5)->GetAsAtkTextNode()->NodeText.ToString();
            var v = NotificationFinderWindow->GetNodeById(2)->GetAsAtkComponentNode()->Component->GetTextNodeById(4)->GetAsAtkTextNode()->NodeText.ToString();

            if (stopwatch.IsRunning && stopwatch.ElapsedMilliseconds < 1000) return;

            if (new ContentsFinderConfirm().BannerWindow != null) return;

            if (int.Parse(s) >= Service.Config.ContentsFinderConfirmRemain) return;

            var l = v.Length - 3;
            if (str[..l] != v[..l]) return;

            new ClickNotificationFinder(NotificationWindow).ClickNotification(str);

            //PluginLog.Information(v);
            //PluginLog.Information(str);
            stopwatch.Restart();
        }


    }
}
