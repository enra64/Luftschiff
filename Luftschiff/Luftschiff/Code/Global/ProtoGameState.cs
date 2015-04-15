using System;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luftschiff.Code.Global {
    public abstract class ProtoGameState{
        public bool Running { get; private set; }

        public ProtoGameState() {

        }

        /// <summary>
        /// This gets called by the controller on each cycle, and should chain the 
        /// update and draw calls down to the smallest class. While not exactly the
        /// most intelligent way to position these calls, it is proven to work.
        /// </summary>
        public void mainUpdate() {
            Controller.Window.Clear(Color.Black);
            Controller.Window.DispatchEvents();
            update();
            draw();
            Controller.Window.Display();
        }

        public void pause() {
            Running = false;
        }

        /// <summary>
        /// This method gets called when the Gamestate is about to be stopped
        /// to notify it of the event. Could be useful for saving the game
        /// </summary>
        public abstract void kill();

        /// <summary>
        /// The draw method. it gets called automatically in every gamestate
        /// </summary>
        public abstract void draw();

        /// <summary>
        /// The draw method. it gets called automatically in every gamestate
        /// </summary>
        internal abstract void update();
    }
}
