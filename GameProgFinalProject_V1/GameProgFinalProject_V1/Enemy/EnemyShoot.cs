using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgFinalProject_V1
{
    class EnemyShoot : MonogameEnemy
    {
        public ShotManager SM;
        public EnemyShoot(Game game) : base(game)
        {
            SM = new RateLimitedShotManager(this.Game);
            ((RateLimitedShotManager)SM).LimitShotRate = 0.1f;
            ((RateLimitedShotManager)SM).LimitShotRate = 100;
            this.Game.Components.Add(SM);
        }

        public override void Update(GameTime gameTime)
        {

            //Shot s = new Shot(this.Game);
            //s.Location = this.Location;
            //s.Direction = this.lastDirection;
            
            //s.Speed = 600;
            //SM.Shoot(s);

            foreach (Shot s in SM.Shots)
            {
                if (s.Enabled)
                {
                    if (s.Intersects(this.MonoPlayer))
                    {
                        if (s.PerPixelCollision(this.MonoPlayer))
                        {
                            this.Enemy.Log("Player Hit Enemy");
                            s.Enabled = false;
                            this.MonoPlayer.TakeDMG();
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
