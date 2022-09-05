using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.ModLoader.IO;
using static Terraria.ModLoader.ModContent;
using System;
using Terraria.GameContent.Events;

namespace tportraits
{
    public class tportraitsWorld : ModSystem
    {
        public override void UpdateUI(GameTime gameTime)
        {
            base.UpdateUI(gameTime);
            tportraits.UserInterfaceManager.UpdateUI(gameTime);
        }

        // not sure what these due, but they look important, I'll figure them out eventually
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            base.ModifyInterfaceLayers(layers);
            tportraits.UserInterfaceManager.ModifyInterfaceLayers(layers);
        }
    }
}
