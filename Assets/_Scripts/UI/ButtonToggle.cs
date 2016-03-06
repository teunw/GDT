using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class ButtonToggle
    {
        private Button button1, button2;

        public Button ToggledButton { get; private set; } 

        public Color OriginColor
        {
            get; private set; }

        public Color ToggleColor
        {
            get; private set; }

        public bool Toggled
        {
            get; private set; }


        public ButtonToggle(Button button1, Button button2, Color orignalColor, Color toggleColor)
        {
            this.button1 = button1;
            this.button2 = button2;
            this.OriginColor = orignalColor;
            this.ToggleColor = toggleColor;
            Toggle();
        }

        public void Toggle()
        {
            if (Toggled)
            {
                button1.image.color = ToggleColor;
                button2.image.color = OriginColor;
                ToggledButton = button1;
            }
            else
            {
                button1.image.color = OriginColor;
                button2.image.color = ToggleColor;
                ToggledButton = button2;
            }
            Toggled = !Toggled;
        }

        public void SetToggleColor(Color color)
        {
            ToggleColor = color;
            ToggledButton.image.color = color;
        }
    }
}
