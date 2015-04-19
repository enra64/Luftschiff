using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Dialogs
{
    class HealthBar{
        private RectangleShape _barShape;
        private int _maxSize;

        //Colors
        public Color NormalColor { get; set; }
        public Color AttentionColor { get; set; }

        /// <summary>
        /// use to change color on attention needed
        /// </summary>
        public bool ForceAttention { get; set; }

        private void CommonConstructor(Vector2f position, Vector2f size)
        {
            _barShape = new RectangleShape(size);
            _barShape.FillColor = Globals.DIALOG_BUTTON_COLOR_NORMAL;
            _barShape.Position = position;
            _barShape.OutlineColor = Color.Black;
            _barShape.OutlineThickness = 2f;

            _maxSize = (int) size.X;

            NormalColor = Globals.DIALOG_BUTTON_COLOR_NORMAL;
            AttentionColor = Globals.DIALOG_BUTTON_COLOR_ATTENTIONSEEKER;
        }

        public HealthBar(Vector2f position, Vector2f size) {
            CommonConstructor(position, size);
        }

        public void Draw(){
            Controller.Window.Draw(_barShape);
        }

        /// <summary>
        /// Changes the color to the attentioncolor for a short time
        /// </summary>
        public void AttentionBlink()
        {
            ForceAttention = true;
            //blink the attention color
            new System.Threading.Timer(obj => { ForceAttention = false; }, null, 500, System.Threading.Timeout.Infinite);
        }

        public void Update(float healthPercent)
        {
            //force attention to the button on special occasions
            if (ForceAttention)
                _barShape.FillColor = AttentionColor;

            Vector2f barSize = _barShape.Size;

            //change the size according to the remaining health
            barSize.X = (healthPercent/100) * _maxSize;

            //cap bar size at 0
            if (barSize.X < 0)
                barSize.X = 0;

            _barShape.Size = barSize;
        }

        public void Update(){
            throw new Exception("Call the Update with life percentage above!");
        }
    }
}
