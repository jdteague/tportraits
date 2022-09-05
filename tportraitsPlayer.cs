using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using System;
using System.Linq;
using Terraria.ModLoader.IO;
using Terraria.GameContent.Events;

namespace tportraits
{
    public class tportraitsPlayer : ModPlayer
    {
        public string currentNPC = "Truffle";
        public override void PreUpdate()
        {
            /*
            int a = Main.player[Main.myPlayer].talkNPC;
            
            if(Main.npc.Length < a)
            {
                int b = Main.npc[a].type;
                string c = Lang.GetNPCName(b).Key;
                string[] d = c.Split('.');
                string e = d[d.Length - 1];
                currentNPC = e;
            }*/
            

            if ((Main.npcChatText != "" || Main.player[Main.myPlayer].sign != -1) && !Main.editChest && Main.player[Main.myPlayer].talkNPC >= 0)
            {
                tportraits.UserInterfaceManager.OpenStatSheet();
            }

            else
            {
                tportraits.UserInterfaceManager.CloseStatSheet();
            }
        }

        public string getCurrentNPC()
        {
            return currentNPC;
        }

        //NOTE: no longer works with the above code, as it instantly closes the second you press the button. It is now redundant
        /*
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (tportraits.StatKey.JustPressed)
            {
                tportraits.UserInterfaceManager.ToggleStatSheet();
            }
        }*/
    }
}
