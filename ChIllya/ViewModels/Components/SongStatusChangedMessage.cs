using ChIllya.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace ChIllya.ViewModels.Components
{
    public class SongStatusChangedMessage : ValueChangedMessage<SongMessage>
    {
        public SongStatusChangedMessage(SongMessage value) : base(value)
        {

        }
    }
}
