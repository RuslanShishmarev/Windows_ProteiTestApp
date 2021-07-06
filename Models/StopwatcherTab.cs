using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteiTestApp.Models
{
    public class StopwatcherTab
    {
        public StopwatcherTab(StopwatchCounter counter)
        {
            Counter = counter;
        }

        public StopwatchCounter Counter { get; set; }


    }
}
