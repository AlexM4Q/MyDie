using System;
using System.Reflection;
using MyDie.Die.Lib;

namespace MyDie.Die
{
    public interface IDieContainer
    {
        T Get<T>() where T : class;
        object Get(Type type);
        void Register<TInter, TImpl>() where TInter : class where TImpl : class, TInter;
        void Register<T>(DieFactory<T> factory) where T : class;
        void Register(Assembly assembly);
    }
}
