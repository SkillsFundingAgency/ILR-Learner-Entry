using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ilrLearnerEntry
{
    ///  http://stackoverflow.com/questions/7587110/basic-user-input-string-validation
    ///  Not used in the end.
	static class InputValiator
	{
		static Boolean IsValueAnInt(String str)
		{
			foreach (char ch in str)
			{
				if (!Char.IsDigit(ch))
					return false;
			}

			return true;
		}
	}
}
