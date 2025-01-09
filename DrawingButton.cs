using SplashKitSDK;
using System;

namespace CustomProject
{
    public class DrawingButton
    {
        public static void DrawButton(string text, int x, int y, Font font) //This is an extension method to appropriately draw a button will well-aligned text
        {
            int buttonWidth = 180;
            int buttonHeight = 80;
            Rectangle startButton = SplashKit.RectangleFrom(x - 1000, y, buttonWidth + 2000, buttonHeight);
            Color rectColorWhenMouseOn = SplashKit.RGBAColor(0, 0, 0, 150);

            // Calculate the width and height of the text
            int textWidth = SplashKit.TextWidth(text, font, 24);
            int textHeight = SplashKit.TextHeight(text, font, 24);

            // Calculate the position to draw the text so that it is centered
            int textX = x + (buttonWidth - textWidth) / 2;
            int textY = y + (buttonHeight - textHeight) / 2;

            if (SplashKit.PointInRectangle(SplashKit.MousePosition(), startButton))//Check whether the mouse is on the button. Check for user interaction
            {
                SplashKit.FillRectangle(rectColorWhenMouseOn, startButton);
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Program.ButtonClicked(text);
                }
            }
            SplashKit.DrawText(text, Color.WhiteSmoke, font, 24, textX, textY);
        }
    }
}
