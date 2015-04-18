using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Luftschiff.Code.Dialogs;

namespace Luftschiff.Code.States
{
    class Menu : Global.ProtoGameState
    {
        public override void kill()
        {
            throw new NotImplementedException();
        }

        public override void draw()
        {
        }

        public override void update()
        {
            
            TwoButtonDialog menu = new TwoButtonDialog("Game", "GraphicsTest", "Entscheide dich!", "Settings");
            menu.show();
            if (menu.show())
            {
                Controller.LoadState(Globals.EStates.game);
            }
            if (!menu.show())
            {
                Controller.LoadState(Globals.EStates.graphicstest);
            }
        }
    }
}
