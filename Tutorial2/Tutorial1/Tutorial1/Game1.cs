using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tutorial2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backgroundTex;
        Texture2D starSprite;
        Rectangle viewportRect;

        SpriteFont kootenayFont;
        Vector2 messagePosition = new Vector2(10, 10);
        String messageString = "Hello World";

        // Sprites

        private Sprite mozzie;
        private AnimatedSprite donut;

        //Sounds
        SoundEffect soundDing;
        SoundEffect soundFly;
        SoundEffectInstance soundDingInstance;
        SoundEffectInstance soundFlyInstance;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // Tutorial 1 Load Content
            backgroundTex = Content.Load<Texture2D>("Textures\\B1_nebula01");
            starSprite = Content.Load<Texture2D>("Textures\\stars");
            viewportRect = new Rectangle(0, 0,
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);
            kootenayFont = Content.Load<SpriteFont>("Fonts\\Kootenay");
            
            //Load mozzie
            Texture2D mozzieTex = Content.Load<Texture2D>("Textures\\mosquito");
                mozzie = new UserControlledSprite(mozzieTex,
                    new Vector2(mozzieTex.Width/2, mozzieTex.Height/2),
                    new Vector2(300, 100),
                    new Rectangle(0,0, mozzieTex.Width, mozzieTex.Height),
                    new Vector2(8,8)
                    );
            //Load Donut
            Texture2D donutTex = Content.Load<Texture2D>("Textures\\donut");
            donut = new AnimatedSprite(donutTex,
                new Vector2(32,32),
                new Vector2(400, 200),
                new Rectangle(0, 0, 64, 64),
                new Vector2(2, 1),
                6,
                10,
                60
            );

            //Load Sounds
            soundDing = Content.Load<SoundEffect>("Sounds\\ding");
            soundFly = Content.Load<SoundEffect>("sounds\\fly");
            soundDingInstance = soundDing.CreateInstance();
            soundFlyInstance = soundFly.CreateInstance();        
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            //Begin my update logic
            mozzie.Update(gameTime, viewportRect);
            donut.Update(gameTime, viewportRect);

            GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
            if (gamePad.IsConnected)
            {
                GamePad.SetVibration(PlayerIndex.One,
                    gamePad.Triggers.Left,
                    gamePad.Triggers.Right);
                if (gamePad.Triggers.Right > 0)
                {
                    if (soundFlyInstance.State == SoundState.Stopped)
                    {
                        soundFlyInstance.Volume = 0.75f;
                        soundFlyInstance.IsLooped = true;
                        soundFlyInstance.Play();
                    }
                    else
                    {
                        soundFlyInstance.Resume();
                    }
                }
                else if (gamePad.Triggers.Right == 0)
                {
                    if (soundFlyInstance.State == SoundState.Playing)
                    {
                        soundFlyInstance.Pause();
                    }
                }
            }

            //End my update logic

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTex, viewportRect, Color.White);
            spriteBatch.Draw(starSprite, viewportRect, Color.White);
            spriteBatch.DrawString(kootenayFont, messageString, messagePosition, Color.Bisque);
            mozzie.Draw(gameTime, spriteBatch, Color.White);
            donut.Draw(gameTime, spriteBatch, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
