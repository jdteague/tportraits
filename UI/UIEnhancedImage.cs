using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

/*  NOTE: this is identical UIImage in every way, EXCEPT Drawself
 *  
 * 
 * 
 * 
 * 
 * */


namespace tportraits.UI
{
    public class UIEnhancedImage : UIElement
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

        public UIEnhancedImage(Asset<Texture2D> texture)
        {
            SetImage(texture);
        }

        public UIEnhancedImage(Texture2D nonReloadingTexture)
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
            //-----------------------------------------------------------------------------------------------------

            CalculatedStyle dimensions = GetDimensions();
            Texture2D texture2D = null;
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
                // The only difference between this and vanilla's UI Image is different render options
                Vector2 vector = texture2D.Size();
                Vector2 vector2 = dimensions.Position() + vector * (1f - ImageScale) / 2f + vector * NormalizedOrigin;
                if (RemoveFloatingPointsFromDrawPosition)
                {
                    vector2 = vector2.Floor();
                }
                

                
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
