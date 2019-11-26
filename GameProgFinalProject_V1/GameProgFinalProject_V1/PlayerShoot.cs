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
            ((RateLimitedShotManager)SM).LimitShotRate = .0001f;
            ((RateLimitedShotManager)SM).MaxShots = 100;
            this.Game.Components.Add(SM);
        }

        public override void Update(GameTime gameTime)
        {
            if (this.controller.input.KeyboardState.HasReleasedKey(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                Shot s = new Shot(this.Game);
                s.Location = this.Location;
                s.Direction = this.lastDirection;
                s.Speed = 600;
                SM.Shoot(s);
            }
            if (this.controller.input.KeyboardState.HasReleasedKey(Microsoft.Xna.Framework.Input.Keys.B))
            {
                Shot s = new Shot(this.Game);
                s.Location = this.Location;
                s.Direction = this.lastDirection;
                s.Speed = 600;
                SM.Shoot(s);
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
