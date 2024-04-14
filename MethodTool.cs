using System.Reflection;

namespace StardewValleyThai
{
    internal static class MethodTool
    {

        //Get params type with method selector
        public static Type[] GetParams(Type classType, string methodName, Predicate<MethodInfo> selectMethod)
        {
            var methods = classType.GetMethods();
            foreach (var method in methods)
            {
                if (method.Name != methodName)
                    continue;

                if (selectMethod.Invoke(method))
                    return method.GetParameters().Select(p => p.ParameterType).ToArray();
            }
            return null;
        }

        public static MethodInfo Get(Type classType, string methodName, Predicate<MethodInfo> selectMethod)
        {
            var methods = classType.GetMethods();
            foreach (var method in methods)
            {
                if (method.Name != methodName)
                    continue;

                if (selectMethod.Invoke(method))
                    return method;
            }
            return null;
        }
    }
}
