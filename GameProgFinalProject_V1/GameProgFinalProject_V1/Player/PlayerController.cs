﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProgFinalProject_V1
{
    sealed class PlayerController
    {
        public InputHandler input; //game service to handle input
        public Vector2 Direction { get; private set; }
        public float Rotate { get; private set; }

        //Checks to see if there is any movement
        public bool hasInputForMoverment
        {
            get
            {

                if (Direction.Length() == 0) return false;
                return true;
            }
        }

        public PlayerController(Game game)
        {
            //get input handler from game services
            input = (InputHandler)game.Services.GetService<IInputHandler>();
            if (input == null)
            {
                throw new Exception("PacMan controller depends on InputHandler service please add Input Handler as a service first");
            }
        }

        public float RotateFunction(Vector2 vector2Angle)
        {
            float RotationalAngle = (float)Math.Atan2(
                vector2Angle.X,
                vector2Angle.Y * -1);

            Rotate = (float)MathHelper.ToDegrees(RotationalAngle - (float)(Math.PI/2 ));
            return Rotate;
                
        }

        public void Update()
        {
            //Input for update from analog stick
            GamePadState gamePad1State = input.GamePads[0]; //HACK hard coded player index
            #region LeftStick
            Vector2 pacStickDir = Vector2.Zero;
            //if (gamePad1State.ThumbSticks.Left.Length() > 0.0f)
            //{
            //    pacStickDir = gamePad1State.ThumbSticks.Left;
            //    pacStickDir.Y *= -1;      //Invert Y Axis

            //    float RotationAngle = (float)Math.Atan2(
            //        gamePad1State.ThumbSticks.Left.X,
            //        gamePad1State.ThumbSticks.Left.Y);

            //    Rotate = (float)MathHelper.ToDegrees(RotationAngle - (float)(Math.PI / 2));
            //}
            #endregion

            //Update for input from DPad
            #region DPad
            Vector2 PacManDPadDir = Vector2.Zero;
            if (gamePad1State.DPad.Left == ButtonState.Pressed)
            {
                //Orginal Position is Right so flip X
                PacManDPadDir += new Vector2(-1, 0);
            }
            if (gamePad1State.DPad.Right == ButtonState.Pressed)
            {
                //Original Position is Right
                PacManDPadDir += new Vector2(1, 0);
            }
            if (gamePad1State.DPad.Up == ButtonState.Pressed)
            {
                //Up
                PacManDPadDir += new Vector2(0, -1);
            }
            if (gamePad1State.DPad.Down == ButtonState.Pressed)
            {
                //Down
                PacManDPadDir += new Vector2(0, 1);
            }
            //if (PacManDPadDir.Length() > 0)
            //{
            //    //Angle in radians from vector
            //    float RotationAngleKey = (float)Math.Atan2(
            //            PacManDPadDir.X,
            //            PacManDPadDir.Y * -1);
            //    //Find angle in degrees
            //    Rotate = (float)MathHelper.ToDegrees(
            //        RotationAngleKey - (float)(Math.PI / 2)); //rotated right already

            //    //Normalize NewDir to keep agled movement at same speed as horilontal/Vert
            //    PacManDPadDir = Vector2.Normalize(PacManDPadDir);
            //}
            #endregion

            //Update for input from Keyboard
#if !XBOX360
            #region KeyBoard
            KeyboardState keyboardState = Keyboard.GetState();
            Vector2 PacManKeyDir = new Vector2(0, 0);

            if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                //Flip Horizontal
                PacManKeyDir += new Vector2(-1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                //No new sprite Effects
                PacManKeyDir += new Vector2(1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                PacManKeyDir += new Vector2(0, -1);
            }
            if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                PacManKeyDir += new Vector2(0, 1);
            }

            //float RotationAngleKey = (float)Math.Atan2(
            //        input.MouseDelta.X,
            //        input.MouseDelta.Y * -1);

            //Rotate = (float)MathHelper.ToDegrees(
            //    RotationAngleKey - (float)(Math.PI / 2));
            if (PacManKeyDir.Length() > 0)
            {
                //Normalize NewDir to keep angled movement at same speed as horizontal/Vert
                PacManKeyDir = Vector2.Normalize(PacManKeyDir);
            }
            #endregion
#endif
            Direction = PacManKeyDir + PacManDPadDir + pacStickDir;
            if (Direction.Length() > 0)
            {
                Direction = Vector2.Normalize(Direction);
            }
        }
    }
}

