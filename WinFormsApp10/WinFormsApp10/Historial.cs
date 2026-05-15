using System;
using System.Collections.Generic;

namespace WinFormsApp10
{
    internal class Historial
    {
        private List<IMemento> mementos;

        public Historial()
        {
            mementos = new List<IMemento>();
        }

        public void addMemento(IMemento memento)
        {
            mementos.Add(memento);
        }

        public IMemento getMemento(int index)
        {
            if (index >= 0 && index < mementos.Count)
                return mementos[index];
            return null;
        }

        public List<IMemento> getMementos()
        {
            return mementos;
        }

        public int getCount()
        {
            return mementos.Count;
        }
    }
}
