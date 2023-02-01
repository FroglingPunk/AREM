public static class ObjectAccessToControllersExtension
{
    public static T GetController<T>(this object obj) where T : class, IController
    {
        return ControllersContainer.GetController<T>();
    }
}