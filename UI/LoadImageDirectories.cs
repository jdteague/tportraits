using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace tportraits.UI
{
    public static class LoadImageDirectories
    {
        public static Texture2D Get(string portrait)
        {
            /*
            string path = "D:\\Files\\Documents\\My Games\\Terraria\\tModLoader\\Portraits\\";
            path = path + portrait + ".png";
            using FileStream reader = new FileStream(path, FileMode.Open);
            Texture2D tex = Texture2D.FromStream(Main.instance.GraphicsDevice, reader);
            return tex;
            */
            return null;
        }
    }
}
