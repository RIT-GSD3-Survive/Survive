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
        GamePadState currentGPS;
        GamePadState previousGPS;
        MouseState currentMS;
        MouseState previousMS;
        KeyboardState currentKS;
        KeyboardState previousKS;

        //Properties
        public GamePadState CurrentGPS
        {
            get { return currentGPS; }
            set { currentGPS = value; }
        }

        public GamePadState PreviousGPS
        {
            get { return previousGPS; }
            set { previousGPS = value; }
        }

        public MouseState CurrentMS
        {
            get { return currentMS; }
            set { currentMS = value; }
        }

        public MouseState PreviousMS
        {
            get { return previousMS; }
            set { previousMS = value; }
        }

        public KeyboardState CurrentKS
        {
            get { return currentKS; }
            set { currentKS = value; }
        }

        public KeyboardState PreviousKS
        {
            get { return previousKS; }
            set { previousKS = value; }
        }

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

        /// <summary>
        /// Refreshes Controller states
        /// </summary>
        public void Refresh()
        {
            previousGPS = currentGPS;
            previousKS = currentKS;
            previousMS = currentMS;
            currentGPS = GamePad.GetState(pi);
            currentKS = Keyboard.GetState();
            currentMS = Mouse.GetState();
        }

        public bool IsJump() {
            return
                (allowKeyboard && (currentKS.IsKeyDown(Keys.Space) || currentKS.IsKeyDown(Keys.W))) // Using Keyboard and Space or W is pressed
                || currentGPS.IsButtonDown(Buttons.A) // Player pushing A
                || currentGPS.IsButtonDown(Buttons.LeftThumbstickUp); // Player pushing up left stick
        }

        public bool IsFire() {
            return
                (allowKeyboard && (currentMS.LeftButton == ButtonState.Pressed)) // Using keyboard, pressing left mouse
                || currentGPS.Triggers.Right > 0.2; // Using gamepad, pressing right trigger
        }

        public bool MoveLeft()
        {
            return
                (allowKeyboard && (currentKS.IsKeyDown(Keys.A)))
                || currentGPS.ThumbSticks.Left.X < -0.1;
        }

        public bool MoveRight()
        {
            return
                (allowKeyboard && (currentKS.IsKeyDown(Keys.D)))
                || currentGPS.ThumbSticks.Left.X > 0.1;
        }

        public bool SwitchWeaponsPrevious()
        {
            return
                (allowKeyboard && (currentMS.ScrollWheelValue > previousMS.ScrollWheelValue))
                || (currentGPS.IsButtonDown(Buttons.LeftShoulder) && previousGPS.IsButtonUp(Buttons.LeftShoulder));
        }

        public bool SwitchWeaponsNext()
        {
            return
                (allowKeyboard && (currentMS.ScrollWheelValue < previousMS.ScrollWheelValue))
                || (currentGPS.IsButtonDown(Buttons.RightShoulder) && previousGPS.IsButtonUp(Buttons.RightShoulder));
        }

        public bool Interact()
        {
            return
                (allowKeyboard && (previousKS.IsKeyUp(Keys.E)) && (currentKS.IsKeyDown(Keys.E)))
                || (previousGPS.IsButtonUp(Buttons.B) && currentGPS.IsButtonDown(Buttons.B));
        }

        public bool Reload()
        {
            return
                (allowKeyboard && (currentKS.IsKeyDown(Keys.R)))
                || currentGPS.IsButtonDown(Buttons.X);
        }

        public bool Pause()
        {
            return
                (allowKeyboard && (currentKS.IsKeyDown(Keys.P)))
                || currentGPS.IsButtonDown(Buttons.Start);
        }

        public bool Heal()
        {
            return 
                (allowKeyboard && (currentKS.IsKeyDown(Keys.H) && previousKS.IsKeyUp(Keys.H)))
                || (currentGPS.IsButtonDown(Buttons.Y) && previousGPS.IsButtonUp(Buttons.Y));
        }
    }
}
