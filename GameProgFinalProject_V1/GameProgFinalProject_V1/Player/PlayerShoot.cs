using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace GameProgFinalProject_V1
{
    class PlayerShoot : MonoGamePlayer
    {

        public ShotManager SM;
        public PlayerShoot(Game game) : base(game)
        {
            SM = new RateLimitedShotManager(this.Game);
            ((RateLimitedShotManager)SM).LimitShotRate = .25f;
            ((RateLimitedShotManager)SM).MaxShots = 100;
            this.Game.Components.Add(SM);
        }

        public override void Update(GameTime gameTime)
        {

            if(this.controller.input.MouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && this.controller.input.PreviousMouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
            {
                Shot s = new Shot(this.Game);
                s.Location = this.Location;
                s.Direction = s.Location - this.controller.input.MouseState.Position.ToVector2();
                s.Direction = s.Direction * -1;
                s.Direction.Normalize();
                s.Speed = 600;
                SM.Shoot(s);
            }
            foreach (Shot s in SM.Shots)
            {
                if (s.Enabled)
                {
                    if (s.Intersects(this.MonoEnemy))
                    {
                        if (s.PerPixelCollision(this.MonoEnemy))
                        {
                            this.Player.Log("Player Hit Enemy");
                            s.Enabled = false;
                            this.MonoEnemy.TakeDMG();
                        }
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            SM.Draw(sb);
            base.Draw(sb);
        }
    }
}
