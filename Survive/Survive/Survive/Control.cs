using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Survive {
    class Control {

        PlayerIndex pi;
        bool allowKeyboard = false;

        /// <summary>
        /// Create the control scheme utilizing the keyboard.  This assumes single-player mode.
        /// </summary>
        public Control() {
            pi = PlayerIndex.One;
            allowKeyboard = true;
        }

        /// <summary>
        /// Create the control scheme using the gamepad.  This will disable the keyboard.
        /// </summary>
        /// <param name="player">Which Gamepad is this Control watching for input?</param>
        public Control(PlayerIndex player) {
            pi = player;
        }

        public bool IsJump() {
            return
                (allowKeyboard && (Keyboard.GetState().IsKeyDown(Keys.Space) || Keyboard.GetState().IsKeyDown(Keys.W))) // Using Keyboard and Space or W is pressed
                || GamePad.GetState(pi).Buttons.A == ButtonState.Pressed // Player pushing A
                || GamePad.GetState(pi).ThumbSticks.Left.Y > 0.1f; // Player pushing up left stick
        }

        public bool IsFire() {
            return
                (allowKeyboard && (Mouse.GetState().LeftButton == ButtonState.Pressed)) // Using keyboard, pressing left mouse
                || GamePad.GetState(pi).Triggers.Right > 0.2; // Using gamepad, pressing right trigger
        }
    }
}
