using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgFinalProject_V1
{
    public class MonoGamePlayer : DrawableSprite
    {
        protected Vector2 lastDirection;

        public Vector2 mouseLocation;
        public Vector2 vector2Angle;

        internal PlayerController controller { get; private set; }
        internal GameConsolePlayer Player
        {
            get;
            private set;
        }

        protected PlayerMovingState moveState;
        public PlayerMovingState MoveState
        {
            get { return this.moveState; }
            set
            {
                if(this.moveState != value)
                {
                    this.moveState = this.Player.MoveState = value;
                }
            }
        }

        protected PlayerDashState dashState;
        public PlayerDashState DashState
        {
            get { return this.dashState; }
            set
            {
                if (this.dashState != value)
                {
                    this.dashState = this.Player.DashState = value;
                }
            }
        }

        protected PlayerDMGState dmgState;
        public PlayerDMGState DMGState
        {
            get { return this.dmgState; }
            set
            {
                if (this.dmgState != value)
                {
                    this.dmgState = this.Player.DMGState = value;
                }
            }
        }

        public MonoGamePlayer(Game game) : base(game)
        {
            this.controller = new PlayerController(game);
            Player = new GameConsolePlayer((GameConsole)game.Services.GetService<IGameConsole>());
            
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.spriteTexture = this.Game.Content.Load<Texture2D>("pacmanSingle");
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
            this.Location = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            this.Speed = 400;
            this.MoveState = PlayerMovingState.Still;
            this.DashState = PlayerDashState.NotUsed;
            this.DMGState = PlayerDMGState.Vulnerable;

        }

        public override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            KeepOnScreen();          
            this.controller.Update();
            UpdateMovement(time);
            base.Update(gameTime);
        }

        public void KeepOnScreen()
        {
            if (this.Location.X > Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2))
            {
                this.Location.X = Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2);
            }
            if (this.Location.X < (this.spriteTexture.Width / 2))
                this.Location.X = (this.spriteTexture.Width / 2);

            if (this.Location.Y > Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2))
                this.Location.Y = Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2);

            if (this.Location.Y < (this.spriteTexture.Height / 2))
                this.Location.Y = (this.spriteTexture.Height / 2);
            //this.Location.X = MathHelper.Clamp(this.Location.X, 0, this.Game.GraphicsDevice.Viewport.Width - this.SpriteTexture.Width);
            //this.Location.Y = MathHelper.Clamp(this.Location.Y, 0, this.Game.GraphicsDevice.Viewport.Height - this.SpriteTexture.Height);
        }

        private void UpdateMovement(float lastUpdateTimed)
        {
            this.Location += ((this.controller.Direction * (lastUpdateTimed / 1000)) * Speed);
            this.mouseLocation = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            this.vector2Angle = this.mouseLocation - this.Location;
            this.Rotate =  this.controller.RotateFunction(this.vector2Angle);
            if (this.controller.hasInputForMoverment)
            {
            this.lastDirection = this.controller.Direction;
                if (moveState != PlayerMovingState.Dashing)
                {
                    this.moveState = PlayerMovingState.Moving;
                }
            }
            else
            {
                if(moveState != PlayerMovingState.Dashing)
                {
                    this.moveState = PlayerMovingState.Still;
                }
            }
        }

    }
}
