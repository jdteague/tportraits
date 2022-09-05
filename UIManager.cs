using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.UI;
using tportraits.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using tportraits;
using Terraria.Audio;
using ReLogic.Content;

namespace tportraits
{
    public class UIManager
    {
        public UserInterface PortraitUserInterface;
        //public UserInterface StatSheetTogglerUserInterface;
        public PortraitUI Portrait;
        private GameTime _lastUpdateUIGameTime;

        public Asset<Texture2D> Border;
        public Asset<Texture2D> BG;
        //public Asset<Texture2D> StatsButton_MouseOverTexture;
        //public Asset<Texture2D>[] Default_Portraits;

        public void LoadUI()
        {
            
            if (!Main.dedServ)
            {
                /*TODO
                 * A: Create an NPC list/Array of strings for NPC types, "Guide, Merchant, etc". Need to fetch NPC types
                 *  - needs to be done dynamically so that modded npc's can be refernced too
                 * 
                 * B: Use NPC List, to fetch images. 
                 *  - images need to be named exactly like how the NPC's are in the games memory
                 *  - images need to be accessed via parsing
                 *  - not going to worry about alts for now, will handle exceptions later
                 * 
                 * 
                 * */
                // initialize an array the size of all available town npc's
                //Default_Portraits = new Asset<Texture2D>[5];

                Border = (ModContent.Request<Texture2D>("tportraits/UI/Assets/FancyBorder", AssetRequestMode.ImmediateLoad));
                BG = (ModContent.Request<Texture2D>("tportraits/UI/Assets/BG", AssetRequestMode.ImmediateLoad));

                PortraitUserInterface = new UserInterface();

                Portrait = new PortraitUI();
                Portrait.Activate();

                // Load textures
                
                //StatsButton_MouseOverTexture = (ModContent.Request<Texture2D>("tportraits/UI/Assets/StatsButton_MouseOver", AssetRequestMode.ImmediateLoad));

                // Initialize UserInterfaces

                //StatButton = new StatButton();
                //StatButton.Activate();

                //StatSheetTogglerUserInterface.SetState(StatButton);
            }
        }

        public void UpdateUI(GameTime gameTime)
        {

            /* see if this works here 
            int a = Main.player[Main.myPlayer].talkNPC;
            int b = Main.npc[a].type;
            string c = Lang.GetNPCName(b).Key;
            string[] d = c.Split('.');
            string e = d[d.Length - 1];
            //Name = e;

            Asset<Texture2D> texture = null;
            ModContent.RequestIfExists<Texture2D>("tportraits/Portraits/" + e, out texture, AssetRequestMode.ImmediateLoad);

            if (texture == null)
            {
                CloseStatSheet();
            }*/

            _lastUpdateUIGameTime = gameTime;

            if (PortraitUserInterface?.CurrentState != null)
                PortraitUserInterface.Update(gameTime);
        }

        public bool IsStatSheetOpen() => PortraitUserInterface?.CurrentState == null;
        public void CloseStatSheet() => PortraitUserInterface?.SetState(null);
        public void OpenStatSheet() => PortraitUserInterface.SetState(Portrait);
        //public void SetCurrentNPC(string Name) => Portrait.

        public void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            // you need one of these for every independent UI element your mod has
            int index = layers.FindIndex((layer) => layer.Name == "Vanilla: Inventory");
            if (index != -1)
            {
                layers.Insert(index - 1, new LegacyGameInterfaceLayer("tPortraits: Portrait Window", delegate
                {
                    if (_lastUpdateUIGameTime != null && PortraitUserInterface?.CurrentState != null)
                        PortraitUserInterface.Draw(Main.spriteBatch, _lastUpdateUIGameTime);
                    return true;
                }, InterfaceScaleType.UI));
            }
        }
    }
}

