
namespace Luftschiff.Code.Game.Monsters {
    class Dragon : Monster{
        public override int makeTurnDamage()
        {
            throw new System.NotImplementedException();
        }

        public override void getTurnDamage(int type, bool hits)
        {
            throw new System.NotImplementedException();
        }

        public override void update()
        {
            if (MouseHandler.UnhandledClick)
            {
                if (MouseHandler.selectedRoom != null)
                {
                    MouseHandler.selectedRoom.inflictDamage((Monster) this);
                }
            }
        }
    }
}
