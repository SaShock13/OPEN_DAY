using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Game.SCRIPTS
{
    public class UIDialogMessage
    {
        public string message;
        public float showingTime;

        public UIDialogMessage(string message, float showingTime)
        {
            this.message = message;
            this.showingTime = showingTime;
        }
    }
}
