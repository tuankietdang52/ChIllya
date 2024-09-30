using ChIllya.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya
{
    public class AppSetup
    {
        public AppSetup()
        {

        }

        public AppSetup InitMusicManager()
        {
            new MusicManager();
            return this;
        }

        public AppSetup LoadSecretFile()
        {
            EnvLoader.Load("Secret/secret.env");
            return this;
        }
    }
}
