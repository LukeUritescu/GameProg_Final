using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgFinalProject_V1
{
    public enum EnemyMoveState { Still, Moving}
    public enum EnemyDirectionState { Up, Left, Down, Right, Idle}
    public enum EnemyAbilityState { TheWave, StraightShot, Cooldown}
    public enum EnemyDMGState { Invulnerable, Vulnerable }
    public enum EnemyState { Alive, Dead}
    class Enemy
    {

        protected EnemyState _state;
        public EnemyState State
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

        protected EnemyMoveState _moveState;
        public EnemyMoveState MoveState
        {
            get
            {
                return this._moveState;
            }
            set
            {
            if(_moveState != value)
                {
                    this.Log(string.Format("{0} was: {1} now {2}", this.ToString(), _moveState, value));
                }
            }
        }

        protected EnemyDirectionState _directionState;
        public EnemyDirectionState DirectionState
        {
            get { return this._directionState;}
            set
            {
                if(_directionState != value)
                {
                    this.Log(string.Format("{0} was: {1} now {2}", this.ToString(), _directionState, value));
                }
            }
        }

        protected EnemyAbilityState _abilityState;
        public EnemyAbilityState AbilityState
        {
            get { return this._abilityState; }
            set
            {
                if(_abilityState != value)
                {
                    this.Log(string.Format("{0} was: {1} now {2}", this.ToString(), _abilityState, value));

                }
            }
        }

        protected EnemyDMGState _enemyDmgState;
        public EnemyDMGState EnemyDMGState
        {
            get { return this._enemyDmgState; }
            set
            {
                if(this._enemyDmgState != value)
                {
                    this.Log(string.Format("{0} was: {1} now {2}", this.ToString(), _enemyDmgState, value));
                }
            }
        }

        public Enemy()
        {
            this.MoveState = EnemyMoveState.Still;
            this.DirectionState = EnemyDirectionState.Idle;
            this.AbilityState = EnemyAbilityState.Cooldown;
            this.EnemyDMGState = EnemyDMGState.Vulnerable;
        }
        public virtual void Log(string s)
        {
            Console.WriteLine(s);
        }
    }
}
