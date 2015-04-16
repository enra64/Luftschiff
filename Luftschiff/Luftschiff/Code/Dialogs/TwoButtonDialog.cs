using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Luftschiff.Code.Dialogs {
    class TwoButtonDialog : Luftschiff.Code.States.Dialog{
        public TwoButtonDialog(string _tag) : base(_tag)
        {

        }

        public TwoButtonDialog(Vector2f _size, Vector2f _pos, string _tag) : base(_size, _pos, _tag)
        {
            
        }

        public bool start()
        {
            return Controller.injectDialog(this);
        }

        public override void kill()
        {
            //rekt
            throw new NotImplementedException();
        }

        public override void draw()
        {
            throw new NotImplementedException();
        }

        public override void update()
        {
            throw new NotImplementedException();
        }
    }
}
