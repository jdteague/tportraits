using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using tportraits.UI;
using static System.Formats.Asn1.AsnWriter;

namespace tportraits.UI
{
    public class UICharacterPortrait : UIElement
    {
        private Asset<Texture2D> _texture;

        public float ImageScale = 1f;

        public float Rotation;

        public bool ScaleToFit;

        public bool AllowResizingDimensions = true;

        public Color Color = Color.White;

        public Vector2 NormalizedOrigin = Vector2.Zero;

        public bool RemoveFloatingPointsFromDrawPosition;

        private Texture2D _nonReloadingTexture;

        public UICharacterPortrait(Asset<Texture2D> texture)
        {
            SetImage(texture);
        }

        public UICharacterPortrait(Texture2D nonReloadingTexture)
        {
            SetImage(nonReloadingTexture);
        }

        public void SetImage(Asset<Texture2D> texture)
        {
            _texture = texture;
            _nonReloadingTexture = null;
            if (AllowResizingDimensions)
            {
                Width.Set((float)_texture.Width(), 0f);
                Height.Set((float)_texture.Height(), 0f);
            }
        }

        public void SetImage(Texture2D nonReloadingTexture)
        {
            _texture = null;
            _nonReloadingTexture = nonReloadingTexture;
            if (AllowResizingDimensions)
            {
                Width.Set((float)_nonReloadingTexture.Width, 0f);
                Height.Set((float)_nonReloadingTexture.Height, 0f);
            }
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            

            //                                    [ Portrait Logic + Loading ]
            //----------------------------------------------------------------------------------------------------
            // this snippet only works when running on the Draw layer
            // probably ought to make a shared logic class since this needs to be used in other locations

            int a = Main.player[Main.myPlayer].talkNPC;
            int b = Main.npc[a].type;
            string c = Lang.GetNPCName(b).Key;
            string[] d = c.Split('.');
            string e = d[d.Length - 1];
            //Name = e;


            //using FileStream reader = new FileStream(path, FileMode.Open);
            //Texture2D tex = Texture2D.FromStream(Main.instance.GraphicsDevice, reader);


            Asset<Texture2D> texture = null;
            
            // fullmoon = 0;
            if (e == "BestiaryGirl" && (Main.moonPhase == 0 || Main.bloodMoon) && !Main.dayTime)
            {
                // special render
                ModContent.RequestIfExists<Texture2D>("tportraits/Portraits/Werefox", out texture, AssetRequestMode.ImmediateLoad);
                
                _texture = texture;
            }
            else
            {
                // normal render
                ModContent.RequestIfExists<Texture2D>("tportraits/Portraits/" + e, out texture, AssetRequestMode.ImmediateLoad);
                _texture = texture;
            }



            /*
             * useful for debugging
             * 
             * 
            if (texture != null)
            {
                _texture = texture;
            }

            else
            {
                // draws an empty portrait
                ModContent.RequestIfExists<Texture2D>("tportraitsUI/Assets/Blank_Canvas", out texture, AssetRequestMode.ImmediateLoad);
                _texture = texture;
            }*/
            //-----------------------------------------------------------------------------------------------------


            Texture2D texture2D = null;
            CalculatedStyle dimensions = GetDimensions();

            if (_texture != null)
            {
                texture2D = _texture.Value;
            }
            if (_nonReloadingTexture != null)
            {
                texture2D = _nonReloadingTexture;
            }
            if (ScaleToFit)
            {
                spriteBatch.Draw(texture2D, dimensions.ToRectangle(), Color);
            }
            else
            {
                Vector2 vector = texture2D.Size();
                Vector2 vector2 = dimensions.Position() + vector * (1f - ImageScale) / 2f + vector * NormalizedOrigin;
                if (RemoveFloatingPointsFromDrawPosition)
                {
                    vector2 = vector2.Floor();
                }

                vector2 = new Vector2(vector2.X - (vector.X /2), vector2.Y - (vector.Y / 2));

                //-----------------------------------------------> Debug
                //Utils.DrawBorderString(spriteBatch, Main.moonPhase.ToString(), new Vector2(300, 300), new Microsoft.Xna.Framework.Color(255, 255, 255));
                

                
                spriteBatch.End();// ends the current sprite batch, allows me to set up a new one with different render options
                // this rendering mode removes the default anti aliasing and allows me to use 1x1 images and scale up to two! Hooray for space efficiency!

                // NonPremultiplied preserves original image alpha
                // PointClamp removes antialiasing
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);

                spriteBatch.Draw(texture2D, vector2, null, Color, Rotation, vector * NormalizedOrigin, ImageScale, SpriteEffects.None, 0f);


                spriteBatch.End();

                //vanilla spritebatch, continueing as if I were never here
                //       L  I  K  E      A      G  H  O  S  T
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
            }
        }
    }
}
