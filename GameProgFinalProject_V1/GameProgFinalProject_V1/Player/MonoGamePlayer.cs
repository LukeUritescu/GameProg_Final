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
        public float PlayerHealth;
        protected Vector2 lastDirection;

        public Vector2 mouseLocation;
        public Vector2 vector2Angle;

        //HACK...or at least feels like the wrong way to approach shot manager collsion between two different game objects
        protected MonogameEnemy _monoEnemy;
        public MonogameEnemy MonoEnemy
        {
            get { return this._monoEnemy; }
            set
            {
                if(this._monoEnemy != value)
                {
                    this._monoEnemy = value;
                }
            }
        }

        internal PlayerController controller { get; private set; }
        internal GameConsolePlayer Player
        {
            get;
            private set;
        }

        protected PlayerState _state;
        public PlayerState State
        {
            get { return this._state; }
            set
            {
                if (this._state != value)
                {
                    this._state = this.Player.State = value;
                }
            }
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
            this.State = PlayerState.Alive;
            this.PlayerHealth = 100;
        }

        public override void Update(GameTime gameTime)
        {
            UpdateHealth();
            if(this.State == PlayerState.Alive)
            {
                float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                KeepOnScreen();          
                this.controller.Update();
                UpdateMovement(time);
                UpdateHealth();
            }
            base.Update(gameTime);
        }

        public void UpdateHealth()
        {
            if(this.PlayerHealth <= 0)
            {
                this.State = PlayerState.Dead;
            }
        }

        public void TakeDMG()
        {
            if(this.DMGState == PlayerDMGState.Vulnerable)
            {
                this.PlayerHealth = PlayerHealth - 2;
                this.Player.Log("Player took damage");
            }
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
            this.MoveState = PlayerMovingState.Moving;
                                
            }
            else
            {
                if(moveState != PlayerMovingState.Dashing)
                {
                    this.MoveState = PlayerMovingState.Still;
                }
            }
        }

    }
}
