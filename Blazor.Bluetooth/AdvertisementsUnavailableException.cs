using System;

namespace Blazor.Bluetooth
{
    public class AdvertisementsUnavailableException : Exception
    {
        private const string ExceptionMessage =
            "Advertisements unavailable.\n" +
            "Because of this feature in experimental mode, please make sure you enable it in your browser!\n" +
            "For chrome/edge about:flags/#enable-web-bluetooth-new-permissions-backend\n";

        public AdvertisementsUnavailableException(Exception inner)
            : base(ExceptionMessage, inner)
        {
        }
    }
}
