using AutoConfirm.Clicks.BaseClicks;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfirm.Clicks
{
    internal unsafe class ClickNotificationFinder : BaseClick
    {
        public ClickNotificationFinder(AtkUnitBase* window) : base(window)
        {
        }
        public void ClickNotification(string str)
        {

            Click(0, 17, str);
            //Click(-2);
        }
    }
}
