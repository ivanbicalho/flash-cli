using System.Collections.Generic;

namespace flash.Functions
{
    public static class Functions
    {
        public static IEnumerable<IFunction> List()
        {
            return new List<IFunction>
            {
                new Camel(),
                new Pascal(),
                new Lower(),
                new Upper()
            };
        }
    }
}