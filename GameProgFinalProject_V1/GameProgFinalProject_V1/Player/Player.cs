using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgFinalProject_V1
{
    public enum PlayerState { Alive, Dead}
    public enum PlayerMovingState { Moving, Still, Dashing }
    public enum PlayerDashState { Startup, Active, Recovery, NotUsed }
    public enum PlayerDMGState { Invulnerable, Vulnerable }
    
    class Player
    {
        protected PlayerState _state;
        public PlayerState State
        {
            get { return this._state; }
            set
            {
                if(this._state != value)
                {
                    this.Log(string.Format("{0} was: {1} now {2}", this.ToString(), _state, value));

                }
            }
        }

        protected PlayerMovingState _moveState;
        public PlayerMovingState MoveState
        {
            get { return _moveState; }
            set
            {
                if(_moveState != value)
                {
                    this.Log(string.Format("{0} was: {1} now {2}", this.ToString(), _moveState, value));
                }
            }
        }

        protected PlayerDashState _dashState;
        public PlayerDashState DashState
        {
            get { return _dashState; }
            set
            {
                if(_dashState != value)
                {
                    this.Log(string.Format("{0} was: {1}  now {2}", this.ToString(), _dashState, value));
                }
            }
        }

        protected PlayerDMGState _dmgState;
        public PlayerDMGState DMGState
        {
            get { return _dmgState; }
            set
            {
                if (_dmgState != value)
                {
                    this.Log(string.Format("{0} was: {1} now {2}", this.ToString(), _dmgState, value));
                }
            }
        }

        public Player()
        {
            this.MoveState = PlayerMovingState.Still;
            this.DashState = PlayerDashState.NotUsed;
            this.DMGState = PlayerDMGState.Vulnerable;
        }

       public virtual void Log(string s)
        {
            Console.WriteLine(s);
        }
    }
}
