using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using AC = AutoCorrect;

namespace AutoCorrectWinService
{
    public partial class AutoCorrectWinService : ServiceBase
    {
        public AC.AutoCorrect autoCorrect;
        public AutoCorrectWinService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            autoCorrect = new AC.AutoCorrect();
        }

        protected override void OnStop()
        {
        }

        public string CorrectText(string text)
        {
            try
            {
                var correctedText = autoCorrect.CorrectText(text);

                return correctedText;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
