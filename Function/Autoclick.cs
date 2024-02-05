using System;
using System.Threading;
using System.Runtime.InteropServices;


namespace AutoClickIt.Function
{
    public class Autoclick
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr DwExtraInfo);

        const uint LEFTDOWN = 0x02;
        const uint LEFTUP = 0x04;

        public static bool EnableClicker = false;

        static void Mouseclick()
        {
            mouse_event(LEFTDOWN, 0, 0, 0, IntPtr.Zero);
            mouse_event(LEFTUP, 0, 0, 0, IntPtr.Zero);
        }
        public static bool IsRepeatTimes;
        public static bool Repeater;
        public static int Times = 10;
        public static int ClickInterval = 1000;

        public static void Click()
        {
            while (true)
            {
                while (EnableClicker)
                {
                    if (Repeater)
                    {

                        Mouseclick();

                    }
                    else if (IsRepeatTimes)
                    {

                        for (int i = 0; i < Times; i++)
                        {
                            Mouseclick();
                            Thread.Sleep(ClickInterval);
                        }
                        EnableClicker = false;

                    }
                    Thread.Sleep(ClickInterval);
                }
            }

        }

    }
}
