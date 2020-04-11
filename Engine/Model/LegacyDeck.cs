using Engine.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Model
{
    public class LegacyDeck : BaseNotificationClass
    {
        public enum TIME
        {
            JanStart,
            FebStart,
            MarStart,
            AprMid,
            MayStart,
            AugStart
        }


        public void ApplyWinBonus()
        {

        }

        public bool QuarantineUnlocked => CurrentTime >= TIME.FebStart;
        public bool MilitaryUnlocked => CurrentTime >= TIME.MarStart;
        public bool FadedUnlocked => CurrentTime >= TIME.AprMid;
        public bool RoadBlocksUnlocked => CurrentTime >= TIME.MayStart;
        public bool SelfSacrificeUnlocked => CurrentTime >= TIME.AugStart;


        private TIME _currentTime;

        public TIME CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
                OnPropertyChanged(nameof(QuarantineUnlocked));
                OnPropertyChanged(nameof(MilitaryUnlocked));
                OnPropertyChanged(nameof(FadedUnlocked));
                OnPropertyChanged(nameof(RoadBlocksUnlocked));
                OnPropertyChanged(nameof(SelfSacrificeUnlocked));
            }
        }

        public LegacyDeck()
        {
            CurrentTime = TIME.JanStart;
        }

    }
}
