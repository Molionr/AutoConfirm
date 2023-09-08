using AutoConfirm.Clicks.BaseClicks;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoConfirm.Clicks
{
    internal unsafe class ClickContentsFinderConfirm : BaseClick
    {
        public ClickContentsFinderConfirm(AtkUnitBase* window) : base(window)
        {
        }

        /// <summary>
        /// 进入副本
        /// </summary>
        public void Commence()
        {
            FireCallback(8);
            FireCallback(-2);
        }

        /// <summary>
        /// Click the commence button.
        /// </summary>
        //public void Withdraw()
        //    => this.ClickAddonButton(this.Addon->WithdrawButton, 9);

        /// <summary>
        /// Click the commence button.
        /// </summary>
        //public void Wait()
        //    => this.ClickAddonButton(this.Addon->WaitButton, 11);
    }
}

