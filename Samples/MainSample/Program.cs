﻿using System;
using SadConsole;
using SadConsole.Input;
using SadRogue.Primitives;
using Console = SadConsole.Console;

namespace FeatureDemo
{
    internal class Program
    {
        //private static Windows.CharacterViewer _characterWindow;
        private static Container MainConsole;

        private static void Main(string[] args)
        {
            //SadConsole.Settings.UseDefaultExtendedFont = true;

            SadConsole.Game.Create(80, 25);
            SadConsole.Game.Instance.OnStart = Init;
            SadConsole.Game.Instance.FrameUpdate += Instance_FrameUpdate;
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        private static void Instance_FrameUpdate(object sender, GameHost e)
        {
            // Called each logic update.
            //if (!_characterWindow.IsVisible)
            {
                // This block of code cycles through the consoles in the SadConsole.Engine.ConsoleRenderStack, showing only a single console
                // at a time. This code is provided to support the custom consoles demo. If you want to enable the demo, uncomment one of the lines
                // in the Initialize method above.
                if (SadConsole.Global.Keyboard.IsKeyReleased(Keys.F1))
                {
                    MainConsole.MoveNextConsole();
                }
                else if (SadConsole.Global.Keyboard.IsKeyReleased(Keys.F2))
                {
                    //_characterWindow.Show(true);
                }
                else if (SadConsole.Global.Keyboard.IsKeyReleased(Keys.F3))
                {
                    //SadConsole.Debug.CurrentScreen.Show();
                }
                else if (SadConsole.Global.Keyboard.IsKeyReleased(Keys.F5))
                {
                    //SadConsole.Settings.ToggleFullScreen();
                }

            }
        }

        /// <summary>
        /// <c>test</c>
        /// </summary>
        private static void Init()
        {
            // Any setup
            //if (Settings.UnlimitedFPS)
            //    SadConsole.Game.Instance.Components.Add(new SadConsole.Game.FPSCounterComponent(SadConsole.Game.Instance));

            //SadConsole.Game.Instance.Window.Title = "DemoProject Core";

            // By default SadConsole adds a blank ready-to-go console to the rendering system. 
            // We don't want to use that for the sample project so we'll remove it.

            //Global.MouseState.ProcessMouseWhenOffScreen = true;
            MainConsole = new Container();

            // We'll instead use our demo consoles that show various features of SadConsole.
            Global.Screen.Renderer = null;
            Global.Screen.Children.Add(MainConsole);
            // Initialize the windows
            //_characterWindow = new Windows.CharacterViewer();

        }

        class MouseTest : SadConsole.Components.MouseConsoleComponent
        {
            public override void ProcessMouse(IScreenObject host, MouseScreenObjectState state, out bool handled)
            {
                if (state.Mouse.IsOnScreen)
                {
                    host.Position = state.WorldCellPosition;
                    handled = true;
                }
                else
                    handled = false;
            }
        }
    }
}