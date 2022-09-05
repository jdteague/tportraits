using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

using Terraria.DataStructures;
using Terraria.UI;
using Terraria.Chat;
using ReLogic.Peripherals.RGB;
using System.Globalization;

namespace tportraits
{
    public class tportraits : Mod
    {
        public static UIManager UserInterfaceManager => Instance._userInterfaceManager;
        private UIManager _userInterfaceManager;

        // seems important looks like a c# thing, will need to read more about this practice
        internal static tportraits Instance;

        internal static ModKeybind StatKey;

        public override void Load()
        {
            Instance = this;

            //NOTE: Deprecated, need to remove
            StatKey = KeybindLoader.RegisterKeybind(this, "Open Stat Sheet", "M");

            _userInterfaceManager = new UIManager();
            _userInterfaceManager.LoadUI();
        }
    }

}
