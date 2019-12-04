using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgFinalProject_V1
{
    public class MonogameEnemy : DrawableSprite
    {
        public Vector2 lastDirection;
        public float EnemyHealth;
        internal GameConsoleEnemy Enemy
        {
            get;
            private set;
        }


        //HACK...or at least feels like the wrong way to approach shot manager collsion between two different game objects
        protected MonoGamePlayer _monoPlayer;
        public MonoGamePlayer MonoPlayer
        {
            get { return this._monoPlayer; }
            set
            {
                if (this._monoPlayer != value)
                {
                    this._monoPlayer = value;
                }
            }
        }

        protected EnemyState _state;
        public EnemyState EnemyState
        {
            get { return this._state; }
            set
            {
                if (this._state != value)
                {
                    this._state = this.Enemy.State = value;
                }
            }
        }

        protected EnemyMoveState _moveState;

        public EnemyMoveState MoveState
        {
            get { return this._moveState; }
            set
            {
                if(this._moveState != value)
                {
                    this._moveState = this.Enemy.MoveState = value;
                }
            }
        }

        protected EnemyDirectionState _enemyDirectionState;
        public EnemyDirectionState EnemyDirectionState
        {
            get { return this._enemyDirectionState; }
            set
            {
                if(this._enemyDirectionState != value)
                {
                    this._enemyDirectionState = this.Enemy.DirectionState = value;
                }
            }
        }

        protected EnemyAbilityState _enemyAbilityState;
        public EnemyAbilityState EnemyAbilityState
        {
            get { return this._enemyAbilityState; }
            set
            {
                if(this._enemyAbilityState != value)
                {
                    this._enemyAbilityState = this.Enemy.AbilityState = value;
                }
            }
        }

        protected EnemyDMGState _enemyDMGState;
        public EnemyDMGState EnemyDMGState
        {
            get { return this._enemyDMGState; }
            set
            {
                if(this._enemyDMGState != value)
                {
                    this._enemyDMGState = this.Enemy.EnemyDMGState = value;
                }
            }
        }

        public MonogameEnemy(Game game) : base(game)
        {
            Enemy = new GameConsoleEnemy((GameConsole)game.Services.GetService<IGameConsole>());
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            this.spriteTexture = this.Game.Content.Load<Texture2D>("TealGhost");
            this.Origin = new Vector2(this.spriteTexture.Width / 2, this.spriteTexture.Height / 2);
            this.Location = new Vector2(GraphicsDevice.Viewport.Width/2 - this.spriteTexture.Width/2, GraphicsDevice.Viewport.Height/2);
            this.Speed = 100;
            this.MoveState = EnemyMoveState.Still;
            this.EnemyDirectionState = EnemyDirectionState.Idle;
            this.EnemyAbilityState = EnemyAbilityState.Cooldown;
            this.EnemyDMGState = EnemyDMGState.Vulnerable;
            this.EnemyState = EnemyState.Alive;
            this.EnemyHealth = 300f;
            this.Direction = new Vector2(1, 0);

        }

        public override void Update(GameTime gameTime)
        {
            UpdateHealth();
            if(this.EnemyState == EnemyState.Alive)
            {
            UpdateMovement(lastUpdateTime);
            KeepOnScreen();
            
            }
            if(this.EnemyState == EnemyState.Dead)
            {
                this.Direction = new Vector2(0, 0);
                this.Visible = false;
            }
            base.Update(gameTime);
        }

        public void UpdateMovement(float lastUpdateTime)
        {
            this.Location += ((this.Direction * (lastUpdateTime / 1000)) * Speed);
            
        }

        public void UpdateHealth()
        {
            if(this.EnemyHealth <= 0)
            {
                this.EnemyState = EnemyState.Dead;
            }
        }

        public void TakeDMG()
        {
            if(this.EnemyDMGState == EnemyDMGState.Vulnerable)
            {
                this.EnemyHealth = EnemyHealth - 10;
                this.MonoPlayer.Player.Log(EnemyHealth.ToString());
            }
        }

        public void KeepOnScreen()
        {
            if (this.Location.X >= Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2))
            {
                this.Location.X = Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2);
                this.Direction = new Vector2(-1, 0);
            }
            if (this.Location.X < (this.spriteTexture.Width / 2))
            {
                this.Location.X = (this.spriteTexture.Width / 2);
                this.Direction = new Vector2(1, 0);

            }

            if (this.Location.Y >= Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2))
            {
                this.Location.Y = Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2);
            }

            if (this.Location.Y < (this.spriteTexture.Height / 2))
            {
                this.Location.Y = (this.spriteTexture.Height / 2);

            }
            //this.Location.X = MathHelper.Clamp(this.Location.X, 0, this.Game.GraphicsDevice.Viewport.Width - this.SpriteTexture.Width);
            //this.Location.Y = MathHelper.Clamp(this.Location.Y, 0, this.Game.GraphicsDevice.Viewport.Height - this.SpriteTexture.Height);
        }


    }
}
