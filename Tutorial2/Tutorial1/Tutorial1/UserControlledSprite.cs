using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tutorial2
{
    class UserControlledSprite : Sprite
    {
        public UserControlledSprite(Texture2D tex, Vector2 centre, Vector2 pos, Rectangle sourceRect, Vector2 vel) : 
            base(tex,centre,pos,sourceRect,vel)
        {}

        public override void Update(GameTime gameTime, Rectangle viewportRect)
        {
            ProcessInput();
            base.Update(gameTime, viewportRect);
            velocity *= 0.95f;
        }

        protected void ProcessInput()
        {
#if !XBOX
            KeyboardState keyboardState = Keyboard.GetState();
            if(keyboardState.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1.0f;
            }
            
            if(keyboardState.IsKeyDown(Keys.Right))
            {
                velocity.X += 1.0f;
            }

            if(keyboardState.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 1.0f;
            }

            if(keyboardState.IsKeyDown(Keys.Down))
            {
                velocity.Y += 1.0f;
            }
//#endif

//#if XBOX
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            velocity.X += gamePadState.ThumbSticks.Left.X;
            velocity.Y -= gamePadState.ThumbSticks.Left.Y;
     
#endif

        }


    }
}
