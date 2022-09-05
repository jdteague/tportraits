using Terraria.GameContent.UI.Elements;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using Terraria.UI;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using System.Drawing;
using ReLogic.Content;
using Terraria.ModLoader;

using Terraria.ID;
using Terraria.Localization;
using System;

namespace tportraits.UI
{
    public class UIPortraitPanel : UIPanel
    {
        // I guess it helps with identification idk, draggable panel had it so this does too.
        public UIElement[] ExtraChildren;
        public float Scale = 1f;

        //constructor
        public UIPortraitPanel() { }
        public UIPortraitPanel(params UIElement[] countMeAsChildren)
        {
            ExtraChildren = countMeAsChildren;
        }

        public string Name = "";

        public UICharacterPortrait Portrait;
        public UIEnhancedImage Border;
        public UIEnhancedImage Background;
        public override void OnActivate()
        {


            //ModContent;
            //var parentSpace = Parent.GetDimensions().ToRectangle();ModContent.TryFind<Texture2D>(path, out Texture2D texture)
            //Main.player[Main.myPlayer].GetModPlayer<tportraitsPlayer>().

            Background = new UIEnhancedImage(tportraits.UserInterfaceManager.BG);
            Background.Left.Set((Width.Pixels / 2) - (Background.Width.Pixels / 2), 0f);
            Background.Top.Set((Width.Pixels / 2) - (Background.Height.Pixels / 2), 0f);
            Background.ImageScale = (1.8f / 2f) * Scale;
            Append(Background);

            Asset<Texture2D> texture = null; 
            ModContent.RequestIfExists<Texture2D>("tportraits/UI/Assets/Blank_Canvas", out texture, AssetRequestMode.ImmediateLoad);
            Portrait = new UICharacterPortrait(texture);
            Portrait.Left.Set((Width.Pixels / 2) - 1*Scale, 0f);
            Portrait.Top.Set((Height.Pixels / 2), 0f);
            Portrait.ImageScale = Scale;
            Append(Portrait);


            float offset_x = (12 / 2f) * Scale;
            float offset_y = (8 / 2f) * Scale;
            Border = new UIEnhancedImage(tportraits.UserInterfaceManager.Border);
            Border.Left.Set(((Width.Pixels / 2) + offset_x) - (Border.Width.Pixels / 2), 0f);
            Border.Top.Set(((Height.Pixels / 2) + offset_y) - (Border.Height.Pixels / 2), 0f);
            Border.ImageScale = (1.8f / 2f) * Scale;
            Append(Border);

            base.OnActivate();

        }

        string debugtxt = "";
        public override void Update(GameTime gameTime)
        {
            /* --------------------------------------------------- Crashes Game
            int a = Main.player[Main.myPlayer].talkNPC;
            int b = Main.npc[a].type;
            string c = Lang.GetNPCName(b).Key;
            string[] d = c.Split('.');
            string e = d[d.Length - 1];
            Name = e;*/

            base.Update(gameTime); // don't remove.


            // this was taken directly from the game, this is the mathematical anchor for the position of NPC chat window text
            // here I am using it as the anchor for the portrait's window
            int vanilla_pos_x = 170 + (Main.screenWidth - 800) / 2;
            int vanilla_pos_y = 120;

            // these values adjust the vanilla anchor position, will need to code logic in the future
            //int justification_x = 480; right
            int justification_x = 0;
            int justification_y = -20;


            Vector2 offset = new Vector2((float)(vanilla_pos_x + justification_x), (float)(vanilla_pos_y + justification_y));
            


            // just setting the position based off of the math above every update, may need to restrict if performance takes a hit.
            // it to only has to recalculate occur on game window resizing.
            // however, constant/redundant calculation can prevent it from accidentially appearing somewhere it shouldn't be, even if it is wasteful
            
            Left.Set(offset.X - (Width.Pixels + 20), 0f);
            Top.Set(offset.Y, 0f);
            Recalculate();

            // Here we check if the UIPortraitPanel is outside the Parent UIElement rectangle. 
            var parentSpace = Parent.GetDimensions().ToRectangle();

            debugtxt = Width.Pixels.ToString();
            if (!GetDimensions().ToRectangle().Intersects(parentSpace))
            {
                Left.Pixels = Utils.Clamp(Left.Pixels, 0, parentSpace.Right - Width.Pixels);
                Top.Pixels = Utils.Clamp(Top.Pixels, 0, parentSpace.Bottom - Height.Pixels);
                // Recalculate forces the UI system to do the positioning math again.
                Recalculate();
            }
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            //------------------------------------------------ Render Toggle
            int a = Main.player[Main.myPlayer].talkNPC;
            int b = Main.npc[a].type;
            string c = Lang.GetNPCName(b).Key;
            string[] d = c.Split('.');
            string e = d[d.Length - 1];
            Name = e;

            Asset<Texture2D> texture = null;
            ModContent.RequestIfExists<Texture2D>("tportraits/Portraits/" + e, out texture, AssetRequestMode.ImmediateLoad);

            if (texture == null)
            {
                // if there does not exist a portrait, do not draw
            }

            else
            {
                //debug
                //Utils.DrawBorderString(spriteBatch, Name, new Vector2(300, 400), new Microsoft.Xna.Framework.Color(255, 255, 255));

                // otherwise draw
                base.Draw(spriteBatch);

            }

            
        }
    }
}
