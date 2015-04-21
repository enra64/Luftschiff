using System;
using System.Threading;
using System.Threading.Tasks;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;

namespace Luftschiff.Code.Dialogs
{
    class HealthBar{
        private RectangleShape _barShape;
        private readonly float _maxSize;

        //Colors
        public Color NormalColor { get; set; }
        public Color AttentionColor { get; set; }

        /// <summary>
        /// use to change color on attention needed
        /// </summary>
        public bool ForceAttention { get; set; }

        public HealthBar(Vector2f position, Vector2f size, Color standardColor) {
            NormalColor = standardColor;
            AttentionColor = Globals.HEALTH_BAR_COLOR_ATTENTION;

            _barShape = new RectangleShape(size);
            _barShape.FillColor = NormalColor;
            _barShape.Position = position;

            _maxSize = size.X;
        }

        public void Draw(){
            Controller.Window.Draw(_barShape);
        }

        public void Update(float healthPercent)
        {
            //force attention to the button on special occasions
            if (ForceAttention)
                _barShape.FillColor = AttentionColor;
            else
                _barShape.FillColor = NormalColor;

            var barSize = _barShape.Size;
            var oldSize = barSize.X;

            //change the size according to the remaining health
            barSize.X = (healthPercent/100) * _maxSize;

            //cap bar size at 0
            if (barSize.X < 0)
                barSize.X = 0;

            //do attention blink on health reduction
            if (oldSize > barSize.X)
            {
                //blink the attention color
                ForceAttention = true;
                StopAttention(400);
            }

            _barShape.Size = barSize;
        }

        /// <summary>
        /// Waits timeoutInMilliseconds ms until setting ForceAttention to false
        /// </summary>
        /// <param name="timeoutInMilliseconds">ms of delay</param>
        private async void StopAttention(int timeoutInMilliseconds) {
            await Task.Delay(timeoutInMilliseconds);
            ForceAttention = false;
        }
    }
}
