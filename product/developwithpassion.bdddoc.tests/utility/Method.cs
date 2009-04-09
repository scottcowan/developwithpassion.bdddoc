using System;
using System.Reflection;

namespace developwithpassion.bdddoc.tests.utility
{
    public static class Method
    {
        public static MemberInfo pointed_at_by(Action action)
        {
            return action.Method;
        }

    }
}