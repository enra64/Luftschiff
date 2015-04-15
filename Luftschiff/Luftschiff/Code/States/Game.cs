using SFML.Graphics;
using SFML.Window;
using spaceShooter.Code.GameClasses;
using spaceShooter.Code.Global;
using spaceShooter.Code.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spaceShooter.Code.Gamestates {
    class Game : ProtoGameState {
        
        /// <summary>
        /// The gamestate constructor. Nothing must be done here, the superclass
        /// constructor is empty anyways
        /// </summary>
        public Game () {
        }

        /// <summary>
        /// Main draw call for our Game. 
        /// </summary>
        public override void draw() {

        }

        /// <summary>
        /// This method gets called when the Gamestate is about to be stopped
        /// to notify it of the event. Could be useful for saving the game
        /// </summary>
        public override void kill() {
            
        }

        /// <summary>
        /// This is the update function that gets called for our Game-Gamestate
        /// </summary>
        internal override void update(){
            
        }
    }
}
