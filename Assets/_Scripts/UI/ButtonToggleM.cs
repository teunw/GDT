using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class ButtonToggleM
    {

        private List<Button> buttons;

        public Button ToggledButton { get; private set; }

        public Color OriginColor { get; private set; }

        public Color ToggleColor { get; private set; }

        public bool Toggled { get; private set; }

        public ButtonToggleM(List<Button> buttons, Color orignalColor, Color toggleColor)
        {
            this.buttons = buttons;
            this.OriginColor = orignalColor;
            this.ToggleColor = toggleColor;
            Toggle(buttons[0]);
        }

        public void Toggle(Button button)
        {
            buttons.ForEach(o => o.image.color = OriginColor);
            button.image.color = ToggleColor;
            ToggledButton = button;
        }

        public void SetToggleColor(Color color)
        {
            ToggleColor = color;
            ToggledButton.image.color = color;
        }

    }
}
