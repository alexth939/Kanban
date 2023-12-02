using System;
using System.Reflection;

public class DispatchFeeder<T> : DispatchProxy where T : class
{
    private T[] _recipients;

    public static T Create(params T[] recipients)
    {
        var proxy = Create<T, DispatchFeeder<T>>() as DispatchFeeder<T>;

        proxy._recipients = recipients;

        return proxy as T;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        if(targetMethod.ReturnType != typeof(void))
            throw new NotSupportedException("U R not allowed to get data from feeder.");

        foreach(var recipient in _recipients)
            _ = targetMethod.Invoke(recipient, args);

        return default(T);
    }
}