﻿using SFML.Graphics;

namespace Luftschiff.Code.Global
{
    public abstract class ProtoGameState
    {
        public bool Running { get; private set; }

        /// <summary>
        ///     This gets called by the controller on each cycle, and should chain the
        ///     update and draw calls down to the smallest class. While not exactly the
        ///     most intelligent way to position these calls, it is proven to work.
        /// </summary>
        public void mainUpdate()
        {
            Controller.Window.Clear(Color.Black);
            Controller.Window.DispatchEvents();
            Cursor.Update();
            update();
            draw();
            Cursor.Draw();
            Controller.Window.Display();
        }

        public void pause()
        {
            Running = false;
        }

        /// <summary>
        ///     This method gets called when the Gamestate is about to be stopped
        ///     to notify it of the event. Could be useful for saving the game
        /// </summary>
        public abstract void kill();

        /// <summary>
        ///     The draw method. it gets called automatically in every gamestate
        /// </summary>
        public abstract void draw();

        /// <summary>
        ///     The draw method. it gets called automatically in every gamestate
        /// </summary>
        public abstract void update();
    }
}