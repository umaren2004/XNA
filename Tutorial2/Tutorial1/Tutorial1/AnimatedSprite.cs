using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Tutorial2
{
    class AnimatedSprite : Sprite
    {
        private int rows;
        private int columns;
        private int frames;
        private int currentFrame;

        public AnimatedSprite(Texture2D tex, Vector2 centre, Vector2 pos, Rectangle sourceRect, Vector2 vel, int row, int columns, int frames)
            : base(tex, centre, pos, sourceRect, vel)
        {
            this.rows = rows;
            this.columns = columns;
            this.frames = frames;
            currentFrame = -1;
        }

        public override void Update(GameTime gameTime, Rectangle viewportRect)
        {
            base.Update(gameTime, viewportRect);
            currentFrame++;
            currentFrame %= frames;
            sourceRect.X = (currentFrame % columns) * sourceRect.Width;
            sourceRect.Y = (currentFrame / columns) * sourceRect.Height;
        }
    }
}
