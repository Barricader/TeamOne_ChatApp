using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginWindow.Views
{
    class HintView
    {
        private bool _showText;
        public HintView()
        {
            _showText = true;
        }
        public bool ShowButton
        {
            get { return _showText; }
        }
        public bool ToggleHint()
        {
            return !(_showText);
        }
    }
}
