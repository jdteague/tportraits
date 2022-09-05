//using Fargowiltas.Items.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
//using System.Drawing;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using static System.Net.Mime.MediaTypeNames;
using static Terraria.ModLoader.PlayerDrawLayer;
using Color = System.Drawing.Color;

/* TODO:
 * RENAME -- I ought to name it to something more basic, it's effectively a blank canvas
 * */

namespace tportraits.UI
{
    public class PortraitUI : UIState
    {

        public static DynamicSpriteFont defaultFont
        {
            get
            {
                return FontAssets.MouseText.Value;
            }
        }

        public static Texture2D backPanel
        {
            get
            {
                return (Texture2D)TextureAssets.SettingsPanel2;
            }
        }

        // need to be adjusted dynamically per portrait
        public const float Scale = 2f;
        public const int BackWidth = 105 * (int)Scale;
        public const int BackHeight = 105 * (int)Scale;
        

        public int LineCounter;
        public int ColumnCounter;

        // I don't want to remove this functionality quite yet, But I will want to simplify the code as it will be a static panel
        // queue it for dissection!! it appears that it extends UIPanel, 
        public UIPortraitPanel pPanel;
        public UIText DebugText;



        public override void OnInitialize()
        {
            pPanel = new UIPortraitPanel();
            //pPanel.Left.Set(69, 0f);
            //pPanel.Top.Set(420, 0f);
            pPanel.Width.Set(BackWidth, 0f);
            pPanel.Height.Set(BackHeight, 0f);
            pPanel.PaddingLeft = pPanel.PaddingRight = pPanel.PaddingTop = pPanel.PaddingBottom = 0;
            pPanel.Scale = Scale;
            //hard coded, just like in Terraria
            pPanel.BackgroundColor = new Microsoft.Xna.Framework.Color(73, 94, 171);
            pPanel.BorderColor = Colors.FancyUIFatButtonMouseOver;

            Append(pPanel);

            base.OnInitialize();
        }

        //Debug Text Draw
        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            // finding these lines about gave me a stroke, I've given up hope on making UI the proper way and have elected to go it my own way.
            //
            // (170 + (Main.screenWidth - 800) / 2) this is the math that sets the terraria UI text where it is supposed to go
            //
            // funnily enough, this means that the text is hard coded where it's at and CANNOT be changed

            /* ---------------- Debug
             * 
            Utils.DrawBorderString(spriteBatch, Main.graphics.GraphicsDevice.Viewport.Width.ToString(), Offset , new Microsoft.Xna.Framework.Color(0,0,0));
            */

        }

        public override void OnActivate()
        {
            
            base.OnActivate();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Recalculate();
        }

        /*
        public void SetPositionToPoint(Point point)
        {
            BackPanel.Left.Set(point.X, 0f);
            BackPanel.Top.Set(point.Y, 0f);
        }
        public Point GetPositinAsPoint() => new Point((int)BackPanel.Left.Pixels, (int)BackPanel.Top.Pixels);
        */
    }
}