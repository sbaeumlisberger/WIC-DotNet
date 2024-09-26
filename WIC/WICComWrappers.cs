using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace WIC;

public class WICComWrappers
{
    public static StrategyBasedComWrappers Instance { get; } = new StrategyBasedComWrappers();

}
