using System.Threading.Tasks;
using Xamarin.Forms;

namespace MAGIApp.Controls
{
    public class ButtonTriggerAction : TriggerAction<VisualElement>
    {
        public Color BackgroundColor { get; set; }

        protected async override void Invoke(VisualElement visual)
        {
            var button = visual as Button;
            if (button == null)
            {
                return;
            }

            if (BackgroundColor != null)
            {
                button.BackgroundColor = BackgroundColor;
                await Task.Delay(600);
                button.BackgroundColor = Color.Transparent;

            }
        }
    }
}
