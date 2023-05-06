using System;
using System.Collections.Generic;

namespace Code.Shared
{
    public sealed class OrderedList<T>
    {
        private readonly List<(T element, EngineUpdateGroup order)> _list = new();

        public void Foreach(Action<T> action)
        {
            foreach (var (e, _) in _list)
            {
                action(e);
            }
        }

        public void Add(T element, EngineUpdateGroup order)
        {
            _list.Add((element, order));
        }

        public void Order()
        {
            _list.Sort(Comparison);
        }

        private int Comparison((T element, EngineUpdateGroup order) x, (T element, EngineUpdateGroup order) y)
        {
            return x.order > y.order ? 1 : -1;
        }
    }
}