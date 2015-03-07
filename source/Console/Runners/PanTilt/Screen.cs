using System;

namespace PiCamCV.ConsoleApp.Runners.PanTilt
{
    public class Screen
    {
        private int _lineNumber;

        public Screen()
        {
            Clear();
        }

        public void Clear()
        {
            Console.Clear();
            
        }

        public void BeginRepaint()
        {
            _lineNumber = 0;
        }


        public void WriteLine(string format, params object[] args)
        {
            if (_lineNumber < Console.BufferHeight)
            {
                Console.SetCursorPosition(0, _lineNumber);
                var message = args.Length > 0 ? string.Format(format, args) : format;
                Console.WriteLine(message.PadRight(Console.BufferWidth, ' '));
            }
            _lineNumber++;
        }
    }
}