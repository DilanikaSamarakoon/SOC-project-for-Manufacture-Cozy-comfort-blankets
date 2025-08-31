using System;

namespace Manufacuter.de
{
    // This static class now lives in its own file.
    // This allows it to be accessed from anywhere in the project
    // without interfering with the form designers.
    public static class AppEvents
    {
        public static event EventHandler BlanketsChanged;

        public static void RaiseBlanketsChanged()
        {
            BlanketsChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
