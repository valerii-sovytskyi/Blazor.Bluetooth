using System;

namespace Blazor.Bluetooth
{
    public class AdvertisementsUnavailableException : Exception
    {
        private const string ExceptionMessage =
            "Advertisements unavailable.\n" +
            "Because of this feature in experimental mode, please make sure you enable it in your browser!\n" +
            "For chrome copy this ref to your browser chrome://flags/#enable-experimental-web-platform-features\n" +
            "For edge copy this ref to your browser edge://flags/#enable-experimental-web-platform-features\n";

        public AdvertisementsUnavailableException(Exception inner)
            : base(ExceptionMessage, inner)
        {
        }
    }
}
