using ChIllya.Models;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.ViewModels.Components
{
    public class SongStatusChangedMessage : ValueChangedMessage<SongMessage>
    {
        public SongStatusChangedMessage(SongMessage value) : base(value)
        {

        }
    }
}
