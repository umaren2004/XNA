using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tutorial2
{
    class Sprite
    {
        protected Texture2D texture;
        protected Vector2 centre;
        protected Vector2 screenPos;
        protected Rectangle sourceRect;
        protected Vector2 velocity;

        public Sprite(Texture2D tex, Vector2 centre, Vector2 pos, Rectangle sourceRect, Vector2 vel)
        {
            texture = tex;
            this.centre = centre;
            this.screenPos = pos;
            this.sourceRect = sourceRect;
            this.velocity = vel;
        }

        public virtual void Update(GameTime gameTime, Rectangle viewportRect)
        {
            screenPos += velocity;

            //Check for collision with the side of the screen
            if (screenPos.X + sourceRect.Width / 2 > viewportRect.Right)
            {
                velocity.X *= -1;
                screenPos.X = viewportRect.Right - sourceRect.Width / 2;

            }

            if (screenPos.X - sourceRect.Width / 2 < viewportRect.Left)
            {
                velocity.X *= -1;
                screenPos.X = viewportRect.Left + sourceRect.Width / 2;
            }

            if (screenPos.Y - sourceRect.Height / 2 < viewportRect.Top)
            {
                velocity.Y *= -1;
                screenPos.Y = viewportRect.Top + sourceRect.Height / 2;   
            }

            if (screenPos.Y + sourceRect.Height / 2 > viewportRect.Bottom)
            {
                velocity.Y *= -1;
                screenPos.Y = viewportRect.Bottom - sourceRect.Width / 2;
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch sb, Color col)
        {
            sb.Draw(texture, screenPos, sourceRect, col, 0.0f, centre, 1.0f, SpriteEffects.None, 0);
        }
    }
}
