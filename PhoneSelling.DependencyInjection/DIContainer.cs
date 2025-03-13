namespace PhoneSelling.DependencyInjection
{
    public static class DIContainer
    {
        static Dictionary<string, object> _singletons = new Dictionary<string, object>();
        public static void AddKeyedSingleton<IParent, Child>()
        {
            Type parent = typeof(IParent);
            Type child = typeof(Child);
            _singletons[parent.Name] = Activator.CreateInstance(child)!;
        }

        public static void AddInstance<IParent>(IParent instance)
        {
            Type parent = typeof(IParent);
            _singletons[parent.Name] = instance!;
        }

        public static IParent GetKeyedSingleton<IParent>()
        {
            Type parent = typeof(IParent);
            return (IParent)_singletons[parent.Name];
        }
    }
}
